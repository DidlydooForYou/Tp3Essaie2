using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 20f;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] float damage = 10f;

    SanityUpdate sanity;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        sanity = other.GetComponent<SanityUpdate>();
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Projectile hit the player!");
            sanity.LoseSanity(damage);
        }

    }
}
