using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : WallObject
{
    public static GameObject prefab;

    [Tooltip("Amount of damage done")]
    public float damage;
    [Tooltip("approach growl")]
    public AudioClip growl;
    [Tooltip("how close to goat for warning growl")]
    public float approachDist;
    [SerializeField]
    [Tooltip("range of random sound timer")]
    public Vector2 growlRange;

    public GameObject wallL;
    public GameObject wallR;
    public List<GameObject> floors;

    public static Bomb[] bombs;

    bool warningDone;
    public static AudioSource currentSource;

    void Start()
    {
        currentSource = GetComponent<AudioSource>();
        currentSource.playOnAwake = false;
        warningDone = false;
        floors[Random.Range(0, floors.Count)].SetActive(false);
    }

    void Update()
    {
        Debug.Log(currentSource.isPlaying);
        if (transform.position.z <= approachDist)
        {
            currentSource.Stop();
            warningDone = true;
            currentSource.PlayOneShot(growl);
        }
    }

    public override void Init()
    {
        bombs = new Bomb[Wall.walls.Length * maxObjects];
        GameObject b;
        prefab = gameObject;
        for(int i = 0; i < bombs.Length; i++)
        {
            b = Instantiate(prefab);
            bombs[i] = b.GetComponent<Bomb>();
            b.SetActive(false);
        }
    }

    public override WallObject Spawn(Transform parent, Vector3 pos)
    {
        foreach(Bomb b in bombs)
        {
            if (!b.gameObject.activeSelf)
            {
                b.gameObject.SetActive(true);

                foreach(GameObject fl in floors) { fl.SetActive(false); }
                GameObject flo = floors[Random.Range(0, floors.Count)];
                flo.transform.Rotate(new Vector3(0, Random.Range(-180, 180), 0));

                b.transform.parent = parent;
                b.transform.localPosition = pos;
                b.transform.Rotate(new Vector3(0, Random.Range(-180, 180), 0));
                return b;
            }
        }
        Debug.LogError("No more objects in pool");
        return null;
    }

    public override void ApplyToPlayer(PlayerController player)
    {
        player.HitFuel(-damage);
        player.score -= damage;
        // show ui flash
        // play bomb sound
        gameObject.SetActive(false);
    }

    public override void Recycle()
    {
        // do things
        warningDone = false;
        gameObject.SetActive(false);
    }
}
