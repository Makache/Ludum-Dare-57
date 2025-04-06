using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float rotateDuration;
    [SerializeField] private float swimForce;
    [SerializeField] private Animator animator;

    private Transform transform;
    private Rigidbody rigidbody;

    public bool isSwim { get; private set; }

    private void Awake()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        animator.SetBool("isSwim", isSwim);
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
        }
    }

    private void Move(Vector3 turnInto,Vector3 swimDirection)
    {
        isSwim = true;
        transform.DORotate(turnInto, rotateDuration);
        rigidbody.AddForce(swimDirection * swimForce * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyBehaviour>())
        {

        }
    }
}
