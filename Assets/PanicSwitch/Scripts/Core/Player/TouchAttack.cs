  

using UnityEngine;

public class TouchAttack : MonoBehaviour
{

    void Update()
    {

       

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 500))
            {
                if(hit.collider.GetComponent<Enemy>())
                    hit.collider.GetComponent<Enemy>().TakeDamage(TapDamage.TapDamageValue, 1000000);

                if (hit.collider.GetComponent<Boss>())
                    hit.collider.GetComponent<Boss>().TakeDamage(TapDamage.TapDamageValue, 5);

                if (hit.collider.GetComponent<PowerUp>())
                    hit.collider.GetComponent<PowerUp>().Pickup();
            }
        }
       
    }
}
