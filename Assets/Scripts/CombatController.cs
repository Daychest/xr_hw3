using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameObject knifeEnemy;
    public Transform target;

    private float timer = 0;
    private float spawnCooldown = 4;
    public float spawnOffset = 3;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Vector3 spawnPos = transform.position;
            spawnPos.x += Random.Range(-spawnOffset, spawnOffset);
            timer = spawnCooldown;
            GameObject enemy = Instantiate(knifeEnemy, spawnPos, Quaternion.identity);
            enemy.GetComponent<KnifeEnemy>().runTarget = target;
        }
    }
}
