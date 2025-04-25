using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    // Speed of the player
    public float Speed = 0;
    
    // Rigid Body of the player 
    private Rigidbody rb;
    // Movement along X and Y axis
    private float movementX;
    private float movementY;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get and store the Rigid Body of the player
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementValue)
    {
        // Convert input value to Vector2 for movement
        Vector2 movementVector = movementValue.Get<Vector2>();
        
        // Store movement values along X and Y axis
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        // Create a 3D movement vector using the x and y input values
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        
        // Apply force to the Rigid Body to move the player
        rb.AddForce(movement * Speed);

     
    }
    

}