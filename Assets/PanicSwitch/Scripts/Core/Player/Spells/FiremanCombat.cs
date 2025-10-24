  

using UnityEngine;
using System.Collections;

public class FiremanCombat : MonoBehaviour
{
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        StartCoroutine(PSStart());
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<Enemy>())
        {
            other.GetComponent<Enemy>().TakeDamage(1+BoostDamage.damage, 0);
        }

        if (other.GetComponent<Boss>())
        {
            other.GetComponent<Boss>().TakeDamage(1 + BoostDamage.damage,0);
        }
    }

    IEnumerator PSStart()
    {
        yield return new WaitForSeconds(Random.Range(0, .7f));
        _anim.enabled = true;
    }
}
