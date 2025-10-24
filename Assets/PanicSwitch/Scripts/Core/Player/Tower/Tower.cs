  

using UnityEngine;

public class Tower : MonoBehaviour
{
    private int activatorId;
    [SerializeField] private GameObject pickUpParticle;
    [SerializeField] private Animator _coinAddAnim;
    [SerializeField] private GameObject[] towerSpells;
    private WarriorsCoreUpgrade[] _warriorCoreUpgrades;
    private Wallet _wallet;
    private bool isActive;
    private float timer;
    private AudioSource _audioSource;


    private void Start()
    {
        _warriorCoreUpgrades = FindObjectsOfType<WarriorsCoreUpgrade>();
        _wallet = FindObjectOfType<Wallet>();
        _audioSource = GetComponentInParent<AudioSource>();
    }
    public void ActivateTower(int id)
    {
        _audioSource.Play();
        activatorId = id;
        towerSpells[id].SetActive(true);
        isActive = true;
        pickUpParticle.SetActive(false);
    }

    public void DeleteTower()
    {
        activatorId = 0;
        pickUpParticle.SetActive(true);

        for (int i = 0; i < towerSpells.Length; i++)
            towerSpells[i].SetActive(false);

        isActive = false;
    }

    private void Update()
    {
        if (isActive)
        {
            switch (activatorId)
            {
                case 0:
                    if (timer <= 0)
                    {
                        _warriorCoreUpgrades[Random.Range(0, 2)].HealSingleWarrior(true);
                        timer = 6;
                    }
                    timer -= Time.deltaTime;
                    break;

                case 2:
                    if (timer <= 0 && !MainBuild.Isdead)
                    {
                        _wallet.SetMoney(20);
                        _coinAddAnim.SetTrigger("in");
                        timer = 5;
                    }
                    timer -= Time.deltaTime;
                    break;
            }                      
        }
    }
}
