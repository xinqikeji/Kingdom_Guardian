  

using UnityEngine;

public class ChestPickUp : MonoBehaviour
{
    [HideInInspector] public ParticleSystem PickUpCHestParticle;
    [HideInInspector] public RandomChest _randomChest;

    private void OnMouseDown()
    {
        _randomChest.OpenChestWindow();
        GetComponent<BoxCollider>().enabled = false;
        PickUpCHestParticle.transform.position = transform.position;
        PickUpCHestParticle.Play();
        Destroy(gameObject);
    }
}
