using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitRoomController : MonoBehaviour
{
    public  GameObject centralPosition;
    public  Transform startPosition;
    public  Transform combatPosition;
    public  Transform respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        RoomController.centralPosition = centralPosition;
        RoomController.startPosition = startPosition;
        RoomController.combatPosition = combatPosition;
        RoomController.respawnPosition = respawnPosition;

        RoomController.centralize = GetComponent<Centralize>();


        RoomController.gotoRespawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
