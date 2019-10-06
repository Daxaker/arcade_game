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

    // Start is called before the first frame update
    void Start()
    {
        _player = gameObject.transform;
        animator = this.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //print(mouse.ToString());
        mouse = new Vector3(mouse.x, 0, mouse.z); 
        _player.LookAt(mouse);
        float animVert, animHoriz;


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        animHoriz = horizontal;
        animVert = vertical;


        //if (mouse.z < _player.position.z)
        //{
        //    animVert = -vertical;
        //}
        //if (mouse.x < _player.position.x)
        //{
        //    animHoriz = -horizontal;
        //}


        Vector3 animPos = new Vector3(animHoriz + _player.position.x, 0, animVert + _player.position.y);

        Vector3 newAnimPos =  Vector3.RotateTowards(animPos, mouse, 1, 0);

        Vector3 movement = new Vector3(horizontal, 0, vertical);

        Debug.Log("animPos:" + animPos);
        Debug.Log("newAnimPos:" + newAnimPos);

        animator.SetFloat("forward", newAnimPos.x - _player.position.x);
        animator.SetFloat("turn", newAnimPos.z - _player.position.z);


      

        _player.Translate(velocity * Time.fixedDeltaTime * movement, relativeTo);
    }
}
