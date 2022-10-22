using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameTilePrefab;
    [SerializeField] Transform originTileTransform;
    [SerializeField] int tileColumnCount = 8;
    [SerializeField] int tileRowCount = 6;
    [SerializeField] float tileSpacing = 1f;
    [SerializeField] float enemySpawnDistance = 6f;

    [SerializeField] Character playerCharacter;
    GameTile[,] gameTiles;

    [SerializeField] GameObject enemyPrefab;
    static Vector3 centerPosition;

    private void Start()
    {
        gameTiles = new GameTile[tileColumnCount, tileRowCount];

        for (int z = 0; z < tileRowCount; z++)
        {
            for (int x = 0; x < tileColumnCount; x++)
            {
                Vector3 spawnPosition = originTileTransform.position;
                spawnPosition.x += x * tileSpacing;
                spawnPosition.z += z * tileSpacing;
                gameTiles[x, z] = Instantiate(gameTilePrefab, spawnPosition, Quaternion.identity).GetComponent<GameTile>();
            }
        }

        centerPosition.x = originTileTransform.position.x + 0.5f * tileSpacing * tileColumnCount - 0.5f * tileSpacing;
        centerPosition.z = originTileTransform.position.z + 0.5f * tileSpacing * tileRowCount - 0.5f * tileSpacing;

        MoveCharacterToTile(4, 3);

        StartCoroutine(EnemySpawnCoroutine());
    }

    void MoveCharacterToTile(int i, int j)
    {
        playerCharacter.transform.position = gameTiles[i, j].transform.position;
    }

    Vector3 GetRandomEnemySpawn()
    {
        return GetRandomPointAroundOrigin(enemySpawnDistance);
    }

    public static Vector3 GetRandomPointAroundOrigin(float radius)
    {
        var spawnPos = centerPosition;
        var circle = Random.insideUnitCircle.normalized * radius;
        spawnPos.x += circle.x;
        spawnPos.z += circle.y;
        return spawnPos;
    }

    IEnumerator EnemySpawnCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                var chaserEnemy = PoolManager.Instance.GetPooledChaserEnemy();
                chaserEnemy.transform.position = GetRandomEnemySpawn();
                yield return new WaitForSeconds(2f);
            }

            var shooterEnemy = PoolManager.Instance.GetPooledShooterEnemy();
            shooterEnemy.transform.position = GetRandomEnemySpawn();
            yield return new WaitForSeconds(2f);
        }
    }
}
