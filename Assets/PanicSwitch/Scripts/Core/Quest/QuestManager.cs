  

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Net.NetworkInformation;
using UnityEditor;
using Newtonsoft.Json;
using System.Collections.Generic;
using TTSDK;
using UnityEngine.Networking;
using System.Collections;

public class QuestManager : MonoBehaviour
{
    public Transform Target;
    [SerializeField] private GameObject[] _starsObject;
    [SerializeField] private TextMeshProUGUI _moneyReward;
    [SerializeField] private TextMeshProUGUI _gemReward;
    [SerializeField] private RandomSpell _randomSpell;
    [SerializeField] private Transform[] _points;
    [SerializeField] private ParticleSystem deadParticles;

    [SerializeField] private Quest[] _quest;

    private MainBuild _mainBuild;
    private GameUI _gameUI;
    private MenuUI _menuUI;
    private int _tempTapKilled;
    private int _tempWave;
    private int _tempBoos;
    private int _tempAllCouner;
    [HideInInspector] public PlayerData _playerData;
    private int _groupCouner;
    private float _groupTimer;
    private int _tempPoint;
    private Wallet _wallet;
    public static bool isWinner;
    public static int ContinueCounter;

    private int _waveLevel;

    private void Awake()
    {
        PlayerPrefs.SetInt("TutorialUI", 1);
        isWinner = false;
    }
    private void Start()
    {
        
        _playerData = FindObjectOfType<PlayerData>();
        _wallet = FindObjectOfType<Wallet>();
        _gameUI = FindObjectOfType<GameUI>();
        _menuUI = FindObjectOfType<MenuUI>();
        _mainBuild = FindObjectOfType<MainBuild>();

        if (PlayerPrefs.HasKey("_waveLevelContinue"))
        {
            _waveLevel = PlayerPrefs.GetInt("_waveLevelContinue");
            _menuUI.StartGame();
            PlayerPrefs.DeleteKey("_waveLevelContinue");
        }
        else
        {
            CheckWaveLevel();
        }

        SetEnemyTarget();

        //_moneyReward.text = _quest[_playerData.GetLevel()].money.ToString();
        // _gemReward.text = _quest[_playerData.GetLevel()].gem.ToString();
    }

    private void SetEnemyTarget()
    {
        foreach (GameObject enemy in _quest[_waveLevel].WavePrefabs)
        {
            foreach (Enemy item in enemy.GetComponentsInChildren<Enemy>())
            {
                item.Target = Target;
                item._wallet = _wallet;
                item._questManager = this;
                item._randomSpell = _randomSpell;
                item.DeadParticles = deadParticles;
            }
        }

        if (_quest[_waveLevel].Boss > 0)
        {
            _quest[_waveLevel].BossPrefab.GetComponent<Boss>().Target = Target;
            _quest[_waveLevel].BossPrefab.GetComponent<Boss>()._wallet = _wallet;
            _quest[_waveLevel].BossPrefab.GetComponent<Boss>()._questManager = this;
        }
    }

    public void AdsX2WinMoneyText()
    {
        _moneyReward.text = (_quest[_waveLevel].money * 2).ToString();
        _moneyReward.color = new Color32(240, 117, 255, 255);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.N))
            TestingWaveDeleteThisFunc();

        if (!MenuUI.IsGame || isWinner) return;

        _groupTimer -= Time.deltaTime;

        if (_groupCouner < 2)
        {
            if (_groupTimer <= 0 && _tempWave < _quest[_waveLevel].Wave)
            {
                _tempPoint++;

                if (_tempPoint >= _points.Length)
                    _tempPoint = 0;

                Instantiate(_quest[_waveLevel].WavePrefabs[_tempWave], _points[_tempPoint].position, Quaternion.identity);

                _groupTimer = 2;
                _groupCouner++;

                if (_quest[_waveLevel].Boss > 0 && _tempWave == _quest[_waveLevel].Wave - 1 && _tempBoos <= 0)
                {
                    Instantiate(_quest[_waveLevel].BossPrefab, _points[_tempPoint].position, Quaternion.identity);
                    _tempBoos++;
                }
            }
        }
        else
        {
            _groupCouner = 0;
            _tempWave++;
        }

        PlayerWinner();
    }

    public void BossDead()
    {
        _tempBoos = 0;
    }

    private void CheckWaveLevel()
    {
        if (_playerData.GetLevel() >= _quest.Length)
            _waveLevel = Random.Range(55, _quest.Length);
        else
            _waveLevel = _playerData.GetLevel();
    }

    public void AdsPlayerContinue()
    {
        PlayerPrefs.SetInt("_waveLevelContinue", _waveLevel);
        PlayerPrefs.SetInt("tempLevel", _playerData.GetLevel());
        ContinueCounter++;
        SceneManager.LoadScene(0);
    }

    private void PlayerWinner()
    {
        if (_tempWave == _quest[_waveLevel].Wave && !isWinner && !MainBuild.Isdead)
        {
            _playerData.SetLevel();
            CheckWaveLevel();
            _groupCouner = 0;
            _tempWave = 0;
            SetEnemyTarget();
            // SetEnemyTarget();
            //_stars++;
            //_gameUI.PlayerWinner(_quest[_playerData.GetLevel()].money, _quest[_playerData.GetLevel()].gem);
            // CheckStars();
            //isWinner = true;
        }
    }

    private void TestingWaveDeleteThisFunc()
    {
        _playerData.SetLevel();
        CheckWaveLevel();
        _groupCouner = 0;
        _tempWave = 0;
        SetEnemyTarget();
    }

    //private void CheckStars()
    //{
    //    if (_mainBuild.health >= 20)
    //        _stars++;

    //    if (_tempTapKilled >= _quest[_playerData.GetLevel()].TapKillEnemy)
    //        _stars++;

    //    for (int i = 0; i < _stars; i++)
    //    {
    //        _starsObject[i].SetActive(true);
    //    }
    //}

    public void TempCouner(int value)
    {
        _tempAllCouner += value;
    }

    public void TapKilled(int value)
    {
        _tempTapKilled += value;
    }

   
}
