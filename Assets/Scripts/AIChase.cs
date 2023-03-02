using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using Pathfinding;

public class AIChase : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform FOV;
    [SerializeField] float moveSpeed;
    [SerializeField] float nextWaypointDistance;
    [SerializeField] Vector3[] positions;

    private int index;
    public FieldOfView fieldOfView;
    private bool inView;
    private float rotationSpeed = 6f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    float movementDir;

    Seeker seeker;
    Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }
    void FixedUpdate()
    {

        //Checks if player is in FOV
        inView = fieldOfView.canSeePlayer;
        animator.SetBool("isMoving", false);

        //Checks if there is a path and checkpoints to move along
        if (path == null)
            return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        
        if (inView)
        {
            //Rotates enemy FOV towards player
            Vector3 faceDirection = player.position - FOV.position;
            float angle = Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg;
            FOV.rotation = Quaternion.RotateTowards(FOV.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed);
        }
        else
        {                
            //Loops designated points
            float posDistance = Vector2.Distance(rb.position, positions[index]);
            if (posDistance <= 0.05)
            {
                if (index == positions.Length - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }

            //Rotates enemy FOV to the direction of the next waypoint
            Vector3 faceDirection = path.vectorPath[currentWaypoint] - FOV.position;
            float angle = Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg;
            FOV.rotation = Quaternion.RotateTowards(FOV.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed);    
            print(faceDirection);

            // animation of moving in the direction of angle
            animator.SetFloat("angle", angle);

        }       

        rb.position = Vector2.MoveTowards(rb.position, ((Vector2)path.vectorPath[currentWaypoint]), Time.deltaTime * moveSpeed);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // handle animation
        movementDir = FOV.rotation.eulerAngles.z;
        Debug.Log(movementDir);
        animator.SetFloat("angle", movementDir / 360);
    }
    void UpdatePath()
    {
        Vector3 target;

        if (inView)
        {
            target = player.position;
        }
        else
        {
            target = positions[index];
        }

        if (seeker.IsDone())
            seeker.StartPath(rb.position, target, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


}
