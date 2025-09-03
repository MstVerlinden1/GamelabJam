using UnityEngine;

public class SwitchingScript : MonoBehaviour
{
    [SerializeField, Tooltip("All the microgames that could be switched to.")]
    private MicroGamesScriptableObjects[] microgames;
    
    private MicroGamesScriptableObjects _chosenMicrogame;
    private GameObject _chosenMicrogameObject;

    private void OnEnable()
    {
        if (microgames != null && microgames.Length > 0)
        {
            _chosenMicrogame = microgames[Random.Range(0, microgames.Length)];
            if (_chosenMicrogame.prefab != null)
            {
                _chosenMicrogameObject = Instantiate(_chosenMicrogame.prefab);
                _chosenMicrogameObject.transform.position = new Vector3(0,0,0);
            }
        }
    }

    private void OnDisable()
    {
        // Play Method of said Microgame
        // _chosenMicrogame.GetComponent<MicroGameManager>().PlayGame()
    }
}
