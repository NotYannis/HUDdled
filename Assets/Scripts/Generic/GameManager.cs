using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public WizardController wizard;
    public WaveManager waves;

    public static GameManager gameManager;

	// Use this for initialization
	void Start () {
		if(gameManager != null)
        {
            Debug.Log("An instance of this object already exists");
            return;
        }
        else
        {
            gameManager = this;
        }
	}

    public void UpdateGame(int lvl)
    {
        switch (lvl)
        {
            case 2:
                UIManager.ui.StartReload();
                break;
            case 3:
                MorePower();
                UIManager.ui.StartCrystalWeapon();
                break;
            case 4:
                UIManager.ui.StartHeatWeapon();
                break;
            case 5:
                MorePower();
                UIManager.ui.StartFuel();
                break;
            case 6:
                UIManager.ui.StartCrystalChair();
                break;
            case 7:
                MorePower();
                UIManager.ui.StartHeatChair();
                break;
            case 8:
                Invoke("Win", 2.0f);
                break;
        }
    }

    void Win()
    {
        EnemyHealthScript[] enemies = GameObject.FindObjectsOfType<EnemyHealthScript>();
        ShotScript[] shots = GameObject.FindObjectsOfType<ShotScript>();

        foreach (EnemyHealthScript hp in enemies)
        {
            hp.Dead();
        }
        foreach(ShotScript shot in shots)
        {
            Destroy(shot.gameObject);
        }

        waves.StopAllCoroutines();
        UIManager.ui.Win();
    }

    private void MorePower()
    {
        wizard.weapon.bulletScale += 0.25f;
        waves.enemyHealth--;
        waves.numberOfEnemies += 2;
        waves.waveTime += 2;
        EnemyHealthScript[] enemies = GameObject.FindObjectsOfType<EnemyHealthScript>();
        foreach (EnemyHealthScript hp in enemies)
        {
            if(hp.hp == waves.enemyHealth + 1)
            {
                hp.hp = waves.enemyHealth;
            }
        }
    }
}
