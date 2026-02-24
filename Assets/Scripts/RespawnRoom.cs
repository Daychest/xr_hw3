using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnRoom : MonoBehaviour
{
    public GameObject knife;
    public Transform knifeSpawnPos;
    private GameObject currentKnife;

    // Start is called before the first frame update
    void Start()
    {
        currentKnife = Instantiate(knife, knifeSpawnPos.position, knifeSpawnPos.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentKnife.transform.position != knifeSpawnPos.position)
        {
            RoomController.gotoCombat();
            currentKnife = Instantiate(knife, knifeSpawnPos.position, knifeSpawnPos.rotation);
        }
    }
}
