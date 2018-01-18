using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCubeForm : MonoBehaviour
{
    #region Variables used for user inputs and adjusting them accordingly to the camera rotation
    private Vector3 rotatedInput;   //Used to adapt the camera rotation to the user input
    private Quaternion lookRot;     //Used to constantly monitor the rotation of the camera arround the Y Axis
    //Used for reading the user inputs from the manager and convert them into rotation accurate transformations
    private float directionX, directionZ;
    #endregion

    #region Variables used for the cube rotation and translation calculations
    private Quaternion fromRotation, toRotation;//Used to lerp the rotation over time
    private bool IsRotating;                    //Boolean used to block further tilting and rotating of the cube
    private Vector3 startPos;                   //Used to save the current position of the cube in order to calculate the position after the moving
    //Used how long and how far one rotation takes 
    private float rotationPeriod, rotationTime, rotationRadius;
    private Rigidbody faller;
    #endregion

    #region Variables used to check the surroundings
    private Ray[] checkRays;        //Used to save the checking raycasts
    private Vector3[] checkOffset;  //Used to save the offsets between raycasts
    private int colCount;
    #endregion

    void Start()
    {
        IsRotating = false;   //Setting the rotation to false by default since the cube should not move by default
        directionX = directionZ = rotationTime = 0; //Setting the User Inputs and the time one rotation takes to 0 by default
        rotationPeriod = 0.3f;                      //Setting the period of one rotation to a fixed value used for rotation duration calculations
        rotationRadius = Mathf.Sqrt(2f) / 2f;       //Rotationradius of a cube = sidelength * Sqr/2 /2  for further information check hypotenuse
        checkRays = new Ray[4];                     //Instantiates the checkrays in later use for checking surroundings
        checkOffset = new Vector3[2];               //Instantiates other checking stuff
        checkOffset[0] = new Vector3(0, 0, 0.2f);   //Offset in Z direction to fire 2 raycasts which are shifted by this value
        checkOffset[1] = new Vector3(0.2f, 0, 0);   //Offset in X direction to fire 2 raycasts which are shifted by this value
        faller = GetComponent<Rigidbody>(); //Reading the cubes rigidbody to strengthen his falling speeds
    }

    void Update()
    {

        if ((rotatedInput.x != 0 || rotatedInput.z != 0) && !IsRotating && !Morphing.morphing && Morphing.grounded)    //Gets triggered as soon as the user throws an input but the cube isnt rotating yet
        {
            if (Wallcheck()) //Checks first if there are any walls nearby to prevent bugging into them
                return;
            else
            {
                //Communicates the rotated input to the actual cube rotation
                directionX = rotatedInput.x;
                directionZ = rotatedInput.z;
                startPos = transform.position;      //Buffers the current position to define from where the movement should start

                //Check the current and the next rotation an buffers both to lerp between them
                fromRotation = transform.rotation;  //Buffers the current rotation
                transform.Rotate(directionZ * 90, 0, directionX * 90, Space.World); //Rotates the cube internally to the given direction
                toRotation = transform.rotation;    //Buffers the wanted rotation

                transform.rotation = fromRotation;  //Sets the rotation of the cube back to normal ultimately
                rotationTime = 0;                   //Sets the rotation time back to zero
                IsRotating = true;                  //Initiates the move
            }
        }
        lookRot = Quaternion.Euler(0, -Mathf.Round(Camera.main.transform.eulerAngles.y / 90) * 90, 0); //Reads the rotation around the Y Axis of the camera
    }

    void FixedUpdate()
    {
        InputMovementCube();    //Monitors the user inputs constantly
        if (IsRotating && !Morphing.morphing)   //If the user wishes to move
        {
            rotationTime += Time.fixedDeltaTime;//Adds the time one rotation should take by framerate
            float ratio = Mathf.Lerp(0, 1, rotationTime / rotationPeriod);  //Simulates the cube rotation animation via lerping

            float thetaRad = Mathf.Lerp(0, Mathf.PI / 2f, ratio); //Increments over time as the cube keeps moving
            //Calculates the exact distances the cube crosses with given user inputs over the distance he already crossed
            float distanceX = -directionX * rotationRadius * (Mathf.Cos(45f * Mathf.Deg2Rad) - Mathf.Cos(45f * Mathf.Deg2Rad + thetaRad));
            float distanceY = rotationRadius * (Mathf.Sin(45f * Mathf.Deg2Rad + thetaRad) - Mathf.Sin(45f * Mathf.Deg2Rad));
            float distanceZ = directionZ * rotationRadius * (Mathf.Cos(45f * Mathf.Deg2Rad) - Mathf.Cos(45f * Mathf.Deg2Rad + thetaRad));

            transform.position = new Vector3(startPos.x + distanceX, startPos.y + distanceY, startPos.z + distanceZ);  //Sets the crossed distance ultimately as new position

            transform.rotation = Quaternion.Lerp(fromRotation, toRotation, ratio);    //Lerps a slow turn of the cube in given direction

            if (ratio == 1) //if the rotation animation is done
            {
                if (!Physics.Raycast(transform.position, Vector3.down, 0.75f))   //Lets the cube free fall if he isnt grounded after a rotation
                {
                    GetComponent<Rigidbody>().freezeRotation = Morphing.grounded = false;
                }
                else
                {
                    GetComponent<Rigidbody>().freezeRotation = Morphing.grounded = true;
                    transform.rotation = Quaternion.Euler(Mathf.Round(transform.rotation.eulerAngles.x / 90) * 90, Mathf.Round(transform.rotation.eulerAngles.y / 90) * 90, Mathf.Round(transform.rotation.eulerAngles.z / 90) * 90);
                }
                IsRotating = false;
                directionX = directionZ = rotationTime = 0;
            }
        }
        if (faller.velocity.y < 0)
        {
            faller.velocity = new Vector3(faller.velocity.x, faller.velocity.y + Physics.gravity.y * 10 * Time.deltaTime, faller.velocity.z);
        }
    }

    /// <summary>
    /// Reads the input given by the user, clamps it automatically to 0 or 1 and rotates it according to the camera
    /// </summary>
    void InputMovementCube()
    {
        rotatedInput.x = -Input.GetAxisRaw("Horizontal");   //Inverts the move alongside the X Axis to avoid inverted controls
        rotatedInput.z = Input.GetAxisRaw("Vertical");      //Reading the forward and backward movement
        rotatedInput = lookRot * rotatedInput;              //Rotates the input by the camera
                                                            //Rounds the inputs to only get either 1 or 0 as input, since unity can for some fking reason throw 0,0000000whateverthefuck as zero
        rotatedInput.x = Mathf.Round(rotatedInput.x);
        rotatedInput.y = Mathf.Round(rotatedInput.y);
        rotatedInput.z = Mathf.Round(rotatedInput.z);
    }

    /// <summary>
    /// Throws raycasts into the direction the user wishes to move
    /// </summary>
    /// <returns>If any wall gets hit the cube movement is blocked</returns>
    public bool Wallcheck()
    {
        //Series of raycasts which will be spit out to check if there are any obstacles in the direction the user wishes to move
        checkRays[0] = new Ray(transform.position + checkOffset[0], new Vector3(-rotatedInput.x, 0, 0));
        checkRays[1] = new Ray(transform.position - checkOffset[0], new Vector3(-rotatedInput.x, 0, 0));
        checkRays[2] = new Ray(transform.position + checkOffset[1], new Vector3(0, 0, rotatedInput.z));
        checkRays[3] = new Ray(transform.position - checkOffset[1], new Vector3(0, 0, rotatedInput.z));

        //If any user input is detected the method checks if any raycast fires true
        if (rotatedInput.x != 0 || rotatedInput.z != 0)
            return
                Physics.Raycast(checkRays[0], 1) ||
                Physics.Raycast(checkRays[1], 1) ||
                Physics.Raycast(checkRays[2], 1) ||
                Physics.Raycast(checkRays[3], 1);
        else
            return true;
    }
    private void OnCollisionStay(Collision collision)
    {
        colCount++;
        if (colCount > 0)
            Morphing.grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        colCount--;
        if (colCount == 0)
            Morphing.grounded = false;
    }

    #region Might be useful in the future

    /// <summary>
    /// Returns a given point rotated arround an pivot point
    /// </summary>
    /// <param name="point">Point to rotate</param>
    /// <param name="pivot">Pivot to rotate around</param>
    /// <param name="rotation">rotation value arround the pivot</param>
    /// <returns></returns>
    static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        Vector3 direction = point - pivot;
        return pivot + rotation * direction;
    }

    //      Wallcheck Method using a for()... It doesnt work for some wierd reason
    //checkDirection[0] = new Vector3(-rotatedInput.x, 0, 0);
    //checkDirection[1] = new Vector3(0, 0, rotatedInput.z);
    //    //Series of raycasts which will be spit out to check if there are any obstacles in the direction the user wishes to move
    //    for (int i = 0; i<checkRays.Length; i++)
    //    {
    //        int j = Mathf.FloorToInt(i / 2);
    //        if (i % 2 == 0)
    //            checkRays[i] = new Ray(transform.position + checkOffset[j], checkDirection[j]);
    //        else
    //            checkRays[i] = new Ray(transform.position - checkOffset[j], checkDirection[j]);
    #endregion
}