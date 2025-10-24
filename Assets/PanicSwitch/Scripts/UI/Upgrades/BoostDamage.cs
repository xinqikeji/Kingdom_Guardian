  

using TMPro;
using UnityEngine;

public class BoostDamage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _DamageDisplayText;
    public static int damage;
    

    private void Start()
    {
        if (PlayerPrefs.HasKey("boostDamage"))
        {
            damage = PlayerPrefs.GetInt("boostDamage");
        }
        else
        {
            damage = 1;
            PlayerPrefs.SetInt("boostDamage", damage);
        }
        UpdateText();
    }


    public void Upgrade()
    {
        damage+=10;
        PlayerPrefs.SetInt("boostDamage", damage);
        UpdateText();
    }

    private void UpdateText()
    {
        _DamageDisplayText.text = damage.ToString();
    }
}
