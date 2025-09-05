using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerTileMovement : MonoBehaviour
{
    private CrossyRoadInput input;
    [SerializeField] private Sprite roadSprite;
    [SerializeField] private Tilemap groundTilemap, boundariesTilemap;

    private Animator _animator;
    
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
        input = new CrossyRoadInput();
        input.Player.Move.performed += ctx => OnMove(ctx.ReadValue<Vector2>());
        _animator = GetComponent<Animator>();
    }
    private void OnMove(Vector2 direction)
    {
        if (GameManager.instance != null && GameManager.instance.gamePlayType != GamePlayType.Running && GameManager.instance.startGame)
            return;
        
        if (CanMove(direction))
        {
            transform.position += (Vector3)direction;
            _animator.SetTrigger("Jump");
            if (direction == Vector2.left)
                transform.rotation = Quaternion.Euler(0, 0, 90);
            else if (direction == Vector2.right)
                transform.rotation = Quaternion.Euler(0, 0, 270);
            else if (direction == Vector2.up)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else if (direction == Vector2.down)
                transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        //if moved on road stop movement and play gameover screen
        Vector3Int currentPosition = groundTilemap.WorldToCell(transform.position);
        if (groundTilemap.GetSprite(currentPosition) == roadSprite)
        {
            CrossroadManager.instance.GameOver(); /*Game over*/
            if (GameManager.instance != null)
                GameManager.instance.winGame = false;
        }
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)direction);
        if(!groundTilemap.HasTile(gridPosition) || boundariesTilemap.HasTile(gridPosition))
            return false;
        return true;
    }
    private void OnBecameInvisible()
    {
        CrossroadManager.instance.GameOver(); /*Game over*/
    }
}
