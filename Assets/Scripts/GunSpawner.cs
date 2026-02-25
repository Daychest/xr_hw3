using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    private Vector3 startPosition;
    private bool started = false;
    public Transform gunCreateTransform;
    public GameObject gun;
    private GameObject currentGun;

    private float timer = 0;
    private float cooldown = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            if (transform.position != startPosition)
            {
                started = true;
            }
        }

        if (started)
        {

            if (currentGun == null)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    currentGun = Instantiate(gun, gunCreateTransform.position, gunCreateTransform.rotation);
                    timer = cooldown;
                }
            }
            else
            {
                if (currentGun.GetComponent<Throwable>().thrown)
                {
                    currentGun = null;
                }
            }
        }
    }
}
