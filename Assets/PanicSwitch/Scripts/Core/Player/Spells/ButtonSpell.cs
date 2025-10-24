  

using UnityEngine;

public class ButtonSpell : MonoBehaviour
{
    [SerializeField] private int price;
    private SpellsWrapper _spellWrapper;

    void Start()
    {
        _spellWrapper = FindObjectOfType<SpellsWrapper>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
