using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using MLAgents;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;



    public JumperAgent agent;

    public Text rewardText;

    private void Update()
    {
        rewardText.text = agent.GetCumulativeReward().ToString("F2");
    }

    public void PlayerDied()
    {

    }

    public void PlayerWon()
    {

    }
}
