using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableUnit> _units;

    private void Awake()
    {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void SpawnPlayers()
    {
        int PlayerCount = 1;

        for(int i = 0; i < PlayerCount; i++)
        {
            BasePlayer randomPrefab = GetRandomUnit<BasePlayer>(Faction.Player);
            BasePlayer spawnedPlayer = Instantiate(randomPrefab);

            Tile randomSpawnedTile = GridManager.Instance.GetPlayerSpawnedTile();

            randomSpawnedTile.SetUnit(spawnedPlayer);
        }

        GameManager.Instance.UpdateGameState(GameState.SpawnRocket);
    }

    public void SpawnRockets()
    {
        int RocketCount = 5;

        for (int i = 0; i < RocketCount; i++)
        {
            BaseRocket randomPrefab = GetRandomUnit<BaseRocket>(Faction.Rocket);
            BaseRocket spawnedRocket = Instantiate(randomPrefab);

            Tile randomSpawnedTile = GridManager.Instance.GetRocketSpawnedTile();

            randomSpawnedTile.SetUnit(spawnedRocket);
        }

        GameManager.Instance.UpdateGameState(GameState.Gameplay);
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }
}
