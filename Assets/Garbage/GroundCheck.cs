using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    public MovementScript PlayerMovement;
    public GameObject Player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player) PlayerMovement.CanJump = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == Player) PlayerMovement.CanJump = false;
    }
}
