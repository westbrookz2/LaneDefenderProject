using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Instantiate")]
    public GameObject explosionPrefab;
    public AudioClip damageSFX;
    public AudioClip deathSFX;

    [Header("Stats")]
    [SerializeField] private HealthScript _health;
    [SerializeField] private Animator animator;
    public float speed = 4f;
    public int scoreGain = 100;

    private int currentHealth;
    private Rigidbody2D rb;
    

    private bool canMove = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _health = GetComponent<HealthScript>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            rb.linearVelocity = (speed * Vector2.left);
        }
        else
        {
            rb.linearVelocity = rb.linearVelocity * 0.8f;
        }
    }
   private void OnDamaged()
    {
        _health.TakeDamage();
        animator.SetTrigger("hit");
        if (_health.isDead)
        {
            SFXManager.instance.PlaySFX(deathSFX, transform, 1f);
            GameManager.instance.IncrementCurrentScore(scoreGain);
            rb.bodyType = RigidbodyType2D.Static;
            GetComponent<Collider2D>().enabled = false;
            animator.SetBool("dead", true);
        }
        StartCoroutine("MovementPauseCoroutine");
        SFXManager.instance.PlaySFX(damageSFX, transform, 1f);
    }
    IEnumerator MovementPauseCoroutine()
    {
        canMove = false;
        yield return new WaitForSeconds(1f);
        canMove = true;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            OnDamaged();
            GameObject _explosion = Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);

        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            GameManager.instance.PlayerHurt();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
    }


}
