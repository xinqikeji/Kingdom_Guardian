  

using UnityEngine;

public class Bayonet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            other.GetComponent<Enemy>().TakeDamage(15 + BoostDamage.damage, 0);
        }

        if (other.GetComponent<Boss>())
        {
            other.GetComponent<Boss>().TakeDamage(15 + BoostDamage.damage, 0);
        }
    }

}
