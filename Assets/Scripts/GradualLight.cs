using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradualLight : MonoBehaviour
{
    private Light light;
    public float targetIntensity;
    public float targetRange;
    public float slowDown = 1;
    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //light.intensity += ((targetIntensity - light.intensity) / slowDown) * Time.deltaTime;
            light.range += ((targetRange - light.range) / slowDown) * Time.deltaTime;
        }
    }

    public void setTargetRange(float range)
    {
        targetRange = range;
    }

    public void setRange(float range)
    {
        targetRange = range;
        light.range = range;
    }

    public void setCooldown(float cooldown)
    {
        timer = cooldown;
    }
}
