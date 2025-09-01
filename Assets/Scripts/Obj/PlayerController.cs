using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Instantiate")]
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject muzzlePos;
    [Header("Stats")]
    public float moveSpeed = 30f;
    private float moveDir = 0f;

    public Rigidbody2D rb;
    public Animator animator;
    private PlayerInput actionMap;
    private InputAction moveAction;
    private InputAction shootAction;
    private InputAction resetAction;

    private bool canMove = true;
    private bool canShoot = true;
    private bool isShoot = false;
    private bool isMove = false;
    private bool canHurt = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        actionMap = GetComponent<PlayerInput>();
        actionMap.currentActionMap.Enable();
        InputSetup();
    }
    private void InputSetup()
    {
        moveAction = actionMap.currentActionMap.FindAction("Move");
        moveAction.started += Handle_Move;
        moveAction.canceled += Handle_MoveStop;

        shootAction = actionMap.currentActionMap.FindAction("Shoot");
        shootAction.started += Handle_Shoot;
        shootAction.canceled += Handle_ShootStop;


        resetAction = actionMap.currentActionMap.FindAction("Reset");
        resetAction.performed += Handle_Reset;

    }
    private void OnDestroy()
    {
        moveAction.started -= Handle_Move;
        moveAction.canceled -= Handle_Move;
        shootAction.started -= Handle_Shoot;
        shootAction.canceled -= Handle_ShootStop;
        resetAction.performed -= Handle_Reset;

    }

    void Update()
    {
        if (canMove)
        {
            OnMove();
            if (isShoot & canShoot)
            {
                StartCoroutine("ShootCoroutine");
            }
        }
    }

    private void OnMove() //movement is terrible for switching lanes with (considering just moving player by a set distance per key press)
    {
        if (isMove)
        {
            rb.linearVelocity = new Vector2(0, moveSpeed * moveDir);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        
    }
    public void SetPlayerMoveable(bool _canMove)
    {
        switch (_canMove)
        {
            case true:
                canMove = true; break;
            case false: 
                canMove = false; break;
        }
    }

    private void OnShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        GameObject _explosion = Instantiate(explosionPrefab, muzzlePos.transform.position, Quaternion.identity, transform);
    }
    IEnumerator ShootCoroutine()
    {
        canShoot = false;
        OnShoot();
        yield return new WaitForSeconds(0.23f);
        canShoot = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (canHurt)
            {
                GameManager.instance.PlayerHurt();
                StartCoroutine("PlayerHurtCoroutine");
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (canHurt)
            {
                GameManager.instance.PlayerHurt();
                Destroy(collision.gameObject);
                StartCoroutine("PlayerHurtCoroutine");
            }
        }
    }

    IEnumerator PlayerHurtCoroutine() //only applies to player getting hit and not the enemies going past you
    {
        canHurt = false;
        yield return new WaitForSeconds(1f);
        canHurt = true;
    }

    private void Handle_Move(InputAction.CallbackContext obj)
    {
        moveDir = moveAction.ReadValue<float>();
        isMove = true;
    }
    private void Handle_MoveStop(InputAction.CallbackContext obj)
    {
        moveDir = 0f;
        isMove = false;
    }
    private void Handle_Shoot(InputAction.CallbackContext obj)
    {
        isShoot = true;
    }
    private void Handle_ShootStop(InputAction.CallbackContext obj)
    {
        isShoot = false;
    }

    private void Handle_Reset(InputAction.CallbackContext obj)
    {
        GameManager.instance.ResetGame();
    }


}
