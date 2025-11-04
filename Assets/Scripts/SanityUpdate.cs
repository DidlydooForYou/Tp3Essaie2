using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SanityUpdate : MonoBehaviour
{
    [SerializeField] Slider sliderSanity;

    bool losingMind = true;
    void Update()
    {
        if(sliderSanity.value > 0 && losingMind)
        {
            StartCoroutine(losingYourMind());
        }
    }

    IEnumerator losingYourMind()
    {
        losingMind = false;
        yield return new WaitForSeconds(10);
        sliderSanity.value -= 1;
        losingMind = true;
    }
}
