using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class JumperAgent : Agent
{
    Character character;
    Vector3 startingPosition;
    float previousJumpAction = 0;
    private FlagPole flagPole;

    public override void InitializeAgent()
    {
        base.InitializeAgent();
        character = GetComponent<Character>();
        startingPosition = transform.position;
        flagPole = FindObjectOfType<FlagPole>();
    }

    public override void AgentReset()
    {
        transform.position = startingPosition;
    }

    public override void AgentAction(float[] vectorAction)
    {
        float jumpAction = vectorAction[0];
        float horizontal = 0f;

        if (vectorAction[1] == 1f)
        {
            horizontal = -1;
        }
        else if (vectorAction[1] == 2f)
        {
            horizontal = 1;
        }

        if (previousJumpAction == 0 && jumpAction == 1)
        {
            character.StartJump();
        }
        else if (previousJumpAction == 1 && jumpAction == 0)
        {
            character.EndJump();
        }

        character.Move(horizontal);

        previousJumpAction = jumpAction;

        if (maxStep > 0)
        {
            AddReward(-2.0f / maxStep);
        }
    }

    public override void CollectObservations()
    {
        //distance to flag pole
        AddVectorObs(Vector2.Distance(flagPole.transform.position, transform.position));
        // Direction to flagpole
        AddVectorObs(new Vector2(flagPole.transform.position.x - transform.position.x, flagPole.transform.position.y - transform.position.y));
    }

    private void FixedUpdate()
    {
        if (GetStepCount() % 5 == 0)
        {
            RequestDecision();
        }
        else
        {
            RequestAction();
        }
    }
    

    public override float[] Heuristic()
    {
        float jumpAction = 0f;
        float horizontalAction = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            jumpAction = 1f;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalAction = 1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontalAction = 2f;
        }

        return new float[] {jumpAction, horizontalAction };
    }


    public void Died ()
    {
        AddReward(-5f);
        Done();
    }

    public void CompleteLevel()
    {
        AddReward(5f);
        Done();
    }

    public void SquishedEnemy ()
    {
        AddReward(2f);
    }

    public void PickedUpCoin()
    {
        AddReward(1f);
    }
}
