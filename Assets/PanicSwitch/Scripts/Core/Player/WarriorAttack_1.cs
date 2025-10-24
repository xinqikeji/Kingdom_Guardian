  

using System.Collections;
using UnityEngine;

public class WarriorAttack_1 : MonoBehaviour
{
    public int id;
    public int groupId;
    public int warriorLevel;
    public int warriorEnable;
    [SerializeField] private bool _shoot;
    [SerializeField] private SkinnedMeshRenderer _skinMesh;
    [SerializeField] private Material[] _upgradeMaterials;
    [SerializeField] private GameObject[] _warriorAmmunition;
    [SerializeField] private ParticleSystem _particleUpgrade;
    [SerializeField] private ParticleSystem _psDead;
    [SerializeField] private GameObject _warriorsUpgrades;
    [SerializeField] private ParticleSystem _particle_Rampage;
    [SerializeField] private ParticleSystem _particles;
    private Animator _animator;
    [SerializeField] private float _health;
    private int _attack;
    private float _attackTimer;
    private Enemy _target;
    [SerializeField] private Boss _targetBoss;

    private float _startHealth;
    private int _startAttack;

    private bool _isRampage;

    private float currentAttackTimer;
    public bool isdead;
    private AudioSource _parentAudioSource;
    private BoxCollider _boxCollider;
    private CapsuleCollider _capsuleCollider;

    //Animation
    private static readonly int _animAttackKey = Animator.StringToHash("attack");
    private static readonly int _animAttackKey1 = Animator.StringToHash("attack1");
    private static readonly int _animAttackKey2 = Animator.StringToHash("attack2");
    // private static readonly int _animDeathKey1 = Animator.StringToHash("death");

    private void Awake()
    {
        if (PlayerPrefs.HasKey("warriorLevel" + id))
            warriorLevel = PlayerPrefs.GetInt("warriorLevel" + id);

        if (PlayerPrefs.HasKey("warriorEnable" + id))
            warriorEnable = PlayerPrefs.GetInt("warriorEnable" + id);

        _animator = _warriorsUpgrades.GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        if (warriorEnable > 0)
        {
            _boxCollider.enabled = true;
            _capsuleCollider.enabled = true;
        }
    }

    private void Start()
    {
        _parentAudioSource = GetComponentInParent<AudioSource>();
        CheckWarriorAmmunition();
    }

