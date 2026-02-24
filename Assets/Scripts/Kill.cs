using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.ToLower() == "player")
        {
            RoomController.gotoRespawn();
        }
    }
}
