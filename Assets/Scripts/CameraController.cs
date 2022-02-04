using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region MemberFields
    [SerializeField, Range(0.0f, 50.0f)]
    private float pursuingSpeed = 5.0f;
    [SerializeField, Range(0.0f, 50.0f)]
    private float speedToRight = 10.0f;
    [SerializeField, Range(0.0f, 50.0f)]
    private float offSet = 5.0f;
    [SerializeField, Range(0.0f, 50.0f)]
    private float offSetToCrouch = 10.0f;
    [SerializeField]
    private Character target;

    private float yOriginalPosition = 0;
    private Quaternion nextRotation;
    private Quaternion endModeRotation;
    private Quaternion originalRotation;

    #endregion MemberFields

    private void Awake()
    {
        yOriginalPosition = this.transform.position.y;
        originalRotation = Quaternion.Euler(20.0f, 0.0f, 0.0f);
        endModeRotation  = Quaternion.Euler(20.0f, 0.0f, 0.0f);
        nextRotation     = Quaternion.Euler(35.0f, -90.0f, 0.0f);
    }

    #region MonoBehaviour Methods
    void Start()
    {
        
    }

    void Update()
    {
        if (target.mode == Character.characterMode.Crouch)
        {
            Vector3 pos = target.checkPoint.transform.position;

            this.transform.position = Vector3.Slerp(this.transform.position,
                new Vector3(pos.x + offSetToCrouch , pos.y + offSetToCrouch, pos.z), speedToRight * Time.deltaTime);

            this.transform.rotation = Quaternion.Lerp(this.transform.rotation,
                nextRotation,
                speedToRight * Time.deltaTime);
        }
        else if (target.mode == Character.characterMode.endGame)
        {
            this.transform.position = Vector3.Lerp(this.transform.position,
                new Vector3(target.transform.position.x, target.transform.position.y + 7, target.transform.position.z -2)
                ,10 * Time.deltaTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation,
                endModeRotation,
                speedToRight * Time.deltaTime);
        }
        else
        {

            this.transform.position = Vector3.Slerp(
                this.transform.position,
                new Vector3(target.transform.position.x, yOriginalPosition, target.transform.position.z - offSet),
                pursuingSpeed * Time.deltaTime);

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                originalRotation, 
                speedToRight * Time.deltaTime);
        }
    }

    #endregion MonoBehaviour Methods
}
