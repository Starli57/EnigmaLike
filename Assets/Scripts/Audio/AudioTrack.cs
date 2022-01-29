using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrack : MonoBehaviour
{
    public const float FadeTime = 1.0f;

    public string Name { get; private set; }

    public bool IsPlaying { get { return _AudioSource.isPlaying; } }
    public int StartPlayTime { get; private set; }

    public void Play(bool rise = false)
    {
        StopAllCoroutines();

        StartPlayTime = (int)Time.realtimeSinceStartup;

        if (rise)
            StartCoroutine(PlayAndRiseTask());
        else
        {
            _AudioSource.volume = _OriginalVolume;
            _AudioSource.priority = _OriginalPriority;
            _AudioSource.Play();
        }
    }

    public void Stop(bool fade = false)
    {
        if (_stopped)
            return;

        _stopped = true;

        StopAllCoroutines();

        if (fade)
            StartCoroutine(FadeAndStopTask());
        else
        {
            StopAudio();
        }
    }

    public void StopAudio()
    {
        _AudioSource.Stop();
        Destroy(this.gameObject);
    }

    public void CheckFinished()
    {
        if (!IsPlaying)
            Stop(false);
    }

    private AudioSource _AudioSource;

    private float _OriginalVolume;
    private int _OriginalPriority;

    private bool _stopped;

    private void Awake()
    {
        _AudioSource = GetComponent<AudioSource>();

        _OriginalVolume = _AudioSource.volume;
        _OriginalPriority = _AudioSource.priority;
    }

    private void Update()
    {
        _AudioSource.priority++;
        CheckFinished();
    }

    private IEnumerator FadeAndStopTask()
    {
        while (_AudioSource.volume > 0)
        {
            _AudioSource.volume -= _OriginalVolume * Time.unscaledDeltaTime / FadeTime;
            yield return null;
        }

        _AudioSource.volume = 0;
        StopAudio();
    }

    private IEnumerator PlayAndRiseTask()
    {
        _AudioSource.volume = 0;
        _AudioSource.Play();

        while (_AudioSource.volume <= _OriginalVolume)
        {
            _AudioSource.volume += _OriginalVolume * Time.unscaledDeltaTime / FadeTime;
            yield return null;
        }

        _AudioSource.volume = _OriginalVolume;
    }
}