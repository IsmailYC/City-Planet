using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public enum GameStates { Start, Play, Pause, End};
    public GameStates gameState;
    public int[] order;
    public int total;

    //UI components
    public GameObject startCanvas;
    public GameObject mainCanvas;
    public GameObject lastCanvas;
    public Text lastText;

    int count;
    List<float> monumentsAngle;
	// Use this for initialization
	void Start () {
        gameState = GameStates.Start;
        Time.timeScale = 0.0f;
        if (instance == null)
            instance = this;
        monumentsAngle = new List<float>();
        count = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddAngle(float angle)
    {
        monumentsAngle.Add(angle);
        count++;
        if(count==total)
        {
            if (CheckOrder())
                YouWin();
            else
               YouLose();
        }
    }

    bool CheckOrder()
   {
        for (int i = 0; i < order.Length - 1; i++)
        {
            float tempDifference = monumentsAngle[order[i]] - monumentsAngle[order[i + 1]];
            Debug.Log(tempDifference);
            bool loseCondition = (tempDifference < 180 && tempDifference > 0) || tempDifference < -180;
            if (loseCondition)
                return false;
        }
        return true;
   }

    public void StartCity()
    {
        gameState = GameStates.Play;
        Time.timeScale = 1.0f;
        startCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public void PauseLevel()
    {
        if (gameState == GameStates.Play)
        {
            gameState = GameStates.Pause;
            Time.timeScale = 0.0f;
        }
        else
        {
            gameState = GameStates.Play;
            Time.timeScale = 1.0f;
        }
    }

    public void YouWin()
    {
        gameState = GameStates.End;
        mainCanvas.SetActive(false);
        lastText.text = "Well Done";
        lastCanvas.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void YouLose()
    {
        gameState = GameStates.End;
        mainCanvas.SetActive(false);
        lastText.text = "Try Again";
        lastCanvas.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}
