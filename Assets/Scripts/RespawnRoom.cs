using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnRoom : MonoBehaviour
{
    public GameObject knife;
    public Transform knifeSpawnPos;
    private GameObject currentKnife;

    private float timer = 0;
    private float respawnCooldown = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentKnife = Instantiate(knife, knifeSpawnPos.position, knifeSpawnPos.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (RoomController.doCombat)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = respawnCooldown;
            if (currentKnife.transform.position != knifeSpawnPos.position)
            {
                RoomController.gotoCombat();
                currentKnife = Instantiate(knife, knifeSpawnPos.position, knifeSpawnPos.rotation);
            }
        }
    }
}
