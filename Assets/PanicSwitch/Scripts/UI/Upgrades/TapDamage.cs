  

using TMPro;
using UnityEngine;

public class TapDamage : MonoBehaviour
{
    public static int TapDamageValue;
    [SerializeField] private TextMeshProUGUI TapDamageValueDisplayText;

    private void Start()
    {
        if (PlayerPrefs.HasKey("TapDamageValue"))
        {
            TapDamageValue = PlayerPrefs.GetInt("TapDamageValue");
        }
        else
        {
            TapDamageValue = 2;
            PlayerPrefs.SetInt("TapDamageValue", TapDamageValue);
        }
        UpdateText();
    }


    public void Upgrade()
    {
        TapDamageValue +=5;
        PlayerPrefs.SetInt("TapDamageValue", TapDamageValue);
        UpdateText();
    }

    private void UpdateText()
    {
        TapDamageValueDisplayText.text = TapDamageValue.ToString();
    }
}
