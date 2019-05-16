using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    PlayerController player;

    public Text fuel;
    public Text score;
    public Text timer;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        fuel.text = player.Fuel + " / " + player.FuelMax;
        score.text = (int)player.score + " pts";
        timer.text = player.elapsedTime + " s";
	}
}
