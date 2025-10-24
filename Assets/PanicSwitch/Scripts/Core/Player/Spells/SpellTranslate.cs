  

using UnityEngine;

public class SpellTranslate : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private float _speed;   

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, point.position, Time.deltaTime * _speed);

        if (transform.position.x > 40)
            Destroy(gameObject);
    }
}
