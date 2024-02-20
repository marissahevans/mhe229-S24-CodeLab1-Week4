using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public TextMeshProUGUI display;
    public int score;

    const string FILE_DIR = "/DATA/";
    const string DATA_FILE = "highScores.csv";
    private string FILE_FULL_PATH;
        

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            
        }
    }

    private string highScoresString = "";
    private List<int> highScores;
    public List<int> HighScores
    {
        get
        {
            if (highScores == null)
            {
                highScores = new List<int>();
                highScoresString = File.ReadAllText(FILE_FULL_PATH);

                highScoresString = highScoresString.Trim();
                
                string[] highScoreArray = highScoresString.Split("\n");

                for (int i = 0; i < highScoreArray.Length; i++)
                {
                    int currentScore = Int32.Parse(highScoreArray[i]);
                    highScores.Add(currentScore);
                }
            }

            return highScores;
        }
    }
    
    private float timer = 0;
    public int maxTime = 10;

    private int finalScore = 0;

    private bool isInGame = true;
    

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        FILE_FULL_PATH = Application.dataPath + FILE_DIR + DATA_FILE;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInGame)
        {
            display.text = "Score: " + Score + "\nTime: " + (maxTime - (int)timer);
        }
        else
        {
            display.text = "GAME OVER" + "\nFINAL SCORE " + finalScore +
                           "\nHigh Scores:\n" + highScoresString;
        }

        // add the fraction of a second between frames to timer
        timer += Time.deltaTime;

        if (timer >= maxTime && isInGame == true)
        {
            isInGame = false;
            finalScore = score;
            if (isHighScore(finalScore))
            {
                int highScoreSlot = -1;
                for (int i = 0; i < HighScores.Count; i++)
                {
                    if (score > highScores[i])
                    {
                        highScoreSlot = i;
                        break;
                    }
                }

                highScores.Insert(highScoreSlot, score);

                highScores = highScores.GetRange(0, 5);

                string scoreBoardText = "";
                foreach (var highScore in highScores)
                {
                    scoreBoardText += highScore + "\n";
                }

                highScoresString = scoreBoardText;
                File.WriteAllText(FILE_FULL_PATH, highScoresString);
            }
            SceneManager.LoadScene("EndScene");
            //display.enabled = false;
        }
    }

    bool isHighScore(int score)
    {
        for (int i = 0; i < HighScores.Count; i++)
        {
            if (highScores[i] < score)
            {
                return true;
            }
        }

        return false;
    }
    
}
