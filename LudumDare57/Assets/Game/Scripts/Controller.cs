using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Player player;
    private Vector2 moveDirection;

    private void Update()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(directionX, directionY).normalized;

        player.MoveInputHandler(moveDirection);
        moveDirection = Vector2.zero;
    }
}
