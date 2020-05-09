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

    private Character character;
    private Enemy[] enemies;
    private Coin[] coins;
    private CameraController camera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }



    private void Start()
    {
        enemies = FindObjectsOfType<Enemy>();
        coins = FindObjectsOfType<Coin>();
        character = agent.GetComponent<Character>();
        camera = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        rewardText.text = agent.GetCumulativeReward().ToString("F2");
    }

    public void PlayerDied()
    {
        character.Died();
        GeneralReset();
    }

    public void PlayerWon()
    {
        character.PlayerWon();
        GeneralReset();
    }

    public void GeneralReset ()
    {
        ResetEnemies();
        ResetCoins();
        camera.ResetPosition();
    }

    private void ResetEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].gameObject.SetActive(true);
            enemies[i].ResetPosition();
        }
    }

    void ResetCoins()
    {
        for (int i = 0; i < coins.Length; i++)
        {
            coins[i].gameObject.SetActive(true);
        }
    }
}
