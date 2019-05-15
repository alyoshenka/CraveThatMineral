using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public Transform player;
    public int tunnelLength;
    public GameObject wall;
    public float wallDepth;

    GameObject[] walls;
    int wallIdx;
    float currentWallZ;
    Vector3 newPos;
   

    // Use this for initialization
    void Start()
    {
        walls = new GameObject[tunnelLength];
        wallIdx = 0;
        newPos = Vector3.zero;
        newPos.z = (tunnelLength - 1) * wallDepth;
       
        // init walls
        GameObject obj;
        Vector3 pos = Vector3.zero;
        for(int i = 0; i < tunnelLength; i++)
        {            
            obj = Instantiate(wall, pos, Quaternion.identity);
            walls[i] = obj;
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
        newPos.z += wallDepth;
        currentWallZ += wallDepth;

        walls[wallIdx].transform.position = newPos;

        wallIdx++;
        if (wallIdx >= tunnelLength) { wallIdx = 0; }
    }

    GameObject WallPrev()
    {
        return wallIdx == 0 ? walls[tunnelLength - 1] : walls[wallIdx];
    }
}
