using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using Input = UnityEngine.Windows.Input;

public class CrossroadCamera : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private GameObject player;
    private void Update()
    {
        if (GameManager.instance != null && GameManager.instance.gamePlayType != GamePlayType.Running && GameManager.instance.startGame)
            return;
        //move the camera
        transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime, transform.position.z);
    }
}
