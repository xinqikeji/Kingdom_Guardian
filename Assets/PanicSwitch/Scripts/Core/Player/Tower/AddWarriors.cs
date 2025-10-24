  

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddWarriors : MonoBehaviour
{
    private int _lineId;
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private GameObject _mainWarriorButtonsWrapper;
    [SerializeField] private WarriorsCoreUpgrade[] warriors;
    [SerializeField] private Button adsButton;
    private int[] lineCounter = new int[2];

    private bool isActive;
    private Wallet _wallet;

    private void Start()
    {
        _wallet = FindObjectOfType<Wallet>();
    }

    private void Update()
    {
        if (isActive)
        {
            if (Input.touchCount > 0)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId) && Tutorial.isTutorial)
                {
                    DiactivateUI();
                }
            }
        }
    }

    public void AddUnit(int lineID)
    {
        if (lineCounter[lineID] > 4) return;

        // if (_wallet.GetMoney() >= 600)
        //  {
        // _wallet.SetMoney(-600);
        AddFunc(lineID);
        //  }
    }

    public void AdsAddUnity()
    {
        if (lineCounter[_lineId] > 4) return;
        //AddFunc();
    }

    private void AddFunc(int id)
    {
       // warriors[id].AddUnits();
        lineCounter[id]++;
        CheckAdsButton();
    }

    private void CheckAdsButton()
    {
        if (lineCounter[_lineId] > 4)
            adsButton.enabled = false;
        else
            adsButton.enabled = true;
    }

    public void ActivateWarriorButtonsWrapper(int id)
    {
        _lineId = id;
        DiactivateUI();
        _mainWarriorButtonsWrapper.GetComponent<RectTransform>().position = _buttons[id].GetComponent<RectTransform>().position + new Vector3(0, 0, 0);
        _mainWarriorButtonsWrapper.SetActive(true);
        StartCoroutine(TowerActivator());
        CheckAdsButton();
    }

    IEnumerator TowerActivator()
    {
        yield return new WaitForSeconds(.15f);
        isActive = true;
    }

    private void DiactivateUI()
    {
        _mainWarriorButtonsWrapper.SetActive(false);
        isActive = false;
    }
}
