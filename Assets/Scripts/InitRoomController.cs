using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitRoomController : MonoBehaviour
{
    public  GameObject centralPosition;
    public  Transform startPosition;
    public  Transform combatPosition;
    public  Transform respawnPosition;
    public  CombatController combatController;
    public  GameObject combatText;
    public  GameObject combatLight;

    // Start is called before the first frame update
    void Start()
    {
        RoomController.centralPosition = centralPosition;
        RoomController.startPosition = startPosition;
        RoomController.combatPosition = combatPosition;
        RoomController.respawnPosition = respawnPosition;

        RoomController.centralize = GetComponent<Centralize>();
        RoomController.combatController = combatController;

        RoomController.combatText = combatText;

        RoomController.combatLight = combatLight;

        RoomController.gotoStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
