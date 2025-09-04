using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public GamePlayType gamePlayType;
    [HideInInspector]
    public bool startGame = false;
    [HideInInspector]
    public int score = 0;
    
    [Tooltip("Set if the player won the last microgame.")]
    public bool winGame = false;
    [Tooltip("GameSwitcher")]
    public GameObject gameSwitcher;
    [SerializeField, Tooltip("Game Over Menu")]
    private GameObject gameOverMenu;
    [SerializeField, Tooltip("Pause Menu")]
    private GameObject pauseMenu;
    
    private InputSystem_Actions _inputSystem;
    [SerializeField]
    private InputActionReference _inputActionReference;
    private InputAction _menuAction;
    
    # region Create Instance
    
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            gamePlayType = GamePlayType.Running;
            // _inputSystem = new InputSystem_Actions();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _inputActionReference.action.Enable();
            _inputActionReference.action.performed += ctx => PauseGame();
        }
    }

    private void OnDisable()
    {
        if (instance == this)
        {
            instance = null;
            _inputActionReference.action.Disable();
        }
    }

    #endregion

    public void ChangeGamePlayType(GamePlayType pGamePlayType)
    {
        gamePlayType = pGamePlayType;

        switch (gamePlayType)
        {
            case GamePlayType.Running:
                gameOverMenu.SetActive(false);
                pauseMenu.SetActive(false);
                break;
            case GamePlayType.GameOver:
                gameOverMenu.SetActive(true);
                pauseMenu.SetActive(false);
                break;
            case GamePlayType.Paused:
                gameOverMenu.SetActive(false);
                pauseMenu.SetActive(true);
                break;
        }
    }

    private void PauseGame()
    {
        if (gamePlayType == GamePlayType.Running)
            ChangeGamePlayType(GamePlayType.Paused);
        else if (gamePlayType == GamePlayType.Paused)
            ChangeGamePlayType(GamePlayType.Running);
    }
}
