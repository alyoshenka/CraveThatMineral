﻿using System.Collections;
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

    public static Bomb[] bombs;

    bool warningDone;
    public static AudioSource currentSource;

    void Start()
    {
        currentSource = GetComponent<AudioSource>();
        currentSource.playOnAwake = false;
    }

    void Update()
    {
        //if (transform.position.z <= approachDist)
        //{
        //    currentSource.Stop();
        //    warningDone = true;
        //    currentSource.PlayOneShot(growl);
        //}
    }

    public override void Init()
    {
        if(null == bombs)
        {
            bombs = new Bomb[Wall.walls.Length * maxObjects];
            GameObject b;
            prefab = gameObject;
            for (int i = 0; i < bombs.Length; i++)
            {
                b = Instantiate(prefab);
                Bomb bom = b.GetComponent<Bomb>();
                bombs[i] = bom;
                bom.recycle = true;
            }
        }
       
    }

    public override WallObject Spawn(Transform parent, Vector3 pos)
    {
        foreach(Bomb b in bombs)
        {
            if (b.recycle)
            {
                b.transform.parent = parent;
                b.transform.localPosition = pos;
                b.transform.Rotate(new Vector3(0, Random.Range(-180, 180), 0));
                b.recycle = false;
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

        Recycle();
    }

    public override void Recycle()
    {
        // do things
        warningDone = false;
        recycle = true;
    }
}
