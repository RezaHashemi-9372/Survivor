using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour, IPlayAble
{
    #region MemberFields
    [SerializeField]
    private Ball ballPrefab;
    [SerializeField]
    private Net[] allNet;
    [SerializeField]
    private GameObject instanPos = null;
    [Header("AI Details")]
    [SerializeField, Range(1, 10)]
    private int shootTiming = 3;
    [SerializeField, Range(0.0f, 30.0f)]
    private float forceMultiplier = 15.0f;
    [SerializeField, Range(-15f, 0f)]
    private float minX = -2f;
    [SerializeField, Range(5.0f, 20.0f)]
    private float maxX = 6;
    [SerializeField, Range(0, 10)]
    private float minY = 5f;
    [SerializeField, Range(20, 40)]
    private float maxY = 25;

    private float nextShoot = 0.0f;
    private bool isFinished = false;
    private Ball ballSample;
    public Character charSet;
    private int netLength;
    private GameMode gameMode;
    public static BasketBall Instance;
    #endregion MemberFields


    #region MonoBehaviour Methods

    private void Awake()
    {
        gameMode = FindObjectOfType<GameMode>();
        Instance = this;
        netLength = allNet.Length;
    }

    private void Update()
    {
        if (charSet && charSet.GetComponent<AIController>())
        {
            if (isFinished)
            {
                return;
            }
            ShootAutomatically();
        }
    }
    #endregion MonoBehaviour Methods


    #region Public Methods
    public void CheckNet()
    {
        netLength -= 1;

        if (charSet && netLength <= 0)
        {
            gameMode.AddWhoFinished(charSet);
        }
    }

    public void RequestForBall()
    {
        Invoke("CreateBall", 1.5f);
    }

    public void SetCharacter(Character characte)
    {
        this.charSet = characte;
        CreateBall();
    }

    public void ShootAutomatically()
    {
        if (!ballSample || nextShoot > Time.time)
        {
            return;
        }
        nextShoot = Time.time + shootTiming;
        float y = Random.Range(minY, maxY);
        Vector3 force = new Vector3(Random.Range(minX, maxX), y, y);
        Debug.Log("vector 3 force is: " + force);
        ballSample.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //ballSample.GetComponent<Ball>().Shoot(force);
        ballSample.GetComponent<Rigidbody>().AddForce(force * forceMultiplier);

        ballSample = null;
        RequestForBall();
    }
    #endregion Public Methods

    #region Private Methods

    private void CreateBall()
    {
        ballSample = null;
        ballSample = Instantiate(ballPrefab, instanPos.transform.position, Quaternion.identity);
        ballSample.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        ballSample.origin = this.gameObject;
    }
    #endregion Private Methods
}
