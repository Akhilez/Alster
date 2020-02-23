using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball
{

    
    public float maxVelocity = 100;

    private GameObject _gameObject;

    private Vector3 _location;
    private Vector3 _velocity;
    private Vector3 _acceleration;
    private List<Vector3> _forces = new List<Vector3>();

    private Rigidbody _rigidBody;

    public Ball(GameObject ballObject)
    {
        _gameObject = ballObject;

        _location = _gameObject.transform.position;
        _velocity = new Vector3(0, 0, 0);
        _acceleration = new Vector3(0, 0, 0);

        _rigidBody = _gameObject.GetComponent<Rigidbody>();

    }

    public void applyForce(Vector3 force)
    {
        var acceleration = force / _rigidBody.mass;
        this._acceleration += acceleration;

        _forces.Add(force);
    }

    public void applyForces(List<Vector3> forces)
    {
        foreach (var force in forces)
        {
            applyForce(force);
        }
    }

    public Vector3 GetAcceleration()
    {
        return _acceleration;
    }

    public float GetMass ()
    {
        return _rigidBody.mass;
    }

    private void _limitVelocity()
    {
        if (_velocity.x > maxVelocity)
            _velocity.x = maxVelocity;
        if (_velocity.y > maxVelocity)
            _velocity.y = maxVelocity;
        if (_velocity.z > maxVelocity)
            _velocity.z = maxVelocity;
    }

    public void Update()
    {
        _velocity += _acceleration;
        _limitVelocity();
        _location += _velocity * 0.0001f;
        _acceleration *= 0;

        _gameObject.transform.position = _location;

        Vector3 totalForce = new Vector3(0, 0, 0);
        _forces.ForEach(force => totalForce += force);

        // _rigidBody.AddForce(totalForce);

        _forces.Clear();

    }
}
