using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskCandle : MonoBehaviour , IPlayAble
{
    #region MemberFields
    [SerializeField]
    private Disk diskPrefab;
    [SerializeField]
    private GameObject instantiatePos = null;
    [SerializeField, Range(0.0f, 10.0f)]
    private float nextBallDuration = 2.0f;
    [Header("AI Section Details")]
    [SerializeField, Range(0, 10)]
    private int aIShootTiming = 2;
    [SerializeField, Range(-.5f, 0)]
    private float minXDir = -.3f;
    [SerializeField, Range(0, .5f)]
    private float maxXdir = .4f;
    [SerializeField, Range(0, 1)]
    private float minZ = .4f;
    [SerializeField, Range(0, 2)]
    private float maxZ = .7f;
    [SerializeField, Range(0, 350.0f)]
    private float AiMinForce = 30.0f;
    [SerializeField, Range(0, 501.0f)]
    private float AiMaxForce = 30.0f;


    private bool isFinished = false;
    private float nextShoot = 0.0f;
    private Disk diskSample;
    private List<GameObject> candleHolder = new List<GameObject>();
    private Character charSet;
    private GameMode gameMode;
    public static DiskCandle Instance;
    #endregion MemberFields

    #region monoBehaviour Methods
    private void Awake()
    {
        Instance = this;
        gameMode = FindObjectOfType<GameMode>();
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
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Lobut"))
        {
            if (!candleHolder.Contains(other.gameObject))
            {
                candleHolder.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lobut"))
        {
            candleHolder.Remove(other.gameObject);
        }
        if (charSet && candleHolder.Count <= 0)
        {
            isFinished = true;
            gameMode.AddWhoFinished(charSet);
        }
    }



    #endregion MonoBehaviour Methods

    #region Public Methods
    public void ShootAutomatically()
    {
        if (diskSample == null || nextShoot > Time.time)
        {
            return;
        }
        nextShoot = Time.time + aIShootTiming;
        diskSample.GetComponent<Disk>().AddForce(new Vector3(Random.Range(minXDir, maxXdir), 0, Random.Range(minZ, maxZ)),
            Random.Range(AiMinForce, AiMaxForce));
        Destroy(diskSample, 6.0f);
        diskSample = null;
        NeedForDisk();
    }
    public void InstantiateDisk()
    {
        diskSample = null;
        diskSample = Instantiate(diskPrefab, instantiatePos.transform.position, Quaternion.identity);
        diskSample.origin = this.gameObject;
    }
    public void NeedForDisk()
    {
        Invoke("InstantiateDisk", nextBallDuration);
    }
    public void SetCharacter(Character charac)
    {
        charSet = charac;
        NeedForDisk();
    }
    #endregion public Methods
}
