using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GreenFireProjYArctanXLogMovement : MonoBehaviour {
    /// <summary>
    /// Initializes, moves and destroys the projectile and damages player if the projectile "hits" the player (also destroys the projectile if that happens).
    /// The tangent of the trajecotry of the projectile si parameterized by the x variable and its formula is
    /// (MultiplicativeCoefLog*log(x*x*LogCoef),MultiplicativeCoefArctan(x*ArctanCoef)), change these coefficients to change the tangent, thus changing the
    /// trajectory.
    /// </summary>

    public int ProjID; //Every projectile gets its unique ID. Might be of use
    public GameObject Self; //This is to be the game object to which this script is attached (GreenFireProjYArctanXLog)
    public LayerMask PlayerLayerMask; //Layer mask of the Player
    public GameObject Player; //The Player
    public float PosInX; //Spawn position in X
    public float PosInY; //Spawn position in Y
    public float SpeedInX; //Speed at which the variable x changes
    public float x; //Used to parameterize tangent (x',y') of the trajectory of the projectile, its starting value determines where on the graph the projectile starts
    public float MultiplicativeCoefArctan; //Multiplies the arctan function used to parameterize y'
    public float MultiplicativeCoefLog; //Multiplies the log function used to parameterize x'
    public float LogCoef; //Multiplies the argument of the log function used to parameterize x'
    public float ArctanCoef; //Multiplies the argument of the arctan function used to parameterize y'

    //Variables defining the edges of the screen used to determine whether or not the projectile should be destroyed
    public float Bottom;
    public float Top;
    public float RightEdge;
    public float LeftEdge;

    //Initializes the position and finds the Player GameObject
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Vector3 StartVector = new Vector3(PosInX, PosInY, 0);
        this.transform.position = StartVector;
    }

    //Moves the projectile and destroys it if it gets outside the screen
    void MoveProjectile()
    {
        Vector3 MoveVector = new Vector3(MultiplicativeCoefLog * Mathf.Log(x * x * LogCoef), MultiplicativeCoefArctan * Mathf.Atan(ArctanCoef * x), 0);
        this.transform.Translate(MoveVector);
        x = x + SpeedInX;
        if (this.transform.position.x > RightEdge || this.transform.position.x < LeftEdge || this.transform.position.y > Top || this.transform.position.y < Bottom)
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

    //Calls MoveProjectile every 0.02 seconds
    private void FixedUpdate()
    {
        MoveProjectile();
    }

    //Calls DamagePlayer every frame
    void Update()
    {
        DamagePlayer();
    }
}
