using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carryover : MonoBehaviour {

    public float playerScore;

	void Start () {
        DontDestroyOnLoad(this);
	}
}
