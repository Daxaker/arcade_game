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

    private Animator _animator;
    private static readonly int Forward = Animator.StringToHash("forward");
    private static readonly int Turn = Animator.StringToHash("turn");

    // Start is called before the first frame update
    void Start()
    {
        _player = gameObject.transform;
        _animator = GetComponentInChildren<Animator>();
    }

    void Move(Vector3 move)
    {
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        var res = transform.InverseTransformDirection(move);
        UpdateAnimator(res.x, res.z);
        _player.Translate(velocity * Time.fixedDeltaTime * move, relativeTo);
    }

    void UpdateAnimator(float turnAmount, float forwardAmount)
    {
        _animator.SetFloat(Forward, forwardAmount);
        _animator.SetFloat(Turn, turnAmount);
    }

    private void FixedUpdate()
    {
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse = new Vector3(mouse.x, 0, mouse.z); 
        _player.LookAt(mouse);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);

        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
        
        Move(movement);
    }
}
