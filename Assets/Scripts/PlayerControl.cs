using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {
    /// <summary>
    /// Moves the player and has Health variable. Moving left and right is straight forward. Jumping is only allowed when CanJump is true. The truth value of CanJump is
    /// decided by whether GroundCheck.Transform is sufficiently near the ground. GroundCheck is a simple GameObject, child of the Player with Transform whose position
    /// is used for checking for the ground.
    /// </summary>

    public int Health; //Health of the player. Should start at 7 (for level 1), 11 (for level 2), is lowered by 1 every time a projectile hits the player (that is done by projectiles) 
    public Transform TF; //Transform of THIS object (the Player)
    public Rigidbody2D RB; //Rigidbody of THIS object (the Player)
    public BoxCollider2D BC; //BoxCollider of THIS object (the Player)
    public float JumpForce; //Velocity the Player should gain when jumping
    public bool CanJump; //Bool indicating, whether the Player is grounded or not
    public LayerMask WhatIsGround; //Layer mask of objects which it is possible to stand on
    public Transform GroundCheck; //Transform of GroundCheck, which is supposed to be a GameObject with Transform, that is attached to the bottom of the Player
    public float MovementSpeed; //How quickly the Player moves left and right
    private Vector2 JumpVector; //Vector of the velocity added when jumping

    //Moving left and right is done in FixedUpdate
    private void FixedUpdate()
    {
        if (Input.GetKey("left"))
        {
            TF.Translate(-MovementSpeed, 0, 0);
        }
        if (Input.GetKey("right"))
        {
            TF.Translate(+MovementSpeed, 0, 0);
        }
    }

    // Jumping and "killing" is done in Update
    void Update () {
        if (Health <= 0) SceneManager.LoadScene(0); //Returns to main menu if the Player dies


        Vector2 PosOffset = new Vector2(GroundCheck.position.x + (BC.size.x * TF.localScale.x), GroundCheck.position.y - 0.01f); //Vector of the bottom right edge of the OverlapArea rectangle
        CanJump = Physics2D.OverlapArea(GroundCheck.position, PosOffset, WhatIsGround);  //Checks if there is ground below the Player
        JumpVector.x = 0;
        JumpVector.y = JumpForce;
        if (Input.GetKey(KeyCode.Space) && CanJump)
        {
            RB.velocity = JumpVector;
        }
    }
}
