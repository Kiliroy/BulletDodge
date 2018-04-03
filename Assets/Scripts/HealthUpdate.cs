using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpdate : MonoBehaviour {
    /// <summary>
    /// Updates the text, so that it shows actual Health of the Player
    /// </summary>
    private Text HealthText; //Text to which the script is attached
    public GameObject Player; //The Player
	//Initialization of HealthText
	void Start () {
        HealthText = GetComponent<Text>();
	}
	
	// Updates the text
	void Update () {
        HealthText.text = "Health:" + Player.GetComponent<PlayerControl>().Health;
	}
}
