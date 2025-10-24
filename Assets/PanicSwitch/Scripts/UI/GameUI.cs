  

using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countDisplayText;
    [SerializeField] private TextMeshProUGUI _levelDisplayText;
    [SerializeField] private TextMeshProUGUI _killDisplayText;
    //[SerializeField] private TextMeshProUGUI _levelWinnerDisplayText;
    [SerializeField] private TextMeshProUGUI _levelDeadDisplayText;
    [SerializeField] private TextMeshProUGUI _MoneyDisplayText;
    [SerializeField] private TextMeshProUGUI gemDispalyText;
    [SerializeField] private GameObject adsX2WinMoneyButton;
    [SerializeField] private GameObject WinnerWindow;
    [SerializeField] private GameObject DeadWindow;
    [SerializeField] private GameObject PauseWindow;
    [SerializeField] private GameObject[] _ButtonsLoading;
    private PlayerData _playerData;
    private AppMetEvents _appMetEvents;
    private TimeLastSession _timelastSession;
    [HideInInspector] public Wallet _wallet;
    private SaveTimers _saveTimer;
    public static int countUnit;
    private bool _isPause;
    private QuestManager _questManager;
    private MAXReward _maxReward;

    private float _money;
    private int _gems;
    public static int gameCouner;
    private float _gameTimer;


    private void Awake()
    {
        _wallet = FindObjectOfType<Wallet>();

        if (!PlayerPrefs.HasKey("tutorial"))
        {
            PlayerPrefs.SetInt("tutorial", 1);
        }

        if (PlayerPrefs.HasKey("gameCouner"))
            gameCouner = PlayerPrefs.GetInt("gameCouner");
        else
            gameCouner = 1;

    }

    void Start()
    {
        _playerData = FindObjectOfType<PlayerData>();
        _timelastSession = FindObjectOfType<TimeLastSession>();
        _appMetEvents = FindObjectOfType<AppMetEvents>();
        _saveTimer = FindObjectOfType<SaveTimers>();
        _questManager = FindObjectOfType<QuestManager>();
        _maxReward = FindObjectOfType<MAXReward>();

        countUnit = 0;


        _levelDisplayText.text = (_playerData.GetLevel() + 1).ToString();
        // _levelWinnerDisplayText.text = "Level " + (_playerData.GetLevel() + 1);       

        _MoneyDisplayText.text = _wallet.GetMoney().ToString();
        gemDispalyText.text = _wallet.GetGems().ToString();

        _wallet.GemsChange.AddListener((_gems) =>
        {
            gemDispalyText.text = _gems.ToString();
        });

        _playerData.LevelChange.AddListener((level) =>
        {
            _levelDisplayText.text = (_playerData.GetLevel() + 1).ToString();
        });

        _wallet.MoneyChange.AddListener((_money) =>
        {
            _MoneyDisplayText.text = _money.ToString("f0");
        });
    }

    public void UpdateCurrencyText()
    {
        StartCoroutine(UpdateCourtineCurrency());
    }

    IEnumerator UpdateCourtineCurrency()
    {
        yield return new WaitForSeconds(2);
        gemDispalyText.text = _wallet.GetGems().ToString();
        _MoneyDisplayText.text = _wallet.GetMoney().ToString("f0");
    }

    private void Update()
    {

        if (!QuestManager.isWinner || !MainBuild.Isdead)
            _gameTimer += Time.deltaTime;
    }

    public void AddGameCounter(int value)
    {
        gameCouner +=value;
        PlayerPrefs.SetInt("gameCouner", gameCouner);
    }

    public void PlayerDeath()
    {
        AdsButton.TimeScaleValue = 1;
        Time.timeScale = 1;        
        _appMetEvents.LevelFinishEvent((_playerData.GetLevel() + 1), "lose", gameCouner, _gameTimer);
        _levelDeadDisplayText.text = "" + (_playerData.GetLevel() + 1);
        DeadWindow.SetActive(true);
    }

    public void PlayerWinner(float money, int gems)
    {
        WinnerWindow.SetActive(true);
        _money = money;
        _gems = gems;
        //  _appMetEvents.LevelFinishEvent((_playerData.GetLevel() + 1), "win", gameCouner, _gameTimer);
    }

    public void AdsWinnerX2Money()
    {
        _money *= 2;
        _questManager.AdsX2WinMoneyText();
        adsX2WinMoneyButton.SetActive(false);
    }

    private void WinnewSetMoneyGems()
    {
        _wallet.SetMoney(_money);
        _wallet.SetGems(_gems);
        _playerData.SetLevel();
    }

    public void PauseActive()
    {
        _isPause = !_isPause;
        PauseWindow.SetActive(_isPause);

        if (_isPause)
            Time.timeScale = 0;
        else
            Time.timeScale = AdsButton.TimeScaleValue;
    }

    public void RestartGame(int id)
    {
        Time.timeScale = 1;

        //0-меню 1-рестарт

        switch (id)
        {
            case 1:
                PlayerPrefs.SetInt("NextLevel", 1);
                break;

            case 2:
                _appMetEvents.LevelFinishEvent((_playerData.GetLevel() + 1), "restart", gameCouner, _gameTimer);
                break;
        }

        AddGameCounter(1);
        _timelastSession.SaveTime();
        _saveTimer.SaveTimerEvent();
        SceneManager.LoadScene(0);
    }


    public void BackToBuildWinner(int id)
    {
        _ButtonsLoading[0].SetActive(false);
        _ButtonsLoading[1].SetActive(true);

        Time.timeScale = 1;
        _timelastSession.SaveTime();
        _saveTimer.SaveTimerEvent();

        if (id > 0)
            WinnewSetMoneyGems();

        SceneManager.LoadScene(1);

    }

    public void NextLevel()
    {
        _ButtonsLoading[0].SetActive(false);
        _ButtonsLoading[1].SetActive(true);

        WinnewSetMoneyGems();
        PlayerPrefs.SetInt("NextLevel", 1);
        _timelastSession.SaveTime();
        _saveTimer.SaveTimerEvent();
        SceneManager.LoadScene(0);
    }

    public void DeleteAllSaves()
    {
        PlayerPrefs.DeleteAll();
    }
}
