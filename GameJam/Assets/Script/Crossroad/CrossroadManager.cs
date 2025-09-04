using System;
using UnityEngine;

public class CrossroadManager : MonoBehaviour
{
    [SerializeField] private PlayerTileMovement playerMovement;
    [SerializeField] private CrossroadCamera crossroadCamera;
    [SerializeField] private Collider2D winCollider;
    [SerializeField] private float maxTime;
    [SerializeField] private Transform carTransform,playerTransform;
    [SerializeField] private float offset;
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

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null; 
            if (GameManager.instance != null)
                GameManager.instance.gameSwitcher.SetActive(true);
        }
    }

    private void Update()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.gamePlayType != GamePlayType.Running && GameManager.instance.startGame)
                return;
            
            started = GameManager.instance.startGame;
        }
        else started = true;
        
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
                if (GameManager.instance != null)
                    GameManager.instance.winGame = true;
            }
        }

        if (_gameOver)
        {
            if (GameManager.instance != null && !GameManager.instance.winGame)
                carTransform.position = new Vector3(Mathf.Lerp(carTransform.position.x,playerTransform.position.x + offset,5f*Time.deltaTime),carTransform.position.y,carTransform.position.z);
            
            if ((int)carTransform.position.x == (int)(playerTransform.position.x + offset) && GameManager.instance != null || GameManager.instance == null || GameManager.instance != null && GameManager.instance.winGame)
                Destroy(gameObject, 1f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.instance != null && GameManager.instance.gamePlayType != GamePlayType.Running)
            return;
        
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance != null)
                GameManager.instance.winGame = false;
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
