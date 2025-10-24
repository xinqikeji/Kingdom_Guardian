using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TTSDK;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Shopwindow : MonoBehaviour
{
    public Button guankanguanggao;
    public ByteGameAdManager ByteGameAdManager;
    public string id;
    public WarriorsCoreUpgrade warriorsCoreUpgrade;
    public WarriorsCoreUpgrade warriorsCoreUpgrade1;
    public IncomeMoney incomeMoney;
    public Button Improve;
    public Button warrior;
    public Button monetforkill;
    public int AD;
    public WindowsUI WindowsUI;
    // Start is called before the first frame update
    void Start()
    {
        Improve.onClick.AddListener(() =>{
            AD = 1;
        });
        warrior.onClick.AddListener(() => {
            AD = 2;
        });
        monetforkill.onClick.AddListener(() => {
            AD = 3;

        });
        guankanguanggao.onClick.AddListener(() =>
        {
            ByteGameAdManager.PlayRewardedAd(id,
                            (isValid, duration) =>
                            {
                                
                                //isValid����Ƿ񲥷��꣬������Ϸ�߼������²���
                                Debug.LogError(0);
                                if (isValid)
                                {
                                    StartCoroutine(SendPostRequest());
                                    switch (AD)
                                    {
                                        case 1:
                                            warriorsCoreUpgrade.UseUpgrade();
                                            warriorsCoreUpgrade.CheckAdsImage();
                                            break;
                                        case 2:
                                            warriorsCoreUpgrade1.UseUpgrade();
                                            warriorsCoreUpgrade1.CheckAdsImage();
                                            break;
                                        case 3:
                                            incomeMoney.Upgrade();
                                            break;
                                    }
                                    WindowsUI.GemShopOpen(0);
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
