using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceScript : MonoBehaviour {
    public int currentLevel = 0;
    public int currentExperience = 0;

    public int totalExperience;
    public int experienceForNextLevel = 50;
    public float experienceForNextLevelMultiplier = 2;

    public static ExperienceScript xp;
    // Use this for initialization
    void Start () {
		if(xp != null)
        {
            Debug.Log("An instance of this object already exists !");
            return;
        }
        else
        {
            xp = this;
        }
        totalExperience = currentExperience;

        UIManager.ui.UpdateXpBar();
        UIManager.ui.UpdateLvl();
    }

    public void AddExperience(int experienceAmount)
    {
        currentExperience += experienceAmount;
        totalExperience += experienceAmount;

        if(currentExperience >= experienceForNextLevel)
        {
			//LEVEL UP
			SoundManagerScript.Instance.MakeSoundLevelUp();
            currentExperience = currentExperience % experienceForNextLevel;
            experienceForNextLevel = (int)(experienceForNextLevel * experienceForNextLevelMultiplier);
            currentLevel++;
            UIManager.ui.UpdateLvl();
            GameManager.gameManager.UpdateGame(currentLevel);
        }

        UIManager.ui.UpdateXpBar();
    }
}
