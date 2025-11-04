using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class AttaqueCamera : MonoBehaviour
{
    [SerializeField] GameObject lighting;
    [SerializeField] int cooldownFlash = 3;
    [SerializeField] GameObject hitboxCamera;
    [SerializeField] GameObject laCamera;
    [SerializeField] GameObject posOrigine;
    [SerializeField] GameObject posFace;

    bool canFlash = true;

    private void Update()
    {
        Flash();
        PutToFace();
    }

    void PutToFace()
    {
        if (Input.GetMouseButtonDown(1))
        {
            laCamera.transform.position = posFace.transform.position;
        }
        if (Input.GetMouseButtonUp(1))
        {
            laCamera.transform.position = posOrigine.transform.position;
        }
    }

    void Flash()
    {
        if (Input.GetMouseButtonDown(0) && canFlash)
        {
            canFlash = false;
            StartCoroutine(cameraFlash());
        }
    }

    IEnumerator cameraFlash()
    {
        yield return new WaitForSeconds(1);
        lighting.SetActive(true);
        hitboxCamera.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        lighting.SetActive(false);
        hitboxCamera.SetActive(false);

        yield return new WaitForSeconds(cooldownFlash);
        canFlash = true;
    }
}
