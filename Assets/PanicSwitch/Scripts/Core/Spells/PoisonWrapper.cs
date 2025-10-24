  

using System.Collections;
using UnityEngine;

public class PoisonWrapper : MonoBehaviour
{
    public int damage = 10;

    private void Start()
    {
        StartCoroutine(Disactivate());
    }

    IEnumerator Disactivate()
    {
        yield return new WaitForSeconds(40);
        Destroy(gameObject);
    }
}
