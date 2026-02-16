using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingGlass : MonoBehaviour
{
    public Transform magnifyingGlassCameraTransform;
    public Camera magnifyingGlassCameraCamera;
    public Transform lens;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        magnifyingGlassCameraTransform.LookAt(lens);

        Vector3 newRotation = lens.eulerAngles;
        newRotation.z = magnifyingGlassCameraTransform.eulerAngles.z;
        lens.eulerAngles = newRotation;

        magnifyingGlassCameraCamera.nearClipPlane = Vector3.Distance(magnifyingGlassCameraTransform.position, lens.position);
    }
}
