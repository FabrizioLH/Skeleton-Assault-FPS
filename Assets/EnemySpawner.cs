using UnityEngine;
using UnityEngine.AI;

public class SkeletonSpawner : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public int skeletonCount = 5;
    public Terrain terrain;
    public int maxAttempts = 10;

    void Start()
    {
        for (int i = 0; i < skeletonCount; i++)
        {
            TrySpawnSkeleton();
        }
    }

    void TrySpawnSkeleton()
    {
        int attempts = 0;
        bool success = false;

        while (attempts < maxAttempts && !success)
        {
            attempts++;

            Vector3 spawnPoint = GetRandomPointOnTerrain();

            // Comprobar si hay NavMesh cerca
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPoint, out hit, 2f, NavMesh.AllAreas))
            {
                Instantiate(skeletonPrefab, hit.position, Quaternion.identity);
                success = true;
            }
        }

        if (!success)
        {
            Debug.LogWarning("No se pudo encontrar una posición válida en el terreno para el esqueleto.");
        }
    }

    Vector3 GetRandomPointOnTerrain()
    {
        Vector3 terrainPos = terrain.transform.position;
        Vector3 terrainSize = terrain.terrainData.size;

        float x = Random.Range(200, 700 );
        float z = Random.Range(200, 700);
        float y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrainPos.y;

        return new Vector3(x, y, z);
    }
}
