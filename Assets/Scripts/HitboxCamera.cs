using UnityEngine;

public class HitboxCamera : MonoBehaviour
{
    BossHealth bossHealth;

    [SerializeField]float damage = 50f;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ennemi"))
        {
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            bossHealth = other.gameObject.GetComponent<BossHealth>();
            bossHealth.TakeDamage(damage);
        }
    }
}
