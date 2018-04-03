using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireProjWithRBShotMovement : MonoBehaviour {
    /// <summary>
    /// Initializes the projectile. Applies primitive velocity so that combined with the movement caused by gravity, the projectile hits the position of the player.
    /// Destroys the projectile if it falls below the bottom of the screen and damages player if the projectile "hits" the player (also destroys the projectile if that happens).
    /// The projectile moves along a path given by the primitive velocity and gravity
    /// </summary>
    public int ProjID; //Every projectile gets its unique ID. Might be of use.
    public float PosInX; //Spawn position in x
    public float PosInY; //Spawn position in y
    public float Bottom; //Bottom of the screen
    public GameObject Player; //The Player GameObject
    public GameObject Self; //This is to be the game object to which this script is attached (FireProjWithRBShot)
    public LayerMask PlayerLayerMask; //Layer mask of the Player
    public Rigidbody2D RB; //Rigidbody of this object
    private Vector2 PrimitiveVelocity; //Vector of the primitive velocity
    private float g; //This should be the gravitational acceleration.
    /*Calculation for the primitive velocity is as follows. Let its y part by 0 (otherwise there is infinitely many solutions). Its x part should be
      (Player.x-PosInX)*sqrt(g)*(1/sqrt(abs(PosInY-Player.y)))*(1/sqrt(2)) where g is the gravitational acceleration.
      This is derived from the free fall formula and will NOT work if the projectile spawns below the player.
      In that case the PrimeVelocity will be as it would be in case the Player would be below the projectile in the same distance.
    */

    // Initializes the position and the PrimitiveVelocity and applies the PrimitiveVelocity. Also finds the Player GameObject
    void Start () {
        RB = GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player");
        Vector3 StartVector = new Vector3(PosInX, PosInY, 0);
        this.transform.position = StartVector;
        g = 9.8f; //No idea what is supposed to be here exactly, but this real life value seems to work well enough
        PrimitiveVelocity.y = 0;
        PrimitiveVelocity.x = (Player.GetComponent<Transform>().position.x - PosInX) * Mathf.Sqrt(g) * (1 / Mathf.Sqrt(Mathf.Abs(PosInY - Player.GetComponent<Transform>().position.y))) * (1 / Mathf.Sqrt(2));
        RB.velocity = PrimitiveVelocity;
    }

    //Casts a circle in the position of the projectile and checks if the collision box of the player is in there. If so, reduces Player's health and Destroys the projectile.
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
    void Update () {
        DamagePlayer();
        DestroyProj();
    }
}
