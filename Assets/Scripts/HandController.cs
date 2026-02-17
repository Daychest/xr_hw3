using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;


public class HandController : MonoBehaviour
{
    public InputActionReference gripAction;
    public InputActionReference triggerAction;
    public Hand hand;

    // Start is called before the first frame update
    void Start()
    {
        gripAction.action.Enable();
        triggerAction.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
        hand.SetGrip(gripAction.action.ReadValue<float>());
        hand.SetTrigger(triggerAction.action.ReadValue<float>());   
    }
}
