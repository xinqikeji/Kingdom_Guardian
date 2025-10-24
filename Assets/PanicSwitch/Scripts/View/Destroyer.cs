  

using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private Enemy[] _enemys;
    private void Start()
    {      
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(40);
        _enemys = GetComponentsInChildren<Enemy>();

        for (int i = 0; i < _enemys.Length; i++)
            _enemys[i].TakeDamage(1000, 0);

        StopCoroutine(Destroy());
    }
}
