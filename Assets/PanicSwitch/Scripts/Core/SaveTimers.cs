  

using UnityEngine;

public class SaveTimers : MonoBehaviour
{
    private UpgradeDetails[] _upgradeDetails;
    private int _count;

    void Start()
    {
        _upgradeDetails = FindObjectsOfType<UpgradeDetails>();
        _count = _upgradeDetails.Length;
    }

    public void SaveTimerEvent()
    {
        for (int i = 0; i < _count; i++)
            _upgradeDetails[i].SaveTimer();
    }
}
