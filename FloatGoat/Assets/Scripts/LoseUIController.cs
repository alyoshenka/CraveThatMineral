using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseUIController : MonoBehaviour {

    public Text scoreTxt;

	// Use this for initialization
	void Start () {
        scoreTxt.text = (int)(GameObject.FindGameObjectWithTag("Carryover").GetComponent<Carryover>().playerScore) + "";
	}
	
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
