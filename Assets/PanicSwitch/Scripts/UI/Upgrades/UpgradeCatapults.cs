  

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCatapults : MonoBehaviour
{
   // [SerializeField] private GameObject[] catapults;
   // [SerializeField] private Image _progress;
    [SerializeField] private TextMeshProUGUI _attackDisplayText;

    [SerializeField] private int[] _attack;

    private UpgradeDetails _upgradeDetails;

    private int _cycle;
    private int _currentLevel;

    void Start()
    {
        _upgradeDetails = GetComponent<UpgradeDetails>();

        if (PlayerPrefs.HasKey("catapultCycle"))
        {
            _cycle = PlayerPrefs.GetInt("catapultCycle");
        }


        //if (PlayerPrefs.HasKey("_currentLevelCatapult"))
        //{
        //    catapults[_cycle].SetActive(true);
        //}

        _currentLevel = PlayerPrefs.GetInt("_currentLevelCatapult");

        UpdateText();
    }


    public void Upgrade()
    {
        _currentLevel++;

        if (_currentLevel > 5)
        {
            if (_cycle < 8)
                _cycle++;

            _currentLevel = 0;
            PlayerPrefs.SetInt("catapultCycle", _cycle);
        }

        PlayerPrefs.SetInt("_currentLevelCatapult", _currentLevel);
       // UpdateCatapults();
        UpdateText();
    }

    //private void UpdateCatapults()
    //{

    //    for (int i = 0; i < catapults.Length; i++)
    //        catapults[i].SetActive(false);

    //    catapults[_cycle].SetActive(true);
    //}

    private void UpdateText()
    {
        _attackDisplayText.text = _attack[_upgradeDetails.Level].ToString();
       // _progress.fillAmount = (float)_currentLevel / 5;
    }
}
