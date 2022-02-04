using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    #region MemberFields

    [SerializeField, Range(0.0f, 10000.0F)]
    private float force = 20.0f;
    [SerializeField]
    private Material lrMaterial;
    private float maxDistance = 15.0f;
    public GameObject origin = null;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 onDragStart;
    private Vector3 onDrag;
    private LineRenderer lr;
    float yStart;
    float yEnd;
    #endregion MemberFields


    #region MonoBehavioue Methods
    private void Awake()
    {
        lr = this.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.material = lrMaterial;
        lr.enabled = false;
    }
    void Start()
    {
        
    }
    private void OnMouseDown()
    {
        lr.enabled = true;
        startPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        yStart = Input.mousePosition.y;
        lr.SetPosition(0, this.transform.position);
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        Vector3 dir = (mousePos - startPoint);
        float dist = Mathf.Clamp(Vector3.Distance(startPoint, mousePos), 0, maxDistance);
        onDrag = startPoint + (dir.normalized * dist);
        lr.SetPosition(1, onDrag);
    }

    private void OnMouseUp()
    {
        endPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        yEnd = Input.mousePosition.y;
        float pull = (yStart - yEnd) / 100.0f; 

        Vector3 dir = endPoint - startPoint;
        dir.y = 0; 
        AddForce(-dir, pull);
        lr.enabled = false;


        if (origin && origin.GetComponent<DiskCandle>())
        {
            origin.GetComponent<DiskCandle>().NeedForDisk();
        }
        else if (origin && origin.GetComponent<DiskTable>())
        {
            origin.GetComponent<DiskTable>().NeedForDisk();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Plane"))
        {
            Destroy(this.gameObject, 2.0f);
        }
    }

    #endregion MonoBehaviour Methods


    #region Private Methods

    #endregion Private Methods


    #region Public Methods
    public void AddForce(Vector3 direction, float pullAmount)
    {
        this.GetComponent<Rigidbody>().AddForce((direction + Vector3.forward).normalized * force * pullAmount, ForceMode.Force);
    }

    public void AddTestForce(Vector3 dir)
    {
        this.GetComponent<Rigidbody>().AddForce(dir);
    }
    #endregion Public Methods
}
