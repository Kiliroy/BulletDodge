using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlackProjSinUpToDownMovement : MonoBehaviour
{
    /// <summary>
    /// Initializes, moves and destroys the projectile and damages player if the projectile "hits" the player (also destroys the projectile if that happens).
    /// The starting position along the x axis is randomly chosen from the given boundaries, along the y axis it is always Top.
    /// The trajectory copies the graph of x~-cos(y), with the proper parameters. 
    /// </summary>

    public int ProjID; //Every projectile gets its unique ID. Might be of use
    public GameObject Self; //This is to be the game object to which this script is attached (BlackProjSinUpToDown)
    public LayerMask PlayerLayerMask; //Layer mask of the Player
    public GameObject Player; //The Player
    public float AmplitudeInverse; //Inverse of how big the amplitude should be (bigger number=>smaller amplitude) NEVER set to 0
    public float SpeedInY; //How quickly the projectile should move along the Y axis
    public float PeriodInverse; //Inverse of how long the period should be (bigger number=>shorter period) if set to 0, the projectile will have constant position along x axis
    public float Top; //Where is the top of the screen along the y axis (the projectile starts there)
    public float Bottom; //Where is the bottom of the screen along the x axis (the projectile ends there)
    public float RightEdge; //Where is the most right position where the projectiles should spawn
    public float LeftEdge;  //Where is the most left position where the projectiles should spawn
    private float PhaseShift; //How much should the projectile be ahead in the period
    private float y; //Used to parameterize the movement along the x axis (x'=x'(y))

    // Initializes the position of the projectile and its phase shift. Also finds the Player GameObject.
    void Start()
    {
        int i = 0;
        i = UnityEngine.Random.Range((int)0,(int)100);
        Vector3 StartVector = new Vector3(LeftEdge + i * ((RightEdge-LeftEdge)/100), Top, 0); //Randomizes the position, where the projectile spawns
        this.transform.position = StartVector;
        Player = GameObject.FindWithTag("Player");
        i = UnityEngine.Random.Range((int)0,(int)100);
        PhaseShift = i * 2 * Mathf.PI / 100;  //Randomizes the phase shift of the projectile
        y = 0; //This can be whatever number

    }
    //Moves the projectile along the proper vector, which is constant in y and ~sin(y) in x. Destroys the projectile once it reaches bottom of the screen.
    void MoveProjectile()
    {
        Vector3 MoveVector = new Vector3(Mathf.Sin(PhaseShift+y)/AmplitudeInverse,  -SpeedInY, 0);
        //Since this moves the projectile in the direction of this vector, it is actually moving it along the graph of the primitive function of the function
        //used to parameterize x (the trajectory is the graph of -cosine, because the function of the tangent is sin)
        y = y + PeriodInverse;
        this.transform.Translate(MoveVector);
        if (this.transform.position.y < Bottom)
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
    //Calls DamagePlayer every frame
    void Update()
    { 
        DamagePlayer();
    }

    //Moves the projectile every 0.02 seconds
    private void FixedUpdate()
    {
        MoveProjectile();
    }
}
