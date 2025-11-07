using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    [SerializeField] float damagePerSecond = 10f;
    public GameObject player;

    List<SanityUpdate> affectedPlayers = new List<SanityUpdate>();
    private void OnTriggerEnter(Collider other)
    {
        SanityUpdate playerHealth = other.GetComponent<SanityUpdate>();
        if (playerHealth != null && other.gameObject != player)
        {
            affectedPlayers.Add(playerHealth);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        affectedPlayers.Remove(other.GetComponent<SanityUpdate>());
    }

    private void Update()
    {
        foreach (var playerHealth in affectedPlayers)
        {
            if (playerHealth != null)
            {
                playerHealth.LoseSanity(damagePerSecond * Time.deltaTime);
            }
        }
    }
}
