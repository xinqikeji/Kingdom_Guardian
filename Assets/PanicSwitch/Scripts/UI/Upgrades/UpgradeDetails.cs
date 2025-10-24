  

using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeDetails : MonoBehaviour
{
    public int Id;
    public float[] TimeResearch;
    public int[] Price;
    public int[] gemPrice;
    [HideInInspector] public int Level;
    [SerializeField] private GameObject[] activeObjects;
    [SerializeField] private TextMeshProUGUI priceDisplayText;
    [SerializeField] private TextMeshProUGUI gemPriceDisplayText;
    [SerializeField] private TextMeshProUGUI LevelDisplayText;
    [SerializeField] private TextMeshProUGUI timerDisplayText;
    [SerializeField] private Image researchProgressDisplay;
    [SerializeField] private GameObject ActiveElemeny;

    private AudioSource _clickAudios;
    private WindowsUI _windowsUI;
    private Wallet _wallet;
    private float timer;
    private float tempTimer;
    private int _isprocess;

    private void Awake()
    {
        _wallet = FindObjectOfType<Wallet>();
        _windowsUI = FindObjectOfType<WindowsUI>();
        Level = PlayerPrefs.GetInt("Level" + Id);

        if (PlayerPrefs.GetInt("_isprocess" + Id) > 0)
        {
            timer = PlayerPrefs.GetFloat("timer" + Id);

            if (TimeLastSession.ts.TotalSeconds >= TimeResearch[Level])
            {
                UpdateLevel();
                _isprocess = 0;
                PlayerPrefs.SetInt("_isprocess" + Id, 0);
            }
            else
            {
                timer -= (float)(TimeLastSession.ts.TotalSeconds + PlayerPrefs.GetFloat("SessionTimer"));
                tempTimer = TimeResearch[Level] - timer;
            }
        }

        _isprocess = PlayerPrefs.GetInt("_isprocess" + Id);
        _clickAudios = FindObjectOfType<WindowsUI>().GetComponent<AudioSource>();

        CheckProcess(_isprocess);
        UpdateText();
    }

    private void Update()
    {
        if (_isprocess == 1)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                tempTimer += Time.deltaTime;
                researchProgressDisplay.fillAmount = tempTimer / TimeResearch[Level];
                timerDisplayText.text = string.Format("{0:0}:{1:00}:{2:00}", (int)(timer / 3600), (int)(timer / 60 - (int)(timer / 3600) * 60), (int)((timer - (int)(timer / 60) * 60))); // мин. и секунды
            }
            else
            {
                _isprocess = 0;
                tempTimer = 0;
                CheckProcess(_isprocess);
                PlayerPrefs.SetInt("_isprocess" + Id, 0);
                UpdateLevel();
                UpdateText();
            }
        }
    }

    private int GetCurrentPrice()
    {
        return 250 * (Level + 1);
    }

    public void Upgrade()
    {
        if (_wallet.GetMoney() >= GetCurrentPrice())
        {
            _wallet.SetMoney(-GetCurrentPrice());
            UseUpgrade();
        }
        else
        {
            _windowsUI.GemShopOpen(0);
        }
    }

    public void UseUpgrade()
    {
        timer = TimeResearch[Level];
        _isprocess = 1;
        CheckProcess(_isprocess);
        PlayerPrefs.SetInt("_isprocess" + Id, 1);
        SaveTimer();
        _clickAudios.Play();
    }

    public void AdsUpgradeProcess()
    {
        timer -= 18000;
        tempTimer += 18000;
        SaveTimer();
        _clickAudios.Play();
    }

    public void GemUpgradeProcess()
    {
        if (_wallet.GetGems() >= gemPrice[Level])
        {
            _wallet.SetGems(-gemPrice[Level]);
            timer = .1f;
        }
        else
        {
           //_windowsUI.GemShopOpen(0);
        }
        _clickAudios.Play();
    }

    private void UpdateText()
    {
        if (LevelDisplayText != null)
            LevelDisplayText.text = (Level + 1).ToString();

        priceDisplayText.text = Price[Level].ToString();
        gemPriceDisplayText.text = gemPrice[Level].ToString();
    }

    private void UpdateLevel()
    {
        Level++;
        PlayerPrefs.SetInt("Level" + Id, Level);

        if (ActiveElemeny.GetComponent<EarnBuild>())
            ActiveElemeny.GetComponent<EarnBuild>().Upgrade();

        if (ActiveElemeny.GetComponent<Rampage>())
            ActiveElemeny.GetComponent<Rampage>().Upgrade();

        if (ActiveElemeny.GetComponent<IncomeMoney>())
            ActiveElemeny.GetComponent<IncomeMoney>().Upgrade();

        if (ActiveElemeny.GetComponent<BoostDamage>())
            ActiveElemeny.GetComponent<BoostDamage>().Upgrade();

        if (ActiveElemeny.GetComponent<TapDamage>())
            ActiveElemeny.GetComponent<TapDamage>().Upgrade();

        if (ActiveElemeny.GetComponent<UpgradeCatapults>())
            ActiveElemeny.GetComponent<UpgradeCatapults>().Upgrade();
    }

    private void CheckProcess(int cheker)
    {
        if (cheker > 0)
        {
            activeObjects[0].SetActive(false);
            activeObjects[1].SetActive(true);
        }
        else
        {
            activeObjects[0].SetActive(true);
            activeObjects[1].SetActive(false);
        }
    }


    public void SaveTimer()
    {
        PlayerPrefs.SetFloat("timer" + Id, timer);
    }
}
