using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50.0f;

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
    
}
