using UnityEngine;

public class CrossroadManager : MonoBehaviour
{
    [SerializeField] private PlayerTileMovement playerMovement;
    [SerializeField] private CrossroadCamera crossroadCamera;
    [SerializeField] private Collider2D winCollider;
    [SerializeField] private float maxTime;
    public bool started = false;
    public static CrossroadManager instance;
    private float timer;
    
    private bool _gameOver = false;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
        //once game is Enabled turn the camera movement script off to start it from gamemanager and stops player movement untill game started
        if (crossroadCamera.enabled == true)
            crossroadCamera.enabled = false;
        if(playerMovement.enabled == true)
            playerMovement.enabled = false;
    }

    private void Update()
    {
        started = GameManager.instance.startGame;
        //once started enable script so the camare moves, starts a time and one timer reaches the max time the camera stops again
        if (started)
        {
            if (!_gameOver)
            {
                crossroadCamera.enabled = true;
                playerMovement.enabled = true;
            }
            
            timer += Time.deltaTime;
            if (timer >= maxTime)
            {
                GameOver();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.winGame = true;
        }
    }

    public void GameOver()
    {
        //stops camera from moving again and stops player from moving further
        crossroadCamera.enabled = false;
        playerMovement.enabled = false;
        _gameOver = true;
    }
}
