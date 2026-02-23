using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Centralize : MonoBehaviour
{
    public InputActionReference action;
    public Transform xrOriginTransform;
    public Transform cameraTransform;
    public Transform startTransform;

    // Start is called before the first frame update
    void Start()
    {
        action.action.Enable();

        setToCenter();
    }

    // Update is called once per frame
    void Update()
    {
        if (action.action.WasPressedThisFrame())
        {
            setToCenter();
        }
    }

    void setToCenter()
    {
        Vector3 newRotation = xrOriginTransform.eulerAngles;
        newRotation.y += startTransform.eulerAngles.y - cameraTransform.eulerAngles.y;
        xrOriginTransform.eulerAngles = newRotation;

        xrOriginTransform.position += startTransform.position - cameraTransform.position;
    }
}
