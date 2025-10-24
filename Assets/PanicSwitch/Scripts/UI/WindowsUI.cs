  

using UnityEngine;
using UnityEngine.UI;

public class WindowsUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttonActive;
    [SerializeField] private GameObject globalWindow;
    [SerializeField] private GameObject[] UpgradeWindows;
    [SerializeField] private GameObject[] ShopWindow;
    private AudioSource _clickAudios;

    private bool _isopen;
    private bool _isopengem;

    private void Start()
    {
        _clickAudios = GetComponent<AudioSource>();
    }


    public void GemShopOpen(int id)
    {
        _clickAudios.Play();

        if (_isopengem)
            ShopWindow[id].SetActive(false);
        else
            ShopWindow[id].SetActive(true);

        _isopengem = !_isopengem;
    }

    public void UpgradeWindowsShow()
    {
        _clickAudios.Play();

        if (_isopen)
            globalWindow.SetActive(false);
        else
            globalWindow.SetActive(true);

        _isopen = !_isopen;
    }

    

    public void WindowTabs(int id)
    {
        _clickAudios.Play();

        for (int i = 0; i < UpgradeWindows.Length; i++)
        {
            UpgradeWindows[i].SetActive(false);
            _buttonActive[i].SetActive(false);
        }

        UpgradeWindows[id].SetActive(true);
        _buttonActive[id].SetActive(true);

    }
}
