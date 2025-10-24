  

using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionMine;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Enemy>())
        {
            _explosionMine.transform.position = transform.position;
            _explosionMine.Play();
            other.GetComponent<Enemy>().TakeDamage(500 + BoostDamage.damage, 0);
            Destroy(gameObject);
        }
    }
}
