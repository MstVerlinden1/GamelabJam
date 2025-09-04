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
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void OnEnable()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void OnDisable()
    {
        instance = null;
    }

    #endregion
    
    [Tooltip("Set if the player won the last microgame.")]
    public bool winGame = false;
    [HideInInspector]
    public bool startGame = false;
    [Tooltip("GameSwitcher")]
    public GameObject gameSwitcher;
}
