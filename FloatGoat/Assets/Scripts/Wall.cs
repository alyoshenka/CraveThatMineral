using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    [Tooltip("How far wall must be behind player to recycle")]
    public float wrapVal;
    [Tooltip("The width for obstacles to spawn between")]
    public float tunnelWidth;
    [Tooltip("The depth for obstacles to spawn between")]
    public float wallDepth;
    [Tooltip("The height range for obstacles to spawn between")]
    [SerializeField]
    public Vector2 tunnelHeight;

    [Header("Objects")]
    [Tooltip("Objects a wall can have")]
    public List<WallObject> potentialObjects;

    public float Speed { set; get; }

    public static float spawnZ;
    public static GameObject obj;
    public static Wall[] walls;

    List<WallObject> objects;

    Vector3 newPos;

	// Use this for initialization
	void Start () {
        objects = new List<WallObject>();
        newPos = transform.position;

        tunnelWidth /= 2f;
        wallDepth /= 2f;

        // instantiate all objects
        
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

        foreach(WallObject thing in objects) { thing.Recycle(); }

        objects.Clear();
        SpawnObjects();
    }

    public void SpawnObjects()
    {
        Vector3 pos = new Vector3();
        foreach (WallObject i in potentialObjects)
        {
            if (Random.Range(0, i.chance) == 0)
            {
                Debug.Log("obj init");
                pos.x = Random.Range(-tunnelWidth, tunnelWidth);
                pos.y = Random.Range(tunnelHeight.x, tunnelHeight.y);
                pos.z = Random.Range(-wallDepth, wallDepth);
                i.Spawn(transform, pos);
            }
        }
    }
}
