using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ComportementBoss : MonoBehaviour
{
    [SerializeField] GameObject player;
    float meleeRange = 2f;
    float rangedRange = 8f;

    private List<Conditions> conditions = new List<Conditions>();
    void Start()
    {
        conditions.Add(new WithinRange(player.transform, player, meleeRange, rangedRange));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (player == null) return;

        Vector3 pos = transform.position;

        // Melee zone
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos, meleeRange);

        // Ranged zone
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(pos, rangedRange);

        // Far zone
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, rangedRange * 1.2f);

        if (player != null)
            Gizmos.DrawLine(pos, player.transform.position);
    }
#endif

}
