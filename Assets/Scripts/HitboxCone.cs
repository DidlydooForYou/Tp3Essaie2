using UnityEngine;

public class HitboxCone : MonoBehaviour
{
    private float damage = 10;
    private SanityUpdate sanity;
    private void Start()
    {
        sanity = GameObject.FindGameObjectWithTag("Player").GetComponent<SanityUpdate>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sanity.LoseSanity(damage);
        }
    }
}
