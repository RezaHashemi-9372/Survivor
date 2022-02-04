using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskTable : MonoBehaviour, IPlayAble
{
    #region MemberFields
    [Header("Disk Details")]
    [SerializeField]
    private Disk diskPrefab;
    [SerializeField]
    private GameObject instanPos = null;
    [SerializeField, Range(0.0f, 5.0f)]
    private float nextBallDuration = 1.0f;
    [SerializeField, Range(0, 10)]
    private int numToFinish = 2;
    [Header("Ai Section Details")]
    [SerializeField, Range(0, 10)]
    private int aiShootTiming = 2;
    [SerializeField, Range(-.5f, 0)]
    private float minXDir = -.3f;
    [SerializeField, Range(0, .5f)]
    private float maxXdir = .4f;
    [SerializeField, Range(0, 1)]
    private float minZ = .4f;
    [SerializeField, Range(0, 2)]
    private float maxZ = .7f;
    [SerializeField, Range(0, 100.0f)]
    private float AiMinForce = 30.0f;
    [SerializeField, Range(0, 150.0f)]
    private float AiMaxForce = 30.0f;

    private bool isfinished = false;
    private float nextShoot = 0;
    private Disk diskSample = null;
    public Character charSet;
    private List<GameObject> objHolder = new List<GameObject>();
    private GameMode gameMode;

    public static DiskTable Instacnce;
    #endregion MemberFields


    #region MonoBehaviour Methods

    private void Awake()
    {
        gameMode = FindObjectOfType<GameMode>();
        Instacnce = this;
    }
    void Start()
    {
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (charSet != null && charSet.GetComponent<AIController>())
        {
            if (GameMode.isFinished)
            {
                return;
            }
            ShootAutomatically();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Disk>())
        {
            if (!objHolder.Contains(other.gameObject))
            {
                objHolder.Add(other.gameObject);
            }
        }
        if (objHolder.Count >= numToFinish)
        {
            gameMode.AddWhoFinished(charSet);
            isfinished = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Disk>())
        {
            objHolder.Remove(other.gameObject);
        }
    }

    #endregion monoBehaviour Methods


    #region Public Methods 

    public void NeedForDisk()
    {
        Invoke("InstantiateDisk", nextBallDuration);
    }

    public void SetCharacter(Character character)
    {
        this.charSet = character;
        Invoke("InstantiateDisk", nextBallDuration);
    }

    public void ShootAutomatically()
    {
        if (diskSample == null || nextShoot > Time.time)
        {
            return;
        }
        nextShoot = Time.time + aiShootTiming;
        diskSample.GetComponent<Disk>().AddForce(new Vector3(Random.Range(minXDir, maxXdir), 0, Random.Range(minZ, maxZ)),
            Random.Range(AiMinForce, AiMaxForce));

        diskSample = null;
        NeedForDisk();
    }

    #endregion Public Methods


    #region Private Methods

    public void InstantiateDisk()
    {
        diskSample = null;
        diskSample = Instantiate(diskPrefab, instanPos.transform.position, Quaternion.identity);
        diskSample.origin = this.gameObject;
    }

    
    #endregion Private Methods
}
