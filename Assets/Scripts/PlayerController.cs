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
        _animator = GetComponent<Animator>();
    }

    void Move(Vector3 move)
    {
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        var res = transform.InverseTransformDirection(move);
        UpdateAnimator(res.x * velocity, res.z * velocity);
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
        
        Move(movement);

        //тестирование анимации стрельбы

    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
           // _animator.SetBool("shooting", true);
            _animator.SetLayerWeight(1,1);

        }
        
        if(Input.GetMouseButtonUp(0))
        {
            //_animator.SetBool("shooting", false);
            _animator.SetLayerWeight(1,0);

        }
    }
}
