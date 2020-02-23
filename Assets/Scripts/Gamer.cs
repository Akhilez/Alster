using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gamer
{
    public string name;
    public Gamer(string name)
    {
        this.name = name;
    }

    public abstract Vector3 captureDirection();
}

public class OnScreenJoyStickPlayer : Gamer
{

    public OnScreenJoyStickPlayer(string name) : base(name) { }

    override public Vector3 captureDirection()
    {
        var vector = new Vector3(0, 0, 0);

        // TODO: Get vector from UI element.

        return vector;
    }

}

public class AttractorPlayer : Gamer
{

    List<Gamer> allPlayers;

    public AttractorPlayer(string name, List<Gamer> allPlayers) : base(name) { }

    override public Vector3 captureDirection()
    {
        var vector = new Vector3(0, 0, 0);

        // TODO: Get vector to the nearest player position.

        return vector;
    }

}

public class ArrowGamer : Gamer
{

    public ArrowGamer(string name) : base(name) { }

    override public Vector3 captureDirection()
    {
        var vector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            vector.x += -1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            vector.x += 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            vector.z -= 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vector.z += 1;
        }
        return vector;
    }

}

public class WsadGamer : Gamer
{

    public WsadGamer(string name) : base(name) { }

    override public Vector3 captureDirection()
    {
        var vector = new Vector3(0, 0, 0);

        if (Input.GetKey("w"))
        {
            vector.x += -1;
        }
        if (Input.GetKey("s"))
        {
            vector.x += 1;
        }
        if (Input.GetKey("a"))
        {
            vector.z -= 1;
        }
        if (Input.GetKey("d"))
        {
            vector.z += 1;
        }
        return vector;
    }

}

enum DiscreteDirection
{
    up,
    down,
    left,
    right,
}
