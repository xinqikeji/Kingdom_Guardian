  

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EarnBuild : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _earnDisplayText;
    [SerializeField] private TextMeshProUGUI _earnDisplayTextWindow;
    [SerializeField] private TextMeshProUGUI _earnOfflineDisplayText;
    [SerializeField] private Image _progress;
    [SerializeField] private GameObject EarnWindow;

    private int level;
    private float timer;
    private Wallet _wallet;
    [HideInInspector] public int OfflineEarn;
    private static bool _inGame;

    private void Awake()
    {
        level = PlayerPrefs.GetInt("earnLevel") + 1;
    }

    void Start()
    {
        _wallet = FindObjectOfType<Wallet>();

        if (TimeLastSession.ts.TotalMinutes > 1 && !_inGame)
        {
            if (TimeLastSession.ts.TotalHours >= 8)
            {
                OfflineEarn = 1000;
            }
            else
            {
                OfflineEarn = (int)TimeLastSession.ts.TotalMinutes * level;
            }

            _earnOfflineDisplayText.text = "+" + OfflineEarn;
            EarnWindow.SetActive(true);
        }

        _inGame = true;
        UpgradeText();
    }

    private void Update()
    {
        if (timer >= 60)
        {
            _wallet.SetMoney(level);
            timer = 0;
        }
        else
        {
            _progress.fillAmount = timer / 60;
            timer += Time.deltaTime;
        }
    }

    public void Upgrade()
    {
        level++;
        PlayerPrefs.SetInt("earnLevel", level);
        UpgradeText();
    }

    private void UpgradeText()
    {
        _earnDisplayText.text = "+" + level + "/min";
        _earnDisplayTextWindow.text = "+" + level+"/min";
    }

    public void CloseWindow()
    {
        _wallet.SetMoney(OfflineEarn);
        Close();
    }

    public void AdsCollectX2()
    {
        _wallet.SetMoney(OfflineEarn*2);
        Close();
    }

    public void Close()
    {
        EarnWindow.SetActive(false);
        OfflineEarn = 0;
    }
}
