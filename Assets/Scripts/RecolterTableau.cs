using UnityEngine;
using UnityEngine.InputSystem;

public class RecolterTableau : MonoBehaviour
{
    [SerializeField] Camera playerCamera; 
    [SerializeField] float rayDistance = 5f;
    [SerializeField] GameObject murSortie;

    int paintingCount = 0;

    public void Recolte(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started)
            return;

        if (playerCamera == null)
        {
            Debug.LogError("Player camera NOT assigned!");
            return;
        }

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = playerCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Tableau"))
            {
                hit.collider.gameObject.SetActive(false);
                if (paintingCount >= 8)
                {
                    Debug.Log("tout est recuperer");
                    murSortie.SetActive(false);
                }
                paintingCount++;
                print(paintingCount);
            }
        }
    }
}
