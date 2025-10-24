  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Enemy", menuName ="Enemy")]
public class EnemyWarrior : ScriptableObject
{
    public int health;
    public int attack;
    public float attackTimer;

    public GameObject hat;
}
