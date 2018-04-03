using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementScript : MonoBehaviour {


    private int invincible=0;
    public int Health;
    public Transform TF;
    public Rigidbody2D RB;
    public BoxCollider2D BC;
    public float JumpForce;
    public bool CanJump;
    private Vector2 JumpVector;
    public LayerMask WhatIsGround;
    public LayerMask WhatIsProjectile;
    float GroundRadius;
    public Transform GroundCheck;

	// Use this for initialization
	void Start () {
	
	}

    /*private void GetShot()
    {
        if (invincible == 0)
        {
            Vector2 BottomRight = new Vector2(TF.position.x + 0.11f, TF.position.y - 0.12f);
            if (Physics2D.OverlapArea(TF.position,BottomRight,WhatIsProjectile))
            {
                invincible = 50;
                Health--;
            }
        }
    }*/


    private void FixedUpdate()
    {
        //if (invincible > 0) invincible--;
        //GetShot();
        Vector2 PosOffset=new Vector2(GroundCheck.position.x + 0.09f, GroundCheck.position.y - 0.01f);
        CanJump = Physics2D.OverlapArea(GroundCheck.position, PosOffset, WhatIsGround);
        if (Input.GetKey("left"))
        {
            TF.Translate(-2 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("right"))
        {
            TF.Translate(+2 * Time.deltaTime, 0, 0);
        }
    }

    // Update is called once per frame
    void Update () {
        if (Health <= 0) SceneManager.LoadScene(0);


        JumpVector.x = 0;
        JumpVector.y = JumpForce;
        if (Input.GetKey(KeyCode.Space)&&CanJump)
        {
            RB.velocity = JumpVector;
        }
    }
}
