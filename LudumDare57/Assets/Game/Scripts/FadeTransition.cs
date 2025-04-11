using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeTransition : MonoBehaviour
{
    [SerializeField] private Color _fadeInColor;
    [SerializeField] private Color _fadeOutColor;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private Image _panel;

    private void Awake()
    {
        FadeOut();
    }

    public IEnumerator FadeIn(int sceneNumber) 
    {
        _panel.gameObject.SetActive(true);
        _panel.color = _fadeOutColor;
        DOTween.To(() => _panel.color, x => _panel.color = x, _fadeInColor, _fadeDuration);

        yield return new WaitForSeconds(_fadeDuration + 1);

        DOTween.KillAll();
        SceneManager.LoadScene(sceneNumber);
    }

    public void FadeOut()
    {
        DOTween.KillAll();
        _panel.color = _fadeInColor;
        DOTween.To(() => _panel.color, x => _panel.color = x, _fadeOutColor, _fadeDuration);
    }
}
