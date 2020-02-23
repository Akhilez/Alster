using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball
{

    
    public float maxVelocity = 100;

    private GameObject _gameObject;

    private List<Vector3> _forces = new List<Vector3>();

    private Rigidbody _rigidBody;

    public Ball(GameObject ballObject)
    {
        _gameObject = ballObject;

        _rigidBody = _gameObject.GetComponent<Rigidbody>();

    }

    public void applyForce(Vector3 force)
    {
        _forces.Add(force);
    }

    public void applyForces(List<Vector3> forces)
    {
        foreach (var force in forces)
        {
            applyForce(force);
        }
    }

    public void Update()
    {

        Vector3 totalForce = new Vector3(0, 0, 0);
        _forces.ForEach(force => totalForce += force);

        _rigidBody.AddForce(totalForce);

        _forces.Clear();

    }
}
