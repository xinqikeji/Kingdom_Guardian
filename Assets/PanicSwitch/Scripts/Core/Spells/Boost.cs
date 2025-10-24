  

using UnityEngine.UI;
using UnityEngine;

public class Boost : MonoBehaviour
{
    [SerializeField] private Image progressDisplayImage;
    [SerializeField] private float timerBoost;
    [SerializeField] private bool isGold;
    private float _tempTimer;
    private bool isActive;
    public static float TimeScaleValue = 1;

    public void UseBoost()
    {

    }

    void Update()
    {
        if (isActive)
        {
            if (_tempTimer > 0)
            {
                _tempTimer -= Time.deltaTime;
            }
            else
            {
                _tempTimer = 0;
                isActive = false;
            }
        }
    }
}
