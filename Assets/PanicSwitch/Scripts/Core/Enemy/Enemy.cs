  

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public ParticleSystem DeadParticles;
    [SerializeField] private EnemyWarrior _enemyWarrior;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem SkeletDead;
    [SerializeField] private SkinnedMeshRenderer _SkinnedMeshRenderer;
    [SerializeField] private Material[] _enemyMaterial;
    [SerializeField] private Transform HatElement;
    [SerializeField] private float healthCoef;
    private NavMeshAgent _agent;
    //[SerializeField] private PathFinderAI _pathFinderAI;
    private float currentAttackTimer = .5f;
    private float poisonTimer;
    [HideInInspector] public Transform Target;
     public WarriorAttack_1 AttackTarget;
    [HideInInspector] public Wallet _wallet;
    [HideInInspector] public RandomSpell _randomSpell;
    [HideInInspector] public QuestManager _questManager;
    [HideInInspector] public bool isStop;
    [HideInInspector] public bool isdead;
    private GameObject _clone;
    private bool isStun;
    private bool isPoison;
    
    [SerializeField]  private int _health;
    [SerializeField]  private int _attack;
    [SerializeField] private float _attackTimer;
    [SerializeField] private bool isBomber;
    [SerializeField] private bool isCoinMan;
    private bool _isAttack;

    [SerializeField] private int runIndex;

    //Animation
    private static readonly int _animAttackKey = Animator.StringToHash("attack0");

    private void Awake()
    {
        currentAttackTimer = .5f;
        _health = (int)(_enemyWarrior.health + (float)_enemyWarrior.health * healthCoef);
        _attack = _enemyWarrior.attack;
        _attackTimer = _enemyWarrior.attackTimer;

        HealthIsBomber();

        if (_enemyWarrior.hat)
        {
            _clone = Instantiate(_enemyWarrior.hat, Vector3.zero, Quaternion.identity);
            _clone.transform.parent = HatElement;
            _clone.transform.localPosition = Vector3.zero;
            _clone.transform.localScale = new Vector3(1, 1, 1);
            _clone.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    private void Start()
    {
        _questManager.TempCouner(1);
        _agent = GetComponent<NavMeshAgent>();
        //_pathFinderAI = GetComponent<PathFinderAI>();
        
        //_pathFinderAI.SetTagret(Target.position);
        _agent.SetDestination(Target.position);
        StartCoroutine(StartAnim());

        StartCoroutine(CheckDistance());
    }

    private void Update()
    {
        if (_isAttack && AttackTarget != null && !AttackTarget.isdead && !isStun)
        {
            if (currentAttackTimer <= 0)
            {
                _animator.SetTrigger(_animAttackKey);
                AttackTarget.TakeDamage(_attack);
                currentAttackTimer = _attackTimer;

                if (AttackTarget.isdead)
                    AttackTarget = null;
            }
            else
            {
                currentAttackTimer -= Time.deltaTime;
            }

            StopDistance();
        }

        if (AttackTarget != null && AttackTarget.isdead)
            AttackTarget = null;

        if (AttackTarget == null && !isdead && !isStun)
        {
            //_pathFinderAI.SetTagret(Target.position);
            //_pathFinderAI.IsStop(false);
            _agent.SetDestination(Target.position);
            _agent.isStopped = false;
            _animator.SetBool("run" + runIndex, true);
        }

        if (isPoison)
        {
            if (poisonTimer <= 0)
            {
                TakeDamage(10 + BoostDamage.damage, 0);
                poisonTimer = 1;
            }
            else
            {
                poisonTimer -= Time.deltaTime;
            }
        }
    }

    IEnumerator CheckDistance()
    {
        while (true)
        {
            CheckerAgro();

            if (MainBuild.Isdead)
                TakeDamage(1000, 0);

            yield return new WaitForSeconds(.6f);
        }
    }

    private void CheckerAgro()
    {
        if (AttackTarget != null && !AttackTarget.isdead && Vector3.Distance(transform.position, AttackTarget.transform.position) < 3f)
        {
            _isAttack = true;
            if (isBomber)
                TakeDamage(10000, 0);
        }
        else
        {
            _isAttack = false;
        }
    }

    public void TakeDamage(int damage, int type)
    {
        _health -= damage;
        _SkinnedMeshRenderer.material = _enemyMaterial[1];
        StartCoroutine(BackMaterial());

        if (_health <= 0 && !isdead)
        {
            _questManager.TempCouner(-1);
            GameUI.countUnit++;
            _randomSpell.RandomSpellProgess();

            if (!MainBuild.Isdead)
                _wallet.SetMoney(IncomeMoney.MoneyForKill * AdsButton.MoneyBonusX);

            GetComponent<BoxCollider>().enabled = false;

            if (SkeletDead != null)
            {
                if (isBomber)
                    SkeletDead.Play();
                else
                    Instantiate(SkeletDead, transform.position, Quaternion.identity);
            }
            else
            {
                DeadParticles.transform.position = transform.position;
                DeadParticles.Play();
            }

            //_pathFinderAI.IsStop(true);
            
            _agent.isStopped = true;
            _agent.enabled = false;
            _animator.gameObject.SetActive(false);

            if (type == 1)
            {
                _questManager.TapKilled(1);
            }

            Destroy(gameObject, 2);
            isdead = true;
        }
    }


    public void StopDistance()
    {
        if (!isdead)
        {
            _animator.SetBool("run" + runIndex, false);
            _agent.isStopped = true;
            //_pathFinderAI.IsStop(true);
        }
    }

    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(Random.Range(0, .9f));
        _animator.enabled = true;
    }

    IEnumerator BackMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        _SkinnedMeshRenderer.material = _enemyMaterial[0];
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<WarriorAttack_1>() && !isCoinMan)
        {
            AttackTarget = other.GetComponent<WarriorAttack_1>();
            CheckerAgro();

            if (!isdead)
                //_pathFinderAI.SetTagret(AttackTarget.transform.position);
                _agent.SetDestination(AttackTarget.transform.position);
        }

        if (other.GetComponent<PoisonWrapper>())
        {
            isPoison = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PoisonWrapper>())
        {
            isPoison = false;
        }
    }

    private void HealthIsBomber()
    {
        if (isBomber)
        {
            _health *= (_questManager._playerData.GetLevel() / 2 + 1);
        }
    }
}
