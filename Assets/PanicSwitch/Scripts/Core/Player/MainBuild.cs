  

using UnityEngine;
using TMPro;

public class MainBuild : MonoBehaviour
{
    public int health;
    [SerializeField] private TextMeshProUGUI _healthDisplayText;
    private GameUI _gameUI;
    public static bool Isdead;

    private void Awake()
    {
        Isdead = false;
    }      

    private void Start()
    {
        _gameUI = FindObjectOfType<GameUI>();
        UpdateText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            other.GetComponent<Enemy>().TakeDamage(50000, 0);
            TakeDamage(1, other);
        }

        if (other.GetComponent<Boss>())
        {
            TakeDamage(50, other);
        }
    }

    private void TakeDamage(int value, Collider other)
    {
        if (Isdead) return;

        health -= value;

        if (health <= 0 && !Isdead)
        {
            health = 0;
            _gameUI.PlayerDeath();
            Isdead = true;
        }

        UpdateText();
    }

    private void UpdateText()
    {
        _healthDisplayText.text = health.ToString();
    }
}
