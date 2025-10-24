  

using UnityEngine;

public class BombParticle : MonoBehaviour
{
     private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            other.GetComponent<Enemy>().TakeDamage(150 + BoostDamage.damage, 0);
        }

        if (other.GetComponent<Boss>())
        {
            other.GetComponent<Boss>().TakeDamage(150 + BoostDamage.damage, 0);
        }
    }
}
