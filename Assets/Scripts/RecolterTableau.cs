using UnityEngine;

public class RecolterTableau : MonoBehaviour
{
    [SerializeField] Camera playerCamera; 
    [SerializeField] float rayDistance = 5f;
    [SerializeField] GameObject murSortie;

    int paintingCount = 0;
    void Update()
    {
        Recolte();
    }

    public void Recolte()
    {

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Tableau"))
            {
                paintingCount++;
                if (paintingCount <= 8)
                    murSortie.SetActive(false);
            }
        }
    }
}
