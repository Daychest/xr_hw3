using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeEnemy : MonoBehaviour
{
    public GameObject knife;
    public Transform runTarget;

    public float runSpeed;
    public float launchAngle = 45f;

    public float multiplier;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(CalculateLaunchVelocity(new Vector3(0, 0, 0), new Vector3(1, 0, 0), 45f));
    }

    // Update is called once per frame
    void Update()
    {
        Transform temp = transform;
        temp.LookAt(runTarget);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, temp.eulerAngles.y, transform.eulerAngles.z);


    }

    private void OnTriggerEnter(Collider other)
    {
        Throwable throwable = other.GetComponent<Throwable>();
        if (throwable != null && throwable.thrown)
        {
            ThrowKnife();
            knife = null;
        }
    }

    void ThrowKnife()
    {
        if (knife != null)
        {
            knife.transform.parent = null;
            knife.transform.LookAt(runTarget);
            Rigidbody rb = knife.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.velocity = CalculateLaunchVelocity(knife.transform.position, runTarget.position, launchAngle) * multiplier;
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
