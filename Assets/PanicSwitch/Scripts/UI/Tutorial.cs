  

using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private int _counter;
    [SerializeField] private GameObject _skipButton;
    [SerializeField] private GameObject pointerImage;
    [SerializeField] private RectTransform[] tutorialObjects;
    [SerializeField] private GameObject[] Descriptions;
    public static bool isTutorial;
    private Vector3 _newPosition;
    [SerializeField] private Button[] _needButtons;
    [SerializeField] private Button[] _buttons;
    private int buttonLenght;

    private void Awake()  
    {
        PlayerPrefs.SetInt("TutorialUI", 1);
        if (PlayerPrefs.HasKey("TutorialUI"))
        {
            isTutorial = true;
            pointerImage.SetActive(false);
            _skipButton.SetActive(false);
        }
        else
        {
            _buttons = Resources.FindObjectsOfTypeAll<Button>();
            buttonLenght = _buttons.Length;

            CheckButtons();
            CheckPointer();
        }
    }

    private void CheckButtons()
    {
        for (int i = 0; i < buttonLenght; i++)
            _buttons[i].enabled = false;

        for (int i = 7; i < _needButtons.Length; i++)
        {
           // _needButtons[i].enabled = true;
        }
        // _needButtons[7].enabled = true;
        // _needButtons[8].enabled = true;
        // _needButtons[9].enabled = true;
        // _needButtons[10].enabled = true;
        // _needButtons[11].enabled = true;
        //_needButtons[12].enabled = true;

        if (_counter > 6) return;
        _needButtons[_counter].enabled = true;
    }

    public void ActiveButtons()
    {
        isTutorial = true;
        _skipButton.SetActive(false);

        for (int i = 0; i < buttonLenght; i++)
            _buttons[i].enabled = true;
    }

    public void NextTutorial(int value)
    {
        if (isTutorial || _counter > 7) return;

        if (_counter + 1 == value)
        {
            _counter = value;
        }

        CheckPointer();
    }

    public void SkipTutorial()
    {
        if (!PlayerPrefs.HasKey("TutorialUI"))
        {
            ActiveButtons();
            pointerImage.SetActive(false);
            MenuUI.IsGame = true;
            PlayerPrefs.SetInt("TutorialUI", 1);
        }
    }

    private void Update()
    {
        if (isTutorial) return;
        pointerImage.GetComponent<RectTransform>().position = Vector3.Lerp(pointerImage.GetComponent<RectTransform>().position, _newPosition, Time.deltaTime * 5);
    }

    private void CheckPointer()
    {
        if (isTutorial) return;

        switch (_counter)
        {
            case 0:
                SetTransformPointer(new Vector3(35, -45, 0), 0);
                break;

            case 1:
                SetTransformPointer(new Vector3(20, -100, 0), 0);
                _skipButton.SetActive(true);
                break;

            case 2:
                SetTransformPointer(new Vector3(50, -80, 0), 0);
                break;

            case 3:
                SetTransformPointer(new Vector3(60, -60, 0), 0);
                break;

            case 4:
                Descriptions[0].SetActive(true);
                SetTransformPointer(new Vector3(-50, 90, 0), -180);
                break;

            case 5:
                SetTransformPointer(new Vector3(0, -90, 0), 0);
                break;

            case 6:
                Descriptions[0].SetActive(false);
                Descriptions[1].SetActive(true);
                SetTransformPointer(new Vector3(-50, 90, 0), -180);
                break;

            case 7:
                pointerImage.SetActive(false);
                Descriptions[1].SetActive(false);
                break;
        }

        CheckButtons();
    }

    private void SetTransformPointer(Vector3 position, float rotation)
    {
        _newPosition = tutorialObjects[_counter].position + position;
        //pointerImage.GetComponent<RectTransform>().position = tutorialObjects[_counter].position + position;
        pointerImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, rotation);
    }
}
