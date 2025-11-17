using UnityEngine;

public class TrappingWall : MonoBehaviour
{
    [SerializeField] GameObject murTrap;
    [SerializeField] GameObject boss;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            murTrap.SetActive(true);
        }
        if (other.CompareTag("TrapBoss"))
        {
            boss.SetActive(true);
        }
    }
}
