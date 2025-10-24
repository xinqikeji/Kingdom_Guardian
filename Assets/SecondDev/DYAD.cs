using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TTSDK;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
[Serializable]
public class ADcoin
{
    public Button ADbtn;
    public string ad;
    public int coin;
}
[Serializable]
public class ADtimecoin
{
    public Button ADbtn;
    public string ad;
    public int beishu;
}
public class DYAD : MonoBehaviour
{
    public ByteGameAdManager ByteGameAdManager;
    public ADcoin[] ADBTNs = new ADcoin[0];
    public ADtimecoin[] ADBTN2 = new ADtimecoin[0];
    private Wallet _wallet;
    public AdsButton _boosts;
    public Button countinuebtn;
    public string countinueid;
    private QuestManager _questManager;

    // Start is called before the first frame update
    void Start()
    {
        _questManager = FindObjectOfType<QuestManager>();
        _wallet = FindObjectOfType<Wallet>();
        InitADBtns();
    }
    public void InitADBtns()
    {
        foreach(var item in ADBTNs)
        {
            item.ADbtn.onClick.AddListener(() =>
            {
                ByteGameAdManager.PlayRewardedAd(item.ad,
                            (isValid, duration) =>
                            {
                               
                                //isValid����Ƿ񲥷��꣬������Ϸ�߼������²���
                                Debug.LogError(0);
                                if (isValid)
                                {
                                    StartCoroutine(SendPostRequest());
                                    _wallet.SetMoney(item.coin);
                                    

                                }


                            },
                            (errCode, errMsg) =>
                            {
                                Debug.LogError(1);
                            });
            });
        }
        foreach (var item in ADBTN2)
        {
            item.ADbtn.onClick.AddListener(() =>
            {
                ByteGameAdManager.PlayRewardedAd(item.ad,
                            (isValid, duration) =>
                            {
                              
                                //isValid����Ƿ񲥷��꣬������Ϸ�߼������²���
                                Debug.LogError(0);
                                if (isValid)
                                {
                                    StartCoroutine(SendPostRequest());
                                    _boosts.UseAdsSpell(item.beishu);


                                }


                            },
                            (errCode, errMsg) =>
                            {
                                Debug.LogError(1);
                            });
            });
        }
        countinuebtn.onClick.AddListener(() =>
        {
            ByteGameAdManager.PlayRewardedAd(countinueid,
                            (isValid, duration) =>
                            {
                              
                                //isValid����Ƿ񲥷��꣬������Ϸ�߼������²���
                                Debug.LogError(0);
                                if (isValid)
                                {
                                    StartCoroutine(SendPostRequest());
                                    _questManager.AdsPlayerContinue();


                                }


                            },
                            (errCode, errMsg) =>
                            {
                                Debug.LogError(1);
                            });
            
        });

    }
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
        // ����һ���ֵ����洢POST���������
        Dictionary<string, object> postData = new Dictionary<string, object>
    {
        { "event_type", "micro_game_ltv" },
        { "context", new Dictionary<string, object>
            {
                { "ad", new Dictionary<string, object>
                {
                        { "callback", launchOption.Query["clickid"]} // �滻Ϊʵ�ʵ�clickid   
                    }
                }
            }
        },
        { "timestamp", System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() } // ��ǰʱ���
    };
        // ���ֵ�ת��ΪJSON��ʽ
        string json = JsonConvert.SerializeObject(postData);
        // ����UnityWebRequest����
        using (UnityWebRequest request = UnityWebRequest.Post(url, json))
        {
            // ��������ͷ
            request.SetRequestHeader("Content-Type", "application/json");

            // ����POST�����body
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            // ��������
            yield return request.SendWebRequest();

            // ��������Ƿ�ɹ�
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
