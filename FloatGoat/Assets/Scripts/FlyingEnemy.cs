using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : WallObject
{
    public static GameObject prefab;

    [Tooltip("Amount of damage done")]
    public float damage;

    Vector3 dir;
    float speed;

    public static FlyingEnemy[] flyers;

    public override void Init()
    {
        GameObject b;
        prefab = gameObject;
        for (int i = 0; i < flyers.Length; i++)
        {
            b = Instantiate(prefab);
            flyers[i] = b.GetComponent<FlyingEnemy>();
            b.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);

        //ADD BACK AND FORTH MOVEMENT
    }

    public override WallObject Spawn(Transform parent, Vector3 pos)
    {
        foreach (FlyingEnemy b in flyers)
        {
            if (!b.gameObject.activeSelf)
            {
                Debug.Log("new flyer @ " + pos);
                gameObject.SetActive(true);
                transform.parent = parent;
                transform.localPosition = pos;
                return b;
            }
        }
        Debug.LogError("Alexi warning");
        return null;
    }

    public override void ApplyToPlayer(PlayerController pc)
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
