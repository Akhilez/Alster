using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    public List<GameObject> balls;
    public List<PlayerBallInterface> pbInterfaces = new List<PlayerBallInterface>();
    public Environment environment;

    void Start()
    {
        environment = new Environment();

        pbInterfaces.Add(new PlayerBallInterface(new ArrowGamer("Player 1"), new Ball(balls[0])));
        pbInterfaces.Add(new PlayerBallInterface(new WsadGamer("Player 2"), new Ball(balls[1])));

    }

    
    void Update()
    {

        for (int i = 0; i < this.pbInterfaces.Count; i++)
        {
            pbInterfaces[i].applyInputForce();
        }

        environment.applyForces(pbInterfaces);

        for (int i = 0; i < this.pbInterfaces.Count; i ++)
        {
            pbInterfaces[i].ball.Update();
        }

        

    }
}
