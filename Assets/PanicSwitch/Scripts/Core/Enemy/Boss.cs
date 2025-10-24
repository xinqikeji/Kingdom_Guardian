  

using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class Boss : MonoBehaviour
{
    [SerializeField] private Transform canvasElement;
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI healthDisplayText;
    [SerializeField] private ParticleSystem _deadParticles;
    [SerializeField] private ParticleSystem _AttackParticle;
    [SerializeField] private int _health;
    [SerializeField] private int _attack;
    [SerializeField] private float _attackTimer;
    [SerializeField] private GameObject coinPickup;
    [SerializeField] private SkinnedMeshRenderer _SkinnedMeshRenderer;
    [SerializeField] private Material[] _enemyMaterial;
    public WarriorAttack_1 AttackTarget;
    private NavMeshAgent _agent;
    private float currentAttackTimer = .5f;
    public Transform Target;
    [HideInInspector] public QuestManager _questManager;
    [HideInInspector] public bool isdead;
    [HideInInspector] public Wallet _wallet;
    [SerializeField] private ParticleSystem tapParticleDamage;
    private BoxCollider _boxCollider;
    private bool _isAttack;
    private int _startHealth;

    private void Awake()
    {
        currentAttackTimer = .5f;
    }

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _questManager.TempCouner(1);
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(Target.position);
        _startHealth = _health;
        StartCoroutine(CheckDistance());
        StartCoroutine(UpdateRotationCanvas());
        DisplayHEalth();
    }

    private void Update()
    {
        canvasElement.rotation = Quaternion.Euler(55, 25 - transform.rotation.y, 0);
        //canvasElement.rotation = Quaternion.Euler(55, 29 - transform.rotation.y, 0);

        if (_isAttack && AttackTarget != null && !AttackTarget.isdead)
        {
            if (currentAttackTimer <= 0)
            {
                _animator.SetTrigger("attack0");
                StartCoroutine(Attack());
                currentAttackTimer = _attackTimer;

                if (AttackTarget.isdead)
                {
                    AttackTarget = null;
                }

            }
            else
            {
                currentAttackTimer -= Time.deltaTime;
            }

            StopDistance();
        }

        if (AttackTarget != null && AttackTarget.isdead)
        {
            AttackTarget = null;
        }


        if (AttackTarget == null && !isdead)
        {
            _agent.SetDestination(Target.position);
            _agent.isStopped = false;
            _animator.SetBool("run0", true);
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(.4f);

        if (AttackTarget != null)
        {
            AttackTarget.TakeDamage(_attack);
            _AttackParticle.Play();
        }
    }

    public void TakeDamage(int damage, int type)
    {
        if (isdead) return;
        DisplayHEalth();
        _health -= damage;
        _SkinnedMeshRenderer.GetComponent<SkinnedMeshRenderer>().material = _enemyMaterial[1];
        StartCoroutine(BackMaterial());

        if (_health <= 0)
        {
            _deadParticles.Play();
            if (type < 2)
            {
                _questManager.TempCouner(-1);
                tapParticleDamage.Play();
            }

            GameUI.countUnit++;
            _wallet.SetMoney(2);
            GetComponent<BoxCollider>().enabled = false;
            _agent.isStopped = true;
            _agent.enabled = false;

            _animator.gameObject.SetActive(gameObject);

            //_animator.SetTrigger("death");
            _health = 0;
            _questManager.BossDead();
            _animator.gameObject.SetActive(false);
            Destroy(gameObject, 2);
            Instantiate(coinPickup, transform.position, Quaternion.identity);
            isdead = true;
        }

        healthDisplayText.text = _health.ToString();
    }

    private void DisplayHEalth()
    {
        healthDisplayText.text = _health.ToString();
    }

    public void StopDistance()
    {
        if (!isdead)
        {
            _animator.SetBool("run0", false);
            _agent.isStopped = true;
        }
    }

    private void DestinationGo()
    {
        if (isdead) return;

        if (AttackTarget != null)
            _agent.SetDestination(AttackTarget.transform.position);
        else
            _agent.SetDestination(Target.transform.position);

        _agent.isStopped = false;
        _animator.SetBool("run0", true);
    }

    IEnumerator CheckDistance()
    {
        while (true)
        {
            CheckerAgro();

            yield return new WaitForSeconds(.8f);
            _boxCollider.enabled = false;

            yield return new WaitForSeconds(.1f);
            _boxCollider.enabled = true;
        }
    }
    private void CheckerAgro()
    {
        if (AttackTarget != null && !AttackTarget.isdead && Vector3.Distance(transform.position, AttackTarget.transform.position) < 3f)
        {
            _isAttack = true;
        }
        else
        {
            _isAttack = false;
            DestinationGo();
        }
    }


    IEnumerator UpdateRotationCanvas()
    {
        while (true)
        {

            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator BackMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        _SkinnedMeshRenderer.GetComponent<SkinnedMeshRenderer>().material = _enemyMaterial[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<WarriorAttack_1>())
        {
            AttackTarget = other.GetComponent<WarriorAttack_1>();
            CheckerAgro();

            if (!isdead)
                _agent.SetDestination(AttackTarget.transform.position);
        }
    }
}
