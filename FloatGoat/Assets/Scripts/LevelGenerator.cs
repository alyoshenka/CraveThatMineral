using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public Transform player;
    public int tunnelLength;
    public GameObject wall;
    public float resetVal;

    GameObject[] walls;
    int wallIdx;
    float currentWallZ;

    // Use this for initialization
    void Start()
    {
        walls = new GameObject[tunnelLength];
        wallIdx = 0;
        currentWallZ = walls[wallIdx].transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z > currentWallZ + resetVal) { WrapWalls(); }
    }

    void WrapWalls()
    {

    }

    GameObject WallPrev()
    {
        return wallIdx == 0 ? walls[tunnelLength - 1] : walls[wallIdx];
    }


}
