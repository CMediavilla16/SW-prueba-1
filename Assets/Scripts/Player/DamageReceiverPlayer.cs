using UnityEngine;

public class DamageReceiverPlayer : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 1;
    private int currentHealth;


    private Rigidbody2D rb2D;
    private Animator animator;
    public float forceImpulse = 5;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        UIManager.Instance.UpdateHealth(currentHealth);

    }


    public void ApplyDamage(int amount, bool applyForceOrNot, bool applyHitAnimation, Vector2 hitDirection)
    {
        currentHealth -= amount;

        UIManager.Instance.UpdateHealth(currentHealth);

        if (applyForceOrNot)
        {
            GetComponent<Player>().canMove = false;
            rb2D.AddForce(hitDirection.normalized * forceImpulse, ForceMode2D.Impulse);
            Invoke("ReseMovement", 0.1f);
        }

        if (applyHitAnimation)
        {
            animator.SetTrigger("Hit");
        }
        if (currentHealth <= 0)
        {
            Die();
        }


    }


    private void ReseMovement()
    {
        GetComponent<Player>().canMove = true;
    }

    void Die()
    {

    }

}
