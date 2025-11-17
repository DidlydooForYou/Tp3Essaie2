using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DisableFogForCamera : MonoBehaviour
{
    bool previousFog;

    void OnPreRender()
    {
        previousFog = RenderSettings.fog;
        RenderSettings.fog = false;
    }

    void OnPostRender()
    {
        RenderSettings.fog = previousFog;
    }
}