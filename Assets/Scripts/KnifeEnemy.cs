using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeEnemy : MonoBehaviour
{
    public GameObject knife;
    public Transform runTarget;

    public float runSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform temp = transform;
        temp.LookAt(runTarget);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, temp.eulerAngles.y, transform.eulerAngles.z);
    }
}
