using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = (speed * Vector2.right);
        StartCoroutine("BulletLifetime");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator BulletLifetime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
