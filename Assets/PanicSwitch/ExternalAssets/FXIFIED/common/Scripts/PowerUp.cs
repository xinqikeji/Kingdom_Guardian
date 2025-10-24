  

using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private int moneyPickUp;
    private Wallet _wallet;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        moneyPickUp = Random.Range(1500, 3400);
        _wallet = FindObjectOfType<Wallet>();
    }

    public void Pickup()
    {
        _audioSource.Play();
        _wallet.SetMoney(moneyPickUp);
        _wallet.CoinSoundPlay();
        Destroy(gameObject);
    }
}
