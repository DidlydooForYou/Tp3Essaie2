using UnityEngine;

public class BossHealth : MonoBehaviour
{
    float health;
    [SerializeField] float maxHealth = 100f;


    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
