using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public AudioClip theme;
    [Range(0, 1)]
    public float initialVolume = 1f;
    Vector2 pitchRange;
    public float volLerpIn;

    public Text scoreTxt;

    AudioSource audioS;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
        audioS.volume = initialVolume;
        audioS.PlayOneShot(theme);
        try { scoreTxt.text = (int)(GameObject.FindGameObjectWithTag("Carryover").GetComponent<Carryover>().playerScore) + ""; }
        catch { }
    }

    void Update()
    {
        audioS.volume = Mathf.Lerp(0, initialVolume, Time.timeSinceLevelLoad / volLerpIn);
    }

    public static void Die(float score)
    {
        GameObject.FindGameObjectWithTag("Carryover").GetComponent<Carryover>().playerScore = score;
        SceneManager.LoadScene("EndScene");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("AlexiTestScene");
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
