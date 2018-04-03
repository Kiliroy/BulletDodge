using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireProjExponentialMovement : MonoBehaviour {
    /// <summary>
    /// Initializes, moves and destroys the projectile and damages player if the projectile "hits" the player (also destroys the projectile if that happens).
    /// The projectile trajectory is given by the starting position and the tangent in each point. The tangent is given by the formula
    /// (SpeedInX , MultiplicativeCoefficient*Exp(ExpCoefficient*x)). Change these coefficients to cahnge the tangent, thus changing the trajectory.
    /// </summary>

    public int ProjID; //Every projectile gets its unique ID. Might be of use
    public GameObject Self; //This is to be the game object to which this script is attached (FireProjExponential)
    public LayerMask PlayerLayerMask; //Layer mask of the Player
    public GameObject Player; //The Player
    public float SpeedInX; //How quickly the projectile moves along the x axis (can be negative)
    public float ExpCoefficient; //Coefficient inside the exp function
    public float MultiplicativeCoefficient; //Coefficient that multiplies the exp function.
    public float PosInX; //Spawn position in X
    public float PosInY; //Spawn position in Y
    public bool RandomizeY; //Whether or not the PosInY should be randomized
    public float RandomizeRange; //By how much at most PosInY can be changed. Recommended to keep it low.
    public float x; //Used to parameterize y' (y'=y'(x)), its starting value determines where on the exponential the projectile begins (it affects the exponential, not the other way round) 

    //These variables are used to determine whether the projectile left the screen and should be destroyed
    public float Top;
    public float Bottom;

    // Initializes the position of the projectile and finds the Player
    void Start () {
        Player = GameObject.FindWithTag("Player");
        Vector3 StartVector = new Vector3(PosInX, PosInY, 0);
        if (RandomizeY)
        {
            float i = UnityEngine.Random.Range(-RandomizeRange, RandomizeRange);
            StartVector.x += i;
        }
        this.transform.position = StartVector;
	}

    //Moves the projectile along the exponential trajectory
    void MoveProjectile()
    {
        Vector3 MoveVector = new Vector3(SpeedInX , MultiplicativeCoefficient*Mathf.Exp(ExpCoefficient*x) , 0);
        x = x + Mathf.Abs(SpeedInX);
        this.transform.Translate(MoveVector);
        if (this.transform.position.y > Top || this.transform.position.y < Bottom)
            Destroy(Self);
    }

    //Casts a circle in the position of the projectile and checks if the collision box of the player is in there. If so, reduces Player's health and Destroys the projectile
    void DamagePlayer()
    {
        Vector2 CircleCastVector = new Vector2(Self.transform.position.x, Self.transform.position.y);
        Vector2 ZeroVector = new Vector2(0, 0);
        if (Physics2D.CircleCast(CircleCastVector, 0.20f, ZeroVector, 0, PlayerLayerMask)) //Second argument has to be changed if the size of the projectile changes
        {
            Player.GetComponent<PlayerControl>().Health--;
            Destroy(Self);
        }
    }

    // DamagePlayer is called every frame
    void Update () {
        DamagePlayer();
	}

    //The projectile is moved every 0.02 seconds
    private void FixedUpdate()
    {
        MoveProjectile();
    }
}
