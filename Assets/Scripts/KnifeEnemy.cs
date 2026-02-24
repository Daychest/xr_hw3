using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeEnemy : MonoBehaviour
{
    public GameObject knife;
    public GameObject gun;
    public Transform runTarget;

    private GameObject weapon;

    public float runSpeed;
    public float launchAngle = 45f;
    public float spinSpeed = 8f;

    public float multiplier;

    public bool usesGun;

    [HideInInspector] public CombatController combatController;


    // Start is called before the first frame update
    void Start()
    {
        if (usesGun)
        {
            weapon = gun;
            Destroy(knife);
        }
        else
        {
            weapon = knife;
            Destroy(gun);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = runTarget.position - transform.position;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }

        Vector3 positionToAdd = transform.forward * runSpeed * Time.deltaTime;
        positionToAdd.y = 0;
        transform.position += positionToAdd;
    }

    private void OnTriggerEnter(Collider other)
    {
        Throwable throwable = other.GetComponent<Throwable>();
        if (throwable != null && throwable.thrown)
        {
            Destroy(other);

            ThrowWeapon();
            weapon = null;

            combatController.enemyDied(gameObject);
            Destroy(gameObject);
        }
    }

    void ThrowWeapon()
    {
        if (weapon != null)
        {
            weapon.transform.parent = null;
            weapon.transform.LookAt(runTarget);
            Rigidbody rb = weapon.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.velocity = CalculateLaunchVelocity(weapon.transform.position, runTarget.position, launchAngle) * multiplier;
            rb.angularVelocity = weapon.transform.right * spinSpeed;
        }
    }

    Vector3 CalculateLaunchVelocity(Vector3 start, Vector3 end, float angle)
    {
        Debug.Log(Physics.gravity.y);
        float gravity = Mathf.Abs(Physics.gravity.y);
        float radAngle = angle * Mathf.Deg2Rad;

        Vector3 direction = end - start;
        Vector3 directionXZ = new Vector3(direction.x, 0f, direction.z);

        float heightDifference = direction.y;
        float distance = directionXZ.magnitude;

        float velocityXZ = distance / (Mathf.Cos(radAngle) *
                         Mathf.Sqrt((2 * (distance * Mathf.Tan(radAngle) - heightDifference)) / gravity));

        float velocityY = velocityXZ * Mathf.Tan(radAngle);

        Vector3 result = directionXZ.normalized * velocityXZ;
        result.y = velocityY;

        return result;
    }
}
