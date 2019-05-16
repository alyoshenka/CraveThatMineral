using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [Tooltip("Player Game Object")]
    public Transform player;
    [Tooltip("The base wall prefab")]
    public GameObject wall;
    [Tooltip("The number of times the base wall prefab repeats")]
    public int tunnelLength;
    [Tooltip("Z dimension width - how far apart walls instantiate")]
    public float wallDepth;
    [Tooltip("Player (wall) speed")]
    public float speed;

    // Use this for initialization
    void Start()
    {
        Wall.obj = wall;
        Wall.walls = new Wall[tunnelLength];

        // init walls
        GameObject obj;
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < tunnelLength; i++)
        {
            obj = Instantiate(wall, pos, Quaternion.identity);
            Wall w = obj.GetComponent<Wall>();
            w.Speed = speed;
            Wall.walls[i] = w;
            pos.z += wallDepth;
        }

        Wall.spawnZ = pos.z;
    }
}
