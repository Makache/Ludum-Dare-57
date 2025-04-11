using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Balloon : MonoBehaviour
{
    [SerializeField] private Slider _balloonView;
    [SerializeField] private Player _player;
    [SerializeField] private float _idleDepletion;
    [SerializeField] private float _swimDepletion;
    [SerializeField] private float _sprintDepletion;
    [SerializeField] private Controller _controller;
    [SerializeField] private FadeTransition _fadeTransition;
    [SerializeField] private TextMeshProUGUI _textCount;
    [SerializeField] private TextMeshProUGUI _textConsumptionPerSecond;

    private float _depletionPerSecond = 0;
    private float _constantDepletion;
    private float _balloonValue = 1;

    private void Awake()
    {
        _balloonView.value = 1f;
        StartCoroutine(OxygenDepletion());
    }

    private void Update()
    {
        if (_player.isSprint)
        {
            _constantDepletion = _sprintDepletion;
        }
        else if (_player.isSwim)
        {
            _constantDepletion = _swimDepletion;
        }
        else
        {
            _constantDepletion = _idleDepletion;
        }
    }

    private IEnumerator OxygenDepletion()
    {
        while (_balloonValue > 0)
        {
            _balloonValue -= _depletionPerSecond + _constantDepletion;
            _balloonView.value = _balloonValue;

            _textCount.text = (Mathf.Round(_balloonValue * 1000)).ToString() + " / 1000";
            _textConsumptionPerSecond.text = "-" + (Mathf.Round((_depletionPerSecond + _constantDepletion) * 1000)).ToString() + " Per Second";

            yield return new WaitForSeconds(1);
        }

        StartCoroutine(_fadeTransition.FadeIn(2));
    }

    public void DepletionIncrease(float additionalDepletion)
    {
        _depletionPerSecond += additionalDepletion;
    }

    public void Replenishment()
    {
        _balloonValue = 1f;
        _depletionPerSecond = 0f;
    }
}
