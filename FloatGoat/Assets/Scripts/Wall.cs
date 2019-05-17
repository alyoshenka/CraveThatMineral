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
    [Tooltip("The variance in height")]
    public float heightScaleVariance;

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
        origScale = transform.localScale;
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

        transform.GetChild(0).Rotate(new Vector3(0, Random.Range(0, 3) * 90, 0));
        transform.GetChild(1).Rotate(new Vector3(0, Random.Range(0, 3) * 90, 0));    

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
                    pos.x = Random.Range(-tunnelWidth, tunnelWidth);
                    pos.y = Random.Range(tunnelHeight.x, tunnelHeight.y);
                    pos.z = Random.Range(-wallDepth, wallDepth);
                    childObjects.Add(i.Spawn(transform, pos));
                }
            }
            else
            {
                for (int j = 0; j < Random.Range(0, i.maxObjects); j++)
                {
                    pos.x = Random.Range(-tunnelWidth, tunnelWidth);
                    pos.y = Random.Range(tunnelHeight.x, tunnelHeight.y);
                    pos.z = Random.Range(-wallDepth, wallDepth);
                    childObjects.Add(i.Spawn(transform, pos));
                }
            }
            
        }
    }
}
