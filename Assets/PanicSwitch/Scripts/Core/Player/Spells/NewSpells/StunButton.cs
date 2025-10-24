  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunButton : MonoBehaviour
{
    private ButtonInfo _buttonInfo;
    private SpellsWrapper _spellWrapper;

    private void Start()
    {
        _buttonInfo = GetComponent<ButtonInfo>();
        _spellWrapper = FindObjectOfType<SpellsWrapper>();
    }

    public void SpellStun()
    {
        _spellWrapper.UseStun(_buttonInfo.price);
    }
}
