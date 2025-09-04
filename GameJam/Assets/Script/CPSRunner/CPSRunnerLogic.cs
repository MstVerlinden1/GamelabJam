using System;
using UnityEngine;

public class CPSRunnerLogic : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float maxTime = 10f;
    [SerializeField] private float maxClicks = 10f;
    [SerializeField] private float minSize, maxSize, minY, maxY;
    [SerializeField] private float speed;
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
            if (timer >= maxTime && clicks <= maxClicks)
            {
                //lose
                print("lose");
            }

            if (timer <= maxTime && clicks >= maxClicks)
            {
                //YOU WINNNNNN WOWOWEEEEE
                print("win");
            }
        }
    }
    void OnClick()
    {
        if (started && player.localScale.x <= maxSize)
        {
            clicks++;
            float temp = maxSize / maxClicks;
            player.localScale += new Vector3(temp, temp, temp);
            float temp1 = maxY / maxSize;
            player.position -= new Vector3(0, temp1, 0);
        }
    }
}
