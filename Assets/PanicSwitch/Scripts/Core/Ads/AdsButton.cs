  

using UnityEngine;
using UnityEngine.UI;

public class AdsButton : MonoBehaviour
{
    /// <summary>
    /// 0 - X3 speed timer;
    /// 1 - X3 money
    /// /// </summary>
    [SerializeField] private float[] generalSpellTimers;
    [SerializeField] private GameObject[] spellObjects;
    [SerializeField] private Image[] timerProgess;
    private float[] _timer;

    private bool _isTimeBonus;
    private bool _isMoneyBonus;
    public static float TimeScaleValue = 1;


    public static int MoneyBonusX = 1;

    private void Awake()
    {
        TimeScaleValue = 1;
        _timer = new float[generalSpellTimers.Length];
    }

    public void UseAdsSpell(int id)
    {
        _timer[id] = generalSpellTimers[id];
        Debug.Log(id);
        switch (id)
        {
            case 0:
                TimeScaleValue = 2;
                Time.timeScale = TimeScaleValue;
                _isTimeBonus = true;
                break;
            case 1:
                MoneyBonusX = 3;
                _isMoneyBonus = true;
                break;
        }

        spellObjects[id].SetActive(true);
    }

    void Update()
    {
        if (_isTimeBonus)
            UseingBonus(0);

        if (_isMoneyBonus)
            UseingBonus(1);
    }

    private void UseingBonus(int id)
    {
        if (_timer[id] > 0)
        {
            _timer[id] -= Time.deltaTime;
            timerProgess[id].fillAmount = _timer[id] / generalSpellTimers[id];
        }
        else
        {
            _timer[id] = 0;
            spellObjects[id].SetActive(false);

            switch (id)
            {
                case 0:
                    TimeScaleValue = 1;
                    Time.timeScale = TimeScaleValue;
                    _isTimeBonus = false;
                    break;
                case 1:
                    MoneyBonusX = 1;
                    _isMoneyBonus = false;
                    break;
            }
        }
    }
}
