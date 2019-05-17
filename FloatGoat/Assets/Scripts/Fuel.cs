using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : WallObject
{
    [Tooltip("amount to increase fuel")]
    public float fuel;

    public static Fuel[] fuels;
    public static GameObject prefab;

    // Use this for initialization
    void Start()
    {
        // fuel = 20;
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
    */

    public override void Init()
    {
        fuels = new Fuel[Wall.walls.Length * maxObjects];
        GameObject b;
        prefab = gameObject;
        for (int i = 0; i < fuels.Length; i++)
        {
            b = Instantiate(prefab);
            fuels[i] = b.GetComponent<Fuel>();
            b.SetActive(false);
        }
    }

    public override WallObject Spawn(Transform parent, Vector3 pos)
    {
        foreach (Fuel f in fuels)
        {
            if (!f.gameObject.activeSelf)
            {
                f.gameObject.SetActive(true);
                f.transform.parent = parent;
                f.transform.localPosition = pos;
                return f;
            }
        }
        Debug.LogError("No more objects in pool");
        return null;
    }

    public override void ApplyToPlayer(PlayerController player)
    {
        player.HitFuel(fuel);
        player.score += fuel;
        gameObject.SetActive(false);
    }

    public override void Recycle()
    {
        gameObject.SetActive(false);
    }
}
