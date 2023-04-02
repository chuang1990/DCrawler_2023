using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DungeonGenerator : MonoBehaviour
{
    private static DungeonGenerator _instance;
    private Vector2Int _mapSize;


    public GameObject wallPrefab;
    public GameObject doorPrefab;
    public GameObject playerPrefab;
    public GameObject endPrefab;

    public string dungeonFile;

    public enum TileType {Wall, Door, Start, End, Empty}

    public TileType[,] tileMap;
    private GameObject[,] _tileMap;

    public Vector2Int mapSize => _mapSize;

    public static DungeonGenerator instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DungeonGenerator>();
                if (_instance == null)
                {
                    Debug.LogError("No DungeonGenerator instance found in the scene!");
                }
            }
            return _instance;
        }
    }

    public void GenerateDungeon()
    {
        // Destroy the previous tiles
        DestroyTiles();

        // Load the dungeon file
        string[] lines = File.ReadAllLines(dungeonFile);

        // Set the map size
        _mapSize = new Vector2Int(lines[0].Length, lines.Length);

        // Create the tile map
        tileMap = new TileType[mapSize.x, mapSize.y];
        _tileMap = new GameObject[mapSize.x, mapSize.y];

        // Create the dungeon tiles
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                char c = line[x];
                Vector3 position = new Vector3(x, 0, y);

                switch (c)
                {
                    case '.':
                        // Create an empty space tile
                        tileMap[x, y] = TileType.Empty;
                        break;

                    case '#':
                        // Create a wall tile
                        _tileMap[x, y] = Instantiate(wallPrefab, position, Quaternion.identity);
                        tileMap[x, y] = TileType.Wall;
                        break;

                    case '+':
                        // Create a door tile
                        _tileMap[x, y] = Instantiate(doorPrefab, position, Quaternion.identity);
                        tileMap[x, y] = TileType.Door;
                        break;

                    case 'S':
                        // Create the player starting point
                        // _tileMap[x, y] = Instantiate(playerPrefab, position, Quaternion.identity);
                        tileMap[x, y] = TileType.Start;
                        break;

                    case 'E':
                        // Create the end point
                        _tileMap[x, y] = Instantiate(endPrefab, position, Quaternion.identity);
                        tileMap[x, y] = TileType.End;
                        break;
                }
            }
        }
    }

    private void Start()
    {
        _instance = this;
        GenerateDungeon();

    }

    public bool IsTileWalkable(Vector2Int position)
    {
        if (position.x < 0 || position.x >= mapSize.x || position.y < 0 || position.y >= mapSize.y)
        {
            return false;
        }

        TileType tile = tileMap[position.x, position.y];

        if (tile != TileType.Wall)
        {
            return true;
        }
        return false;

        // return tile.CompareTag("Walkable");
    }

    private void DestroyTiles()
    {
        // Destroy the previous tiles
        if (_tileMap != null)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    if (_tileMap[x, y] != null)
                    {
                        DestroyImmediate(_tileMap[x, y]);
                    }
                }
            }
        }
    }
}
