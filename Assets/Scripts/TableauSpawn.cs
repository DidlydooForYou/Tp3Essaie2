using UnityEngine;

public class TableauSpawn : MonoBehaviour
{
    [SerializeField] int nbreDeTableau;
    [SerializeField] GameObject[] tableaux;
    void Start()
    {
        SpawnRandomTableau();
    }

    void SpawnRandomTableau()
    {
        for (int i = 0; i < tableaux.Length; i++)
        {
            int rand = Random.Range(i, tableaux.Length);
            GameObject temp = tableaux[i];
            tableaux[i] = tableaux[rand];
            tableaux[rand] = temp;
        }

        for (int i = 0; i < nbreDeTableau; i++)
        {
            tableaux[i].SetActive(true);
        }
    }
}
