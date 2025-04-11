using System.Collections.Generic;
using UnityEngine;

public class Porcupine_Fish : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _increaseClips = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _decreaseClips = new List<AudioClip>();

    public void PlayChangeScaleSound(int _isIncrease)
    {
        if (_isIncrease == 1)
        {
            _audioSource.PlayOneShot(_increaseClips[Random.Range(0, _increaseClips.Count)]);
        }
        else
        {
            _audioSource.PlayOneShot(_decreaseClips[Random.Range(0, _decreaseClips.Count)]);
        }
    }
}