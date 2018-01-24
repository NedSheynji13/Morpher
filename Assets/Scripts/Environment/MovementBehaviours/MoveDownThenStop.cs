using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownThenStop : MonoBehaviour, IMove
{

    #region Variables
    #endregion

    public void Move()
    {
        transform.Translate(transform.rotation * Vector3.down * 0.1f);
        if (transform.position.y <= -10)
            Destroy(this);
    }
}
