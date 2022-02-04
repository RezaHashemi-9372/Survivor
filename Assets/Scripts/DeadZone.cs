using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    #region MonoBehaviour Methods
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Character"))
        {
            collision.transform.GetComponent<Character>().mode = Character.characterMode.Idle;
            float collide_z = collision.collider.GetComponent<Character>().checkPoint.bounds.size.z;
            Vector3 newPosition = collision.collider.transform.position;
            newPosition.z -= collide_z;
            collision.collider.transform.position = newPosition;
        }
    }

    #endregion MonoBehaviour Methods
}
