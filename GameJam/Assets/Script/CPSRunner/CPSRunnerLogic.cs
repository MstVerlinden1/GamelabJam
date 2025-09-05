using System;
using UnityEngine;

public class CPSRunnerLogic : MonoBehaviour
{
    [SerializeField] private Transform player, enemy;
    [SerializeField] private float maxTime = 10f;
    [SerializeField] private float maxClicks = 10f;
    [SerializeField] private float minSize, maxSize, minY, maxY;
    [SerializeField] private float enemyBuffIndex;
    private float timer;
    private CPSRunnerInput input;
    [SerializeField]private int clicks;
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
        if (started)
        {
            timer += Time.deltaTime;
        }
        if (timer >= maxTime && clicks <= maxClicks && started)
        {
            //lose
            print("lose");
            stop();
        }
        if (timer <= maxTime && clicks >= maxClicks && started)
        {
            //YOU WINNNNNN WOWOWEEEEE
            print("win");
            stop();
        }
    }
    void OnClick()
    {
        if (started)
        {
            clicks++;
            float temp = maxSize / maxClicks;
            player.localScale += new Vector3(temp, temp, temp);
            enemy.localScale += new Vector3(temp * enemyBuffIndex, temp * enemyBuffIndex, temp * enemyBuffIndex);
            float temp1 = maxY / maxClicks;
            player.position -= new Vector3(0, temp1, 0);
            enemy.position -= new Vector3(0, temp1 * enemyBuffIndex, 0);
            if (clicks > maxClicks)
            {
                started = false;
            }
        }
    }

    private void stop()
    {
        player.GetComponent<Animator>().enabled = false;
        enemy.GetComponent<Animator>().enabled = false;
        started = false;
    }
}
