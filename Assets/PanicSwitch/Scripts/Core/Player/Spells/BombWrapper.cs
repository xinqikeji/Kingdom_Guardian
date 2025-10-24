  

using System.Collections;
using UnityEngine;

public class BombWrapper : MonoBehaviour
{
    [SerializeField] private ParticleSystem boolParticle;
    [SerializeField] private AudioSource _audioSource;
    private Animator _camera;  

    private void Start()
    {
        _camera = Camera.main.GetComponent<Animator>();
        StartCoroutine(BoombDetected());
    }

    private void OnMouseDown()
    {
        Boom();
    }

    private void Boom()
    {
        boolParticle.transform.position = transform.position + new Vector3(0, 1.2f, 0);
        _audioSource.Play();
        //boolParticle.GetComponent<Animator>().SetTrigger("boom");
        // _camera.SetTrigger("shake");
        boolParticle.Play();
        Destroy(gameObject);
        Destroy(boolParticle.gameObject, 6);
    }

    IEnumerator BoombDetected()
    {
        yield return new WaitForSeconds(15);
        Boom();
        StopCoroutine(BoombDetected());
    }
}
