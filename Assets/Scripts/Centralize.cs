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
    }

    // Update is called once per frame
    void Update()
    {
        if (action.action.WasPressedThisFrame())
        {
            xrOriginTransform.position += startTransform.position - cameraTransform.position;
        }
    }
}
