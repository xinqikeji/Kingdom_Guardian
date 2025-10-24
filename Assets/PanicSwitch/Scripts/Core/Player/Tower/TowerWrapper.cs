  

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerWrapper : MonoBehaviour
{
    private int towerId;
    private int[] towersBusy = new int[4];
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private GameObject[] _mainTowerButtonsWrapper;
    [SerializeField] private Tower[] towers;
    [SerializeField] private ButtonInfo[] _towerButtonInfo;

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

    public void ActivateTowerButtonsWrapper(int id)
    {
        towerId = id;
        DiactivateUI();
        _mainTowerButtonsWrapper[towersBusy[id]].GetComponent<RectTransform>().position = _buttons[id].GetComponent<RectTransform>().position + new Vector3(0, 45f, 0);
        _mainTowerButtonsWrapper[towersBusy[id]].SetActive(true);
        StartCoroutine(TowerActivator());
    }

    IEnumerator TowerActivator()
    {
        yield return new WaitForSeconds(.15f);
        isActive = true;
    }

    public void DeleteTower()
    {
        towers[towerId].DeleteTower();
        towersBusy[towerId] = 0;
        DiactivateUI();
    }

    public void ActivateTower(int id)
    {
        if (_wallet.GetMoney() >= _towerButtonInfo[id].price)
        {
            UseTower(id);
            _wallet.SetMoney(-_towerButtonInfo[id].price);
        }
    }

    public void UseTower(int id)
    {
        towers[towerId].ActivateTower(id);
        DiactivateUI();
        towersBusy[towerId] = 1;
    }

    private void DiactivateUI()
    {
        _mainTowerButtonsWrapper[0].SetActive(false);
        _mainTowerButtonsWrapper[1].SetActive(false);
        isActive = false;
    }
}
