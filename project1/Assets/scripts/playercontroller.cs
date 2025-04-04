using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
 // Rigidbody of the player.
 private Rigidbody rb; 

 private int count;

 // Movement along X and Y axes.
 private float movementX;
 private float movementY;
 private PlayerInput playerInput;

 // Speed at which the player moves.
 public float speed = 10;
 public TextMeshProUGUI countText;

 public GameObject winTextObject;

 // Start is called before the first frame update.
 void Start()
    {
 // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
        count = 0;
        winTextObject.SetActive(false);
    }
 
 // This function is called when a move input is detected.
 void OnMove(InputValue movementValue)
    {
 // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

 // Store the X and Y components of the movement.
        movementX = movementVector.x; 
        movementY = movementVector.y; 

    }

   void SetCountText() 
   {
       countText.text =  "Count: " + count.ToString();
       if (count >= 12)
       {
           winTextObject.SetActive(true);
       }
   }

 // FixedUpdate is called once per fixed frame-rate frame.
 private void FixedUpdate() 
    {
 // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

 // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed); 

        Debug.Log("Hello, these are my current input coordinates:");
        Debug.Log(movementX);
        Debug.Log(movementY);
    }

 void OnTriggerEnter (Collider other) 
   {
       if (other.gameObject.CompareTag("pickup")) 
       {
         other.gameObject.SetActive(false);
         count = count + 1;
         SetCountText();
       }
   }

  
}

