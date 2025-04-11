using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _rotateDuration;
    [SerializeField] private float _swimForce;
    [SerializeField] private float _fastSwimForce;
    [SerializeField] private Animator _animator;
    [SerializeField] private Balloon _balloon;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _damageColor;
    [SerializeField] private Color _healingColor;
    [SerializeField] private Volume _volume;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _strokeClips = new List<AudioClip>();
    [SerializeField] private FadeTransition _fadeTransition;

    private Vignette _vignette;
    private Transform _transform;
    private Rigidbody _rigidbody;

    public bool isSwim { get; private set; }
    public bool isSprint { get; private set; }

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();

        StartCoroutine(PlayClipsInOrder());
    }

    private void Update()
    {
        _animator.SetBool("isSprint", isSprint);
        _animator.SetBool("isSwim", isSwim);
    }

    private IEnumerator PlayClipsInOrder()
    {
        float seconds;

        while (true)
        {
            if (isSprint)
            {
                _audioSource.PlayOneShot(_strokeClips[Random.Range(0, _strokeClips.Count)]);
                seconds = 0.43f;
            }
            else if (isSwim)
            {
                _audioSource.PlayOneShot(_strokeClips[Random.Range(0, _strokeClips.Count)]);
                seconds = 0.86f;
            }
            else
            {
                seconds = 1f;
            }

            yield return new WaitForSeconds(seconds);
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.z != 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    public void MoveInputHandler(Vector2 moveDirection)
    {
        if (moveDirection == new Vector2(1, 0))
        {
            Move(new Vector3(0, 0, 90), new Vector3(-1, 0, 0));
        }
        else if (moveDirection == new Vector2(0, 1))
        {
            Move(new Vector3(0, 90, 0), new Vector3(0, 1, 0));
        }
        else if (moveDirection == new Vector2(-1, 0))
        {
            Move(new Vector3(0, 180, 90), new Vector3(1, 0, 0));
        }
        else if (moveDirection == new Vector2(0, -1))
        {
            Move(new Vector3(0, 90, -180), new Vector3(0, -1, 0));
        }
        else if (moveDirection == new Vector2(1, 1).normalized)
        {
            Move(new Vector3(0, 0, 45), new Vector3(-1, 1, 0));
        }
        else if (moveDirection == new Vector2(-1, -1).normalized)
        {
            Move(new Vector3(0, 180, 135), new Vector3(1, -1, 0));
        }
        else if (moveDirection == new Vector2(1, -1).normalized)
        {
            Move(new Vector3(0, 0, 135), new Vector3(-1, -1, 0));
        }
        else if (moveDirection == new Vector2(-1, 1).normalized)
        {
            Move(new Vector3(0, 180, 45), new Vector3(1, 1, 0));
        }
        else if (moveDirection == new Vector2(0, 0))
        {
            isSwim = false;
            isSprint = false;
        }
    }

    private void Move(Vector3 turnInto,Vector3 swimDirection)
    {
        isSwim = true;
        _transform.DORotate(turnInto, _rotateDuration);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprint = true;
            _rigidbody.AddForce(swimDirection.normalized * _fastSwimForce * Time.deltaTime);

            return;
        }

        isSprint = false;
        _rigidbody.AddForce(swimDirection.normalized * _swimForce * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            TakeDamage(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Oxygen_Cylinder>()) 
        {
            other.gameObject.GetComponent<Oxygen_Cylinder>().PlayReplenishmentSound();
            _balloon.Replenishment();

            if (_volume.profile.TryGet(out _vignette))
            {
                _vignette.color.value = _healingColor;
                DOTween.To(() => _vignette.color.value, x => _vignette.color.value = x, _defaultColor, 1f);
            }
        }
        else if (other.gameObject.GetComponent<ExitTrigger>())
        {
            StartCoroutine(_fadeTransition.FadeIn(3));
        }

        if (other.gameObject.GetComponent<Ring>())
        {
            other.gameObject.GetComponent<Ring>().PickUpRing();
        }
    }

    private void TakeDamage(GameObject enemy)
    {
        _balloon.DepletionIncrease(enemy.GetComponent<Enemy>().Damage);
        _rigidbody.AddForce((_transform.position - enemy.transform.position) * Time.deltaTime * enemy.GetComponent<Enemy>().HitForce);

        if (_volume.profile.TryGet(out _vignette))
        {
            _vignette.color.value = _damageColor;
            DOTween.To(() => _vignette.color.value, x => _vignette.color.value = x, _defaultColor, 1f);
        }
        
        enemy.GetComponent<Enemy>().PlayDamageClip();
    }
}
