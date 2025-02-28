using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player stats
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;

    //Gravity
    private bool isGrounded;
    private float gravity = -9.8f;

    //Jump
    public float JumpHeight = 3f;
    void Start()
    {
        controller = GetComponent<CharacterController>(); //Get CharacterController component from Player Object
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if Player collided with any collider
        //Change it to check if Player is colliding with "Ground tag" instead of checking from Character controller
        isGrounded = controller.isGrounded; 
    }

    //Recieves input from InputManager script
    //& apply them to character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirec = Vector3.zero;
        moveDirec.x = input.x;
        moveDirec.z = input.y;

        //Since we are moving along x and z axis
        controller.Move(transform.TransformDirection(moveDirec) * speed * Time.deltaTime);

        //Gravity control
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y<0)
        {
            playerVelocity.y = -2f; //(0,2,0)
        }
        controller.Move(playerVelocity * Time.deltaTime);
        //print(playerVelocity);
    }

    public void Jump()
    {
        //Check if Player isGrounded 
        if(isGrounded) //Jump only if Player is on Ground
        {
            //Calculate to which height player is allowed to jump
            playerVelocity.y = Mathf.Sqrt(JumpHeight * -3.0f * gravity); 
        }
    }
}
