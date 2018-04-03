using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueProjTargetPlayerMovement : MonoBehaviour {
    /// <summary>
    /// Initializes, moves and destroys the projectile and damages player if the projectile "hits" the player (also destroys the projectile if that happens).
    /// The starting position is given by PosInX and PosInY and the projectile automatically moves along the line connecting the Player and the starting
    /// position of the projectile
    /// </summary>

    public int ProjID; //Every projectile gets its unique ID. Might be of use.
    public float PosInX; //Spawn position in x
    public float PosInY; //Spawn position in y
    public float Speed; //Speed at which the projectile should move

    //Variables defining the edges of the screen used to determine whether or not the projectile should be destroyed
    public float Bottom; 
    public float Top; 
    public float RightEdge;
    public float LeftEdge;

    public GameObject Player; //The Player GameObject
    public GameObject Self; //This is to be the game object to which this script is attached (BlueProjTargetPlayerMovement)
    public LayerMask PlayerLayerMask; //Layer mask of the Player

    private Vector3 MoveVector; //The constant vector along which the projectile is supposed to move. 
    
    //Initializes the starting position and MoveVector, finds the Player GameObject
    void Start () {
        Player = GameObject.FindWithTag("Player");
        Vector3 StartVector = new Vector3(PosInX, PosInY, 0);
        this.transform.position = StartVector;
        MoveVector.x = (Player.transform.position.x - PosInX);
        MoveVector.y = (Player.transform.position.y - PosInY);
        MoveVector.z = 0;
        MoveVector.Normalize();
        MoveVector = Speed * MoveVector;
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

    //Moves the projectile every 0.02 seconds
    void FixedUpdate () {
        this.transform.Translate(MoveVector);
	}

    //Calls DamagePlayer and checks if the projectile is outside the screen, in which case it destroys it.
    private void Update()
    {
        DamagePlayer();
        if (this.transform.position.x > RightEdge || this.transform.position.x < LeftEdge || this.transform.position.y > Top || this.transform.position.y < Bottom)
            Destroy(Self);
    }
}