    private void Update()
    {
        if (_target != null && !_target.isdead && !isdead || _targetBoss != null && !_targetBoss.isdead && !isdead)
        {
            if (currentAttackTimer <= 0)
            {

                switch (Random.Range(0, 3))
                {
                    case 0:
                        _animator.SetTrigger(_animAttackKey);
                        break;

                    case 1:
                        _animator.SetTrigger(_animAttackKey1);
                        break;

                    case 2:
                        _animator.SetTrigger(_animAttackKey2);
                        break;
                }

                currentAttackTimer = _attackTimer;

                _boxCollider.enabled = false;
                _capsuleCollider.enabled = false;

                StartCoroutine(attackDelay());

                if (_shoot)
                {
                    _parentAudioSource.Play();
                }
            }
            else
            {
                currentAttackTimer -= Time.deltaTime;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0 && !isdead)
        {
            _boxCollider.enabled = false;
            _capsuleCollider.enabled = false;
            isdead = true;
            //_animator.SetTrigger(_animDeathKey1);
            Dead(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckTrigger(other);
    }

    IEnumerator attackDelay()
    {

        if (_shoot)
        {
            yield return new WaitForSeconds(.5f);
            _particles.Play();
        }

        else
        {
            yield return new WaitForSeconds(.3f);
            _particles.Play();
        }

        if (_target != null)
            _target.TakeDamage(_attack, 0);

        if (_targetBoss != null)
        {
            _targetBoss.TakeDamage(_attack, 0);

            if (_targetBoss.AttackTarget == null)
                _targetBoss.AttackTarget = this;
        }

        if (!isdead)
        {
            _boxCollider.enabled = true;
            _capsuleCollider.enabled = true;
        }


    }

    public void CheckTrigger(Collider other)
    {
        if (other.GetComponent<Boss>())
        {
            _targetBoss = other.GetComponent<Boss>();
        }
        else if (other.GetComponent<Enemy>())
        {
            _target = other.GetComponent<Enemy>();
        }
    }

    public void Rampage(float value)
    {
        _health = _startHealth * value;
        _attack = (int)(_startAttack * value);
        // _animator.SetBool("rampage", true);
        _particle_Rampage.Play();
        _isRampage = true;
    }

    public void CheckStats(int healthValue, int attackValue, float attackTimerValue, int type)
    {
        if (isdead && type == 0) return;

        _health = healthValue;
        _attack = attackValue;
        _attackTimer = attackTimerValue;

        if (type == 1)
        {
            isdead = false;
            _boxCollider.enabled = true;
            _capsuleCollider.enabled = true;

            _startHealth = healthValue;
            _startAttack = attackValue;

            _warriorsUpgrades.SetActive(true);

        }
        else
        {
            _particle_Rampage.Stop();
            //_animator.SetBool("rampage", false);
            _isRampage = false;
        }

        _particleUpgrade.Play();
    }

    public void UpgradeWarriors()
    {
        if (warriorLevel < 10)
            warriorLevel++;

        CheckWarriorAmmunition();

        PlayerPrefs.SetInt("warriorLevel" + id, warriorLevel);
        _particleUpgrade.Play();
    }

    public void AddUnit()
    {
        CheckWarriorAmmunition();
        warriorEnable = 1;
        PlayerPrefs.SetInt("warriorEnable" + id, 1);
        _particleUpgrade.Play();
    }

    private void CheckWarriorAmmunition()
    {
        //if (warriorLevel % 2 == 0 && warriorLevel != 0)
        //{
        //    _skinMesh.material = _upgradeMaterials[1];
        //}
        //else
        //{
        //    _skinMesh.material = _upgradeMaterials[0];
        //}
        if (warriorLevel < 6 && warriorLevel>0)
            _skinMesh.material = _upgradeMaterials[warriorLevel-1];

        if (id > 33)
        {
            switch (warriorLevel)
            {
                case 3:
                case 4:
                    _warriorAmmunition[0].SetActive(true);
                    break;

                case 5:
                case 6:
                    _warriorAmmunition[0].SetActive(false);
                    _warriorAmmunition[1].SetActive(true);
                    break;

                case 7:
                case 8:
                    _warriorAmmunition[0].SetActive(false);
                    _warriorAmmunition[1].SetActive(false);
                    _warriorAmmunition[2].SetActive(true);
                    break;

                case 9:
                case 10:
                    _warriorAmmunition[0].SetActive(false);
                    _warriorAmmunition[1].SetActive(false);
                    _warriorAmmunition[2].SetActive(false);
                    _warriorAmmunition[3].SetActive(true);
                    break;
            }
        }
        else
        {
            switch (warriorLevel)
            {
                case 3:
                case 4:
                    _warriorAmmunition[0].SetActive(true);
                    break;

                case 5:
                case 6:
                    _warriorAmmunition[0].SetActive(true);
                    _warriorAmmunition[1].SetActive(true);
                    break;

                case 7:
                case 8:
                    _warriorAmmunition[0].SetActive(false);
                    _warriorAmmunition[1].SetActive(true);
                    _warriorAmmunition[2].SetActive(true);
                    break;

                case 9:
                case 10:
                    _warriorAmmunition[0].SetActive(false);
                    _warriorAmmunition[1].SetActive(false);
                    _warriorAmmunition[2].SetActive(true);
                    _warriorAmmunition[3].SetActive(true);
                    break;
            }
        }
    }

    private void UpgradeDelay()
    {
        isdead = false;
        _boxCollider.enabled = true;
        _capsuleCollider.enabled = true;
        _animator = _warriorsUpgrades.GetComponent<Animator>();

        _warriorsUpgrades.SetActive(true);
    }

    private void Dead(int _dead)
    {
        _warriorsUpgrades.SetActive(false);

        _particle_Rampage.Stop();
        _isRampage = false;

        if (_dead > 0)
            _psDead.Play();
    }
}
