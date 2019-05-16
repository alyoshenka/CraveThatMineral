using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : WallObject {

    [Tooltip("Amount of damage done")]
    public float damage;

    public static Bomb[] bombs;

    public override void Init()
    {
        GameObject b;
        prefab = gameObject;
        for(int i = 0; i < bombs.Length; i++)
        {
            b = Instantiate(prefab);
            bombs[i] = b.GetComponent<Bomb>();
            b.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public override void Spawn(Transform parent, Vector3 pos)
    {
        foreach(Bomb b in bombs)
        {
            if (!b.gameObject.activeSelf)
            {
                Debug.Log("new bomb @ " + pos);
                gameObject.SetActive(true);
                transform.parent = parent;
                transform.localPosition = pos;
                return;
            }
        }

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
