  

using UnityEngine;

public class Mortira : MonoBehaviour
{
    [SerializeField] private float attackTImer;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Animator _animator;
   private AudioSource _audioSource;
    private float _timer;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _timer = Random.Range(8f,10f);
    }

    void Update()
    {
        if (!MenuUI.IsGame || QuestManager.isWinner) return;

        if (_timer <= 0)
        {
            particle.Play();
            _animator.SetTrigger("attack");
            _timer = attackTImer;
            _audioSource.Play();
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }
}
