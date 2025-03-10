using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;

    public Transform tankPawnSpawnPoint;

    // List that holds our player(s)
    public List<PlayerController> players;

    private void Awake()
    {
        // If the instance doesn't exist yet
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        players = new List<PlayerController>();
    }

    public void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        GameObject playerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject pawnObj = Instantiate(tankPawnPrefab, tankPawnSpawnPoint.position, Quaternion.identity);

        Controller playerController = playerObj.GetComponent<Controller>();
        Pawn tankPawn = pawnObj.GetComponent<Pawn>();

        playerController.pawn = tankPawn;
    }
}
