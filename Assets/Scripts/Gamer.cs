using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gamer
{
    public string name;
    public Ball ball;

    public Gamer(string name, Ball ball)
    {
        this.name = name;
        this.ball = ball;
    }

    public abstract Vector3 captureDirection();

    public void applyForce() {
        ball.applyForce(captureDirection());
    }
}

public class OnScreenJoyStickPlayer : Gamer
{

    public Joystick joystick;

    public OnScreenJoyStickPlayer(string name, Ball ball, Joystick joystick) : base(name, ball) {
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

    List<Gamer> allPlayers;
    private System.Random _random = new System.Random();

    public AttractorPlayer(string name, Ball ball, List<Gamer> allPlayers) : base(name, ball) {
        this.allPlayers = allPlayers;
    }

    override public Vector3 captureDirection()
    {
        var probability = _random.NextDouble();

        if (probability > 0.8)
        {
            return new Vector3(_random.Next(0, 5), _random.Next(0, 5), _random.Next(0, 5)).normalized;
        }

        Vector3 direction = AttractorPlayer.GetMinDistanceDirection(ball, allPlayers);
        direction -= ball.GetVelocity();

        return direction.normalized;

    }

    public static Vector3 GetMinDistanceDirection(Ball ball, List<Gamer> allPlayers)
    {
        var distances = new List<float>();

        for (var i = 0; i < allPlayers.Count; i++)
        {
            distances.Add(Vector3.Distance(allPlayers[i].ball.GetPosition(), ball.GetPosition()));
        }

        int argMin2 = AttractorPlayer.GetNonZeroMin(distances);
        Vector3 direction = (allPlayers[argMin2].ball.GetPosition() - ball.GetPosition());

        return direction;
    }

    static int GetNonZeroMin(List<float> distances)
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

    public ArrowGamer(string name, Ball ball) : base(name, ball) { }

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

    public WsadGamer(string name, Ball ball) : base(name, ball) { }

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
