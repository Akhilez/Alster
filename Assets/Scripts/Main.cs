using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    public List<GameObject> balls;
    public Joystick joystick;

    public List<Gamer> players = new List<Gamer>();
    public Environment environment;

    void Start()
    {
        environment = new Environment();

        var ball2 = new Ball(balls[0]);
        var ball1 = new Ball(balls[1]);

        players.Add(new AttractorPlayer("Player 1", ball1, players));
        players.Add(new OnScreenJoyStickPlayer("Player 2", ball2, joystick));

    }

    
    void Update()
    {

        for (int i = 0; i < this.players.Count; i++)
        {
            players[i].applyForce();
        }

        environment.applyForces(players);

        for (int i = 0; i < this.players.Count; i ++)
        {
            players[i].ball.Update();
        }

        

    }
}
