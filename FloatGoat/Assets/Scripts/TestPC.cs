using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPC : MonoBehaviour {

    public float speed;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
	}
}
