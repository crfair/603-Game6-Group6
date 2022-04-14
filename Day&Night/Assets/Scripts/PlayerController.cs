using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement Variables
    public float moveSpeed = 5f;
    public Rigidbody2D playerRB;
    Vector2 movement;

    // Player animator component
    public Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get the horizontal and veritcal input of the player and store info in a vector
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // set the floats in the player's animator to transition between different animation states
        playerAnim.SetFloat("Horizontal", movement.x);
        playerAnim.SetFloat("Vertical", movement.y);
        playerAnim.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // move the player character through their rigid body and the player input from the Update method
        playerRB.MovePosition(playerRB.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
