using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 0.5f;
    private Vector2 xBounds = new Vector2(-50, 50);
    private Vector2 zBounds = new Vector2(-50, 50);

    [SerializeField] private Animator playerAnim;
    private float oldMousePositionX;
    private float eulerY;

    private GameStateManager _state;

    private void Start()
    {
        _state = GetComponent<GameStateManager>();        
    }

    private void Update()
    {
        if (!_state.IsGameOver) { HandleMovementInput(); }
    }

    private void HandleMovementInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            oldMousePositionX = Input.mousePosition.x;
        }

        if (Input.GetMouseButton(0))
        {
            MoveForward();
            RotatePlayer();
            playerAnim.SetFloat("Speed_f", 1);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            playerAnim.SetFloat("Speed_f", 0);
        }
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        ClampPosition();
    }

    private void RotatePlayer()
    {
        float deltaX = Input.mousePosition.x - oldMousePositionX;
        oldMousePositionX = Input.mousePosition.x;
        eulerY += deltaX * rotationSpeed;
        transform.eulerAngles = new Vector3(0, eulerY, 0);
    }

    private void ClampPosition()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, xBounds.x, xBounds.y);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, zBounds.x, zBounds.y);
        clampedPosition.y = Mathf.Max(clampedPosition.y, 0);
        transform.position = clampedPosition;
    }
}
