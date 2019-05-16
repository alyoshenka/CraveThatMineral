﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static void Die()
    {
        SceneManager.LoadScene("Death");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("AlexiScene");
    }
}
