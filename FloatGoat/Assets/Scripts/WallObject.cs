using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WallObject : MonoBehaviour
{
    public float chance;

    public static GameObject prefab;
    public WallObject[] objects;

    public abstract void Init();

    public abstract void Recycle();

    public abstract void ApplyToPlayer();

    public abstract void Spawn(Transform parent, Vector3 pos);
}

