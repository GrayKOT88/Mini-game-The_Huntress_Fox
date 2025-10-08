using UnityEngine;

public class PlayerMovement : MonoBehaviour
{    
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Animator _playerAnim;

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
            _playerAnim.SetFloat("Speed_f", 1);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _playerAnim.SetFloat("Speed_f", 0);
        }
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * _gameConfig.PlayerSpeed * Time.deltaTime);
        ClampPosition();
    }

    private void RotatePlayer()
    {
        float deltaX = Input.mousePosition.x - oldMousePositionX;
        oldMousePositionX = Input.mousePosition.x;
        eulerY += deltaX * _gameConfig.PlayerRotationSpeed;
        transform.eulerAngles = new Vector3(0, eulerY, 0);
    }

    private void ClampPosition()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, _gameConfig.XBounds.x, _gameConfig.XBounds.y);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, _gameConfig.ZBounds.x, _gameConfig.ZBounds.y);
        clampedPosition.y = Mathf.Max(clampedPosition.y, 0);
        transform.position = clampedPosition;
    }
}
