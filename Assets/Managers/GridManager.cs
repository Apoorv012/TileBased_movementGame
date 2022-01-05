using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform _cam;
    [SerializeField] private Transform tilesParentObject;

    public static GridManager Instance;

    private Dictionary<Vector2, Tile> tiles;

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        for(int x = 0; x < _width; x++)
        {
            for(int y = 0; y < _height; y++)
            {
                Tile spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.parent = tilesParentObject;

                bool isOffset = ((x + y) % 2 == 1);

                spawnedTile.init(isOffset);

                tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

        GameManager.Instance.UpdateGameState(GameState.SpawnPlayer);
    }

    public Tile GetPlayerSpawnedTile()
    {
        Tile randomTile = GetTileAtPosition(new Vector2(Random.Range(1, _width - 2), Random.Range(1, _height - 2)));
        return randomTile.Walkable() ? randomTile : GetPlayerSpawnedTile();
    }

    public Tile GetRocketSpawnedTile()
    {
        Tile randomTile = GetTileAtPosition(new Vector2(Random.Range(1, _width - 2), Random.Range(1, _height - 2)));
        return randomTile.Walkable() ? randomTile : GetRocketSpawnedTile();
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (tiles.TryGetValue(pos, out Tile tile))
            return tile;

        return null;
    }
}
