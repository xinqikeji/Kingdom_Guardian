  

using UnityEngine;

public class MineButton : MonoBehaviour
{
    private ButtonInfo _buttonInfo;
    private SpellsWrapper _spellWrapper;

    private void Start()
    {
        _buttonInfo = GetComponent<ButtonInfo>();
        _spellWrapper = FindObjectOfType<SpellsWrapper>();
    }

    public void SpellMine()
    {
        _spellWrapper.UseMine(_buttonInfo.price);
    }
}
