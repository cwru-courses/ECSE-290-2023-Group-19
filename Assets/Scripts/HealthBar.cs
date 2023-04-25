using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public float fillAmount;

    void Start()
    {
        bar = GetComponent<Image>();
    }

    void Update()
    {
        bar.fillAmount = fillAmount;
    }
}
