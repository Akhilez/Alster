using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment
{
    public List<Vector3> forces;

    static float rollingFriction = 0.9f;
    static Vector3 gravity = new Vector3(0, -9.8f, 0);

    public Environment ()
    {
        forces = new List<Vector3>();
        forces.Add(gravity);
    }

    public void applyForces (List<PlayerBallInterface> pbInterfaces)
    {
        foreach (var pbInterface in pbInterfaces)
        {
            forces.ForEach(force => pbInterface.ball.applyForce(force));
            
            var acceleration = pbInterface.ball.GetAcceleration();
            var mass = pbInterface.ball.GetMass();
            var ballForce = acceleration * mass;

            ballForce += ballForce / rollingFriction;
            pbInterface.ball.applyForce(ballForce);

        }
 
    }

}
