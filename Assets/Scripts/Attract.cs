using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attract : MonoBehaviour
{
    public InputActionReference attractButton;
    public Transform controller;
    public Transform thingToAttract;

    // Start is called before the first frame update
    void Start()
    {
        attractButton.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {

        if (attractButton.action.IsPressed())
        {
            thingToAttract.position += (controller.position - thingToAttract.position) / 10;
        }
    }
}
