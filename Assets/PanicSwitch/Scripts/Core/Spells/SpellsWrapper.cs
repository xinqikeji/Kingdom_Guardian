  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsWrapper : MonoBehaviour
{
    /// <summary>
    /// 0 Heal
    /// 1 mines
    /// 2 Asteroids
    /// 3 rampage 1st Line
    /// 4 bomber
    /// 5 rampage 2nd Line
    /// 6 Stun
    /// 7 Ice puddle
    /// 8 Poisonous puddle
    /// </summary>
    /// 
    [SerializeField] private int[] spellQty = new int[9];
    private Wallet _wallet;
    [SerializeField] private Transform spawnPoint;
    private WarriorsCoreUpgrade[] _warriorsCoreUpgrade;
    [SerializeField] private ParticleSystem _iceParticles;
    [SerializeField] private GameObject mineActivator;
    [SerializeField] private GameObject meteorActivator;
    [SerializeField] private GameObject bomberWrapper;
    [SerializeField] private GameObject stunWrapper;
    [SerializeField] private GameObject poisonActivator;
    private float _timerMeteor;
    private bool _meteorActive;
    [HideInInspector] public bool isActiveMine;
    [HideInInspector] public bool isActivePoison;
    private Vector3[] _fieldPosition = new Vector3[2];

    public int GetSpellQtyId(int id)
    {
        return spellQty[id];
    }
    private void Start()
    {
        _wallet = FindObjectOfType<Wallet>();
        _warriorsCoreUpgrade = FindObjectsOfType<WarriorsCoreUpgrade>();

        _fieldPosition[0] = mineActivator.transform.position;
        _fieldPosition[1] = poisonActivator.transform.position;
    }

    private void Update()
    {
        if (_meteorActive)
        {
            if (_timerMeteor > 0)
            {
                _timerMeteor -= Time.deltaTime;
            }
            else
            {
                _timerMeteor = 0;
                meteorActivator.SetActive(false);
                _meteorActive = false;
            }
        }
    }

    private void AddSpellQty(int spellId)
    {
        spellQty[spellId]++;
    }

    private void BuySpell(int price, int spellId)
    {
        if (_wallet.GetMoney() >= price)
        {
            AddSpellQty(spellId);
            _wallet.SetMoney(-price);

            switch (spellId)
            {
                case 0:
                    UseHeal(price);
                    break;

                case 1:
                    UseMine(price);
                    break;

                case 2:
                    UseMeteor(price);
                    break;

                case 4:
                    UseBomber(price);
                    break;

                case 6:
                    UseStun(price);
                    break;

                case 7:
                    UsePoison(price);
                    break;

                case 8:
                    UseIce(price);
                    break;
            }
        }
    }

    public void AdsSpells( int spellId)
    {
            AddSpellQty(spellId);

            switch (spellId)
            {
                case 0:
                    UseHeal(10000);
                    break;

                case 1:
                    UseMine(10000);
                    break;

                case 2:
                    UseMeteor(10000);
                    break;

                case 4:
                    UseBomber(10000);
                    break;

                case 6:
                    UseStun(10000);
                    break;

                case 7:
                    UsePoison(10000);
                    break;

                case 8:
                    UseIce(10000);
                    break;
            }        
    }

    public void UseHeal(int price)
    {
        if (spellQty[0] > 0)
        {
            spellQty[0]--;
            _warriorsCoreUpgrade[0].CheckWarriors(1);
            _warriorsCoreUpgrade[1].CheckWarriors(1);
        }
        else
        {
            BuySpell(price, 0);
        }
    }

    public void UseMine(int price)
    {
        if (isActiveMine) return;

        if (spellQty[1] > 0)
        {
            spellQty[1]--;
            mineActivator.transform.position = _fieldPosition[0];
            mineActivator.SetActive(true);
            isActiveMine = true;
        }
        else
        {
            BuySpell(price, 1);
        }
    }

    public void UseMeteor(int price)
    {
        if (spellQty[2] > 0)
        {
            spellQty[2]--;
            meteorActivator.SetActive(true);
            _meteorActive = true;
            _timerMeteor = 15;
        }
        else
        {
            BuySpell(price, 2);
        }
    }

    public void UseBomber(int price)
    {
        if (spellQty[4] > 0)
        {
            spellQty[4]--;
            Instantiate(bomberWrapper, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            BuySpell(price, 4);
        }
    }

    public void UseStun(int price)
    {
        if (spellQty[6] > 0)
        {
            spellQty[6]--;
            stunWrapper.GetComponent<Animator>().SetTrigger("show");
            StartCoroutine(StunHide());
        }
        else
        {
            BuySpell(price, 6);
        }
    }

    public void UseIce(int price)
    {
        if (spellQty[8] > 0)
        {
            spellQty[8]--;
            _iceParticles.Play();
            StartCoroutine(IceHide());
        }
        else
        {
            BuySpell(price, 8);
        }
    }

    public void UsePoison(int price)
    {
        if (isActivePoison) return;

        if (spellQty[7] > 0)
        {
            spellQty[7]--;
            poisonActivator.transform.position = _fieldPosition[1];
            poisonActivator.SetActive(true);
            isActivePoison = true;
        }
        else
        {
            BuySpell(price, 7);
        }
    }

    IEnumerator StunHide()
    {
        yield return new WaitForSeconds(25);
        stunWrapper.GetComponent<Animator>().SetTrigger("hide");
    }


    IEnumerator IceHide()
    {
        yield return new WaitForSeconds(25);
        _iceParticles.Stop();
    }

}
