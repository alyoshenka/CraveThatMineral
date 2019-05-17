using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public AudioClip theme;
    [Range(0, 1)]
    public float initialVolume;
    [SerializeField]
    [Tooltip("volume range, after initial")]
    Vector2 volRange;
    [SerializeField]
    [Tooltip("pitch range, after initial")]
    Vector2 pitchRange;

    AudioSource audioS;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
        audioS.volume = initialVolume;
        audioS.PlayOneShot(theme);
    }

    void Update()
    {
        if (!audioS.isPlaying)
        {
            audioS.volume = Random.Range(volRange.x, volRange.y);
            audioS.pitch = Random.Range(pitchRange.x, pitchRange.y);
            audioS.PlayOneShot(theme);
        }
    }

    public static void Die()
    {
        SceneManager.LoadScene("Death");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("AlexiScene");
    }
}
