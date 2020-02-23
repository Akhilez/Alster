using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment
{
    public List<Vector3> forces;

    static Vector3 gravity = new Vector3(0, -9.8f, 0);

    public Environment ()
    {
        forces = new List<Vector3>();
        forces.Add(gravity);
    }

    public void applyForces (List<Gamer> players)
    {
        foreach (var player in players)
        {
            forces.ForEach(force => player.ball.applyForce(force));
        }
 
    }

}
