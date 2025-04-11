using System.Collections.Generic;
using UnityEngine;

public class Oxygen_Cylinder : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _replenishmentClips = new List<AudioClip>();

    public void PlayReplenishmentSound()
    {
        _audioSource.clip = _replenishmentClips[Random.Range(0, _replenishmentClips.Count)];
        _audioSource.Play();
    }
}
