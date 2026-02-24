using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameObject knifeEnemy;
    public Transform target;

    private float timer = 0;
    private float spawnCooldown = 3;
    public float spawnOffset = 3;

    private List<GameObject> enemies = new List<GameObject>();

    int typeCounter = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!RoomController.doCombat)
        {
            return;
        }

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Vector3 spawnPos = transform.position;
            spawnPos.x += Random.Range(-spawnOffset, spawnOffset);
            timer = spawnCooldown;
            GameObject enemy = Instantiate(knifeEnemy, spawnPos, Quaternion.identity);
            enemies.Add(enemy);
            enemy.GetComponent<KnifeEnemy>().runTarget = target;
            enemy.GetComponent<KnifeEnemy>().usesGun = (typeCounter == 2);
            if (typeCounter > 2)
            {
                typeCounter = 0;
            }
            typeCounter++;
        }
    }

    public void deleteEnemies()
    {
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
