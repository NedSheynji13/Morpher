using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
[System.Serializable]
public class Consolecreator : MonoBehaviour
{
#region Variables
    public Affector puzzleAffector;
    public GameObject Light, Heavy, Red, Green;
    public int[] correct = new int[3];
    public List<int> pressedIndices = new List<int>();
    private Vector3 xOffset = Vector3.right * 3;
    private Vector3 yOffset = Vector3.up * 3;
    private Vector3 zOffset = Vector3.forward * 3;

    private Vector3[] switchOffsets = new Vector3[9];
    private Vector3[] hintOffsets = new Vector3[9];
#endregion

    public void Build(GameObject switchType)
    {
        randomize(); calcOffsets();
        for (int i = 0; i < switchOffsets.Length; i++)
        {
            Instantiate(switchType, switchOffsets[i], Quaternion.identity, transform.GetChild(0));
        }

        for (int i = 0; i < hintOffsets.Length; i++)
        {
            if (i == correct[0] || i == correct[1] || i == correct[2])
                Instantiate(Green, hintOffsets[i], Quaternion.identity, transform.GetChild(1));
            else
                Instantiate(Red, hintOffsets[i], Quaternion.identity, transform.GetChild(1));
        }
    }

    public void BuildLight()
    {
        Build(Light);
    }

    public void BuildHeavy()
    {
        Build(Heavy);
    }

    public void Delete()
    {
        for (int i = transform.GetChild(0).transform.childCount - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(transform.GetChild(0).transform.GetChild(i).gameObject);
            GameObject.DestroyImmediate(transform.GetChild(1).transform.GetChild(i).gameObject);
        }
    }

    private void randomize()
    {
        bool contains;
        int randInt = 0;
        int switches = 9;

        for (int i = 0; i < correct.Length; i++)
        {
            contains = true;
            while (contains)
            {
                randInt = (int)Mathf.Round(Random.value * (switches - 1));
                contains = false;
                for (int j = 0; j < i; j++)
                {
                    if (correct[j] == randInt)
                    {
                        contains = true;
                        break;
                    }
                }
            }
            correct[i] = randInt;
        }
    }

    private void calcOffsets()
    {
        switchOffsets[0] = transform.position - xOffset - zOffset;
        switchOffsets[1] = transform.position - xOffset;
        switchOffsets[2] = transform.position - xOffset + zOffset;
        switchOffsets[3] = transform.position - zOffset;
        switchOffsets[4] = transform.position;
        switchOffsets[5] = transform.position + zOffset;
        switchOffsets[6] = transform.position + xOffset - zOffset;
        switchOffsets[7] = transform.position + xOffset;
        switchOffsets[8] = transform.position + xOffset + zOffset;

        hintOffsets[0] = transform.position + yOffset - zOffset + Vector3.down + Vector3.back;
        hintOffsets[1] = transform.position + yOffset + Vector3.down + Vector3.back;
        hintOffsets[2] = transform.position + yOffset + zOffset + Vector3.down + Vector3.back;
        hintOffsets[3] = transform.position - zOffset + Vector3.down + Vector3.back;
        hintOffsets[4] = transform.position + Vector3.down + Vector3.back;
        hintOffsets[5] = transform.position + zOffset + Vector3.down + Vector3.back;
        hintOffsets[6] = transform.position - yOffset - zOffset + Vector3.down + Vector3.back;
        hintOffsets[7] = transform.position - yOffset + Vector3.down + Vector3.back;
        hintOffsets[8] = transform.position - yOffset + zOffset + Vector3.down + Vector3.back;

        for (int i = 0; i < hintOffsets.Length; i++)
        {
            hintOffsets[i] += (Vector3.left * 5) + (Vector3.up * 5);
        }
    }

    private bool hasIndex(int index)
    {
        foreach (int i in pressedIndices)
            if (i == index) return true;
        return false;
    }

    private bool isCorrect(int index)
    {
        foreach (int i in correct)
            if (i == index) return true;
        return false;
    }

    private void resetGrid()
    {
        pressedIndices.Clear();
        for (int j = 0; j < switchOffsets.Length; j++)
        {
            Switch sw = transform.GetChild(0).transform.GetChild(j).transform.GetChild(0).gameObject.GetComponent<Switch>();
            if (!sw.pressedActual) sw.Reset();
        }
    }

    private void Start()
    {
        if (transform.GetChild(0).transform.childCount == 0)
            DestroyImmediate(gameObject);
    }

    private void Update()
    {
        for (int i = 0; i < switchOffsets.Length; i++)
        {
            if (transform.GetChild(0).transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<Switch>().pressed)
            {
                if (isCorrect(i))
                {
                    if (!hasIndex(i))
                    {
                        pressedIndices.Add(i);
                    }
                }
                else
                {
                    resetGrid();
                }
            }
        }

        if (pressedIndices.Count == correct.Length)
            puzzleAffector.affected = true;
    }
}
#endif