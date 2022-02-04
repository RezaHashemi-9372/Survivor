using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region MemberFields
    [SerializeField, Range(0.0f, 20.0f)]
    private float forceMultiplier = 2.0f;
    [SerializeField, Range(0, 50)]
    private int lineSegment = 20;


    private Vector3 startPoint;
    private Vector3 endpoint;
    private Vector3 onDragPoint;
    private LineRenderer lr;
    private Rigidbody rb;
    private List<Vector3> points = new List<Vector3>();
    public GameObject origin;
    #endregion MemberFields

    #region MonoBehaviour Methods
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        startPoint = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        onDragPoint = Input.mousePosition;
        Vector3 forceAmount = onDragPoint - startPoint;
        CreateLine(new Vector3(forceAmount.x, forceAmount.y, forceAmount.y) * forceMultiplier);
    }

    private void OnMouseUp()
    {
        endpoint = Input.mousePosition;
        Shoot(endpoint - startPoint);
        Invoke("HideLine", 1.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject, 3.0f);
    }

    #endregion MonoBehaviour Methods

    #region Public Methods

    #endregion Public Methods

    #region Private Methods

    public void Shoot(Vector3 force)
    {
        Debug.Log("Vector force is: " + force);
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(new Vector3(force.x, force.y, force.y) * forceMultiplier);
        origin.GetComponent<BasketBall>().RequestForBall();
    }
    private void CreateLine(Vector3 force)
    {
        Vector3 velocity = (force /rb.mass) * Time.fixedDeltaTime;
        float totalTimeToEnd = (2 * velocity.y) / Physics.gravity.y;

        float step = totalTimeToEnd / lineSegment;
        points.Clear();

        for (int i = 0; i < lineSegment; i++)
        {
            float momentomStep = step * i;
            Vector3 momentomVector = new Vector3(velocity.x * momentomStep,
                velocity.y * momentomStep - 0.5f * Physics.gravity.y * momentomStep * momentomStep,
                velocity.z * momentomStep);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, -momentomVector, out hit, momentomVector.magnitude))
            {
                break;
            }
            points.Add(-momentomVector + transform.position);
        }

        lr.positionCount = points.Count;
        lr.SetPositions(points.ToArray());
    }

    private void HideLine()
    {
        lr.positionCount = 0;
    }
    #endregion Private Methods
}
