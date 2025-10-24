using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class videoBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            //ADSS.Ins.rewardCallback = () =>
            //{
            //    gameObject.SetActive(false);
            //};
            //ADSS.Ins.ShowRewardVideoAd();
        });
    }
}
