using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoomController
{

    public static GameObject centralPosition;
    public static Transform startPosition;
    public static Transform combatPosition;
    public static Transform respawnPosition;

    public static bool doCombat = false;

    public static Centralize centralize;
    public static CombatController combatController;

    public static void gotoStart()
    {
        doCombat = false;
        centralPosition.transform.position = startPosition.position;
        centralize.setToCenter();
        combatController.deleteEnemies();
    }

    public static void gotoCombat()
    {
        doCombat = true;
        centralPosition.transform.position = combatPosition.position;
        centralize.setToCenter();
        combatController.deleteEnemies();
    }

    public static void gotoRespawn()
    {
        doCombat = false;
        centralPosition.transform.position = respawnPosition.position;
        centralize.setToCenter();
        combatController.deleteEnemies();
    }
}
