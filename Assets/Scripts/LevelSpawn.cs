using System.Collections.Generic;
using UnityEngine;

public class LevelSpawn : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate = 2.5f;
    public float minHeight = 0f; 
    public float maxHeight = 4f;   
    public float moveSpeed = 3f;
    public List<GameObject> spawnedPipes = new List<GameObject>();

    private void Start()
    {
        InvokeRepeating("SpawnPipe", 0f, spawnRate);
    }

    void SpawnPipe()
    {
        float spawnY = Random.Range(minHeight, maxHeight);

        Vector3 spawnPosition = new Vector3(transform.position.x + spawnRate, spawnY, transform.position.z);
        GameObject newPipe = Instantiate(pipePrefab, spawnPosition, Quaternion.identity, transform);
        spawnedPipes.Add(newPipe);
        newPipe.AddComponent<PipeMovement>().speed = moveSpeed;

        if (spawnedPipes.Count > 1)
        {
            spawnedPipes.RemoveAt(0);
        }

    }
}
