using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    [Header("Wave Timing")]
    public float waveTime = 10.0f;
    public float waveTimeMultiplier = 5.0f;
    public float pauseBetweenWaves = 2.0f;

    private float currentPauseCooldown;
    private float currentWaveCooldown;


    [Header("Enemies")]
    public float numberOfEnemies = 2;
    public float numberOfEnemiesMultiplier = 1.5f;
    public int experienceGivenPerEnemy = 10;
    public float experienceMultiplier = 1.1f;
    public float xPosition;
    public float yPositionMin;
    public float yPositionMax;
    public GameObject enemies;
    public int enemyHealth;

    private int numberOfEnemiesLeft;

    [System.NonSerialized]
    public int waveNumber = 1;
	// Use this for initialization
	void Start () {
        StartCoroutine(WavePause());
        enemyHealth = enemies.GetComponent<HealthScript>().hp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator WavePause()
    {
        //WAVE NUMBER ###
        UIManager.ui.UpdateWave();
        currentPauseCooldown = pauseBetweenWaves;
        while(currentPauseCooldown > 0.0f)
        {
            currentPauseCooldown -= Time.deltaTime;
            yield return null;
        }

        StartCoroutine(Wave());
        yield return null;
    }

    IEnumerator Wave()
    {
        //Set wave values
        currentWaveCooldown = waveTime;
        numberOfEnemiesLeft = (int)numberOfEnemies;

        float enemySpawnCooldown = waveTime / numberOfEnemies;

        Vector3 position = new Vector3(xPosition, 0.0f, 10.0f); 
        GameObject enemy;

        while (currentWaveCooldown > 0.0f && numberOfEnemiesLeft > 0)
        {
            position.y = RandYPos();
            enemy = Instantiate(enemies, position, Quaternion.identity);
            enemy.GetComponent<EnemyController>().experienceGiven = experienceGivenPerEnemy;
            enemy.GetComponent<HealthScript>().hp = enemyHealth;

            currentWaveCooldown -= Time.deltaTime;
            numberOfEnemiesLeft--;

            yield return new WaitForSeconds(enemySpawnCooldown);
        }

        //Multiply wave values
        numberOfEnemies = numberOfEnemies * numberOfEnemiesMultiplier;
        waveTime *= waveTimeMultiplier;
        experienceGivenPerEnemy = (int)((float)experienceGivenPerEnemy * experienceMultiplier);

        waveNumber++;
        StartCoroutine(WavePause());

        yield return null;
    }

    private float RandYPos()
    {
        return Random.Range(yPositionMin, yPositionMax);
    }
}
