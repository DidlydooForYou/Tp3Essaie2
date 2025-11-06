using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SanityUpdate : MonoBehaviour
{
    private float maxSanity = 100f;
    public float sanity = 100f;
    [SerializeField] Image barSanity;

    private void Start()
    {
        if(sanity > maxSanity)
            sanity = maxSanity;
    }
    void Update()
    {
        if(sanity > 0)
            LosingYourMind();

        float fillPercentage = sanity / maxSanity;

        barSanity.fillAmount = fillPercentage;
    }

    public void LosingYourMind()
    {
        barSanity.fillAmount = Mathf.Clamp01(barSanity.fillAmount - Time.deltaTime * 0.01f);
    }

    public void LoseSanity(float damage)
    {
        sanity -= damage;
        sanity = Mathf.Clamp(sanity,0,maxSanity);
        if(sanity <= 0)
        {
            Debug.Log("You have lost your mind!");
        }
    }
}
