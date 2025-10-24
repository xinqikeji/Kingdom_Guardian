using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
using TTSDK;
using UnityEngine.Networking;
using System.Collections;

public class MenuUI : MonoBehaviour
{
    public static bool IsGame;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject GameUIWrapper;
    [SerializeField] private GameObject MenuUIWrapper;
    [SerializeField] private GameObject[] PlayerInfoLevel;
    [SerializeField] private TextMeshProUGUI[] RecordLevelDisplayText;

    [SerializeField] private GameObject startscene;

    private PlayerData _playerData;
    [SerializeField] TMP_InputField _usernameText;
    private AudioManager _audioManager;
    private Wallet _wallet;
    private AppMetEvents _appMetEvents;
    private RandomChest _randomChest;
    //private AppMetEvents _appMetEvents;

    private void Awake()
    {
        IsGame = false;
        PlayerInfoLevel[1].SetActive(false);
        PlayerInfoLevel[0].SetActive(true);
        _audioManager = GetComponent<AudioManager>();
        _playerData = FindObjectOfType<PlayerData>();
        _appMetEvents = FindObjectOfType<AppMetEvents>();
        _randomChest = FindObjectOfType<RandomChest>();
    }

    private void Start()
    {
       
        // _wallet = GetComponent<GameUI>()._wallet; 
        // _appMetEvents = FindObjectOfType<AppMetEvents>();

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        RecordLevelDisplayText[0].text = (_playerData.GetRecordLevel() + 1).ToString();
        RecordLevelDisplayText[1].text = (_playerData.GetRecordLevel() + 1).ToString();

        if (PlayerPrefs.HasKey("NextLevel"))
        {
            StartGame();
            PlayerPrefs.DeleteKey("NextLevel");
        }

        

        if (PlayerPrefs.HasKey("username"))
            _usernameText.text = PlayerPrefs.GetString("username");
        StartCoroutine(SendPostRequest());
        StartCoroutine(SendPostRequest1());
        StartCoroutine(SendPostRequest2());

    }

    public void SaveUserName()
    {
        PlayerPrefs.SetString("username", _usernameText.text);
    }

    public void StartGame()
    {
        
            IsGame = true;
        

        if (!PlayerPrefs.HasKey("_waveLevelContinue"))
        {
            QuestManager.ContinueCounter = 0;
            _appMetEvents.LevelStartEvent("normal", (_playerData.GetLevel() + 1), GameUI.gameCouner);
        }


        _randomChest.StartChestTimer();
        _pauseButton.SetActive(true);
        _audioManager.SFXPlay(7);
        GameUIWrapper.SetActive(true);
        MenuUIWrapper.SetActive(false);
        PlayerInfoLevel[0].SetActive(false);
        PlayerInfoLevel[1].SetActive(true);

        startscene.SetActive(false);


        // _musicDelay.StartMusic();
        Time.timeScale = AdsButton.TimeScaleValue;
    }

    //ecpm返回值
    // 替换为实际的URL
    private string url = "https://analytics.oceanengine.com/api/v2/conversion";
    IEnumerator SendPostRequest()
    {
        TTSDK.LaunchOption launchOption = TT.GetLaunchOptionsSync();
        Debug.LogError(launchOption.Query);
        if (launchOption.Query != null)
        {
            foreach (KeyValuePair<string, string> kv in launchOption.Query)
                if (kv.Value != null)
                    Debug.LogError(kv.Key + ": " + kv.Value);
                else
                    Debug.Log(kv.Key + ": " + "null ");
        }
        // 创建一个字典来存储POST请求的数据
        Dictionary<string, object> postData = new Dictionary<string, object>
        {
            { "event_type", "active" },
            { "context", new Dictionary<string, object>
                {
                    { "ad", new Dictionary<string, object>
                    {
                            { "callback", launchOption.Query["clickid"]} // 替换为实际的clickid   
                        }
                    }
                }
            },
            { "timestamp", System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() } // 当前时间戳
        };
        // 将字典转换为JSON格式
        string json = JsonConvert.SerializeObject(postData);
        // 创建UnityWebRequest对象
        using (UnityWebRequest request = UnityWebRequest.Post(url, json))
        {
            // 设置请求头
            request.SetRequestHeader("Content-Type", "application/json");

            // 设置POST请求的body
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            // 发送请求
            yield return request.SendWebRequest();

            // 检查请求是否成功
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("sssError: " + request.error);
            }
            else
            {
                Debug.Log("sssResponse: " + request.downloadHandler.text);
            }
        }
    }
    IEnumerator SendPostRequest1()
    {
        yield return new WaitForSeconds(1.5f); // 新增：等待1秒
        TTSDK.LaunchOption launchOption = TT.GetLaunchOptionsSync();
        Debug.LogError(launchOption.Query);
        if (launchOption.Query != null)
        {
            foreach (KeyValuePair<string, string> kv in launchOption.Query)
                if (kv.Value != null)
                    Debug.LogError(kv.Key + ": " + kv.Value);
                else
                    Debug.Log(kv.Key + ": " + "null ");
        }
        // 创建一个字典来存储POST请求的数据
        Dictionary<string, object> postData = new Dictionary<string, object>
        {
            { "event_type", " effective_active" },
            { "context", new Dictionary<string, object>
                {
                    { "ad", new Dictionary<string, object>
                    {
                            { "callback", launchOption.Query["clickid"]} // 替换为实际的clickid   
                        }
                    }
                }
            },
            { "timestamp", System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() } // 当前时间戳
        };
        // 将字典转换为JSON格式
        string json = JsonConvert.SerializeObject(postData);
        // 创建UnityWebRequest对象
        using (UnityWebRequest request = UnityWebRequest.Post(url, json))
        {
            // 设置请求头
            request.SetRequestHeader("Content-Type", "application/json");

            // 设置POST请求的body
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            // 发送请求
            yield return request.SendWebRequest();

            // 检查请求是否成功
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("sssError: " + request.error);
            }
            else
            {
                Debug.Log("sssResponse: " + request.downloadHandler.text);
            }
        }
    }
    IEnumerator SendPostRequest2()
    {
        yield return new WaitForSeconds(1f); // 新增：等待1秒
        TTSDK.LaunchOption launchOption = TT.GetLaunchOptionsSync();
        Debug.LogError(launchOption.Query);
        if (launchOption.Query != null)
        {
            foreach (KeyValuePair<string, string> kv in launchOption.Query)
                if (kv.Value != null)
                    Debug.LogError(kv.Key + ": " + kv.Value);
                else
                    Debug.Log(kv.Key + ": " + "null ");
        }
        // 创建一个字典来存储POST请求的数据
        Dictionary<string, object> postData = new Dictionary<string, object>
        {
            { "event_type", "create_gamerole" },
            { "context", new Dictionary<string, object>
                {
                    { "ad", new Dictionary<string, object>
                    {
                            { "callback", launchOption.Query["clickid"]} // 替换为实际的clickid   
                        }
                    }
                }
            },
            { "timestamp", System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() } // 当前时间戳
        };
        // 将字典转换为JSON格式
        string json = JsonConvert.SerializeObject(postData);
        // 创建UnityWebRequest对象
        using (UnityWebRequest request = UnityWebRequest.Post(url, json))
        {
            // 设置请求头
            request.SetRequestHeader("Content-Type", "application/json");

            // 设置POST请求的body
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            // 发送请求
            yield return request.SendWebRequest();

            // 检查请求是否成功
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("sssError: " + request.error);
            }
            else
            {
                Debug.Log("sssResponse: " + request.downloadHandler.text);
            }
        }
    }

}
