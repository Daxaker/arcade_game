using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [Range(0, 255)]
    public float velocity = 0.5f;

    public Space relativeTo = Space.World;
    private Transform _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = gameObject.transform;
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
        
        float forward = 0;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            forward = 1;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            forward = -1;
        float strife = 0;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            strife = 1;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            strife = -1;

        Vector3 movement = new Vector3(strife,0,forward);
        _player.Translate(velocity * Time.fixedDeltaTime * movement, relativeTo);
    }
}
