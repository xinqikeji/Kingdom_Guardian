  

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] _audios;
    public AudioSource[] Sfx;

    public void MuteAudio(bool _ismute)
    {
       _audios = FindObjectsOfType<AudioSource>();

        for (int i = 0; i < _audios.Length; i++)
        {
            _audios[i].mute = _ismute;
        }
    }

    public void SFXPlay(int id)
    {
        Sfx[id].Play();
    }
}
