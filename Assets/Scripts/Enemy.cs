using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 startingPosition;
    void Start()
    {
        
    }

 
    void ResetPosition ()
    {
        transform.position = startingPosition;
    }
}
