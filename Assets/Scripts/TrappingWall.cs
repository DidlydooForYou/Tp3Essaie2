using UnityEngine;

public class TrappingWall : MonoBehaviour
{
    [SerializeField] GameObject murTrap;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            murTrap.SetActive(true);
        }
    }
}
