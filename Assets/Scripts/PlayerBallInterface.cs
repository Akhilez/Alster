using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallInterface
{

    public Gamer player;
    public Ball ball;

    public PlayerBallInterface (Gamer player, Ball ball)
    {
        this.player = player;
        this.ball = ball;
    }

    public void applyInputForce()
    {
        Vector3 force = player.captureDirection();
        Debug.Log("Finding input force" + force);
        ball.applyForce(force);
    }

}
