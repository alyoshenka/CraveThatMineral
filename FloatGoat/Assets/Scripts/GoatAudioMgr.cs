using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatAudioMgr : MonoBehaviour {

    [SerializeField]
    [Tooltip("Min and max for next random goal noise")]
    public Vector2 randomGoatNoise;

    public AudioClip defaultGoat;
    public AudioClip damage;
    public AudioClip powerup;

    AudioSource effects;

    float RGNElapsed;
    float RGNTimer;

    // Use this for initialization
    void Start () {
        effects = GetComponent<AudioSource>();

        effects.playOnAwake = true;
        effects.clip = defaultGoat;
        RGNElapsed = RGNTimer = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (!effects.isPlaying && RGNElapsed >= RGNTimer)
        {
            // make new random goat noise
            RGNElapsed = 0f;
            RGNTimer = Random.Range(randomGoatNoise.x, randomGoatNoise.y);
            effects.PlayOneShot(defaultGoat);
        }

        if (!effects.isPlaying) { RGNElapsed += Time.deltaTime; }
    }

    public void DamageSound()
    {
        effects.PlayOneShot(damage);
    }

    public void PowerupSound()
    {
        effects.PlayOneShot(powerup);
    }
}


