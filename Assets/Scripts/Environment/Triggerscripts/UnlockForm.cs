using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockForm : MonoBehaviour
{
    #region Variables
    [Tooltip("0=None|1=Cube|2=Spring|4=Star|8=Bridge   Add numbers together to unlock multiple forms")]
    public int bitmask;
    #endregion

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            Morphing.unlocked |= (Morphing.abilities)bitmask;
        }
    }
}
