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

    public Joystick joystick;

    public OnScreenJoyStickPlayer(string name, Joystick joystick) : base(name) {
        this.joystick = joystick;
    }

    override public Vector3 captureDirection()
    {

        Vector3 vector = joystick.Direction.normalized;
        var x = -vector.y;
        var z = vector.x;
        vector = new Vector3(x, 0, z);

        return vector;
    }

}

public class AttractorPlayer : Gamer
{

    List<PlayerBallInterface> allPlayers;
    Ball ball;

    public AttractorPlayer(string name, List<PlayerBallInterface> allPBInterfaces, Ball ball) : base(name) {
        this.allPlayers = allPBInterfaces;
        this.ball = ball;
    }

    override public Vector3 captureDirection()
    {

        // TODO: Get vector to the nearest player position.
        var distances = new List<float>();

        for (var i = 0; i < allPlayers.Count; i++)
        {
            distances.Add(Vector3.Distance(allPlayers[i].ball.GetPosition(), ball.GetPosition()));
        }

        int argMin2 = GetNonZeroMin(distances);
        Vector3 direction = (allPlayers[argMin2].ball.GetPosition() - ball.GetPosition()).normalized;

        return direction;
    }

    int GetNonZeroMin(List<float> distances)
    {
        float nonZeroMin = 99999999999999f;
        int nonZeroMinIndex = -1;
        for (int i=0; i< distances.Count; i++)
        {
            if (distances[i] != 0f && distances[i] < nonZeroMin)
            {
                nonZeroMin = distances[i];
                nonZeroMinIndex = i;
            }
        }
        return nonZeroMinIndex;
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
