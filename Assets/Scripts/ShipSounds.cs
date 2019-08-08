using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSounds : MonoBehaviour
{
    private AudioSource _source;
    [SerializeField] private AudioClip _destroySound;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _thrustersSound;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        if (_source == null)
            Debug.LogError("Ship Audio Source not found");
    }

    public void PlayDestroyShipSound()
    {
        _source.Stop();
        if (!_source.isPlaying)
        {
        _source.PlayOneShot(_destroySound, 1f);
        }
    }

    public void PlayShipHitSound()
    {
        _source.Stop();
        _source.PlayOneShot(_hitSound, 1f);
    }

    public void PlayThrustersSound(int numberOfThrusters)
    {
        _source.clip = _thrustersSound;

        if (numberOfThrusters == 2)
        {
            _source.volume = 1f;
        }

        else if (numberOfThrusters == 1)
        {
            _source.volume = 0.5f;
        }

        if (!_source.isPlaying)
        {
            _source.Play();
        }
    }

    public void StopThrustersSound()
    {
        _source.Stop();
        _source.clip = null;
    }
}
