using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [Range(0, 25)]
    public float velocity = 0.5f;

    public Space relativeTo = Space.World;
    private Transform _player;

    private Animator animator;

    Transform cam;
    Vector3 camForward;
    Vector3 move;
    Vector3 moveInput;

    float forwardAmount;
    float turnAmount;

    // Start is called before the first frame update
    void Start()
    {
        _player = gameObject.transform;
        animator = this.GetComponentInChildren<Animator>();

        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move(Vector3 move)
    {
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        this.moveInput = move;

        ConvertMoveInput();
        UpdateAnimator();
    }

    void ConvertMoveInput()
    {
        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        turnAmount = localMove.x;
        forwardAmount = localMove.z;
    }

    void UpdateAnimator()
    {
        animator.SetFloat("forward", forwardAmount);//, 0.1f,Time.deltaTime);
        animator.SetFloat("turn", turnAmount);//, 0.1f, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //print(mouse.ToString());
        mouse = new Vector3(mouse.x, 0, mouse.z); 
        _player.LookAt(mouse);


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        if (cam != null)
        {
            camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1)).normalized;
            move = vertical * camForward + horizontal * cam.right;
        }
        else
        {
            move = vertical * Vector3.forward + horizontal * Vector3.right;
        }

        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        Move(move);

        Vector3 movement = new Vector3(horizontal, 0, vertical);


        _player.Translate(velocity * Time.fixedDeltaTime * movement, relativeTo);
    }
}
