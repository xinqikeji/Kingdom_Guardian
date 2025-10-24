  

using UnityEngine;

public class MineController : MonoBehaviour
{
    [SerializeField] private GameObject Mine;
    [SerializeField] private GameObject pointer;
    [SerializeField] private ButtonInfo _buttonInfo;
    private AudioManager _audioManager;
    private Tutorial _tutorial;
    private SpellsWrapper _spellsWrapper;
    Vector3 screenSpace;
    Vector3 offset;


    private void Start()
    {
        PlayerPrefs.SetInt("TutorialUI", 1);
        _audioManager = FindObjectOfType<AudioManager>();
        _tutorial = FindObjectOfType<Tutorial>();
        _spellsWrapper = FindObjectOfType<SpellsWrapper>();

        if (!PlayerPrefs.HasKey("TutorialUI"))
        {
            pointer.SetActive(true);
        }
    }

    void OnMouseDown()
    {
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
    }

    void OnMouseDrag()
    {
        var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
        transform.position = new Vector3(curPosition.x, -0.8f, curPosition.z);

    }

    private void OnMouseUp()
    {
        Instantiate(Mine, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        _audioManager.SFXPlay(2);

        //if (!PlayerPrefs.HasKey("TutorialUI"))
        //{
        //    _tutorial.ActiveButtons();
        //    MenuUI.IsGame = true;
        //    PlayerPrefs.SetInt("TutorialUI", 1);
        //}

        if (pointer)
        {
            pointer.SetActive(false);
            _spellsWrapper.isActiveMine = false;
        }
        else
        {
            _spellsWrapper.isActivePoison = false;
        }

        _buttonInfo.AdsButtonChecker(true);
    }
}
