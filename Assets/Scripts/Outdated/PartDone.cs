using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartDone : MonoBehaviour {

    public GameObject End;
    public bool isPressed = false;
    public bool finished = false;
    private Vector3 startPos;
    private Vector3 pressedPosition;

    void Start()
    {
        startPos = transform.position;
        pressedPosition = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);

    }

    void OnCollisionEnter(Collision other)
    {

        switch (other.gameObject.tag)
        {
            case "Basic":
                break;
            case "Player":
                if (!isPressed)
                {
                    if (!finished)
                    {
                        finished = true;
                        End.GetComponent<EndingJumpAndRun>().PartDone();
                    }
                    isPressed = true;
                }
                break;
        }
    }

    void OnCollisionExit(Collision other)
    {

        switch (other.gameObject.tag)
        {
            case "Basic":
                break;
            case "Player":
                if (isPressed)
                {
                    isPressed = false;
                }
                break;
        }
    }

    void FixedUpdate()
    {
        if (isPressed)
        {
            transform.position = Vector3.Lerp(transform.position, pressedPosition, Time.deltaTime);
        }
        if (!isPressed)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, 0.2f);
        }
    }
}
