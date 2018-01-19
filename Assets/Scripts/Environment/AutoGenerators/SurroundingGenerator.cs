using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurroundingGenerator : MonoBehaviour
{
    #region Wallparts
    public GameObject Empty, Horizontal, LeftDown, LeftUp, RightDown, RightUp, Vertical;
    #endregion

    #region Variables
    [Tooltip("Set this to true to render everything over the Z and Y axis.\nSet this to false to render everything over the Z and X axis.")]
    public bool DoubleSided = false;
    [Range(2, 100)] public int HeightOrDepth;
    [Range(2, 100)] public int Length;
    #endregion

    public void BuildWall()
    {
        HeightOrDepth--; Length--; //To keep the number of cubes consistent to the number the user puts into the editor

        //Spawning the corners
        Instantiate(RightUp, transform.position + new Vector3(-0.5f, 0.5f, 0.5f), Quaternion.identity, gameObject.transform);
        Instantiate(LeftUp, transform.position + new Vector3(-0.5f, 0.5f, 0.5f + Length), Quaternion.identity, gameObject.transform);
        Instantiate(RightDown, transform.position + new Vector3(-0.5f, 0.5f + HeightOrDepth, 0.5f), Quaternion.identity, gameObject.transform);
        Instantiate(LeftDown, transform.position + new Vector3(-0.5f, 0.5f + HeightOrDepth, 0.5f + Length), Quaternion.identity, gameObject.transform);

        for (int i = 1; i < Length; i++)
        {
            Instantiate(Horizontal, transform.position + new Vector3(-0.5f, 0.5f, 0.5f + i), Quaternion.identity, gameObject.transform);
        }//Creates the lower Horizontal Line
        for (int i = 1; i < HeightOrDepth; i++)
        {
            Instantiate(Vertical, transform.position + new Vector3(-0.5f, 0.5f + i, 0.5f), Quaternion.identity, gameObject.transform);
        }//Creates the left Vertical Line
        for (int i = 1; i < Length; i++)
        {
            Instantiate(Horizontal, transform.position + new Vector3(-0.5f, 0.5f + HeightOrDepth, 0.5f + i), Quaternion.identity, gameObject.transform);
        }//Creates the upper Horizontal Line
        for (int i = 1; i < HeightOrDepth; i++)
        {
            Instantiate(Vertical, transform.position + new Vector3(-0.5f, 0.5f + i, 0.5f + Length), Quaternion.identity, gameObject.transform);
        }//Creates the right Vertical Line
        for (int i = 1; i < Length; i++)
        {
            for (int j = 1; j < HeightOrDepth; j++)
            {
                Instantiate(Empty, transform.position + new Vector3(-0.5f, 0.5f + j, 0.5f + i), Quaternion.identity, gameObject.transform);
            }
        }//Fills it up
        if (DoubleSided)
        {
            Instantiate(LeftUp, transform.position + new Vector3(-0.52f, 0.5f, 0.5f), Quaternion.identity * Quaternion.Euler(0, 180, 0), gameObject.transform);
            Instantiate(RightUp, transform.position + new Vector3(-0.52f, 0.5f, 0.5f + Length), Quaternion.identity * Quaternion.Euler(0, 180, 0), gameObject.transform);
            Instantiate(LeftDown, transform.position + new Vector3(-0.52f, 0.5f + HeightOrDepth, 0.5f), Quaternion.identity * Quaternion.Euler(0, 180, 0), gameObject.transform);
            Instantiate(RightDown, transform.position + new Vector3(-0.52f, 0.5f + HeightOrDepth, 0.5f + Length), Quaternion.identity * Quaternion.Euler(0, 180, 0), gameObject.transform);

            Instantiate(Empty, transform.position + new Vector3(-0.51f, 0.49f, 0.49f), Quaternion.identity * Quaternion.Euler(0, 180, 0), gameObject.transform);
            Instantiate(Empty, transform.position + new Vector3(-0.51f, 0.49f, 0.51f + Length), Quaternion.identity * Quaternion.Euler(0, 180, 0), gameObject.transform);
            Instantiate(Empty, transform.position + new Vector3(-0.51f, 0.51f + HeightOrDepth, 0.49f), Quaternion.identity * Quaternion.Euler(0, 180, 0), gameObject.transform);
            Instantiate(Empty, transform.position + new Vector3(-0.51f, 0.51f + HeightOrDepth, 0.51f + Length), Quaternion.identity * Quaternion.Euler(0, 180, 0), gameObject.transform);

            for (int i = 1; i < Length; i++) //Creates the lower Horizontal Line
            {
                Instantiate(Horizontal, transform.position + new Vector3(-0.52f, 0.5f, 0.5f + i), Quaternion.identity * Quaternion.Euler(0, 180, 0), gameObject.transform);
                Instantiate(Empty, transform.position + new Vector3(-0.51f, 0.49f, 0.5f + i), Quaternion.identity * Quaternion.Euler(0, 180, 0), gameObject.transform);
            }
            for (int i = 1; i < HeightOrDepth; i++) //Creates the left Vertical Line
            {
                Instantiate(Vertical, transform.position + new Vector3(-0.52f, 0.5f + i, 0.5f), Quaternion.identity * Quaternion.Euler(0, 180, 0), gameObject.transform);
                Instantiate(Empty, transform.position + new Vector3(-0.51f, 0.5f + i, 0.49f), Quaternion.identity * Quaternion.Euler(0, 180, 0), gameObject.transform);
            }
            for (int i = 1; i < Length; i++) //Creates the upper Horizontal Line
            {
                Instantiate(Horizontal, transform.position + new Vector3(-0.52f, 0.5f + HeightOrDepth, 0.5f + i), Quaternion.identity * Quaternion.Euler(0, 180, 0), gameObject.transform);
                Instantiate(Empty, transform.position + new Vector3(-0.51f, 0.51f + HeightOrDepth, 0.5f + i), Quaternion.identity * Quaternion.Euler(0, 180, 0), gameObject.transform);
            }
            for (int i = 1; i < HeightOrDepth; i++) //Creates the right Vertical Line
            {
                Instantiate(Vertical, transform.position + new Vector3(-0.52f, 0.5f + i, 0.5f + Length), Quaternion.identity * Quaternion.Euler(0, 180, 0), gameObject.transform);
                Instantiate(Empty, transform.position + new Vector3(-0.51f, 0.5f + i, 0.51f + Length), Quaternion.identity * Quaternion.Euler(0, 180, 0), gameObject.transform);
            }
        }
        HeightOrDepth++; Length++; //To set the number back to correct
        HideWallparts();
    }
    public void BuildFloor()
    {
        HeightOrDepth--; Length--; //To keep the number of cubes consistent to the number the user puts into the editor

        //Spawning the corners
        Instantiate(LeftDown, transform.position + new Vector3(0.5f, -0.5f, 0.5f), Quaternion.identity, gameObject.transform);
        Instantiate(LeftUp, transform.position + new Vector3(0.5f, -0.5f, 0.5f + Length), Quaternion.identity, gameObject.transform);
        Instantiate(RightDown, transform.position + new Vector3(0.5f + HeightOrDepth, -0.5f, 0.5f), Quaternion.identity, gameObject.transform);
        Instantiate(RightUp, transform.position + new Vector3(0.5f + HeightOrDepth, -0.5f, 0.5f + Length), Quaternion.identity, gameObject.transform);

        for (int i = 1; i < Length; i++)
        {
            Instantiate(Vertical, transform.position + new Vector3(0.5f, -0.5f, 0.5f + i), Quaternion.identity, gameObject.transform);
        }//Creates the lower Horizontal Line
        for (int i = 1; i < HeightOrDepth; i++)
        {
            Instantiate(Horizontal, transform.position + new Vector3(0.5f + i, -0.5f, 0.5f), Quaternion.identity, gameObject.transform);
        }//Creates the left Vertical Line
        for (int i = 1; i < Length; i++)
        {
            Instantiate(Vertical, transform.position + new Vector3(0.5f + HeightOrDepth, -0.5f, 0.5f + i), Quaternion.identity, gameObject.transform);
        }//Creates the upper Horizontal Line
        for (int i = 1; i < HeightOrDepth; i++)
        {
            Instantiate(Horizontal, transform.position + new Vector3(0.5f + i, -0.5f, 0.5f + Length), Quaternion.identity, gameObject.transform);
        }//Creates the right Vertical Line
        for (int i = 1; i < Length; i++)
        {
            for (int j = 1; j < HeightOrDepth; j++)
            {
                Instantiate(Empty, transform.position + new Vector3(0.5f + j, -0.5f, 0.5f + i), Quaternion.identity, gameObject.transform);
            }
        }//Fills it up
        if (DoubleSided)
        {
            Instantiate(LeftUp, transform.position + new Vector3(0.5f, -0.52f, 0.5f), Quaternion.identity, gameObject.transform);
            Instantiate(LeftDown, transform.position + new Vector3(0.5f, -0.52f, 0.5f + Length), Quaternion.identity, gameObject.transform);
            Instantiate(RightUp, transform.position + new Vector3(0.5f + HeightOrDepth, -0.52f, 0.5f), Quaternion.identity, gameObject.transform);
            Instantiate(RightDown, transform.position + new Vector3(0.5f + HeightOrDepth, -0.52f, 0.5f + Length), Quaternion.identity, gameObject.transform);

            Instantiate(Empty, transform.position + new Vector3(0.49f, -0.51f, 0.49f), Quaternion.identity, gameObject.transform);
            Instantiate(Empty, transform.position + new Vector3(0.49f, -0.51f, 0.51f + Length), Quaternion.identity, gameObject.transform);
            Instantiate(Empty, transform.position + new Vector3(0.51f + HeightOrDepth, -0.51f, 0.49f), Quaternion.identity, gameObject.transform);
            Instantiate(Empty, transform.position + new Vector3(0.51f + HeightOrDepth, -0.51f, 0.51f + Length), Quaternion.identity, gameObject.transform);

            for (int i = 1; i < Length; i++) //Creates the lower Horizontal Line
            {
                Instantiate(Vertical, transform.position + new Vector3(0.5f, -0.52f, 0.5f + i), Quaternion.identity, gameObject.transform);
                Instantiate(Empty, transform.position + new Vector3(0.49f, -0.51f, 0.5f + i), Quaternion.identity, gameObject.transform);
            }
            for (int i = 1; i < HeightOrDepth; i++) //Creates the left Vertical Line
            {
                Instantiate(Horizontal, transform.position + new Vector3(0.5f + i, -0.52f, 0.5f), Quaternion.identity, gameObject.transform);
                Instantiate(Empty, transform.position + new Vector3(0.5f + i, -0.51f, 0.49f), Quaternion.identity, gameObject.transform);
            }
            for (int i = 1; i < Length; i++) //Creates the upper Horizontal Line
            {
                Instantiate(Vertical, transform.position + new Vector3(0.5f + HeightOrDepth, -0.52f, 0.5f + i), Quaternion.identity, gameObject.transform);
                Instantiate(Empty, transform.position + new Vector3(0.51f + HeightOrDepth, -0.51f, 0.5f + i), Quaternion.identity, gameObject.transform);
            }
            for (int i = 1; i < HeightOrDepth; i++) //Creates the right Vertical Line
            {
                Instantiate(Horizontal, transform.position + new Vector3(0.5f + i, -0.52f, 0.5f + Length), Quaternion.identity, gameObject.transform);
                Instantiate(Empty, transform.position + new Vector3(0.5f + i, -0.51f, 0.51f + Length), Quaternion.identity, gameObject.transform);
            }
            for (int i = 1; i < Length; i++) //Fills it up
            {
                for (int j = 1; j < HeightOrDepth; j++)
                {
                    Instantiate(Empty, transform.position + new Vector3(0.5f + j, -0.51f, 0.5f + i), Quaternion.identity, gameObject.transform);
                }
            }
        }
        HeightOrDepth++; Length++; //To set the number back to correct
        HideWallparts();
    }
    public void Delete()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
    public void HideWallparts()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            transform.GetChild(i).hideFlags = HideFlags.HideInHierarchy;
        }
    }
    public void ShowWallParts()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            transform.GetChild(i).hideFlags = HideFlags.None;
        }
    }
}
