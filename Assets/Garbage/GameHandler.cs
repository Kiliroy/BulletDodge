using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {
    /// <summary>
    /// GameHandler to be used for Level1. Generates projectiles and ends the game if the Player survives long enough
    /// </summary>
    int Timer;
    public int ProjectileID;
    public GameObject BlackProjDiagDownToUp;
    public GameObject BlackProjSinUpToDown;
	// Use this for initialization
	void Start () {
		
	}

    void GenerateBlackProjDiagDownToUp()
    {
        Instantiate(BlackProjDiagDownToUp);
        ProjectileID = ProjectileID + 1;
        BlackProjDiagDownToUp.GetComponent<MovementBlackProjDiagDownToUp>().ProjID = ProjectileID;
    }
	void GenerateBlackProjSinUpToDown()
    {
        Instantiate(BlackProjSinUpToDown);
        ProjectileID = ProjectileID + 1;
        BlackProjSinUpToDown.GetComponent<BlackProjSinUpToDownMovement>().ProjID = ProjectileID;
    }
	void FixedUpdate () {
        Timer = Timer + 1;
   
        if ((Timer % 5 == 0)&&Timer>100)
            GenerateBlackProjDiagDownToUp(); 
        if (Timer % 40 == 0)
            GenerateBlackProjSinUpToDown(); 
        if (Timer == 10000) SceneManager.LoadScene(2);
	}
}
