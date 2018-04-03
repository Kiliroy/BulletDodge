using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    public int ProjID;
    public GameObject Self;
    public LayerMask PlayerLayerMask;
    public GameObject Player;
    private float PhaseShift;
    // Use this for initialization
    void Start()
    {
        System.Random random = new System.Random();
        int i = 0;
        i = random.Next(100);
        Vector3 StartVector = new Vector3(0f + i * 0.042f, -1.9f, 0);
        this.transform.position = StartVector;
        Player = GameObject.FindWithTag("Player");
        i = random.Next(100);
        PhaseShift = i * 2 * Mathf.PI / 100;

    }

    void MoveProjectile()
    {
        Vector3 MoveVector = new Vector3(Mathf.Sin(0.6f * Time.deltaTime + PhaseShift), -0.6f * Time.deltaTime, 0);
        this.transform.Translate(MoveVector);
        if (this.transform.position.y > 1.8)
            Destroy(Self);
    }
    void DamagePlayer()
    {
        Vector2 CircleCastVector = new Vector2(Self.transform.position.x, Self.transform.position.y);
        Vector2 ZeroVector = new Vector2(0, 0);
        if (Physics2D.CircleCast(CircleCastVector, 0.1f, ZeroVector, 0, PlayerLayerMask))
        {
            Player.GetComponent<MovementScript>().Health--;
            Destroy(Self);
        }
    }
    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
        DamagePlayer();
    }
}
