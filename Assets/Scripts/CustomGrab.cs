using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    // This script should be attached to both controller objects in the scene
    // Make sure to define the input in the editor (LeftHand/Grip and RightHand/Grip recommended respectively)
    CustomGrab otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;
    bool grabbing = false;

    Vector3 lastPosition;
    Quaternion lastRotation;

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
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                grabbedObject.GetComponent<Rigidbody>().detectCollisions = false;

                Throwable throwable = grabbedObject.GetComponent<Throwable>();
            }

            if (grabbedObject)
            {
                // Change these to add the delta position and rotation instead
                // Save the position and rotation at the end of Update function, so you can compare previous pos/rot to current here

                if (false)
                {
                    Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(lastRotation);

                    grabbedObject.rotation = deltaRotation * grabbedObject.rotation;

                    Vector3 posDiff = lastPosition - grabbedObject.transform.position;
                    posDiff = deltaRotation * posDiff;
                    grabbedObject.transform.position = transform.position - posDiff;
                }

                Throwable throwable = grabbedObject.GetComponent<Throwable>();
                if (throwable != null)
                {
                    grabbedObject.position = transform.position + throwable.grabPositionOffset;
                    grabbedObject.rotation = transform.rotation * Quaternion.Euler(throwable.grabRotationOffset);

                    lastPosition = transform.position;
                    lastRotation = transform.rotation;

                    Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(grabbedObject.rotation);

                    grabbedObject.rotation = deltaRotation * grabbedObject.rotation;

                    Vector3 posDiff = lastPosition - grabbedObject.transform.position;
                    posDiff = deltaRotation * posDiff;
                    grabbedObject.transform.position = transform.position - posDiff;
                }
            }
        }
        // If let go of button, release object
        else if (grabbedObject)
        {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.GetComponent<Rigidbody>().detectCollisions = true;
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
