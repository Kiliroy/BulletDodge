using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MovementBlackProjDiagDownToUp : MonoBehaviour {
    /// <summary>
    /// Initializes, moves and destroys the projectile and damages player if the projectile "hits" the player (also destroys the projectile if that happens).
    /// The starting position along the x axis is randomly chosen from the given boundaries, along the y axis it is always Bottom.
    /// The projectile moves along a diagonal path given by SpeedInY and SpeedInX.
    /// </summary>


    public int ProjID; //Every projectile gets its unique ID. Might be of use.
    public GameObject Self; //This is to be the game object to which this script is attached (BlackProjDiagDownToUp)
    public LayerMask PlayerLayerMask; //Layer mask of the Player
    public GameObject Player; //The Player
    public float Bottom; //Where is the bottom of the screen along the x axis (the projectile starts there)
    public float RightEdge; //Where is the most right position where the projectiles should spawn along the x axis
    public float LeftEdge;  //Where is the most left position where the projectiles should spawn along the x axis
    public float Top; //Where is the top of the screen along the y axis
    public float SpeedInY; //How quickly the projectile should move along the y axis
    public float SpeedInX; //How quickly the projectile should move along the x axis

    //Initializes the position of the projectile. Also finds the Player GameObject.
    void Start () {
        int i=0;
        i = UnityEngine.Random.Range((int)0,(int)100);
        Vector3 StartVector = new Vector3(LeftEdge + i * ((RightEdge-LeftEdge)/100), Bottom, 0); //Randomizes the position, where the projectile spawns
        this.transform.position=StartVector;
        Player = GameObject.FindWithTag("Player");

    }
    //Moves the projectile along the proper vector, which is constant in y and x. Destroys the projectile once it reaches top of the screen.
    void MoveProjectile()
    {
        Vector3 MoveVector = new Vector3(SpeedInX*Time.deltaTime, SpeedInY*Time.deltaTime, 0);
        this.transform.Translate(MoveVector);
        if (this.transform.position.y > Top)
            Destroy(Self);
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
    // Calls MoveProjectile and DamagePlayer every frame
    void Update () {
        MoveProjectile();
        DamagePlayer();
	}
}
