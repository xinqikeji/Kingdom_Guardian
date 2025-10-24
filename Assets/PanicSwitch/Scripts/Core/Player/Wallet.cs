  

using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public DataEventFloat MoneyChange;
    public DataEvent GemsChange;
    private float _money;
    private int _gems;
    private AudioSource _audioSource;


    private void Awake()
    {

        _money = PlayerPrefs.GetFloat("_money");
        _gems = PlayerPrefs.GetInt("_gems");

        if (GemsChange == null)
            GemsChange = new DataEvent();

        if (MoneyChange == null)
            MoneyChange = new DataEventFloat();

    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("firstStart"))
        {
            SetMoney(1500);
            SetGems(10);
            PlayerPrefs.SetInt("firstStart", 1);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddMoneyCheat();
        }
    }

    //Нужно удалить метод !!!!!!!!!!!!!!!
    public void AddMoneyCheat()
    {
        SetMoney(100000);
        SetGems(1000);
    }

    public void CoinSoundPlay()
    {
        _audioSource.Play();
    }

    public int GetGems()
    {
        return _gems;
    }

    public int GetMoney()
    {
        return (int)_money;
    }

    public void SetGems(int gem)
    {
        _gems += gem;
        PlayerPrefs.SetInt("_gems", _gems);
        GemsChange.Invoke(_gems);
    }

    public void SetMoney(float moneys)
    {
        _money += moneys;
        MoneyChange.Invoke(_money);
        PlayerPrefs.SetFloat("_money", _money);
    }
}

[System.Serializable]
public class DataEvent : UnityEvent<int>
{
}

[System.Serializable]
public class DataEventFloat : UnityEvent<float>
{
}
