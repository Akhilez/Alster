using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Main : MonoBehaviour
{

    public ARSessionOrigin aRSessionOrigin;
    public GameObject planeSelectorButton;
    public GameObject ballPrefab;

    public Joystick joystick;

    private List<Gamer> players = new List<Gamer>();
    private Environment environment;

    private ARPlaneManager _aRPlaneManager;
    private ARPlane _selectedARPlane;


    void Start()
    {
        environment = new Environment();
        _aRPlaneManager = aRSessionOrigin.GetComponent<ARPlaneManager>();
        Debug.Log("ARPlane Manager is set to: " + _aRPlaneManager);
        Debug.Log("Res = " + ballPrefab);
    }

    
    void Update()
    {

        if (_selectedARPlane != null && players.Count == 0)
        {
            Debug.Log("About to Add some ballsss:");

            GameObject ballObject1 = Instantiate(ballPrefab, _selectedARPlane.transform.position + new Vector3(0.03f, 0.02f, 0.03f), Quaternion.identity) as GameObject;
            GameObject ballObject2 = Instantiate(ballPrefab, _selectedARPlane.transform.position + new Vector3(-0.03f, 0.02f, -0.03f), Quaternion.identity) as GameObject;

            var ball2 = new Ball(ballObject1);
            var ball1 = new Ball(ballObject2);

            Debug.Log("Added some ballsss:" + ball1);

            players.Add(new AttractorPlayer("Player 1", ball1, players));
            players.Add(new OnScreenJoyStickPlayer("Player 2", ball2, joystick));
        }

        if (_selectedARPlane != null && players.Count > 0)
        {
            if (players[0].ball.GetPosition().y < -1)
            {
                players[0].ball._gameObject.transform.position = _selectedARPlane.transform.position + new Vector3(0.03f, 0.04f, 0.03f);
            }
            if (players[1].ball.GetPosition().y < -1)
            {
                players[1].ball._gameObject.transform.position = _selectedARPlane.transform.position + new Vector3(-0.03f, 0.04f, -0.03f);
            }

            for (int i = 0; i < this.players.Count; i++)
            {
                players[i].applyForce();
            }

            environment.applyForces(players);

            for (int i = 0; i < this.players.Count; i++)
            {
                players[i].ball.Update();
            }
        }
        

    }

    private ARPlane GetPlaneAtCenter()
    {
        Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Transform selectionTransform = hit.transform;
            ARPlane plane = selectionTransform.GetComponent<ARPlane>();
            return plane;
        }
        return null;
    }


    public void SelectCenterPlane()
    {

        ARPlane plane = GetPlaneAtCenter();
        if (plane != null)
        {
            Debug.Log("Stopping detection of " + _aRPlaneManager);
            _aRPlaneManager.enabled = false;

            Debug.Log("plane after disabling = " + plane);
            _selectedARPlane = plane;
            Destroy(planeSelectorButton);
        }
    }
}
