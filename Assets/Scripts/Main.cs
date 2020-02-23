using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;
using System;

public class Main : MonoBehaviour
{

    public ARSessionOrigin aRSessionOrigin;
    public GameObject planeSelectorButton;
    public GameObject ballPrefab;
    public GameObject findSurfaceText;
    public GameObject timerText;
    public GameObject scoreText;
    public GameObject opponentScoreText;
    public Joystick joystick;

    private List<Gamer> players = new List<Gamer>();

    private ARPlaneManager _aRPlaneManager;
    private ARPlane _selectedARPlane;
    public static int scoreAi = 0;
    public static int yourScore = 0;

    public float currentTime = 0f;


    void Start()
    {
        _aRPlaneManager = aRSessionOrigin.GetComponent<ARPlaneManager>();
        scoreAi = 0;
        yourScore = 0;
    }

    
    void Update()
    {

        if (_selectedARPlane != null && players.Count == 0)
        {

            GameObject ballObject1 = Instantiate(ballPrefab, _selectedARPlane.transform.position + new Vector3(0.03f, 0.02f, 0.03f), new Quaternion(0, 0, 0, 0)) as GameObject;
            GameObject ballObject2 = Instantiate(ballPrefab, _selectedARPlane.transform.position + new Vector3(-0.03f, 0.02f, -0.03f), new Quaternion(0, 0, 0, 0)) as GameObject;

            var ballObject2Renderer = ballObject2.GetComponent<Renderer>();
            ballObject2Renderer.material.SetColor("_Color", Color.blue);

            var ball2 = new Ball(ballObject1);
            var ball1 = new Ball(ballObject2);

            players.Add(new AttractorPlayer("Player 1", ball1, players));
            players.Add(new OnScreenJoyStickPlayer("Player 2", ball2, joystick));

            currentTime = 60f;
        }

        if (_selectedARPlane != null && players.Count > 0)
        {
            currentTime -= Time.deltaTime;

            updateTimerText();

            if (currentTime <= 0.0f)
            {
                timerText.GetComponent<TextMeshProUGUI>().text = "Times up!";
                FinishMatch();
            }

            if (players[0].ball.GetPosition().y < -1)
            {
                players[0].ball._gameObject.transform.position = _selectedARPlane.transform.position + new Vector3(0.03f, 0.04f, 0.03f);
                players[0].ball._rigidBody.velocity = new Vector3(0,0,0);
                yourScore += 1;
                scoreText.GetComponent<TextMeshProUGUI>().text = "You: " + yourScore;
            }
            if (players[1].ball.GetPosition().y < -1)
            {
                players[1].ball._gameObject.transform.position = _selectedARPlane.transform.position + new Vector3(-0.03f, 0.04f, -0.03f);
                players[1].ball._rigidBody.velocity = new Vector3(0, 0, 0);
                scoreAi += 1;
                opponentScoreText.GetComponent<TextMeshProUGUI>().text = "AI: " + scoreAi;
            }

            for (int i = 0; i < this.players.Count; i++)
            {
                players[i].applyForce();
            }


            for (int i = 0; i < this.players.Count; i++)
            {
                players[i].ball.Update();
            }
        }
        

    }

    private void updateTimerText()
    {
        timerText.GetComponent<TextMeshProUGUI>().text = (Convert.ToInt32(currentTime)).ToString();
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

    private void FinishMatch()
    {
        GameOver.scoreAi = scoreAi;
        GameOver.yourScore = yourScore;
        SceneManager.LoadScene(2);
    }


    public void SelectCenterPlane()
    {

        ARPlane plane = GetPlaneAtCenter();
        if (plane != null)
        {
            _aRPlaneManager.enabled = false;

            plane.transform.rotation = new Quaternion(0f, plane.transform.rotation.y, 0f, 0f);
            _selectedARPlane = plane;

            Destroy(planeSelectorButton);
            Destroy(findSurfaceText);
        }
    }


    public void Quit ()
    {
        if (_selectedARPlane != null)
        {
            FinishMatch();
        } else
        {
            SceneManager.LoadScene(0);
        }
    }
}
