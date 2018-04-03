using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class GameHandler1 : MonoBehaviour {
    /// <summary>
    /// GameHandler to be used for level 1. Generates projectiles and ends the game if the Player has survived long enough.
    /// </summary>
    public int ProjectileID; //Variable used for assigning each projectile a different ID (Basically a projectile counter).

    //AudioSource for this level (each level just has one song ~ AudioSource)
    public AudioSource MusicForThisLevel;

    //Text that shows the remaining time of the level
    public Text TimerText;

    //Prefabs with the same name are supposed to be assigned to the following variables
    public GameObject BlackProjDiagDownToUp;
    public GameObject BlackProjSinUpToDown;
    public GameObject FireProjWithRBShot;
    public GameObject FireProjExponential;
    public GameObject PurpleFireProjRB;

    //Following variables are used to controll the generation of all kinds of projectiles throughout the game. To really see what they do, one needs to read the code of FixedUpdate.
    private int FireProjRBController; //Parity of this decides whether the FireProjWithRBShot are spawned on the left edge, or the right edge of the screen.
    private int FireProjExpController; //Parity of this decides whether the FireProjExponential are spawned on the left edge, or the right edge of the screen.
    private int EndingController; //Controls the angle at which the final purple projectiles are fired.
    private int EndTime; //Value of the timer at which the level ends.
    private int Timer; //Controls all the projectile generation and ending of the level.
    private int Seconds; //Seconds remaining till the end of the level

    //A shortcut for assigning the proper values to the MovementBlackProjDiagDownToUp script. You're not allowed to work with
    //the script itself as if it was a variable, so it has to be done in this way (or macros).
    //Following functions do the same for different kinds of projectiles.
    void LocalAssignToMovementBlackProjDiagDownToUp(float RightEdge, float LeftEdge, float SpeedInX, float SpeedInY)
    {
        BlackProjDiagDownToUp.GetComponent<MovementBlackProjDiagDownToUp>().RightEdge = RightEdge;
        BlackProjDiagDownToUp.GetComponent<MovementBlackProjDiagDownToUp>().LeftEdge = LeftEdge;
        BlackProjDiagDownToUp.GetComponent<MovementBlackProjDiagDownToUp>().SpeedInX = SpeedInX;
        BlackProjDiagDownToUp.GetComponent<MovementBlackProjDiagDownToUp>().SpeedInY = SpeedInY;
    }
    void LocalAssignToBlackProjSinUpToDownMovement(float RightEdge, float LeftEdge, float SpeedInY, float AmplitudeInverse, float PeriodInverse)
    {
        BlackProjSinUpToDown.GetComponent<BlackProjSinUpToDownMovement>().RightEdge = RightEdge;
        BlackProjSinUpToDown.GetComponent<BlackProjSinUpToDownMovement>().LeftEdge = LeftEdge;
        BlackProjSinUpToDown.GetComponent<BlackProjSinUpToDownMovement>().SpeedInY = SpeedInY;
        BlackProjSinUpToDown.GetComponent<BlackProjSinUpToDownMovement>().AmplitudeInverse = AmplitudeInverse;
        BlackProjSinUpToDown.GetComponent<BlackProjSinUpToDownMovement>().PeriodInverse = PeriodInverse;
    }
    void LocalAssignToFireProjWithRBShotMovement(float PosInX, float PosInY)
    {
        FireProjWithRBShot.GetComponent<FireProjWithRBShotMovement>().PosInX = PosInX;
        FireProjWithRBShot.GetComponent<FireProjWithRBShotMovement>().PosInY = PosInY;
    }
    void LocalAssingToFireProjExponentialMovement(float PosInX, float PosInY, float SpeedInX, float ExpCoef, float MultCoef, float X, float RandomizeRange)
    {
        FireProjExponential.GetComponent<FireProjExponentialMovement>().PosInX = PosInX;
        FireProjExponential.GetComponent<FireProjExponentialMovement>().PosInY = PosInY;
        FireProjExponential.GetComponent<FireProjExponentialMovement>().SpeedInX = SpeedInX;
        FireProjExponential.GetComponent<FireProjExponentialMovement>().ExpCoefficient = ExpCoef;
        FireProjExponential.GetComponent<FireProjExponentialMovement>().MultiplicativeCoefficient = MultCoef;
        FireProjExponential.GetComponent<FireProjExponentialMovement>().x = X;
        FireProjExponential.GetComponent<FireProjExponentialMovement>().RandomizeY = !(RandomizeRange == 0);
        FireProjExponential.GetComponent<FireProjExponentialMovement>().RandomizeRange = RandomizeRange;
    }
    void LocalAssignToPurpleFireProjRBMovement(float PosInX, float PosInY, float PrimitiveForceX, float PrimitiveForceY)
    {
        PurpleFireProjRB.GetComponent<PurpleFireProjRBMovement>().PosInX = PosInX;
        PurpleFireProjRB.GetComponent<PurpleFireProjRBMovement>().PosInY = PosInY;
        PurpleFireProjRB.GetComponent<PurpleFireProjRBMovement>().PrimitiveForceX = PrimitiveForceX;
        PurpleFireProjRB.GetComponent<PurpleFireProjRBMovement>().PrimitiveForceY = PrimitiveForceY;
    }

    //Generates BlackProjDiagDownToUp, assigns proper values to it
    void GenerateBlackProjDiagDownToUp(float RightEdge, float LeftEdge, float SpeedInX, float SpeedInY)
    {
        LocalAssignToMovementBlackProjDiagDownToUp(RightEdge, LeftEdge, SpeedInX, SpeedInY);
        Instantiate(BlackProjDiagDownToUp);
        ProjectileID = ProjectileID + 1;
        BlackProjDiagDownToUp.GetComponent<MovementBlackProjDiagDownToUp>().ProjID = ProjectileID;
    }
    //Generates BlackProjSinUpToDown, assigns proper variables to it
    void GenerateBlackProjSinUpToDown(float RightEdge, float LeftEdge, float SpeedInY, float AmplitudeInverse, float PeriodInverse)
    {
        LocalAssignToBlackProjSinUpToDownMovement(RightEdge, LeftEdge, SpeedInY, AmplitudeInverse, PeriodInverse);
        Instantiate(BlackProjSinUpToDown);
        ProjectileID = ProjectileID + 1;
        BlackProjSinUpToDown.GetComponent<BlackProjSinUpToDownMovement>().ProjID = ProjectileID;
    }
    //Generates FireProjWithRBShot, assigns proper values to it
    void GenerateFireProjWithRBShot(float PosInX, float PosInY)
    {
        LocalAssignToFireProjWithRBShotMovement(PosInX, PosInY);
        Instantiate(FireProjWithRBShot);
        ProjectileID = ProjectileID + 1;
        FireProjWithRBShot.GetComponent<FireProjWithRBShotMovement>().ProjID = ProjectileID;
    }
    //Generates FireProjExponential, assigns proper values to it
    void GenerateFireProjExponential(float PosInX, float PosInY, float SpeedInX, float ExpCoef, float MultCoef, float X, float RandomizeRange)
    {
        LocalAssingToFireProjExponentialMovement(PosInX, PosInY, SpeedInX, ExpCoef, MultCoef, X, RandomizeRange);
        Instantiate(FireProjExponential);
        ProjectileID = ProjectileID + 1;
        FireProjExponential.GetComponent<FireProjExponentialMovement>().ProjID = ProjectileID;
    }
    //Generate PurpleFireProjRB, assigns proper values to it
    void GeneratePurpleFireProjRB(float PosInX, float PosInY, float PrimitiveForceX, float PrimitiveForceY)
    {
        LocalAssignToPurpleFireProjRBMovement(PosInX, PosInY, PrimitiveForceX, PrimitiveForceY);
        Instantiate(PurpleFireProjRB);
        ProjectileID = ProjectileID + 1;
        PurpleFireProjRB.GetComponent<PurpleFireProjRBMovement>().ProjID = ProjectileID;
    }

    //Changes the Text showing the remaining time
    void ReduceRemainingTime()
    {
        if (Seconds % 60 >= 10)
            TimerText.GetComponent<Text>().text = (Seconds / 60).ToString() + " : " + (Seconds % 60).ToString();
        else
            TimerText.GetComponent<Text>().text = (Seconds / 60).ToString() + " : 0" + (Seconds % 60).ToString();
    }

    //Initialization of variables
    private void Start()
    {
        FireProjExpController = 0;
        FireProjRBController = 0;
        EndingController = 0;
        EndTime = 0;
        Seconds = 8500 / 50;
    }

    //Generation of projectiles throughout the whole game. FixedUpdate is called every 0.02 seconds, so every 50 ticks on the Timer correspond to 1 second real time.
    void FixedUpdate () {
        Timer = Timer + 1;

        //Changing the TimerText
        if (Timer % 50 == 0)
        {
            Seconds--;
            ReduceRemainingTime();
        }
            

        //Projectile generation in the first part of the level. BlackProjDiagDownTopUp block the left side of the screen and BlackProjSinUpToDown are spawning on the right side of the screen.
        if ((Timer % 5 == 0)&&Timer>100&&Timer<2500)
            GenerateBlackProjDiagDownToUp(0f, -10f, 0.7f, 1.5f); 
        if (Timer % 40 == 0&&Timer<2250)
            GenerateBlackProjSinUpToDown(10f, 0f, 0.02f, 30f, 0.08f);

        //Projectile generation in the second part of the level. BlackProjDiagDownTopUp block the right side of the screen and BlackProjSinUpToDown are spawning on the left side of the screen.
        if ((Timer % 5 == 0) && Timer > 2700 && Timer<4800)
            GenerateBlackProjDiagDownToUp(10f, 0f, -0.7f, 1.5f);
        if (Timer % 55 == 0 && Timer > 2100 && Timer<4500)
            GenerateBlackProjSinUpToDown(0f, -10f, 0.02f, 30f, 0.08f);

        //Projectile generation throughout second, third and fourth part of the level. Shoots FireProjWithRBShot from left and right side (alternates).
        if (Timer % 300==0 && Timer>=2800)
        {
            if (FireProjRBController % 2 == 0)
                GenerateFireProjWithRBShot(-10f, 1f);
            else
                GenerateFireProjWithRBShot(10f, 1f);
            FireProjRBController++;
        }

        //Projectile generation throughout third and fourth part of the level. Spawns BlackProjSinUpToDown randomly across the whole width of the screen
        //and spawns FireProjExponential from both sides (alternates)
        if (Timer % 50 == 0 && Timer > 4500)
            GenerateBlackProjSinUpToDown(-10f, 10f, 0.02f, 25f, 0.08f);
        if (Timer % 80 == 0 && Timer > 5000)
        {
            if (FireProjExpController % 2 == 0)
                GenerateFireProjExponential(-10f, -3.9f, 0.1f, 0.5f, 0.1f, -10, 0.1f);
            else
                GenerateFireProjExponential(10f, -3.9f, -0.1f, 0.5f, 0.1f, -10, 0.1f);
            FireProjExpController++;
        }

        //Projectile generation throughout fourth part of the level. Spawns PurpleFireProj in the middle. It flies up and down. Shoots FireProjWithRBShot from both sides at the same time
        if (Timer % 350 == 0 && Timer > 6300)
        {
            GeneratePurpleFireProjRB(0, -5f, 0, 8);
        }
        if (Timer % 220 ==0 && Timer>7000)
        {
            GenerateFireProjWithRBShot(-10f, 4f);
            GenerateFireProjWithRBShot(10f, 4f);
        }

        //Projectile generation at the very end of the level. Shoots PurpleFireProjRB from the sides in a circular fashion.
        if (Timer % 30 == 0 && Timer>8000 && EndingController<8)
        {
            float EndingControllerF = (float)EndingController;
            float AngleShift = (EndingControllerF / 7)*Mathf.PI*(45f/180f);
            GeneratePurpleFireProjRB(-10f, -5f, 7 * Mathf.Cos((40f / 180f)*Mathf.PI + AngleShift), 7 * Mathf.Sin((40f / 180f) * Mathf.PI + AngleShift));
            GeneratePurpleFireProjRB(10f, -5f, -7 * Mathf.Cos((40f / 180f) * Mathf.PI + AngleShift), 7 * Mathf.Sin((40f / 180f) * Mathf.PI + AngleShift));
            EndingController++;
        }

        //Ends the level
        if (Timer >= 8500)
            SceneManager.LoadScene(3);

        //Fading out music
        if (Timer >= 8400)
            MusicForThisLevel.volume = MusicForThisLevel.volume - 0.01f;
       
	}
}
