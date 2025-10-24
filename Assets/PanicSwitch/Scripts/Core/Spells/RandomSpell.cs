  

using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using TTSDK;
using UnityEngine.Networking;

public class RandomSpell : MonoBehaviour
{
    [SerializeField] private Animator randomSpellPopup;
    [SerializeField] private GameObject adsImage;
    [SerializeField] private Image progress;
    [SerializeField] private GameObject[] spellImages;
    [SerializeField] private int[] spellID;
    private SpellsWrapper spellsWrapper;
    private int progressCounter;
    private bool isActive;
    private int _randomSpell;
    private float _spinTimer;
    private MAXReward _maxReward;
    public ByteGameAdManager ByteGameAdManager;
    // Start is called before the first frame update
    void Start()
    {
        spellsWrapper = FindObjectOfType<SpellsWrapper>();
        _maxReward = FindObjectOfType<MAXReward>();
    }

    public void AdsUpdateSpell()
    {
        ClosePopup();
        progressCounter = 250;
        RandomSpellProgess();
    }

    public void RandomSpellProgess()
    {
        if (isActive) return;

        progressCounter++;

        if (progressCounter >= 250)
        {
            isActive = true;
            StartCoroutine(ActivatorSpell());
        }

        progress.fillAmount = (float)progressCounter / 250;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (_spinTimer <= .3f)
            {
                _randomSpell = Random.Range(0, spellID.Length);
                _spinTimer = 0;
                SpinImage(_randomSpell);
            }
            else
                _spinTimer -= Time.deltaTime;
        }
    }

    IEnumerator ActivatorSpell()
    {
        yield return new WaitForSeconds(3f);
        progressCounter = 0;
        isActive = false;
        SpinImage(_randomSpell);
        spellsWrapper.AdsSpells(spellID[_randomSpell]);
        yield return new WaitForSeconds(3f);
        SpinImage(7);
    }

    private void SpinImage(int _id)
    {
        for (int i = 0; i < spellImages.Length; i++)
            spellImages[i].SetActive(false);

        spellImages[_id].SetActive(true);
    }

    public void OpenPopup()
    {
        CheckAdsImage();
        randomSpellPopup.SetTrigger("show");
    }

    public void AdsUseRandomSpell()
    {
        if (PlayerPrefs.HasKey("freeRandomSpell"))
        {
            ByteGameAdManager.PlayRewardedAd("mi5ag76n0bg4506577",
                            (isValid, duration) =>
                            {
                                
                                //isValid����Ƿ񲥷��꣬������Ϸ�߼������²���
                                Debug.LogError(0);
                                if (isValid)
                                {
                                    StartCoroutine(SendPostRequest());
                                    AdsUpdateSpell();


                                }


                            },
                            (errCode, errMsg) =>
                            {
                                Debug.LogError(1);
                            });
            //_maxReward.AdsGetReward(33);
            
        }
        else
        {
            AdsUpdateSpell();
            PlayerPrefs.SetInt("freeRandomSpell", 1);
        }
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
    public void ClosePopup()
    {
        randomSpellPopup.SetTrigger("hide");
    }

    private void CheckAdsImage()
    {
        if (PlayerPrefs.HasKey("freeRandomSpell"))
            adsImage.SetActive(true);
        else
            adsImage.SetActive(false);
    }

}
