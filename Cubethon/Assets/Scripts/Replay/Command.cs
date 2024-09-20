using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public Rigidbody _p;
    public float timestamp; // for logging purposes
    public abstract void Execute();
}

class MoveRight : Command
{
    private float _force;

    public MoveRight(Rigidbody player, float force)
    {
        _p = player;
        _force = force;
    }

    public override void Execute()
    {
        timestamp = Time.timeSinceLevelLoad;
        _p.AddForce(_force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }
}

class MoveLeft : Command
{
    private float _force;

    public MoveLeft(Rigidbody player, float force)
    {
        _p = player;
        _force = force;
    }

    public override void Execute()
    {
        timestamp = Time.timeSinceLevelLoad;
        _p.AddForce(-_force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }
}