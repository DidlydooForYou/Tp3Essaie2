using UnityEngine;

public class HitboxCamera : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ennemi"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
