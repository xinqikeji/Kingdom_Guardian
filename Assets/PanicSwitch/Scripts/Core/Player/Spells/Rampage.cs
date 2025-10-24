  

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Rampage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerRampageDisplayText;
    [SerializeField] private TextMeshProUGUI[] multiplierDisplayText;
    [SerializeField] private WarriorsCoreUpgrade[] _warriorsLine;
    [SerializeField] private int[] price;
    private float timerRampage;
    [SerializeField] private float[] _cycle;
    [SerializeField] private Image[] progress;
    [SerializeField] private GameObject[] _activeUITimer;
    private int[] _multiplier = new int[2];
    private float[] _timer = new float[2];
    private bool _isActive1;
    private bool _isActive2;
    private Wallet _wallet;
    private AudioManager _audioManager;

    private void Start()
    {
        _wallet = FindObjectOfType<Wallet>();
        _audioManager = FindObjectOfType<AudioManager>();

        if (PlayerPrefs.HasKey("timerRampage"))
        {
            timerRampage = PlayerPrefs.GetFloat("timerRampage");
        }
        else
        {
            timerRampage = 10;
            PlayerPrefs.SetFloat("timerRampage", timerRampage);
        }

        UpdateText();
    }
    private void Update()
    {
        if (_isActive1)
        {
            ActivePhase(0);
        }

        if (_isActive2)
        {
            ActivePhase(1);
        }
    }

    public void UseRampage(int id)
    {
        if (id > 0)
        {
            if (_wallet.GetMoney() >= 950)
            {
                _wallet.SetMoney(-950);
                RampageFunc(id);
            }            
        }
        else
        {
            if (_wallet.GetMoney() >= 800)
            {
                _wallet.SetMoney(-800);
                RampageFunc(id);
            }
        }       
    }

    public void AdsRampage(int id)
    {
        RampageFunc(id);
    }

    private void RampageFunc(int id)
    {
        _timer[id] = timerRampage;
        _warriorsLine[id].UseRampage(_cycle[_multiplier[id]]);
        _activeUITimer[id].SetActive(true);

        if (_multiplier[id] < 4)
            _multiplier[id]++;

        multiplierDisplayText[id].text = "X" + (_multiplier[id] + 1);

        if (id > 0)
            _isActive2 = true;
        else
            _isActive1 = true;

        _audioManager.SFXPlay(8);
    }

    public void RampageButtonLine(int id)
    {
        if (_wallet.GetMoney() >= price[id])
        {
            _wallet.SetMoney(-price[id]);
            UseRampage(id);
        }
    }

    private void StopRampage(int id)
    {
        _warriorsLine[id].StopRampage();

        if (id > 0)
            _isActive2 = false;
        else
            _isActive1 = false;
    }

    private void ActivePhase(int id)
    {
        _timer[id] -= Time.deltaTime;
        progress[id].fillAmount = _timer[id] / timerRampage;

        if (_timer[id] <= 0)
        {
            StopRampage(id);
            _activeUITimer[id].SetActive(false);
        }
    }

    public void Upgrade()
    {
        timerRampage += .5f;
        PlayerPrefs.SetFloat("timerRampage", timerRampage);
        UpdateText();
    }

    private void UpdateText()
    {
        timerRampageDisplayText.text = timerRampage.ToString("f1") + "s";
    }
}
