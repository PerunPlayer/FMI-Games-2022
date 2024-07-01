using UnityEngine;
using TMPro;

public class PrizesBar : MonoBehaviour
{
    public TextMeshProUGUI textField;

    public void SetCollectedPrizes(int count)
    {
        textField.text = count.ToString();
    }
}
