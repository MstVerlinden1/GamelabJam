using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CPSRunnerLogic : MonoBehaviour
{
    [SerializeField] private Transform player, enemy;
    [SerializeField] private float speed;
    private CPSRunnerInput input;
    private int clicksThisSecond;
    private int currentCPS;
    private float cpsTimer;
    private float time;
    [SerializeField] private float maxTime = 10f;
    public bool started = false;
    private bool won = false;

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Awake()
    {
        input = new CPSRunnerInput();
        input.PlayerAction.Click.performed += ctx => OnClick();
    }

    private void Update()
    {
        if (won)
            OnWin();
        if (started)
        {
            time += Time.deltaTime;
            cpsTimer += Time.deltaTime;
            if (time <= 2f)
            {
                currentCPS = 2;
                
            }
            PartObjects();
            if (time >= maxTime)
            {
                started = false;
                won = true;
                print("You win");
            }

            if (player.position.y < enemy.position.y +1)
            {
                started = false;
                print("You lose");
            }
        }
        if (cpsTimer >= 1f)
        {
            /*calculate current clicks er second add it to currentCPS then reset the clicksthissecond and time*/
            currentCPS = clicksThisSecond;
            clicksThisSecond = 0;
            cpsTimer = 0;
        }
    }
    void OnClick()
    {
        clicksThisSecond++;
    }

    private void PartObjects()
    {
        Vector3 newDistance = enemy.position + new Vector3(0, currentCPS, 0);
        Vector3 newis = Vector3.Lerp(player.position,newDistance , speed * Time.deltaTime);
        player.position = newis;
    }

    private void OnWin()
    {
        player.position += new Vector3(0, 5 * Time.deltaTime, 0);
        enemy.position -= new Vector3(0, 5 * Time.deltaTime, 0);
    }
}
