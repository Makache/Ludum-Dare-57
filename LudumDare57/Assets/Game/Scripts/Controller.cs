using UnityEngine;
using DG.Tweening;

public class Controller : MonoBehaviour
{
    [SerializeField] private Game game;
    private Vector2 moveDirection;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            
        }

        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(directionX, directionY).normalized;

        game.player.MoveInputHandler(moveDirection);
        moveDirection = Vector2.zero;
    }
}
