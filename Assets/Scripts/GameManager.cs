using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameTilePrefab;

    [SerializeField] Transform originTileTransform;

    [SerializeField] int tileColumnCount = 8;
    [SerializeField] int tileRowCount = 6;

    [SerializeField] float tileSpacing = 1f;

    private void Start()
    {

        for (int z = 0; z < tileRowCount; z++)
        {
            for (int x = 0; x < tileColumnCount; x++)
            {
                Vector3 spawnPosition = originTileTransform.position;
                spawnPosition.x += x * tileSpacing;
                spawnPosition.z += z * tileSpacing;
                Instantiate(gameTilePrefab, spawnPosition, Quaternion.identity);
            } 
        }
    }
}
