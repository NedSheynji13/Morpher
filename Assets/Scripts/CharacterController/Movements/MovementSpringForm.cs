using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpringForm : MonoBehaviour
{
    private bool groundAnimation, strongJump;
    private int colCount = 0;
    private Rigidbody physix;
    private Vector3 Physix2Move;    //Used for rewriting the game objects velocities
    private Quaternion lookRot;     //Used for rotating the player according to the mouse input
    private float groundAnimationDuration, groundAnimationCos, speed;

    void Start()
    {
        physix = GetComponent<Rigidbody>();
        groundAnimationDuration = 0;
        Morphing.grounded = false;
        speed = 2;
        strongJump = false;
    }

    private void Update()
    {
        if (Morphing.grounded)
            if (physix.velocity.y == 0)
                groundAnimation = true;

        if (groundAnimation)
        {
            groundAnimationCos = (Mathf.Cos((Mathf.PI * 10 * groundAnimationDuration * speed / 4) + 0.75f) + 1) / 2;
            Morphing.wantedscale.y = groundAnimationCos;
            groundAnimationDuration += Time.deltaTime;
        }

        if (Morphing.grounded)
        {
            Physix2Move.x = Input.GetAxisRaw("Horizontal") * 5;
            Physix2Move.z = Input.GetAxisRaw("Vertical") * 5;
            lookRot = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0); //Reads the rotation arround the Y Axis of the main camera
        }
        else
            physix.AddForce(Vector3.down * 10);

        if (Input.GetKey(KeyCode.Space) && Morphing.grounded)
        {
            strongJump = true;
            speed = 1f;
        }
    }

    void FixedUpdate()
    {
        if (groundAnimation)
        {
            if (groundAnimationCos >= 0.95f && groundAnimationDuration >= 0.5f / speed)
            {
                if (strongJump)
                {
                    physix.velocity = Vector3.up * 20;
                    strongJump = false;
                    speed = 2f;
                }
                else
                    physix.velocity = Vector3.up * 5;
                groundAnimation = false;
                groundAnimationDuration = 0;
                Morphing.wantedscale = Vector3.one;
            }
        }

        if (!Morphing.morphing && !Morphing.grounded)
            physix.velocity = lookRot * new Vector3(Physix2Move.x, physix.velocity.y, Physix2Move.z);
        else if (Morphing.morphing)
            physix.velocity = new Vector3(0, physix.velocity.y, 0);

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, 10 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision col)
    {
        colCount++;
        for (int i = 0; i < col.contacts.Length; i++)
        {
            if (transform.position.y - col.contacts[i].point.y <= 0.74f)
                Morphing.grounded = true;
            else
            {
                Physix2Move.x = -Physix2Move.x;
                Physix2Move.z = -Physix2Move.z;
                break;
            }
        }
    }

    private void OnCollisionExit(Collision col)
    {
        colCount--;
        if (colCount < 1)
            Morphing.grounded = false;
    }
}
