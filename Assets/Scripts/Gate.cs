using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    #region MonoBehaviour Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            if (other.transform.position.x != other.GetComponent<Character>().originalPath)
            {
                Vector3 pos = other.transform.position;
                pos.x = other.GetComponent<Character>().originalPath;
                other.transform.position = pos;
            }
        }
    }
    #endregion MonoBehaviour methods
}
