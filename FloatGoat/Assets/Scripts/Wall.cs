﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    [Tooltip("How far wall must be behind player to recycle")]
    public float wrapVal;
    [SerializeField]
    [Tooltip("The width for obstacles to spawn between")]
    public Vector2 tunnelWidth;
    [Tooltip("The depth for obstacles to spawn between")]
    public float wallDepth;
    [Tooltip("The height range for obstacles to spawn between")]
    [SerializeField]
    public Vector2 tunnelHeight;

    public GameObject wallL;
    public GameObject wallR;
    public List<GameObject> floors;

    [Header("Objects")]
    [Tooltip("Objects a wall can have")]
    public List<WallObject> potentialObjects;

    public float Speed { set; get; }

    public static float spawnZ;
    public static GameObject obj;
    public static Wall[] walls;
    public static bool hasInit = false;

    List<WallObject> childObjects;
    Vector3 newPos;
    Vector3 origScale;

	// Use this for initialization
	void Start () {
        childObjects = new List<WallObject>();
        newPos = transform.position;

        tunnelWidth /= 2f;
        wallDepth /= 2f;

        // instantiate all objects
        if (!hasInit)
        {
            foreach (WallObject w in potentialObjects) { w.Init(); }
            hasInit = true;
        }

        foreach (GameObject fl in floors) { fl.SetActive(false); }
        floors[Random.Range(0, floors.Count)].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        newPos.z -= Speed * Time.deltaTime;
        transform.position = newPos;

        if (newPos.z < wrapVal) { Recycle(); }
	}

    public void Recycle()
    {
        newPos.z = spawnZ;
        transform.position = newPos;

        foreach(WallObject thing in childObjects) { thing.Recycle(); }

        wallL.transform.Rotate(new Vector3(0, Random.Range(0, 3) * 90, 0));
        wallR.transform.Rotate(new Vector3(0, Random.Range(0, 3) * 90, 0));

        foreach (GameObject fl in floors) { fl.SetActive(false); }
        floors[Random.Range(0, floors.Count)].SetActive(true);

        childObjects.Clear();
        SpawnObjects();
    }

    public void SpawnObjects()
    {
        Vector3 pos = new Vector3();
        foreach (WallObject i in potentialObjects)
        {
            if(i.maxObjects <= 0)
            {
                if (Random.Range(0, i.chance) == 0)
                {
                    pos.x = Random.Range(tunnelWidth.x, tunnelWidth.y);
                    pos.y = Random.Range(tunnelHeight.x, tunnelHeight.y);
                    pos.z = Random.Range(-wallDepth, wallDepth);
                    childObjects.Add(i.Spawn(transform, pos));
                }
            }
            else
            {
                for (int j = 0; j < Random.Range(0, i.maxObjects); j++)
                {
                    pos.x = Random.Range(tunnelWidth.x, tunnelWidth.y);
                    pos.y = Random.Range(tunnelHeight.x, tunnelHeight.y);
                    pos.z = Random.Range(-wallDepth, wallDepth);
                    childObjects.Add(i.Spawn(transform, pos));
                }
            }
            
        }
    }
}
