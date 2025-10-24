  

using UnityEngine;

public class Line_1 : MonoBehaviour
{
    [SerializeField] private int id;
    [HideInInspector] public int level;
    [SerializeField] private WarriorAttack_1[] _warriorAttack;
   // [SerializeField] private ButtonInfo _buttonInfo;
    private AudioSource _audioSource;
    private Wallet _wallet;

    private void Awake()
    {
        level = PlayerPrefs.GetInt("Level_Line" + id);
    }
    void Start()
    {
        //_audioSource = GetComponent<AudioSource>();
        _wallet = FindObjectOfType<Wallet>();
        Upgrade();
    }

    public void AdsUpdateWarriors()
    {
        level++;
        Upgrade();
       // _buttonInfo.UpgradeText();
    }

    public void HealLine()
    {
        Upgrade();
    }

  
    private void Upgrade()
    {
       // _audioSource.Play();        

        //_buttonInfo.UpgradeText();
    }
}
