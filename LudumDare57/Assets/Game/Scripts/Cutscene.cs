using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private Color _manColor;
    [SerializeField] private Color _womanColor;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private FadeTransition _fadeTransition;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _dialogueStateClips = new List<AudioClip>();
    [SerializeField] private AudioClip _toiletFlush;

    private void Start()
    {
        _panel.SetActive(true);
        _text.gameObject.SetActive(true);
        StartCoroutine(Dialogue());
    }

    private IEnumerator Dialogue()
    {
        _text.text = "OH NO! I DROPTED IT!";
        _text.color = _womanColor;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return null;

        _audioSource.PlayOneShot(_dialogueStateClips[Random.Range(0, _dialogueStateClips.Count)]);
        _text.text = "What happened, honey?";
        _text.color = _manColor;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return null;

        _audioSource.PlayOneShot(_dialogueStateClips[Random.Range(0, _dialogueStateClips.Count)]);
        _text.text = "It flushed down the toilet!";
        _text.color = _womanColor;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return null;

        _audioSource.PlayOneShot(_dialogueStateClips[Random.Range(0, _dialogueStateClips.Count)]);
        _text.text = "What flushed?";
        _text.color = _manColor;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return null;

        _audioSource.PlayOneShot(_dialogueStateClips[Random.Range(0, _dialogueStateClips.Count)]);
        _text.text = "My ring!";
        _text.color = _womanColor;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return null;

        _audioSource.PlayOneShot(_dialogueStateClips[Random.Range(0, _dialogueStateClips.Count)]);
        _text.text = "Don't worry, I'll get it right away!";
        _text.color = _manColor;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return null;

        _audioSource.PlayOneShot(_dialogueStateClips[Random.Range(0, _dialogueStateClips.Count)]);
        _panel.SetActive(false);
        _text.gameObject.SetActive(false);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return null;

        _audioSource.PlayOneShot(_toiletFlush);
        StartCoroutine(_fadeTransition.FadeIn(1));
    }
}
