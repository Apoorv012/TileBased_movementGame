using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform _cam;
    [SerializeField] private Transform tilesParentObject;
    [SerializeField] private List<Sprite> OceanTiles;           // Top, Right, Bottom, Left, Top-right, Bottom-Right, Bottom-Left, Top-Left
    [SerializeField] private List<Sprite> GroundTiles;

    public static GridManager Instance;

    private Dictionary<Vector2, Tile> tiles;

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        for(int x = -1; x < _width+1; x++)
        {
            for(int y = -1; y < _height+1; y++)
            {
                Tile spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.parent = tilesParentObject;

                bool isOffset = (Mathf.Abs(x + y) % 2 == 1);

                spawnedTile.init(isOffset);

                tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

        GameManager.Instance.UpdateGameState(GameState.RecolorGrid);
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

    public void RecolorGrid()
    {
        #region groundBorderTiles
        // Top
        for (int _x = 1; _x < _width - 1; _x++)
        {
            int _y = _height - 1;
            tiles[new Vector2(_x, _y)].ChangeSpriteTo(GroundTiles[0]);
        }

        // Right
        for (int _y = 1; _y < _height - 1; _y++)
        {
            int _x = _width - 1;
            tiles[new Vector2(_x, _y)].ChangeSpriteTo(GroundTiles[1]);
        }

        // Bottom
        for (int _x = 1; _x < _width - 1; _x++)
        {
            int _y = 0;
            tiles[new Vector2(_x, _y)].ChangeSpriteTo(GroundTiles[2]);
        }

        // Left
        for (int _y = 1; _y < _height - 1; _y++)
        {
            int _x = 0;
            tiles[new Vector2(_x, _y)].ChangeSpriteTo(GroundTiles[3]);
        }

        tiles[new Vector2(_width - 1, _height - 1)].ChangeSpriteTo(GroundTiles[4]);     // Top-right ground tile
        tiles[new Vector2(_width - 1, 0)].ChangeSpriteTo(GroundTiles[5]);     // Bottom-right ground tile
        tiles[new Vector2(0, 0)].ChangeSpriteTo(GroundTiles[6]);     // Bottom-left ground tile
        tiles[new Vector2(0, _height - 1)].ChangeSpriteTo(GroundTiles[7]);     // Top-left ground tile

        #endregion

        #region oceanTiles

        Tile currentTile;

        // Top
        for (int _x = 0; _x < _width; _x++)
        {
            int _y = _height;
            currentTile = tiles[new Vector2(_x, _y)];
            currentTile.ChangeSpriteTo(OceanTiles[0]);
            currentTile.isWalkable = false;
        }

        // Right
        for (int _y =0; _y < _height; _y++)
        {
            int _x = _width;
            currentTile = tiles[new Vector2(_x, _y)];
            currentTile.ChangeSpriteTo(OceanTiles[1]);
            currentTile.isWalkable = false;
        }

        // Bottom
        for (int _x = 0; _x < _width; _x++)
        {
            int _y = -1;
            currentTile = tiles[new Vector2(_x, _y)];
            currentTile.ChangeSpriteTo(OceanTiles[2]);
            currentTile.isWalkable = false;
        }

        // Left
        for (int _y = 0; _y < _height; _y++)
        {
            int _x = -1;
            currentTile = tiles[new Vector2(_x, _y)];
            currentTile.ChangeSpriteTo(OceanTiles[3]);
            currentTile.isWalkable = false;
        }

        currentTile = tiles[new Vector2(_width, _height)];
        currentTile.ChangeSpriteTo(OceanTiles[4]);      // Top-right ground tile
        currentTile.isWalkable = false;

        currentTile = tiles[new Vector2(_width, -1)];
        currentTile.ChangeSpriteTo(OceanTiles[5]);      // Bottom-right ground tile
        currentTile.isWalkable = false;

        currentTile = tiles[new Vector2(-1, -1)];
        currentTile.ChangeSpriteTo(OceanTiles[6]);      // Bottom-left ground tile
        currentTile.isWalkable = false;

        currentTile = tiles[new Vector2(-1, _height)];
        currentTile.ChangeSpriteTo(OceanTiles[7]);      // Top-left ground tile
        currentTile.isWalkable = false;

        #endregion

        GameManager.Instance.UpdateGameState(GameState.SpawnPlayer);
    }
}
