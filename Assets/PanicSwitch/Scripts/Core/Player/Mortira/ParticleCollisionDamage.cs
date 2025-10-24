  

using UnityEngine;

public class ParticleCollisionDamage : MonoBehaviour
{
    [SerializeField] private int damage = 1000;
    [SerializeField] private bool isBomber;
    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<Enemy>())
        {
            other.GetComponent<Enemy>().TakeDamage(damage + BoostDamage.damage, 0);
        }

        if (other.GetComponent<Boss>())
        {
            other.GetComponent<Boss>().TakeDamage(damage + BoostDamage.damage, 0);
        }

        if (isBomber && other.GetComponent<WarriorAttack_1>())
        {
            other.GetComponent<WarriorAttack_1>().TakeDamage(damage);
        }

    }
}
