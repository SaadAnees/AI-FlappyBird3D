using System.Collections.Generic;
using UnityEngine;

public class EnvironmentScroller : MonoBehaviour
{
    public GameObject environmentPrefab;
    public float scrollSpeed = 2f;
    public float environmentLength = 50f; 
    public int initialSegments = 5; 
    public float destroyOffset = 10f; 

    private List<GameObject> spawnedEnvironments = new List<GameObject>();
    private float nextSpawnX;

    void Start()
    {
        float currentX = Camera.main.transform.position.x - (initialSegments * environmentLength) / 2f;
        for (int i = 0; i < initialSegments; i++)
        {
            SpawnEnvironment(currentX);
            currentX += environmentLength;
        }

        nextSpawnX = currentX;
    }

    void Update()
    {
        foreach (var environment in spawnedEnvironments)
        {
            environment.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        }

        float cameraRightEdge = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        if (cameraRightEdge + destroyOffset > nextSpawnX)
        {
            SpawnEnvironment(nextSpawnX);
            nextSpawnX += environmentLength;
        }

        float cameraLeftEdge = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;
        if (spawnedEnvironments.Count > 0 && spawnedEnvironments[0].transform.position.x < cameraLeftEdge - destroyOffset)
        {
            Destroy(spawnedEnvironments[0]);
            spawnedEnvironments.RemoveAt(0);
        }
    }

    void SpawnEnvironment(float xPosition)
    {
        GameObject newEnvironment = Instantiate(environmentPrefab, new Vector3(xPosition, transform.position.y, transform.position.z), Quaternion.identity, transform);
        spawnedEnvironments.Add(newEnvironment);
    }
}
