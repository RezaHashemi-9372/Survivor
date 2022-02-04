using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum characterMode : short
    {
        Idle,
        Walk,
        Run,
        Crouch,
        endGame
    }

    #region MemberFields
    [SerializeField, Range(0.0f, 50.0f)]
    private float runSpeed = 5.0f;
    [SerializeField, Range(0.0f, 30.0f)]
    private float walkSpeed = 2.0f;
    [SerializeField, Range(0.0f, 20.0f)]
    private float crowlSpeed = 1.0f;
    [SerializeField]
    private Collider walkCollider;
    [SerializeField]
    private Collider crouchCollider;

    public bool isGrovling = false;
    public float originalPath = 0;
    public Collider checkPoint = new Collider();
    public characterMode mode = characterMode.Idle;
    private Animator animator;
    #endregion MemberFields


    #region MonoBehaviour Methods

    private void Awake()
    {
        originalPath = this.transform.position.x;
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (mode == characterMode.endGame)
        {
            this.GetComponent<Animator>().enabled = false;
            return;
        }
        Move();
    }

    private void LateUpdate()
    {
    }

    #endregion MonoBehaviour Methods


    #region Public Methods


    #endregion Public Methods


    #region Private Methods

    private void Move()
    {
        switch (mode)
        {
            case characterMode.Idle:
                Idle();
                break;
            case characterMode.Walk:
                Walk();
                break;
            case characterMode.Run:
                Run();
                break;
            case characterMode.Crouch:
                Crawling();
                break;
            case characterMode.endGame:
                Idle();
                break;
        }
    }

    private void Run()
    {
        walkCollider.enabled = true;
        crouchCollider.enabled = false;
        animator.SetFloat("speed", 5);
        this.transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void Idle()
    {
        walkCollider.enabled = true;
        crouchCollider.enabled = false;
        animator.SetFloat("speed", 0);
    }

    private void Crawling()
    {
        walkCollider.enabled = false;
        crouchCollider.enabled = true;
        animator.SetFloat("speed", 1);
        transform.Translate(Vector3.forward * crowlSpeed * Time.deltaTime);
    }

    private void Walk()
    {
        walkCollider.enabled = true;
        crouchCollider.enabled = false;
        this.transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
        animator.SetFloat("speed", 2);
    }
    private void Climbing()
    {
        animator.SetBool("isClimbing", true);
        this.transform.Translate(Vector3.up * runSpeed * Time.deltaTime);
    }
    #endregion Private Methdos
}
