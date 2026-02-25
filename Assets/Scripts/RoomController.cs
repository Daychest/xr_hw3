using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoomController
{
    public static GameObject startRoom;
    public static GameObject endRoom;

    public static GameObject centralPosition;
    public static Transform startPosition;
    public static Transform combatPosition;
    public static Transform respawnPosition;

    public static GameObject combatLight;

    public static GameObject combatText;

    public static bool doCombat = false;

    public static Centralize centralize;
    public static CombatController combatController;

    public static bool bigGrabColliders = false;

    private static bool haveBeenInCombat = false;

    public static int wave = 0;
    public static int maxWaves = 5;

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
            combatLight.GetComponent<GradualLight>().setCooldown(1f);
            combatLight.GetComponent<GradualLight>().setTargetRange(5.87f);

            startRoom.SetActive(false);
            endRoom.SetActive(true);
        }
        else
        {
            combatText.GetComponent<GradualText>().displayWave();
            combatController.enemiesLeft = combatController.enemiesPerWave;
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
