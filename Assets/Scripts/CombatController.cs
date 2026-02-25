using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameObject staticWall;
    public GameObject knifeEnemy;
    public Transform target;

    private float timer = 2;
    private float spawnCooldown = 3;
    public float spawnOffset = 3;

    public int enemiesPerWave = 5;
    public int enemiesLeft = 0;

    private float lastWaveSpawnCooldown = 1.5f;

    private float waveCooldown = 3;

    private float waveSpeedup = 0;
    private int waveEnemyIncrease = 1;

    public List<GameObject> enemies = new List<GameObject>();

    int typeCounter = 1;

    bool gameWon = false;
    private float victoryTimer = 3;

    bool starting = true;


    // Start is called before the first frame update
    void Start()
    {
        waveSpeedup = (spawnCooldown - lastWaveSpawnCooldown) / (RoomController.maxWaves - 1);
    }

    // Update is called once per frame
    void Update()
    {
        enemies.RemoveAll(e => e == null);

        if (!RoomController.doCombat)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (starting)
        {
            if (timer <= 0)
            {
                starting = false;
                enemyDied(null);
            }
            return;
        }

        if (gameWon)
        {
            if (timer < 0)
            {
                RoomController.gotoStart();
            }
            return;
        }

        
        
        if (timer < 0)
        {
            Vector3 spawnPos = transform.position;
            spawnPos.x += Random.Range(-spawnOffset, spawnOffset);
            timer = spawnCooldown;
            GameObject enemy = Instantiate(knifeEnemy, spawnPos, Quaternion.identity);
            enemies.Add(enemy);
            KnifeEnemy enemyScript = enemy.GetComponent<KnifeEnemy>();
            enemyScript.combatController = this;
            enemyScript.runTarget = target;
            enemyScript.usesGun = (typeCounter == 2);
            if (typeCounter > 2)
            {
                typeCounter = 0;
            }
            typeCounter++;
        }
    }

    public void deleteEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    public void enemyDied(GameObject enemy)
    {
        
        if (RoomController.doCombat)
        {
            enemiesLeft--;
            if (enemiesLeft <= 0 && enemies.Count <= 1)
            {
                enemiesPerWave += waveEnemyIncrease;
                enemiesLeft = enemiesPerWave;
                spawnCooldown -= waveSpeedup;
                RoomController.wave++;
                timer = waveCooldown;
                if (RoomController.wave > RoomController.maxWaves)
                {
                    //Victory
                    timer = victoryTimer;
                    staticWall.SetActive(true);
                    gameWon = true;
                    return;
                }
                RoomController.combatText.GetComponent<GradualText>().displayWave();
            }
        }
    }
}
