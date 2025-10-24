  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDelay : MonoBehaviour
{
    [SerializeField] private float _delayTimer;
    private AudioSource _auidoSource;

    private void Start()
    {
        _auidoSource = GetComponent<AudioSource>();
    }

    public void StartMusic()
    {
        StartCoroutine(AudioStrat());
    }

    IEnumerator AudioStrat()
    {
        yield return new WaitForSeconds(_delayTimer);
        _auidoSource.Play();
    }
}
