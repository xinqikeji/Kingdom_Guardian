  

using UnityEngine;

public class spellButton : MonoBehaviour
{
    public int id;
    private ButtonInfo _buttonInfo;
    private SpellsWrapper _spellWrapper;
    private AudioManager _audioManager;

    private void Awake()
    {
        _buttonInfo = GetComponent<ButtonInfo>();
        _audioManager = FindObjectOfType<AudioManager>();
        _spellWrapper = FindObjectOfType<SpellsWrapper>();
        _buttonInfo._spellButton = this;
    }   

    public void SpellButtonActive()
    {
        switch (id)
        {
            case 0:
                _audioManager.SFXPlay(10);
                _spellWrapper.UseHeal(_buttonInfo.price);
                break;

            case 1:
                _buttonInfo.AdsButtonChecker(false);
                _spellWrapper.UseMine(_buttonInfo.price);
                break;

            case 2:
                _audioManager.SFXPlay(11);
                _spellWrapper.UseMeteor(_buttonInfo.price);
                break;

            case 4:
                _audioManager.SFXPlay(12);
                _spellWrapper.UseBomber(_buttonInfo.price);
                break;

            case 6:
                _audioManager.SFXPlay(1);
                _spellWrapper.UseStun(_buttonInfo.price);
                break;

            case 7:
                _buttonInfo.AdsButtonChecker(false);
                _spellWrapper.UsePoison(_buttonInfo.price);
                break;

            case 8:
                _audioManager.SFXPlay(4);
                _spellWrapper.UseIce(_buttonInfo.price);
                break;
        }

        _buttonInfo.CheckStatus();
    }

    public int CheckQty()
    {
        return _spellWrapper.GetSpellQtyId(id);
    }
}
