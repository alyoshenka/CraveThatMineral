using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    [Tooltip("Player Game Object")]
    public Transform player;
    [Tooltip("The base wall prefab")]
    public GameObject wall;
    [Tooltip("The number of times the base wall prefab repeats")]
    public int tunnelLength;
    [Tooltip("Z dimension width - how far apart wlls instantiate")]
    public float wallDepth;
    [Tooltip("The width for obstacles to spawn between")]
    public float tunnelWidth;
    [Tooltip("Enemy prefab")]
    public GameObject enemyPrefab;

    [Header("Enemies")]
    [Tooltip("Inverse chance a tile will have an enemy")]
    public int spawnFrequency;

    Wall[] walls;
    GameObject[] enemies;
    int wallIdx;
    int enemyIdx;
    float currentWallZ;
    float newZ;
    float spawnElapsed;
    float spawnNext;

    // Use this for initialization
    void Start()
    {
        Wall.obj = wall;

        walls = new Wall[tunnelLength];
        wallIdx = 0;
        newZ = (tunnelLength - 1) * wallDepth;
       
        // init walls
        GameObject obj;
        Vector3 pos = Vector3.zero;
        for(int i = 0; i < tunnelLength; i++)
        {            
            obj = Instantiate(wall, pos, Quaternion.identity);
            walls[i] = obj.GetComponent<Wall>();
            pos.z += wallDepth;
        }

        currentWallZ = wallDepth;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z > currentWallZ) { WrapWalls(); }

    }

    void WrapWalls()
    {            
        newZ += wallDepth;
        currentWallZ += wallDepth;

        walls[wallIdx].Reset(newZ);

        wallIdx++;
        if (wallIdx >= tunnelLength) { wallIdx = 0; }

        SpawnEnemy();
    }

    Wall WallPrev()
    {
        return wallIdx == 0 ? walls[tunnelLength - 1] : walls[wallIdx];
    }

    void SpawnEnemy()
    {
        if(Random.Range(0, spawnFrequency) == 0)
        {

        }
    }
}
