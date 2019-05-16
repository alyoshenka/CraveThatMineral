using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WallObject : MonoBehaviour
{
    public float chance;

    public WallObject[] objects;

    public abstract void Init();

    public abstract void Recycle();

    public abstract void ApplyToPlayer();

    public abstract WallObject Spawn(Transform parent, Vector3 pos);
}

