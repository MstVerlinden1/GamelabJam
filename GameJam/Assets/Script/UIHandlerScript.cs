using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandlerScript : MonoBehaviour
{
    [SerializeField, Tooltip("The scene where the game is played.")]
    private SceneAsset gameScene;

    public void StartGame()
    {
        SceneManager.LoadScene(gameScene.name);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exiting game");
    }

    public void ContinueGame()
    {
        GameManager.instance.ChangeGamePlayType(GamePlayType.Running);
    }
}
