  

using UnityEngine;
using System;

[Serializable]
public class Quest
{
    public int Wave;
    public int Boss;

    public int TapKillEnemy;

    [Header("REWARD")]
    public int money;
    public int gem;

    public GameObject[] WavePrefabs;
    public GameObject[] GeneralsPrefab;
    public GameObject BossPrefab;
}
