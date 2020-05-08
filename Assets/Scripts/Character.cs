using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float groundMovementSpeed;
    public float airMovementSpeed;
    public float jumpHeight;
    public float kyoteTime = 0.25f;
    public float jumpBufferTime = 0.25f;
    [Range(0, 1.0f)]
    public float cutJumpHeight = 0.5f;

    public LayerMask groundLayer;
    public LayerMask platformLayer;

    private JumperAgent jumperAgent;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

    private float kyoteTimer;
    private float jumpBufferTimer;
    private bool isGrounded;
    private float horizontal;
    private bool isHoldingJumpKey;
    private bool canDoubleJump;
    private int airJumps = 0;



    // Get components and initialise stats from design master here
    void Start()
    {
        jumperAgent = GetComponent<JumperAgent>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    

    private void FixedUpdate()
    {
        // Sprite & animation Update starts here
        //animator.SetFloat("Speed", Mathf.Abs(rigidbody.velocity.x));

        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }

        // ends here


        // Grounded & jump logic update starts here

        isGrounded = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 0.5f), 0.25f, groundLayer) || Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 0.5f), 0.25f, platformLayer);

        kyoteTimer -= Time.fixedDeltaTime;
        jumpBufferTimer -= Time.fixedDeltaTime;

        if (isGrounded)
        {
            kyoteTimer = kyoteTime;
            airJumps = 0;
        }

        BetterJump();

        if (kyoteTimer > 0 && jumpBufferTimer > 0)
        {
            Jump();
        }

        // ends here

        // movement update starts here

        rigidbody.velocity = new Vector2(horizontal * ((isGrounded) ? groundMovementSpeed : airMovementSpeed) * Time.fixedDeltaTime, rigidbody.velocity.y);


        //ends here
    }

    public void Move(float value)
    {
        horizontal = value;
    }

    public void StartJump()
    {
        isHoldingJumpKey = true;
        jumpBufferTimer = jumpBufferTime;

        if (isGrounded == false)
        {
            if (canDoubleJump && airJumps < 1)
            {
                airJumps++;
                Jump();
            }
        }
    }

    public void EndJump()
    {
        isHoldingJumpKey = false;
    }


    void Jump()
    {
        kyoteTimer = 0;
        jumpBufferTimer = 0;
        float jumpVelocity = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rigidbody.gravityScale));
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpVelocity);
    }

    void BetterJump()
    {
        if (isHoldingJumpKey == false)
        {
            if (rigidbody.velocity.y > 0)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y * cutJumpHeight);
            }
        }
    }


    public void CanDoubleJump(bool value)
    {
        canDoubleJump = value;
    }

    public void Died ()
    {
        jumperAgent.Died();
    }
}
