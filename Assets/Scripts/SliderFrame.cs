using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderFrame : MonoBehaviour
{
    #region MemberFields
    [SerializeField]
    private GameObject slider;

    private Vector3 originalRot;
    private float slider_X;
    #endregion MemberFields


    #region MonoBehaviour Methods

    private void Awake()
    {
       //slider_X = slider.transform.position.x;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character")/* && other.GetComponent<Character>().mode != Character.characterMode.Idle*/)
        {
            originalRot = other.transform.eulerAngles;
            Vector3 rot = this.transform.localRotation.eulerAngles;
            float y = rot.y;
            other.transform.eulerAngles = rot;
            //other.GetComponent<Rigidbody>().isKinematic = true;
            if (Input.GetMouseButton(0))
            {
                other.GetComponent<Character>().mode = Character.characterMode.Walk;
            }
            else
            {
                other.GetComponent<Character>().mode = Character.characterMode.Idle;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Character")/* && other.GetComponent<Character>().mode != Character.characterMode.Idle*/)
        {
            originalRot = other.transform.eulerAngles;
            Vector3 rot = this.transform.localRotation.eulerAngles;
            float y = rot.y;
            other.transform.eulerAngles = rot;
            //other.GetComponent<Rigidbody>().isKinematic = true;

            if (other.GetComponent<PlayerController>() && Input.GetMouseButton(0))
            {
                other.GetComponent<Character>().mode = Character.characterMode.Walk;
            }
            else if (other.GetComponent<AIController>())
            {
                other.GetComponent<Character>().mode = Character.characterMode.Walk;
            }
            else
            {
                other.GetComponent<Character>().mode = Character.characterMode.Idle;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            other.transform.eulerAngles = Vector3.zero;
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<Character>().mode = Character.characterMode.Idle;
        }
    }

    #endregion MonoBehaviour Methods


    #region Public Methods

    #endregion Public Methods


    #region Private Methods

    #endregion Private Methods
}
