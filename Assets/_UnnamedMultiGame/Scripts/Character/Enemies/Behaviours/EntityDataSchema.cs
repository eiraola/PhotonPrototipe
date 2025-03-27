
using System;
using UnityEngine;

public class EntityDataSchema
{
    public Transform transform;
    public Transform target;
    public float speed = 0.0f;
    public bool isAtacking = false;
    public Action lightAttackAction = default;
}
