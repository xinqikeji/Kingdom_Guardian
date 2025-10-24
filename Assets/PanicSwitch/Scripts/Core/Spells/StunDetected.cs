  

using UnityEngine;

public class StunDetected : MonoBehaviour
{
    private BoxCollider _boxCollider;
    [SerializeField] private Transform particleMissile;
    private ParticleSystem _ps;
    private float _timer;
    private bool _isActive;
    private Enemy _triggerEnemy;
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _ps = particleMissile.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (_isActive)
        {
            if (_triggerEnemy && !_triggerEnemy.isdead)
            {
                _timer -= Time.deltaTime;

                if (_timer <= 0)
                    _triggerEnemy.TakeDamage(15, 0);
                else
                    _timer = .2f;
            }
            else
            {
                _isActive = false;
                _boxCollider.enabled = true;
                _ps.Stop();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive)
        {
            if (other.GetComponent<Enemy>())
            {
                particleMissile.LookAt(other.transform);
                _ps.Play();
                _triggerEnemy = other.GetComponent<Enemy>();
                _isActive = true;
                _boxCollider.enabled = false;
            }
        }
    }
}
