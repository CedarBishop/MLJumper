using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 startingPosition;
    void Start()
    {
        startingPosition = transform.position;
    }



    void ResetPosition ()
    {
        transform.position = startingPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>())
        {
            if ((collision.transform.position.y - transform.position.y) > 1)
            {
                collision.GetComponent<Character>();
            }
            GameManager.instance.PlayerDied();
        }
    }
}
