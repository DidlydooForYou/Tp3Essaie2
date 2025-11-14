using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    [SerializeField] float damagePerSecond = 10f;
    [SerializeField] float lifeTime = 5f;
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
        lifeTime -= Time.deltaTime;
        foreach (var playerSanity in affectedPlayers)
        {
            if (playerSanity != null)
            {
                playerSanity.LoseSanity(damagePerSecond * Time.deltaTime);
            }
        }
        if (lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
