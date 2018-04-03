using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameHandler2 : MonoBehaviour {
    /// <summary>
    /// GameHandler to be used for level 1. Generates projectiles and ends the game if the Player has survived long enough.
    /// </summary>
    public int ProjectileID; //Variable used for assigning each projectile a different ID (Basically a projectile counter).

    //AudioSource for this level (each level just has one song ~ AudioSource)
    public AudioSource MusicForThisLevel;

    //Prefabs with the same name are supposed to be assigned to the following variables
    public GameObject BlackProjDiagDownToUp;
    public GameObject BlackProjSinUpToDown;
    public GameObject FireProjWithRBShot;
    public GameObject FireProjExponential;
    public GameObject PurpleFireProjRB;
    public GameObject BlueProjTargetPlayer;
    public GameObject GreenFireProjYArctanXLog;

    //Text that shows the remaining time of the level
    public Text TimerText;

    //Following variables are used to controll the generation of all kinds of projectiles throughout the game. To really see what they do, one needs to read the code of FixedUpdate.
    private int Timer; //Controls all the projectile generation and ending of the level.
    private int BlackProjStartControll; //Controlls generation of BlackProjDiagDownToUp at the start of the level
    private int BlueProjControll; //Controlls generation of BlueProjTargetPlayer
    private int FireProjControll; //Controlls generation of FireProjWithRBShot
    private int FireProjExpControll; //Controlls generation of FireProjExponential
    private int Seconds; //Seconds remaining till the end of the level

    //A shortcut for assigning the proper values to the Movement scripts. You're not allowed to work with
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
    void LocalAssignToBlueProjTargetPlayerMovement(float PosInX, float PosInY, float Speed)
    {
        BlueProjTargetPlayer.GetComponent<BlueProjTargetPlayerMovement>().PosInX = PosInX;
        BlueProjTargetPlayer.GetComponent<BlueProjTargetPlayerMovement>().PosInY = PosInY;
        BlueProjTargetPlayer.GetComponent<BlueProjTargetPlayerMovement>().Speed = Speed;
    }
    void LocalAssignToGreenFireProjYArctanXLogMovement(float PosInX, float PosInY, float SpeedInX, float X, float MultiplicativeCoefArctan, float MultiplicativeCoefLog, float LogCoef, float ArctanCoef)
    {
        GreenFireProjYArctanXLog.GetComponent<GreenFireProjYArctanXLogMovement>().PosInX = PosInX;
        GreenFireProjYArctanXLog.GetComponent<GreenFireProjYArctanXLogMovement>().PosInY = PosInY;
        GreenFireProjYArctanXLog.GetComponent<GreenFireProjYArctanXLogMovement>().SpeedInX = SpeedInX;
        GreenFireProjYArctanXLog.GetComponent<GreenFireProjYArctanXLogMovement>().x = X;
        GreenFireProjYArctanXLog.GetComponent<GreenFireProjYArctanXLogMovement>().MultiplicativeCoefArctan = MultiplicativeCoefArctan;
        GreenFireProjYArctanXLog.GetComponent<GreenFireProjYArctanXLogMovement>().MultiplicativeCoefLog = MultiplicativeCoefLog;
        GreenFireProjYArctanXLog.GetComponent<GreenFireProjYArctanXLogMovement>().LogCoef = LogCoef;
        GreenFireProjYArctanXLog.GetComponent<GreenFireProjYArctanXLogMovement>().ArctanCoef = ArctanCoef;
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
    //Generates PurpleFireProjRB, assigns proper values to it
    void GeneratePurpleFireProjRB(float PosInX, float PosInY, float PrimitiveForceX, float PrimitiveForceY)
    {
        LocalAssignToPurpleFireProjRBMovement(PosInX, PosInY, PrimitiveForceX, PrimitiveForceY);
        Instantiate(PurpleFireProjRB);
        ProjectileID = ProjectileID + 1;
        PurpleFireProjRB.GetComponent<PurpleFireProjRBMovement>().ProjID = ProjectileID;
    }
    //Generates BlueProjTargetPlayer, assigns proper values to it
    void GenerateBlueProjTargetPlayer(float PosInX, float PosInY, float Speed)
    {
        LocalAssignToBlueProjTargetPlayerMovement(PosInX, PosInY, Speed);
        Instantiate(BlueProjTargetPlayer);
        ProjectileID = ProjectileID + 1;
        BlueProjTargetPlayer.GetComponent<BlueProjTargetPlayerMovement>().ProjID = ProjectileID;
    }
    //Generates GreenFireProjYArctanXLog, assigns proper values to it
    void GenerateGreenFireProjYArctanXLog(float PosInX, float PosInY, float SpeedInX, float X, float MultiplicativeCoefArctan, float MultiplicativeCoefLog, float LogCoef, float ArctanCoef)
    {
        LocalAssignToGreenFireProjYArctanXLogMovement(PosInX, PosInY, SpeedInX, X, MultiplicativeCoefArctan, MultiplicativeCoefLog, LogCoef, ArctanCoef);
        Instantiate(GreenFireProjYArctanXLog);
        ProjectileID = ProjectileID + 1;
        GreenFireProjYArctanXLog.GetComponent<GreenFireProjYArctanXLogMovement>().ProjID = ProjectileID;
    }

    //Changes the Text showing the remaining time
    void ReduceRemainingTime()
    {
        if (Seconds % 60 >= 10)
            TimerText.GetComponent<Text>().text = (Seconds / 60).ToString() + " : " + (Seconds % 60).ToString();
        else
            TimerText.GetComponent<Text>().text = (Seconds / 60).ToString() + " : 0" + (Seconds % 60).ToString();
    }

    // Initialization
    void Start () {
        ProjectileID = 0;
        Seconds = 8400 / 50;
	}
    
    //Generation of projectiles throughout the whole game. FixedUpdate is called every 0.02 seconds, so every 50 ticks on the Timer correspond to 1 second real time.
    void FixedUpdate () {
        Timer++;

        //Changing the TimerText
        if (Timer % 50 == 0)
        {
            Seconds--;
            ReduceRemainingTime();
        }

        //Start of the level
        if (Timer % 40==0 && BlackProjStartControll<=10)
        {
            float f = (float)BlackProjStartControll;
            GenerateBlackProjDiagDownToUp(-10f + (f/10f) * 20f, -9.5f + (f/10f) * 20f, 0.7f, 1.5f);
            BlackProjStartControll++;
        }

        //Blue projectiles that start spawning after the start of the level
        if (Timer % 60 == 0 && Timer >= 300 && BlueProjControll<=10)
        {
            GenerateBlueProjTargetPlayer(-10f, -3.9f + ((float)BlueProjControll / 10f) * 10.5f, 0.04f);
        }
        if (Timer % 60 == 30 && Timer >= 300 && BlueProjControll <= 10)
        {
            GenerateBlueProjTargetPlayer(10f, -3.9f + ((float)BlueProjControll / 10f) * 10.5f, 0.04f);
            BlueProjControll++;
        }

        //FireProjWithRBShot from the top
        if (Timer % 50 == 0 && Timer>=800 && FireProjControll<=20)
        {
            GenerateFireProjWithRBShot(-10f + ((float)FireProjControll / 20f) * 20f, 6.6f);
        }
        if (Timer % 50 == 25 && Timer >= 800 && FireProjControll<=20)
        {
            GenerateFireProjWithRBShot(10f - ((float)FireProjControll / 20f) * 20f, 6.6f);
            FireProjControll++;
        }

        //FireProjExponential from both sides
        if (Timer % 100 == 0 && Timer>=1300 && Timer<=2500)
        {
            if (FireProjExpControll % 2 == 0)
                GenerateFireProjExponential(-10f, -3.9f, 0.1f, 0.5f, 0.1f, -10, 0.1f);
            else
                GenerateFireProjExponential(10f, -3.9f, -0.1f, 0.5f, 0.1f, -10, 0.1f);
            FireProjExpControll++;
        }

        //BlueProjTargetPlayer from the Top for the rest of the level
        if (Timer % 35 == 0 && Timer >=1600)
        {
            float f = UnityEngine.Random.Range(-10f,10f);
            GenerateBlueProjTargetPlayer(f, 6.6f, 0.1f);
        }

        //BlackProjDiagDownToUp blocking left side, FireProjExponential blocking ground on right side (forces the player to use the platforms), and constant fire of FireProjWithRBShot from the right side
        if (Timer>2500 && Timer<=6000)
        {
            if (Timer % 5 == 0 && Timer < 4000)
                GenerateBlackProjDiagDownToUp(0, -10, 0.2f, 1.5f);
            if (Timer % 300 == 0 && Timer <4200)
            {
                GenerateFireProjExponential(10f, -3.9f, -0.1f, 0.5f, 0.1f, -10, 0.1f);
            }
            if (Timer % 120 == 2)
                GenerateFireProjWithRBShot(10f, 3f);
        }

        //Randomized purple projectiles flying from the top with some randomzied horizontal velocity
        if (Timer>=4000 && Timer<=6000)
        {
            if (Timer % 40==15)
            {
                float x = UnityEngine.Random.Range(-5f, 5f);
                float PVX = UnityEngine.Random.Range(-4f, 4f);
                GeneratePurpleFireProjRB(x, 6.6f, PVX, 0);
            }
        }

        //Sine projectiles blocking the right side
        if (Timer % 20 == 0 && Timer > 5000 && Timer<=6000)
            GenerateBlackProjSinUpToDown(-3f, 10f, 0.03f, 20f, 0.08f);

        //Change of pace at the end. GreenFireProjYArctanXLog blocking the middle, PurpleFireProjRB blocking both edges
        if (Timer > 6000 && Timer <=8000)
        {
            if (Timer % 35 == 0)
                GenerateGreenFireProjYArctanXLog(-12f, 5f, 0.03f, -6f, 0.03f, 0.04f, 1f, 6f);
            if (Timer % 40 == 0)
                GeneratePurpleFireProjRB(-9.5f, -5f, 0.5f, 6f);
            if (Timer % 40 == 20)
                GeneratePurpleFireProjRB(9.5f, -5f, -0.5f, 6f);
        }

        //FireProjWithRBShot coming from the middle of the Top
        if (Timer % 120 == 0 && Timer > 6800 && Timer <= 8000)
            GenerateFireProjWithRBShot(0f, 6.6f);

        //Ends the level
        if (Timer >= 8400)
            SceneManager.LoadScene(3);

        //Starting music
        if (Timer == 800)
            MusicForThisLevel.Play();

        //Fading out music
        if (Timer >= 8300)
            MusicForThisLevel.volume = MusicForThisLevel.volume - 0.01f;
    }
}
