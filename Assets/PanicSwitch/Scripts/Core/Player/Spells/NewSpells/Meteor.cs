  

using UnityEngine;

public class Meteor : MonoBehaviour
{
    private ButtonInfo _buttonInfo;
    private SpellsWrapper _spellWrapper;

    private void Start()
    {
        _buttonInfo = GetComponent<ButtonInfo>();
        _spellWrapper = FindObjectOfType<SpellsWrapper>();
    }

    public void SpellMeteor()
    {
        _spellWrapper.UseMeteor(_buttonInfo.price);
    }
}
