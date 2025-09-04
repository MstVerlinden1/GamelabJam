using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BalancingScript : MonoBehaviour
{
    [SerializeField, Tooltip("The text object that displays the balancing value.")]
    private TMP_Text text = null;
    [SerializeField, Tooltip("The game object that rotates depending on the balancing value.")]
    private GameObject balancingBoard = null;
    [SerializeField, Tooltip("The game object that rotates depending on the balancing value.")]
    private int momentumSpeed = 0;
    [SerializeField, Tooltip("How much degrees the object rotates on both sides max.")]
    private float maxAngleDegrees = 45;
    [SerializeField, Tooltip("Until this point should the object fall until the script moves on.")]
    private float fallingTreshold = -12.0f;
    [SerializeField, Tooltip("Speed of how fast the character is falling.")]
    private float fallingSpeed = -12.0f;
    [SerializeField, Tooltip("Speed of how fast the character is falling.")]
    private float startDelay = 1.0f;
    [SerializeField, Tooltip("How long after the game starts until the game ends.")]
    private float timeUntilCompletion = 5f;
    [SerializeField, Tooltip("The speed the character rotates at the start of the game.")]
    private float startingSpeed = 0.01f;
    
    private float _balancingValue = 0;
    private float _xOldMousePosition = 0;
    private float _xMouseDifference = 0;
    private float _balancingMomentum = 0.0f;

    private float timer = 0;
    private bool gameStarted = false;
    
    private InputSystem_Actions _inputSystem;
    private InputAction _mouseAction;

    private void Awake()
    {
        _inputSystem = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        Debug.Log(_inputSystem.ToString());
        _mouseAction = _inputSystem.BalancingMicrogame.Mouse;
        _mouseAction.Enable();
    }

    private void OnDisable()
    {
        _mouseAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GameManager.instance.gamePlayType);
        if (GameManager.instance.gamePlayType != GamePlayType.Running)
            return;
        
        UpdateTimer();
        
        if (gameStarted)
        {
            if ((int)Mathf.Abs(_balancingValue) == 1)
            {
                _balancingMomentum = 0.0f;

                // Game over
                GameOver();
                return;
            }

            if (GameManager.instance != null)
                GameManager.instance.winGame = true;
            
            _xMouseDifference = _mouseAction.ReadValue<Vector2>().x - _xOldMousePosition;
            _xOldMousePosition = _mouseAction.ReadValue<Vector2>().x;

            // 1000 and 1000 are lowering balancing speed.
            _balancingValue += _xMouseDifference / 1000.0f;

            _balancingMomentum += _xMouseDifference / (momentumSpeed == 0 ? 1000.0f : momentumSpeed * 1000.0f);
            _balancingMomentum += _balancingMomentum / momentumSpeed * Time.deltaTime;

            _balancingValue += _balancingMomentum;
            _balancingValue = Mathf.Clamp(_balancingValue, -1.0f, 1.0f);

            string balancingText = _balancingValue.ToString("0.0");
            if (text != null) text.SetText(balancingText);
            if (balancingBoard != null)
                balancingBoard.transform.rotation = Quaternion.Euler(0f, 0f,
                    Mathf.Clamp(_balancingValue, -1.0f, 1.0f) * -maxAngleDegrees);
        }
    }

    private void GameOver()
    {
        if (GameManager.instance != null)
            GameManager.instance.winGame = false;
        
        if (balancingBoard != null)
        {
            if (balancingBoard.transform.position.y > fallingTreshold)
            {
                balancingBoard.transform.position += Vector3.up * fallingSpeed * Time.deltaTime;
                return;
            }
        }
        
        EndGame();
    }

    private void EndGame()
    {
        Destroy(gameObject);
        Debug.Log(GameManager.instance);
        if (GameManager.instance != null)
        {
            GameManager.instance.gameSwitcher.SetActive(true);
            Debug.Log($"Game Switcher active? {GameManager.instance.gameSwitcher.activeSelf}");
        }
    }
    
    private void UpdateTimer()
    {
        if (GameManager.instance == null)
            return;
        if (!GameManager.instance.startGame)
            return;
        
        timer += Time.deltaTime;

        if (timer >= startDelay && !gameStarted)
        {
            _xOldMousePosition = _mouseAction.ReadValue<Vector2>().x;
            _balancingMomentum = Random.Range(-startingSpeed, startingSpeed);
            timer = 0.0f;
            gameStarted = true;
        }

        if (timer >= timeUntilCompletion && gameStarted)
        {
            EndGame();
        }
    }
}
