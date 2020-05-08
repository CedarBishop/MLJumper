using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Character character;
    public float distanceTillCatchup = 4;
    public float lerpSpeed;

    public GameObject leftWall;

    Vector3 startingPosition;
    Camera main;
    private void Start()
    {
        main = GetComponent<Camera>();
        transform.position = new Vector3(character.transform.position.x, transform.position.y, transform.position.z);
        startingPosition = transform.position;
    }

    private void Update()
    {
        if ((character.transform.position.x - transform.position.x) > distanceTillCatchup )
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(character.transform.position.x, transform.position.y, transform.position.z), lerpSpeed * Time.deltaTime);
        }
        leftWall.transform.localPosition = new Vector3(main.aspect * -5.2f, 0, 0);
    }

}
