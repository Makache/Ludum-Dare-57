using System.Collections;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private FadeTransition _fadeTransition;

    private void Awake()
    {
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return null;

        StartCoroutine(_fadeTransition.FadeIn(1));
    }
}
