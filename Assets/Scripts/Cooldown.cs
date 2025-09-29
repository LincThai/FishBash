using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    public Image cooldownOverlay;

    public void SetFillAmount(float amount)
    {
        cooldownOverlay.fillAmount = amount;
    }
}
