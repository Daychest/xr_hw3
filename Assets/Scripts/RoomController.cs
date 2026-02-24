using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoomController
{

    public static GameObject centralPosition;
    public static Transform startPosition;
    public static Transform combatPosition;
    public static Transform respawnPosition;

    public static GameObject combatText;

    public static bool doCombat = false;

    public static Centralize centralize;
    public static CombatController combatController;

    public static bool bigGrabColliders = false;

    private static bool haveBeenInCombat = true;

    public static int wave = 1;
    public static int maxWaves = 3;

    public static void gotoStart()
    {
        doCombat = false;
        centralPosition.transform.position = startPosition.position;
        centralize.setToCenter();
        combatController.deleteEnemies();
        bigGrabColliders = false;
    }

    public static void gotoCombat()
    {
        if (!haveBeenInCombat)
        {
            haveBeenInCombat = true;

        }
        else
        {
            combatText.GetComponent<GradualText>().displayText("Wave " + wave + "/" + maxWaves);
        }
        doCombat = true;
        centralPosition.transform.position = combatPosition.position;
        centralize.setToCenter();
        combatController.deleteEnemies();
        bigGrabColliders = true;
    }

    public static void gotoRespawn()
    {
        doCombat = false;
        centralPosition.transform.position = respawnPosition.position;
        centralize.setToCenter();
        combatController.deleteEnemies();
        bigGrabColliders = true;
    }
}
