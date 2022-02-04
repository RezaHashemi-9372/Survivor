using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderStrict : MonoBehaviour
{
    #region MemberFields
    [SerializeField]
    private GameObject slider;

    private float slider_x;
    #endregion MemberFields

    #region MonoBehaviour Methods
    private void Awake()
    {
        slider_x = slider.transform.position.x;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            Vector3 pos = other.transform.position;
            pos.x = slider_x;
            other.transform.position = pos;
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

    #endregion monoBehaviour methods
}
