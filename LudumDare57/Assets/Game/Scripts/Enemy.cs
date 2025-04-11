using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _damageClips = new List<AudioClip>();

    public float Damage;
    public float HitForce;

    public void PlayDamageClip()
    {
        _audioSource.clip = _damageClips[Random.Range(0, _damageClips.Count)];
        _audioSource.Play();
    }
}
