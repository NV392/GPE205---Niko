using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState {Guard, Chase, Attack, Flee, Patrol};

    public AIState currentState;

    public GameObject target;

    public float triggerDistance;

    public float fleeDistance;

    public Transform[] waypoints;
    public float waypointStepDistance;

    public float hearingDistance;

    public float fovAngle;

    private int currentWaypoint = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        //currentState = AIState.Guard;
        if (!IsHasTarget())
        {
            TargetPlayerOne();
        }

        if (IsHasTarget())
        {
            ProcessInputs();
        }

        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        ProcessInputs();

        base.Update();
    }

    public override void ProcessInputs()
    {
        // This is where the decision making happens
        switch (currentState)
        {
            case AIState.Guard:
                // Any work that happens for Guard

                if (CanSee(target))
                {
                    ChangeState(AIState.Chase);
                }
                break;

            case AIState.Chase:
                // Any work for Chase
                DoChaseState();
                if (IsDistanceLessThan(target, triggerDistance))
                {
                    ChangeState(AIState.Guard);
                }
                break;
            
            case AIState.Attack:
                DoAttackState();
                // Check for transitions out of it
                break;

            case AIState.Flee:
                DoFleeState();
                // Check for transitions out of it
                break;

            case AIState.Patrol:
                DoPatrolState();
                // Check for transitions out of it
                break;
        }
    }

    // Executing State functions
    public void DoGuardState()
    {
        // Do nothing
    }

    public void DoChaseState()
    {
        // Seek our target
        Seek(target);
    }

    public void DoAttackState()
    {
        Seek(target);

        Shoot();
    }

    public void DoFleeState()
    {
        Flee();
    }

    public void DoPatrolState()
    {
        Patrol();
    }

    // Set of functions representing the Behaviors of the FSM
    public void Shoot()
    {
        pawn.Shoot();
    }
    public void Seek(GameObject target)
    {
        Seek(target.transform.position);
    }
    public void Seek(Vector3 targetPosition)
    {
        pawn.RotateTowards(targetPosition);

        pawn.MoveForward();
    }
    public void Seek(Transform targetTransform)
    {
        Seek(targetTransform.position);
    }
    public void Seek(Pawn targetPawn)
    {
        Seek(targetPawn.transform);
    }
    
    public void Flee()
    {
        // Find the vector to our target
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        // Find the vector away from the target by negating the vectorToTarget
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        // Find the vector we would travel down in order to flee
        Vector3 fleeVector = vectorAwayFromTarget.normalized * fleeDistance;
        // Seek the point that is "fleeVector" away from our current position

        Seek(pawn.transform.position + fleeVector);
    }

    public void Patrol()
    {
        if (waypoints.Length > currentWaypoint)
        {
            Seek(waypoints[currentWaypoint]);

            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) <= waypointStepDistance)
            {
                currentWaypoint++;
                print(currentWaypoint);
            }
        }
        else
        {
            RestartPatrol();
        }
    }

    // Helper Transitions Functions
    public bool IsDistanceLessThan(GameObject target, float distance)
    {
        if ((Vector3.Distance(pawn.transform.position, target.transform.position)) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RestartPatrol()
    {
        currentWaypoint = 0;
    }

    // Change state helper function
    public void ChangeState(AIState state)
    {
        currentState = state;
    }

    public void TargetPlayerOne()
    {
        // If the GameManager exists
        if (GameManager.instance != null)
        {
            // And the array of players exists
            if (GameManager.instance.players != null)
            {
                // And there are players in it
                if (GameManager.instance.players.Count > 0)
                {
                    //Then target the gameObject of the pawn of the first player controller in the list
                    target = GameManager.instance.players[0].pawn.gameObject;
                }
            }
        }
    }

    protected bool IsHasTarget()
    {
        // return true if we have a target, false if we don't
        return (target != null);
    }

    public bool CanHear(GameObject target)
    {
        NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();

        if (noiseMaker == null)
        {
            return false;
        }

        if (noiseMaker.volumeDistance <= 0)
        {
            return false;
        }

        float totalDistance = noiseMaker.volumeDistance + hearingDistance;

        if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
        {
            return true;
        }

        return false;
    }

    public bool CanSee(GameObject target)
    {
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;

        float angleToTarget = Vector3.Angle(vectorToTarget, pawn.transform.forward);

        if (angleToTarget < fovAngle)
        {
            RaycastHit hit;

            if (Physics.Raycast(pawn.transform.position, pawn.transform.forward, out hit))
            {
                if (hit.transform.gameObject == target)
                {
                    return true;
                }
            }
        }

        return false;
    }
}