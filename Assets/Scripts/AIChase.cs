using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float moveSpeed;
    [SerializeField] float agroRange;

    private float distanceToPlayer;
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < agroRange && distanceToPlayer > 0.01)
        {
            //chasePlayerX();
            //chasePlayerY(); //Overwrites x movement
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed);
        }
        else
        {
            stopChasingPlayer();
        }
        
    }
    /*
    bool canSeePlayer(float agroDistance)
    {
        bool val = false;
        float castDist = agroDistance;
        Vector2 endPos = cast
        RaycastHit2D hit = Physics2D.Linecast(transform.position, );    
    }
    */
    void chasePlayerX()
    {

        if (transform.position.x < player.transform.position.x)
        {
            //Enemy is on the left of player
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.x >= player.transform.position.x)
        {
            //Enemy is on the right of player
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
    }
    void chasePlayerY() 
    {
        if (transform.position.y < player.transform.position.y)
        {
            rb2d.velocity = new Vector2(0, moveSpeed);
            transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.y > player.transform.position.y)
        {
            rb2d.velocity = new Vector2(0, -moveSpeed);
            transform.localScale = new Vector2(1, 1);

        }
    }
    void stopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
    }
}
