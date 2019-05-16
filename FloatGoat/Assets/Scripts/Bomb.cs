using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : WallObject
{
    public static GameObject prefab;

    [Tooltip("Amount of damage done")]
    public float damage;

    public static Bomb[] bombs;

    public override void Init()
    {
        bombs = new Bomb[Wall.walls.Length];
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
                Debug.Log("new bomb @ " + pos);
                b.gameObject.SetActive(true);
                b.transform.parent = parent;
                b.transform.localPosition = pos;
                return b;
            }
        }
        Debug.LogError("Alexi warning");
        return null;
    }

    public override void ApplyToPlayer()
    {
        // health -= damage
        // explode
    }

    public override void Recycle()
    {
        // do things

        gameObject.SetActive(false);
    }
}
