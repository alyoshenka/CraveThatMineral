﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WallObject : MonoBehaviour
{
    [Tooltip("Inverse chance that a wall will have an enemy")]
    public int chance;
    [Tooltip("Max number of enemies on a wall ( <= 0 not eve wall has one")]
    public int maxObjects;

    public bool recycle;

    public abstract void Init();

    public abstract void Recycle();

    public abstract void ApplyToPlayer(PlayerController player);

    public abstract WallObject Spawn(Transform parent, Vector3 pos);
}

