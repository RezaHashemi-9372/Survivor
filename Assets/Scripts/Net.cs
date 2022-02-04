using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    #region MemberFields

    public bool isTriggered = false;

    #endregion MemberFields

    #region MonoBehaviour Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            isTriggered = true;
            BasketBall.Instance.CheckNet();
        }
    }

    #endregion MonoBehaviour Methods
}
