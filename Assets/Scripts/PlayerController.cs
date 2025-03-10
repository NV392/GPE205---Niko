using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : Controller
{
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode shootKey;

    public float volumeDistance;

    // Start is called before the first frame update
    public override void Start()
    {
        //  If we have a GameManager
        if (GameManager.instance != null)
        {
            // And it tracking the players()
            if (GameManager.instance.players != null)
            {
               // Register with the GameManager
               GameManager.instance.players.Add(this);
            }
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
       if (Input.GetKey(moveForwardKey))
       {
            pawn.MoveForward();
            pawn.noiseMaker.volumeDistance = 5;
       }
       if (Input.GetKeyUp(moveForwardKey))
       {
          pawn.noiseMaker.volumeDistance = 0;
       }

       if (Input.GetKey(moveBackwardKey))
       {
            pawn.MoveBackward();
            pawn.noiseMaker.volumeDistance = 5;
       }
       if (Input.GetKeyUp(moveBackwardKey))
       {
          pawn.noiseMaker.volumeDistance = 0;
       }

       if (Input.GetKey(rotateClockwiseKey))
       {
            pawn.RotateClockwise();
            pawn.noiseMaker.volumeDistance = 5;
       }
       if (Input.GetKeyUp(rotateClockwiseKey))
       {
          pawn.noiseMaker.volumeDistance = 0;
       }

       if (Input.GetKey(rotateCounterClockwiseKey))
       {
            pawn.RotateCounterClockwise();
            pawn.noiseMaker.volumeDistance = 5;
       }
       if (Input.GetKeyUp(rotateCounterClockwiseKey))
       {
          pawn.noiseMaker.volumeDistance = 0;
       }

       if (Input.GetKeyDown(shootKey))
       {
            pawn.Shoot();
            pawn.noiseMaker.volumeDistance = 5;
            pawn.noiseMaker.volumeDistance = 0;
       }
    }

    public void OnDestroy()
    {
        // If we have a GameManager
        if (GameManager.instance != null)
        {
          // And it tracks the player(s)
          if (GameManager.instance.players != null)
          {
               // Deregister with the GameManager
               GameManager.instance.players.Remove(this);
          }
        }
    }
}