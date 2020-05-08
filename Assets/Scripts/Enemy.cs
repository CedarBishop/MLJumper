using MLAgents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movementSpeed;
    public float timeBetweenFlips;
    public float posDifferenceToKill;


    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Vector3 startingPosition;
    private bool facingRight;




    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startingPosition = transform.position;
        facingRight = true;
        movementSpeed = Academy.Instance.FloatProperties.GetPropertyWithDefault("movement_speed", 50.0f);
        StartCoroutine("FlipDirection");
    }

    private void FixedUpdate()
    {
        if (facingRight)
        {
            rigidbody.velocity = (Vector2.right * movementSpeed * Time.fixedDeltaTime);
            spriteRenderer.flipX = false;
        }
        else
        {
            rigidbody.velocity = (Vector2.left * movementSpeed * Time.fixedDeltaTime);
            spriteRenderer.flipX = true;
        }
    }

    public void ResetPosition ()
    {
        transform.position = startingPosition;
        StopAllCoroutines();
        StartCoroutine("FlipDirection");
        movementSpeed = Academy.Instance.FloatProperties.GetPropertyWithDefault("movement_speed", 50.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Character>())
        {
            if ((collision.transform.position.y - transform.position.y) > posDifferenceToKill)
            {
                collision.gameObject.GetComponent<Character>().SquishedEnemy();
                gameObject.SetActive(false);
            }
            else
            {
                GameManager.instance.PlayerDied();
            }
            
        }
    }

    IEnumerator FlipDirection ()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenFlips);
            facingRight = !facingRight;
        }
    }
}
