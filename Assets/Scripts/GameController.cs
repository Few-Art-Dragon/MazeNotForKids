using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(MazeConstructor))]
public class GameController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _FinishText;
    [SerializeField]
    private int timeLimit;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private TMP_Text timeLabel;
    [SerializeField]
    private int _lengthMaze;
    [SerializeField]
    private int _widthMaze;

    private MazeConstructor generator;

    private DateTime startTime;
    
    private int reduceLimitBy;

    void Start()
    {
        _FinishText.enabled = false;
        generator = GetComponent<MazeConstructor>();
        StartNewGame();
    }

    void Update()
    {
        if (!player.activeSelf)
        {
            return;
        }

        int timeUsed = (int)(DateTime.Now - startTime).TotalSeconds;
        int timeLeft = timeLimit - timeUsed;

        if (timeLeft > 0)
        {
            timeLabel.text = timeLeft.ToString();
        }
        else
        {
            timeLabel.text = "TIME UP";
            player.SetActive(false);

            Invoke("StartNewGame", 4);
        }
    }

    private void StartNewGame()
    {
        timeLimit = 0;
        reduceLimitBy = 5;
        startTime = DateTime.Now;

        StartNewMaze();
    }

    private void StartNewMaze()
    {
        UpDifficult();
        _FinishText.enabled = false;
        generator.GenerateNewMaze(_lengthMaze, _widthMaze, OnGoalTrigger);

        float x = generator.startCol * generator.hallWidth;
        float y = 1f;
        float z = generator.startRow * generator.hallWidth;
        player.transform.position = new Vector3(x, y, z);

        player.SetActive(true);

        // restart timer
        timeLimit -= reduceLimitBy;
        startTime = DateTime.Now;
    }

    private void UpDifficult()
    {
        _lengthMaze += 2;
        _widthMaze += 2;
        timeLimit += 30;
    }



    private void OnGoalTrigger(GameObject trigger, GameObject other)
    {
        _FinishText.enabled = true;
        Debug.Log("Finish!");
        player.SetActive(false);
        
        Invoke("StartNewMaze", 6);      
    }
}
