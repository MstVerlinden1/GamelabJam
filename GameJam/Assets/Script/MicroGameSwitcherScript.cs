using TMPro;
using UnityEngine;

public class MicroGameSwitcherScript : MonoBehaviour
{
    #region variables
    
    [SerializeField, Tooltip("MicroGame Switcher Animator.")]
    private Animator animator;

    [SerializeField, Tooltip("Lifes from left to right.")]
    private GameObject[] lifes = null;
    [SerializeField, Tooltip("Score counter.")]
    private TMP_Text scoreCounter = null;
    
    private int _pNextAnimID;
    private bool _isPlaying;
    
    private int _score = 0;
    private bool _UIUpdated = false;
    
    private bool _gameOver = false;
    
    #endregion
    
    # region On Activation Methods
    
    private void Awake()
    {
        if (animator == null)
            return;

        animator = GetComponent<Animator>();
        _pNextAnimID = Animator.StringToHash("NextAnimation");
    }
    
    #endregion
    
    #region Animations
    
    void Update()
    {
        if (animator == null && _gameOver)
            return;
        
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Empty") && !AnimatorPlaying())
        {
            AnimationFinished();
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
        {
            if (GameManager.instance != null)
                animator.SetBool("Win", GameManager.instance.winGame);
            animator.SetTrigger(_pNextAnimID);
        }
        else if (AnimatorPlaying())
        {
            _UIUpdated = false;
        }
    }

    void AnimationFinished()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("NextGameTransition"))
        {
            gameObject.SetActive(false);
        }
        else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("WinGame") && !_UIUpdated)
            {
                _UIUpdated = true;
                AddPoint();
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("LoseGame") && !_UIUpdated)
            {
                _UIUpdated = true;
                RemoveLife();
            }
            
            animator.SetTrigger(_pNextAnimID);  
        }
    }
    
    bool AnimatorPlaying()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime>1 && 
           !animator.IsInTransition(0))
        {
            return false;
        }
        
        return true;
    }

    #endregion
    
    # region Updating Stats
    
    void RemoveLife()
    {
        if (lifes == null)
            return;
        if (lifes.Length == 0)
            return;

        int lifesRemoved = 0;
        
        foreach (GameObject life in lifes)
        {
            if (life.activeSelf)
            {
                life.SetActive(false);

                lifesRemoved++;
                
                break;
            }
        
            lifesRemoved++;
        }

        _gameOver = lifesRemoved >= lifes.Length;
    }

    void AddPoint()
    {
        _score++;
        
        scoreCounter.SetText(_score.ToString());
    }
    
    #endregion
}
