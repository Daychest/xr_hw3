using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class CustomGrab : MonoBehaviour
{
    // This script should be attached to both controller objects in the scene
    // Make sure to define the input in the editor (LeftHand/Grip and RightHand/Grip recommended respectively)
    CustomGrab otherHand = null;
    [HideInInspector] public List<Transform> nearObjects = new List<Transform>();
    [HideInInspector] public Transform grabbedObject = null;
    public InputActionReference action;
    bool grabbing = false;

    Vector3 lastPosition;
    Quaternion lastRotation;

    public bool rightHand;
    private Vector3 grabPositionCorrection = new Vector3(1, 1, 1);
    private Vector3 grabRotationCorrection = new Vector3(0, 0, 0);

    public float throwSpeed = 8;
    public float spinSpeed = -20;
    public float throwOffset = 0.5f;
    private Vector3 throwAngle = new Vector3(5, -10, 0);


    private void Start()
    {
        lastPosition = transform.position;
        lastRotation = transform.rotation;

        action.action.Enable();

        // Find the other hand
        foreach (CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }

        if (rightHand)
        {
            grabPositionCorrection = new Vector3(-1, 1, 1);
            grabRotationCorrection = new Vector3(0, 0, 180);

            throwAngle = Vector3.Scale(throwAngle, new Vector3(1, -1, 1));
            spinSpeed = -spinSpeed;
        }
    }

    void Update()
    {
        grabbing = action.action.IsPressed();
        if (grabbing)
        {
            // Grab nearby object or the object in the other hand
            if (!grabbedObject)
            {
                grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;

                if (grabbedObject)
                {
                    Rigidbody rigidbody = grabbedObject.GetComponent<Rigidbody>();
                    if (rigidbody != null)
                    {
                        rigidbody.isKinematic = true;
                        rigidbody.detectCollisions = false;
                    }

                    Throwable throwable = grabbedObject.GetComponent<Throwable>();
                    if (throwable != null)
                    {
                        grabbedObject.position = transform.position + transform.TransformDirection(Vector3.Scale(throwable.grabPositionOffset, grabPositionCorrection));

                        grabbedObject.rotation = transform.rotation;
                        grabbedObject.rotation *= Quaternion.Euler(grabRotationCorrection);
                        grabbedObject.rotation *= Quaternion.Euler(throwable.grabRotationOffset);
                    }
                }
            }

            if (grabbedObject)
            {
                // Change these to add the delta position and rotation instead
                // Save the position and rotation at the end of Update function, so you can compare previous pos/rot to current here

                Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(lastRotation);

                grabbedObject.rotation = deltaRotation * grabbedObject.rotation;

                Vector3 posDiff = lastPosition - grabbedObject.transform.position;
                posDiff = deltaRotation * posDiff;
                grabbedObject.transform.position = transform.position - posDiff;

                Throwable throwable = grabbedObject.GetComponent<Throwable>();
                if (throwable != null)
                {
                    grabbedObject.position = transform.position + transform.TransformDirection(Vector3.Scale(throwable.grabPositionOffset, grabPositionCorrection));

                    grabbedObject.rotation = transform.rotation;
                    grabbedObject.rotation *= Quaternion.Euler(grabRotationCorrection);
                    grabbedObject.rotation *= Quaternion.Euler(throwable.grabRotationOffset);
                }
            }
        }
        // If let go of button, release object
        else if (grabbedObject)
        {
            Rigidbody rigidbody = grabbedObject.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.isKinematic = false;
                rigidbody.detectCollisions = true;

                Throwable throwable = grabbedObject.GetComponent<Throwable>();
                if (throwable != null)
                {
                    Transform angleTransform = transform;
                    angleTransform.rotation *= Quaternion.Euler(throwAngle);
                    grabbedObject.position += angleTransform.forward * throwOffset;
                    rigidbody.velocity = angleTransform.forward * throwSpeed;

                    rigidbody.angularVelocity = angleTransform.up * spinSpeed;
                }
            }

            grabbedObject = null;
        }

        // Should save the current position and rotation here
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Make sure to tag grabbable objects with the "grabbable" tag
        // You also need to make sure to have colliders for the grabbable objects and the controllers
        // Make sure to set the controller colliders as triggers or they will get misplaced
        // You also need to add Rigidbody to the controllers for these functions to be triggered
        // Make sure gravity is disabled though, or your controllers will (virtually) fall to the ground

        Transform t = other.transform;
        if (t && t.tag.ToLower() == "grabbable")
            nearObjects.Add(t);
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if (t && t.tag.ToLower() == "grabbable")
            nearObjects.Remove(t);
    }
}
