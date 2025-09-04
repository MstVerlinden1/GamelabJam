using UnityEngine;
using TMPro;

public class SwitchingScript : MonoBehaviour
{
    [SerializeField, Tooltip("All the microgames that could be switched to.")]
    private MicroGamesScriptableObjects[] microgames;
    [SerializeField, Tooltip("Text object that shows the description of the microgames.")]
    private TMP_Text descriptionText;
    
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

            if (descriptionText != null)
            {
                descriptionText.SetText(_chosenMicrogame.description);
            }
        }
    }

    private void OnDisable()
    {
        // Play Method of said Microgame
        // _chosenMicrogame.GetComponent<MicroGameManager>().PlayGame()
    }
}
