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
        List<DiscreteDirection> actions = new List<DiscreteDirection>();
        if (Input.GetKey(KeyCode.UpArrow))
        {
            actions.Add(DiscreteDirection.up);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            actions.Add(DiscreteDirection.down);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            actions.Add(DiscreteDirection.left);
            Debug.Log("PRESSED LEFT");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            actions.Add(DiscreteDirection.right);
            Debug.Log("PRESSED RIGHT");
        }
        return GetVectorFromDiscreteActions(actions);
    }

    private Vector3 GetVectorFromDiscreteActions(List<DiscreteDirection> actions)
    {
        var vector = new Vector3(0, 0, 0);

        if (actions.Contains(DiscreteDirection.up)) {
            vector.x += 1;
        }
        if (actions.Contains(DiscreteDirection.down))
        {
            vector.x -= 1;
        }
        if (actions.Contains(DiscreteDirection.left))
        {
            vector.z -= 1;
        }
        if (actions.Contains(DiscreteDirection.right))
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

        if (Input.GetKeyDown("w"))
        {
            vector.x += 1;
        }
        if (Input.GetKeyDown("s"))
        {
            vector.x -= 1;
        }
        if (Input.GetKeyDown("a"))
        {
            vector.z -= 1;
        }
        if (Input.GetKeyDown("d"))
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
