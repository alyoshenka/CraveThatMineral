using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public static GameObject obj;

    List<IWallObject> objects;

    Vector3 tempPos;

	// Use this for initialization
	void Start () {
        objects = new List<IWallObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reset(float zPos)
    {
        tempPos = transform.position;
        tempPos.z = zPos;
        transform.position = tempPos;

        foreach(IWallObject thing in objects) { thing.Recycle(); }
    }
}
