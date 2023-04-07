using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    private Vector3Int currentTile;
    private bool isMoving;
    public EventReference footsteps;
    

    void Start()
    {
        currentTile = Vector3Int.RoundToInt(transform.position);
        isMoving = false;
    }

    void Update()
    {
        // Check for movement input
        if (!isMoving)
        {
            int horizontal = 0;
            int vertical = 0;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                horizontal -= 1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                horizontal += 1;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                vertical += 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                vertical -= 1;
            }

            // Move to new tile if there is input
            if (horizontal != 0 || vertical != 0)
            {
                Vector3Int targetTile = currentTile + new Vector3Int(horizontal, 0, vertical);
                Vector3 targetPos = targetTile;

                // Check if target tile is valid
                Debug.Log(IsTileWalkable(targetTile));
                if (IsTileWalkable(targetTile))
                {
                    StartCoroutine(MoveToTile(targetPos));
                    currentTile = targetTile;
                }
            }
        }
    }

    private bool IsTileWalkable(Vector3Int tile)
    {
        // Check if tile is within bounds
        if (tile.x < 0 || tile.x >= DungeonGenerator.instance.mapSize.x || tile.z < 0 || tile.z >= DungeonGenerator.instance.mapSize.y)
        {
            return false;
        }

        // Check if tile is not blocked
        if (DungeonGenerator.instance.tileMap[tile.x, tile.z] == DungeonGenerator.TileType.Wall)
        {
            return false;
        }

        return true;
    }

    private IEnumerator MoveToTile(Vector3 targetPos)
    {
        isMoving = true;

        while (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }
}