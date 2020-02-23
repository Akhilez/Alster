using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public GameObject yourScoreText;
    public GameObject aiScoreText;

    public static int scoreAi = 0;
    public static int yourScore = 0;


    void Start()
    {
        yourScoreText.GetComponent<TextMeshProUGUI>().text = "Your Score: " + yourScore;
        aiScoreText.GetComponent<TextMeshProUGUI>().text = "AI Score: " + scoreAi;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    
}
