﻿using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Mover : MonoBehaviour
{

    private GamePadState state;
    private GamePadState prevState;
    private Vector3 speed = new Vector3(10, 10, 0);
    private Vector3 movement;
    private float inputX, inputY;

    public bool isGrounded;

    void Start()
    {
        state = GamePad.GetState(PlayerIndex.One);
        prevState = state;

    }

    // Update is called once per frame
    private void Update()
    {

        state = GamePad.GetState(PlayerIndex.One);

        inputX = state.ThumbSticks.Left.X;
        inputY = state.ThumbSticks.Left.Y;

        movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);

        //transform.Translate(movement * Time.deltaTime);

        if (state.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Released)
        {
            prevState = state;
            
            if (isGrounded)
                rigidbody2D.AddForce(Vector2.up * 500);
        }

        prevState = state;

    }

    private bool CheckIfGrounded()
    {

        // dont work, since it hits myself or my clone ...

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, 0.1f);

        print(hit.transform.name);
        if (hit != null && hit.transform.name != "Player1_clone" && hit.transform.name != transform.name)
            return true;
        else
            return false;

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "floor")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "floor")
        {
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {

        Vector3 v3 = rigidbody2D.velocity;
        v3 = new Vector3(movement.x, v3.y, v3.z);
        rigidbody2D.velocity = v3;
    }
}