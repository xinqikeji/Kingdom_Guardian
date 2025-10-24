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
                                
                                //isValid广告是否播放完，正常游戏逻辑在以下部分
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
        // 创建一个字典来存储POST请求的数据
        Dictionary<string, object> postData = new Dictionary<string, object>
    {
        { "event_type", "micro_game_ltv" },
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
