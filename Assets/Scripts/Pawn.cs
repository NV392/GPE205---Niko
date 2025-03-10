using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Pawn : MonoBehaviour
{
    // Variable for move speed
    public float moveSpeed;
    
    // Variable for turn speed
    public float turnSpeed;

    // Shooter variables
    // Variable for our shell prefab
    public GameObject shellPrefab;
    // Variable for our firing force
    public float fireForce;
    // Variable for our damage done
    public float damageDone;
    // Variable for how long our bullets survive if they don't collide
    public float shellLifespan;
    // Variable for rate of fire
    public float fireRate;

    public Mover mover;

    public Shooter shooter;

    public NoiseMaker noiseMaker;

    // Start is called before the first frame update
    public virtual void Start()
    {
        mover = GetComponent<Mover>();

        shooter = GetComponent<Shooter>();

        noiseMaker = GetComponent<NoiseMaker>();
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public abstract void MoveForward();
    public abstract void MoveForward(float speed);
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void Shoot();

    public abstract void RotateTowards(Vector3 targetPosition);
}