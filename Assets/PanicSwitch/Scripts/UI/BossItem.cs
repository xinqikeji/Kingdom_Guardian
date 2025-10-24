
using UnityEngine;
using UnityEngine.UI;

public class BossItem : MonoBehaviour
{
    [SerializeField] private Image avatar;
    [SerializeField] private GameObject checker;

    public void Check(int value)
    {
        avatar.color = new Color(1, 1, 1, 1);

        if (value > 0)
            checker.SetActive(true);
    }
}
