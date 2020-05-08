using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>())
        {
            GameManager.instance.PlayerWon();
        }
    }
}
