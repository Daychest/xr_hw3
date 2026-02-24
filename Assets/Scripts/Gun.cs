using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletCreationTransform;
    public InputActionReference shootActionLeft;
    public InputActionReference shootActionRight;
    public int ammo = 1000;
    public bool rightHand;
    public float bulletSpeed;


    private List<GameObject> bullets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        shootActionLeft.action.Enable();
        shootActionRight.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        bool shoot = false;
        if (rightHand)
        {
            if (shootActionRight.action.WasPressedThisFrame())
            {
                shoot = true;
            }
        }
        else
        {
            if (shootActionLeft.action.WasPressedThisFrame())
            {
                shoot = true;
            }
        }

        if (shoot)
        {
            if (ammo > 0)
            {
                ammo--;
                GameObject bullet = Instantiate(bulletPrefab, bulletCreationTransform.position, bulletCreationTransform.rotation);
                bullets.Add(bullet);

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = transform.forward * bulletSpeed;
            }
        }
    }

    private void OnDestroy()
    {
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }
}
