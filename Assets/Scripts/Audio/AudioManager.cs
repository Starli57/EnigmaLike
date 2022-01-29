using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public void PlayTypeSound()
    {
        Instantiate(_typeSound, transform).Play();
    }

    [SerializeField] private AudioTrack _typeSound;
}
