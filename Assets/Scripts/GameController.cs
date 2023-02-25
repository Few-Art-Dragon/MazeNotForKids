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
    private TMP_Text scoreLabel;
    [SerializeField]
    private int _lengthMaze;
    [SerializeField]
    private int _widthMaze;

    private MazeConstructor generator;

    private DateTime startTime;
    
    private int reduceLimitBy;

    private int score;
    private bool goalReached;

    void Start()
    {
        _FinishText.enabled = false;
        generator = GetComponent<MazeConstructor>();
        StartNewGame();
    }

    private void StartNewGame()
    {
        timeLimit = 200;
        reduceLimitBy = 5;
        startTime = DateTime.Now;

        score = 0;
        scoreLabel.text = score.ToString();

        StartNewMaze();
    }

    private void StartNewMaze()
    {
        generator.GenerateNewMaze(_lengthMaze, _widthMaze, OnStartTrigger, OnGoalTrigger);

        float x = generator.startCol * generator.hallWidth;
        float y = 1f;
        float z = generator.startRow * generator.hallWidth;
        player.transform.position = new Vector3(x, y, z);

        goalReached = false;
        player.SetActive(true);

        // restart timer
        timeLimit -= reduceLimitBy;
        startTime = DateTime.Now;
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

    private void OnGoalTrigger(GameObject trigger, GameObject other)
    {
        Debug.Log("Goal!");
        goalReached = true;

        score += 1;
        scoreLabel.text = score.ToString();

        Destroy(trigger);
    }

    private void OnStartTrigger(GameObject trigger, GameObject other)
    {
        if (goalReached)
        {
            _FinishText.enabled = true;
            Debug.Log("Finish!");
            player.SetActive(false);

            Invoke("StartNewMaze", 10);
        }
    }


}
