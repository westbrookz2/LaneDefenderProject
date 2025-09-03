using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AudioClip shootSFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SFXManager.instance.PlaySFX(shootSFX, transform, 0.7f);
        animator = GetComponent<Animator>();
        animator.SetTrigger("fire");
    }

    public void DeleteSelf()
    {
        Destroy(gameObject);
    }

}
