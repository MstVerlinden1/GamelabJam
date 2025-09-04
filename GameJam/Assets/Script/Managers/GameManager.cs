using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    # region Create Instance
    
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    #endregion
    
    [Tooltip("Set if the player won the last microgame.")]
    public bool winGame = false;
    [HideInInspector]
    public bool startGame = false;
}
