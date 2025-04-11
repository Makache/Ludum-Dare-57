using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [SerializeField] private GameObject _ringPlace;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<GameObject> _hatches = new List<GameObject>();

    public void PickUpRing()
    {
        _ringPlace.SetActive(true);
        gameObject.transform.SetParent(_ringPlace.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0, 0, -20);

        _audioSource.Play();

        foreach (var hatch in _hatches)
        {
            hatch.SetActive(!hatch.activeSelf);
        }
    }
}
