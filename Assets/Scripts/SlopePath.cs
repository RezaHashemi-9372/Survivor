using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopePath : MonoBehaviour
{

    #region MonoBehaviour Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            other.GetComponent<Character>().mode = Character.characterMode.Walk;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Character") && Input.GetMouseButton(0))
        {
            other.GetComponent<Character>().mode = Character.characterMode.Walk;
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
