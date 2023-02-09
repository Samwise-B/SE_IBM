using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform enemy;
    [SerializeField] float moveSpeed;
    

    public FieldOfView fieldOfView;
    private bool inView;
    private float rotationSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        inView = fieldOfView.canSeePlayer;

        if (inView)
        {
            //Rotates the enemy to face player
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed);

            chasePlayer();
        }
        
    }

    void chasePlayer()
    {
        enemy.position = Vector2.MoveTowards(enemy.position, player.position, moveSpeed);
    }
}
