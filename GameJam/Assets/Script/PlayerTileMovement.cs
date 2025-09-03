using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerTileMovement : MonoBehaviour
{
    private CrossyRoadInput input;
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap boundariesTilemap;

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
    }
    private void OnMove(Vector2 direction)
    {
        if (CanMove(direction))
            transform.position += (Vector3)direction;
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)direction);
        if(!groundTilemap.HasTile(gridPosition) || boundariesTilemap.HasTile(gridPosition))
            return false;
        return true;
    }
}
