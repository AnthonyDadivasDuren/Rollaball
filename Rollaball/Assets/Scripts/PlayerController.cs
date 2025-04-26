using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{   
    // Speed of the player
    public float Speed = 0;
    
    // UI text component to display count of "PickUp" objects collected.
    public TextMeshProUGUI countText;
    
    // UI object to display winning text.
    public GameObject winTextGameObject;
    
    // Rigid Body of the player 
    private Rigidbody rb;
    
    // Variable to keep track of collected "PickUp" objects.
    private int count;
    // Movement along X and Y axis
    private float movementX;
    private float movementY;
    
    //variables for the dash system
    public float dashSpeed = 10.0f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1.0f;
    
    private bool canDash = true;
    private bool isDashing = false;
    private float dashTimer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get and store the Rigid Body of the player
        rb = GetComponent<Rigidbody>();
        
        // Initialize count to zero.
        count = 0;
        
        // Update the count display.
        SetCountText();
        
        // Initially set the win text to be inactive.
        winTextGameObject.SetActive(false);
    }
    
    // This function is called when a move input is detected.
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
        //stops regular movement while dashing
        if (isDashing)
        {
            HandleDash();
        }
        else
        {
            // Create a 3D movement vector using the x and y input values
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        
            // Apply force to the Rigid Body to move the player
            rb.AddForce(movement * Speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);
            
            // Increment the count of "PickUp" objects collected.
            count = count + 1;
            
            // Update the count display.
            SetCountText();
        }
        
    }
    
    // Function to update the displayed count of "PickUp" objects collected.
    void SetCountText()
    {
        // Update the count text with the current count.
        countText.text = "Count: " + count.ToString();
        
        // Check if the count has reached or exceeded the win condition.
        if (count >= 12)
        {
            // Display the win text.
            winTextGameObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Destroy the current object
            Destroy(gameObject);
            // Update winText to display "You Lose"
            winTextGameObject.SetActive(true);
            winTextGameObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }

    private void OnDash(InputValue value)
    {
        //only allow dashing if not already dashing or there is no dash cooldown
        if (canDash && !isDashing)
        {
            StartDash();
        }
    }
    
    private void StartDash()
    {
        isDashing = true;
        canDash = false;
        dashTimer = dashDuration;
        
        //Get the players direction to calculate the dash direction 
        Vector3 dashDirection = new Vector3(movementX, 0.0f, movementY);
    
        //if there is no input then dash in the players direction
        if (dashDirection == Vector3.zero)
        {
            dashDirection  = transform.forward; //default to forward
        }
        
        // apply dash force
        rb.linearVelocity = dashDirection * dashSpeed;
    }

    private void HandleDash()
    {
        // decrease remaining dash time
        dashTimer -= Time.fixedDeltaTime;
        
        //check if dash should stop
        if (dashTimer <= 0)
        {
            isDashing = false;
            rb.linearVelocity = Vector3.zero; //stops dash movement
            StartCoroutine(DashCooldown());
        }
    }
    
    private IEnumerator DashCooldown()
    {
        // wait for cooldown period
        yield return new WaitForSeconds(dashCooldown);
        //allow dash again
        canDash = true;
    }
}