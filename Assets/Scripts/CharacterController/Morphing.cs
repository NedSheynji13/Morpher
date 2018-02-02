using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Morphing : MonoBehaviour
{
    #region Used for Morphing
    public GameObject[] objects;    //Array of objects the morpher can swap into. Thanks to its public state the size is editable in the unity editor
    public static GameObject currentForm;  //Game Object where the array will get initialized into
    public static Vector3 wantedscale;     //Variable used for interpolation
    public static bool grounded, teleportSickness, forced = false;
    [Flags] public enum abilities { None = 0, Cube = 1, Spring = 2, Star = 4, Bridge = 8 };
    public static abilities unlocked;
    #endregion

    #region Used for gravity controlling during the morph
    public static bool morphing;        //Variable to check if the object already hovers/morphs to prevent multiple lerps
    #endregion

    #region Used for user inputs
    private enum Forms { BasicForm = 0, Form1, Form2, Form3 };              //Used to make the indication to which form is switching easier
    private string[] cForms = new string[] { "Form1", "Form2", "Form3" };   //Array of strings needed for the input
    #endregion

    #region Used for buffering during the morph
    private string pressed; //Variable to buffer user inputs for the ScaleUp method
    private Vector3 Pos;    //Variable to buffer the current object position to use it for the going up effect
    #endregion

    #region Used for the particles
    public ParticleSystem[] morphs;
    #endregion

    #region Used for the sounds
    public AudioSource[] sounds;
    #endregion

    void Start()
    {
        currentForm = objects[0];                                   //Sets the basic form as start object
        wantedscale = Vector3.one;                                  //Sets the wantedScale to 1 to lerp the basic form up to 1
        currentForm.GetComponent<Rigidbody>().useGravity = true;    //Activates the gravity
        currentForm.SetActive(true);                                //Activates the object itself
        morphing = false;                                           //Frees the character
        try
        {
            SaveAndLoad.Load();
        }
        catch
        {
            if (unlocked == 0)
                unlocked = (Morphing.abilities)0;
        }
        try
        {
            Vector3 temp = SaveAndLoad.LoadPosition();
            currentForm.transform.localPosition = temp;
        }
        catch
        {
            currentForm.transform.localPosition = Vector3.up;
        }
    }

    public void ForceMorph()
    {
        if (currentForm.name != "BasicForm")
        {
            Pos.y++;                                    //Increases the Y position by one and creates this little hovering effect
            morphing = true;                            //Prevents from multiple morphings
            switch (currentForm.name)
            {
                case "CubeForm":
                    {
                        pressed = "Form1";
                        break;
                    }
                case "SpringForm":
                    {
                        pressed = "Form2";
                        break;
                    }
                case "StarForm":
                    {
                        pressed = "Form3";
                        break;
                    }
            }                            //Used to morph into the corrent form in the scaleUp method
            if (currentForm != objects[(int)Forms.Form3])
                InverseGravity();                       //Disables the gravity for the active object
            currentForm.transform.localPosition = Vector3.Lerp(currentForm.transform.localPosition, Pos, 5 * Time.deltaTime);
            ScaleDown();                                //Sets the Scale of the active object down to nothing
        }
    }

    private void FixedUpdate()
    {
        Debug.Log(unlocked);
        Pos = currentForm.transform.localPosition;  //Checks the objects current position
        //Pos = currentForm.transform.localPosition;  //Checks the objects current position
        //General Scaling Lerp which runs constantly in the background. Only the object, its Active state and its wantedscale to which he lerps changes
        currentForm.transform.localScale = Vector3.Lerp(currentForm.transform.localScale, wantedscale, 10 * Time.deltaTime);
        if (forced)
        {
            forced = false;
            ForceMorph();
        }
        //User input loop
        foreach (string str in cForms) //iterates between all 3 possible inputs
        {
            if (Input.GetButtonUp(str) && !morphing && grounded) //checks if the input is given
            {
                if ((str == "Form1") && (abilities.Cube == (unlocked & abilities.Cube))
                    || (str == "Form2") && (abilities.Spring == (unlocked & abilities.Spring))
                    || (str == "Form3") && (abilities.Star == (unlocked & abilities.Star)))
                {
                    Pos.y++;                                    //Increases the Y position by one and creates this little hovering effect
                    morphing = true;                            //Prevents from multiple morphings
                    pressed = str;                              //Used to morph into the corrent form in the scaleUp method
                    if (currentForm != objects[(int)Forms.Form3])
                        InverseGravity();                       //Disables the gravity for the active object
                    currentForm.transform.localPosition = Vector3.Lerp(currentForm.transform.localPosition, Pos, 5 * Time.deltaTime);
                    ScaleDown();                                //Sets the Scale of the active object down to nothing
                    break;                                      //Breaks to start the update method again
                }
            }
        }
    }

    #region Methods for the morphing size controls, gravity controls, form changes, etc.
    private void ScaleDown()
    {
        wantedscale = Vector3.zero; //Used to scale down the active object
        Invoke("ScaleUp", 0.1f);     //Invoked the scale up for the next object
    }
    private void ScaleUp()
    {
        currentForm.transform.localScale = Vector3.zero;
        switch (pressed)    //Used to determine which button has been pressed
        {
            case "Form1":   //In case the user wants to cube
                {
                    if (currentForm != objects[(int)Forms.Form1])
                    {
                        currentForm.SetActive(false);
                        currentForm = objects[(int)Forms.Form1];
                        Instantiate(morphs[1], currentForm.transform);
                        Instantiate(sounds[1], currentForm.transform);
                    }
                    else    //If the user already is a cube
                    {
                        currentForm.SetActive(false);
                        currentForm = objects[(int)Forms.BasicForm];
                        Instantiate(morphs[0], currentForm.transform);
                        Instantiate(sounds[0], currentForm.transform);
                    }
                    break;
                }
            case "Form2":   //In case the user wants to spring
                {
                    if (currentForm != objects[(int)Forms.Form2])
                    {
                        currentForm.SetActive(false);
                        currentForm = objects[(int)Forms.Form2];
                        Instantiate(morphs[2], currentForm.transform);
                        Instantiate(sounds[2], currentForm.transform);
                    }
                    else    //If the user already is a spring
                    {
                        currentForm.SetActive(false);
                        currentForm = objects[(int)Forms.BasicForm];
                        Instantiate(morphs[0], currentForm.transform);
                        Instantiate(sounds[0], currentForm.transform);
                    }
                    break;
                }
            case "Form3":   //In case the user wants to star
                {
                    if (currentForm != objects[(int)Forms.Form3])
                    {
                        currentForm.SetActive(false);
                        currentForm = objects[(int)Forms.Form3];
                        Instantiate(morphs[3], currentForm.transform);
                        Instantiate(sounds[3], currentForm.transform);
                    }
                    else    //If the user already is a star
                    {
                        currentForm.SetActive(false);
                        currentForm = objects[(int)Forms.BasicForm];
                        Instantiate(morphs[0], currentForm.transform);
                        Instantiate(sounds[0], currentForm.transform);
                    }
                    break;
                }
            default:    //In case of failure
                currentForm = objects[(int)Forms.BasicForm];
                break;
        }
        wantedscale = Vector3.one;                  //Set to activate the upscaling again
        currentForm.SetActive(true);                //Sets the new form true
        currentForm.transform.localPosition = Pos;  //Sets the new form to the higher position
        currentForm.transform.rotation = Quaternion.identity;
        Invoke("InverseGravity", 0.1f);             //Invokes the gravity activation
        Invoke("DestroyAudiosource", 1f);
        morphing = false;
    }

    private void InverseGravity() //Once all lerping is done the object drops down to the ground
    {
        currentForm.GetComponent<Rigidbody>().useGravity = !currentForm.GetComponent<Rigidbody>().useGravity;
    }

    private void DestroyAudiosource()
    {
        foreach (GameObject Sound in GameObject.FindGameObjectsWithTag("Sound"))
        {
            Destroy(Sound);
        }
    }
    #endregion
}
