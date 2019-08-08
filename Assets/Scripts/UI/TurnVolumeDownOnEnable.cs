using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnVolumeDownOnEnable : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] [Range(0.0f, 1.0f)] private float _volumeOnEnable = 0.3f;
    [SerializeField] [Range(0.0f, 1.0f)] private float _volumeOnDisable = 1.0f;

    private void Start()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<Canvas>().GetComponent<AudioSource>();
        }
    }

    private void OnEnable()
    {
        if (_audioSource != null)
        {
            _audioSource.volume = _volumeOnEnable;
        }
    }

    private void OnDisable()
    {
        if (_audioSource != null)
        {
            _audioSource.volume = _volumeOnDisable;
        }
    }
}
