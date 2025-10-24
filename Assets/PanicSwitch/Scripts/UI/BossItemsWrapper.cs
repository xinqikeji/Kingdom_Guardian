  

using UnityEngine;

public class BossItemsWrapper : MonoBehaviour
{
    private BossItem[] _bossItem;
    private PlayerData _playerData;

    void Start()
    {
        _bossItem = GetComponentsInChildren<BossItem>();
        _playerData = FindObjectOfType<PlayerData>();

        for (int i = 0; i < (_playerData.GetLevel()) / 5; i++)
        {
            _bossItem[i].Check(1);
        }

        _bossItem[_playerData.GetLevel() / 5].Check(0);
    }
}
