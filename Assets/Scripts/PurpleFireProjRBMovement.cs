using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleFireProjRBMovement : MonoBehaviour {
    /// <summary>
    /// Initializes the projectile. Applies given primitive velocity to it. All the movement of the projectile is given by gravity and this primitive velocity.
    /// Destroys the projectile if it falls below the bottom of the screen. Also damages player if it hits him (in which case it destroys the projectile). 
    /// </summary>
    public int ProjID; //Every projectile gets its unique ID. Might be of use
    public GameObject Self; //This is to be the game object to which this script is attached (PurpleFireProjRBMovement)
    public LayerMask PlayerLayerMask; //Layer mask of the Player
    public GameObject Player; //The Player
    public Rigidbody2D RB; //Rigidbody of this object
    public float PosInX; //Spawn position in X
    public float PosInY; //Spawn position in Y
    public float PrimitiveForceX; //X part of the primitive velocity to be applied
    public float PrimitiveForceY; //Y part of the primitive velocity to be applied
    public float Bottom; //Bottom of the screen (should be set lower, in case the projectile is meant to spawn below the screen and fly up)

    //Initializes the starting position and applies the primitive velocity, finds the Player GameObject
    void Start () {
        Player = GameObject.FindWithTag("Player");
        Vector3 StartVector = new Vector3(PosInX, PosInY, 0);
        this.transform.position = StartVector;
        Vector2 PrimitiveForce = new Vector2(PrimitiveForceX, PrimitiveForceY);
        RB.velocity = PrimitiveForce;
    }

    //Casts a circle in the position of the projectile and checks if the collision box of the player is in there. If so, reduces Player's health and Destroys the projectile
    void DamagePlayer()
    {
        Vector2 CircleCastVector = new Vector2(Self.transform.position.x, Self.transform.position.y);
        Vector2 ZeroVector = new Vector2(0, 0);
        if (Physics2D.CircleCast(CircleCastVector, 0.20f, ZeroVector, 0, PlayerLayerMask))
        {
            Player.GetComponent<PlayerControl>().Health--;
            Destroy(Self);
        }
    }
    //Destroys the projectile if it falls below the bottom of the screen
    void DestroyProj()
    {
        if (this.transform.position.y < Bottom)
            Destroy(Self);
    }

    // Calls DamagePlayer and DestroyProj every frame
    void Update()
    {
        DamagePlayer();
        DestroyProj();
    }
}
