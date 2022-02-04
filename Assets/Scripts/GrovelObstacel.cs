using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrovelObstacel : MonoBehaviour
{

    #region MonoBehaviour Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other && other.CompareTag("Character"))
        {
            other.GetComponent<Character>().mode = Character.characterMode.Crouch;
            other.GetComponent<Character>().checkPoint = this.GetComponent<Collider>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            other.GetComponent<Character>().mode = Character.characterMode.Idle;
        }
    }
    #endregion MonoBehaviour Methods
}
