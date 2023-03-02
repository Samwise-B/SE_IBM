using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] [Range(1, 360)] float angle;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] LayerMask obstructionLayer;
    [SerializeField] Transform player;

    public bool canSeePlayer = false;

    void Start()
    {
        
    }

    void Update()
    {
        FovCheck();
    }

    private void FovCheck()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        if (rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.right, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);
                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        Vector3 angle1 = DirectionFromAngle(transform.eulerAngles.z, (-angle/2) + 90);
        Vector3 angle2 = DirectionFromAngle(transform.eulerAngles.z, (angle/2) + 90);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + angle1 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle2 * radius);

        if (canSeePlayer)
        {
            Gizmos.color= Color.green;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }

    private Vector2 DirectionFromAngle (float eulerY, float angleInDegrees)
    {
        angleInDegrees -= eulerY;
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees* Mathf.Deg2Rad));
    }
}
