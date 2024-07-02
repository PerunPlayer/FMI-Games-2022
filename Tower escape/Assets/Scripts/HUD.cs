using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI textField;

    public void SetCollectedPrizes(int count)
    {
        textField.text = count.ToString();
    }
}
