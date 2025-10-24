  

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public int price;
    [SerializeField] private GameObject _blockImage;
   // [SerializeField] private GameObject _priceWrapper;
    [SerializeField] private TextMeshProUGUI _priceDisplayText;
    [SerializeField] private Wallet _wallet;
    [HideInInspector] public spellButton _spellButton;
        

    public void AdsButtonChecker(bool value)
    {
        _blockImage.GetComponent<Button>().enabled = value;
    }

    private void Start()
    {
        _wallet = FindObjectOfType<Wallet>();

        if (_priceDisplayText)
            _priceDisplayText.text = price.ToString();

        CheckStatus();

        _wallet.MoneyChange.AddListener((level) =>
        {
            CheckStatus();
        });
    }

    public void CheckStatus()
    {
        if (_spellButton)
        {
            if (_spellButton.CheckQty() > 0)
            {
                ChangeText(1);
            }
            else
            {
                CheckInner();
            }
        }
        else
        {
            CheckInner();
        }
    }

    private void CheckInner()
    {
        if (_wallet.GetMoney() >= price)
        {
            _blockImage.SetActive(false);

       //     if (_priceWrapper)
       //         _priceWrapper.SetActive(true);
        }
        else
        {
            _blockImage.SetActive(true);
       //     if (_priceWrapper)
         //       _priceWrapper.SetActive(false);
        }

        ChangeText(0);
    }

    public void ChangeText(int value)
    {
        if (!_priceDisplayText) return;

        if (value > 0)
            _priceDisplayText.text = "Free";
        else
            _priceDisplayText.text = price.ToString();
    }
}
