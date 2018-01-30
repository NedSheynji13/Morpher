using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float sensivity;         //Defines how smooth the camera is moving
    public float MaxLookDown;       //Defines how far the user can turn the camera looking down
    private Vector3 inputAngle;     //Used for communication with unity's input manager
    private float cubeTurn;         //Used for instant turns in cube form
    private float MinFoV = -2f;
    private float MaxFoV = -5f;
    private float FoV = -5f;

    void Start()
    {
        inputAngle = new Vector3(0, 0, 0); //Sets the start position of the camera more conviniently
    }

    void Update()
    {
        FollowMorpher(Morphing.currentForm); //Follows the current active object and also changes depending on the form
        Zoom();
        WallZoom();
    }

    /// <summary>
    /// Lets the camera follow a given Object. Keep in mind that this requires a camera root
    /// </summary>
    /// <param name="_currentForm">Put the game object the camera should follow here</param>
    void FollowMorpher(GameObject _currentForm)
    {
        switch (_currentForm.name)  //Depending which form is the active one, the camera control might change its behaviour
        {
            case "BasicForm":
                FollowBasicForm();
                break;
            case "CubeForm":
                FollowCubeForm();
                break;
            case "SpringForm":
                FollowBasicForm();
                break;
            case "StarForm":
                FollowStarForm();
                break;
        }

        transform.position = _currentForm.transform.position;   //If the object moves the camera keeps following
        if (_currentForm.name == "CubeForm")
            inputAngle.y = Mathf.Round(inputAngle.y / 90) * 90;
    }

    /// <summary>
    /// The camera behaviour used for the basic form
    /// </summary>
    void FollowBasicForm()  //The Camera follow behaviour for the basic form
    {
        //Takes informations from the mouse input, read by the input manager and recalculates them to allow smoother camera turns
        inputAngle.y += Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        inputAngle.x += Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;

        //Snaps the rotation of the basic form between 0 and 360 degrees
        if (inputAngle.y > 360)
            inputAngle.y -= 360;
        else if (inputAngle.y < 0)
            inputAngle.y += 360;

        inputAngle.x = Mathf.Clamp(inputAngle.x, -10, MaxLookDown);   //Clamps how high/low and the user can turn the camera. Prevents the camera from looking through the ground.
        transform.localRotation = Quaternion.Euler(inputAngle);            //Sets the rotation of the camera according to the users preference calculated earlier
    }

    /// <summary>
    /// The camera behaviour used for the cube form
    /// </summary>
    void FollowCubeForm()
    {
        cubeTurn += Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        inputAngle.x += Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;

        if (cubeTurn < -20)
        {
            cubeTurn = 0;
            inputAngle.y -= 90f;
        }
        else if (cubeTurn > 20)
        {
            cubeTurn = 0;
            inputAngle.y += 90f;
        }

        inputAngle.x = Mathf.Clamp(inputAngle.x, -10, MaxLookDown);   //Clamps how high/low and the user can turn the camera. Prevents the camera from looking through the ground.

        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(inputAngle), 10 * Time.deltaTime);
        //Using the Quaternion rotation to avoid overturning over or under +-360 degrees life with eulerAngles
        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, inputAngle, 10 * Time.deltaTime);
    }

    void FollowStarForm()
    {
        //Takes informations from the mouse input, read by the input manager and recalculates them to allow smoother camera turns
        inputAngle.y += Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        inputAngle.x += Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;

        //Snaps the rotation of the basic form between 0 and 360 degrees
        if (inputAngle.y > 360)
            inputAngle.y -= 360;
        else if (inputAngle.y < 0)
            inputAngle.y += 360;

        transform.localRotation = Quaternion.Euler(inputAngle);                         //Sets the rotation of the camera according to the users preference calculated earlier
    }

    void Zoom()
    {
        float temp = Camera.main.transform.localPosition.z;
        FoV += (Input.GetAxis("Mouse ScrollWheel"));
        if (FoV < -5) FoV = -5;
        else if (FoV > -2) FoV = -2;
        temp = Mathf.Lerp(Camera.main.transform.localPosition.z, FoV, Time.time * 10);
        Camera.main.transform.localPosition = new Vector3 (Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, temp);
    }

    void WallZoom()
    {

    }
}
