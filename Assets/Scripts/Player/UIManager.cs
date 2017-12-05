using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Top Bar")]
    public Image healthBar;
    public Image xpBar;
    public Text lvl;

    [Header("Weapon")]
    public FillBarMonitor reloadBar;
    public TextMonitor crystalWeaponText;
    public FillBarMonitor heatWeaponBar;

    [Header("Chair")]
    public FillBarMonitor fuelBar;
    public TextMonitor crystalChairText;
    public FillBarMonitor heatChairBar;

    [Header("Wave annoucement")]
    public Text waveText;
    public Text waveNumber;
    public WaveManager waves;

    [Header("GameObjects")]
    public HealthScript machineHp;
    public WizardController wizard;
    public MenuEnd menuEnd;
    public GameObject levelUp;
    public ParticleSystem levelUpParticles;
    public ParticleSystem levelUpParticlesUI;

    private int machineTotalHp;

    public static UIManager ui;

    private Coroutine crystalCoroutine;
    // Use this for initialization
    void Start()
    {
        if (ui != null)
        {
            Debug.Log("An instance of this object already exists !");
            return;
        }
        else
        {
            ui = this;
        }

        machineTotalHp = machineHp.hp;
    }

    public void UpdateHpBar()
    {
        float amount = (float)machineHp.hp / (float)machineTotalHp;
        UpdateFillBar(healthBar, amount);
    }

    public void UpdateXpBar()
    {
        float amount = (float)ExperienceScript.xp.currentExperience / (float)ExperienceScript.xp.experienceForNextLevel;
        UpdateFillBar(xpBar, amount);
    }

    public void UpdateLvl()
    {
        lvl.text = ExperienceScript.xp.currentLevel.ToString();
        if(ExperienceScript.xp.currentLevel > 1)
        {
            levelUp.SetActive(true);
            levelUpParticles.Play();
            levelUpParticlesUI.Play();
            StartCoroutine(LevelUp());
        }
    }

    IEnumerator LevelUp()
    {
        float levelUpTime = 1.0f;
        float deltaYPos = 20.0f;
        Text levelUpText = levelUp.GetComponent<Text>();
        int fontSize = levelUpText.fontSize;

        while(levelUpTime > 0.0f)
        {
            deltaYPos += 2.0f;
            levelUp.transform.position = new Vector3(levelUp.transform.position.x,
                                                wizard.transform.position.y + deltaYPos,
                                                levelUp.transform.position.z);
            levelUpTime -= Time.deltaTime;
            if(deltaYPos % 6 == 0)
            {
                levelUpText.fontSize++;
            }

            yield return null;
        }

        levelUpText.fontSize = fontSize;
        levelUp.SetActive(false);


        yield return null;
    }

    private void UpdateFillBar(Image fillBar, float percent)
    {
        fillBar.fillAmount = percent;
    }

    #region Wave
    public void UpdateWave()
    {
        waveNumber.text = "WAVE : " + waves.waveNumber;
        waveText.text = "Wave " + waves.waveNumber;
        waveText.fontSize += 2;
        StartCoroutine(WaveAnnouncer());
    }

    IEnumerator WaveAnnouncer()
    {
        float annoucementTime = 0.0f;
        while (waveText.rectTransform.localScale != Vector3.one)
        {
            annoucementTime += Time.deltaTime;
            waveText.rectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, annoucementTime / 0.5f);
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        while (waveText.rectTransform.localScale != Vector3.zero)
        {
            annoucementTime -= Time.deltaTime;
            waveText.rectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, annoucementTime / 0.5f);
            yield return null;
        }

        yield return null;
    }
    #endregion

    public void StartReload()
    {
        reloadBar.gameObject.SetActive(true);
        wizard.weapon.reloadBar = reloadBar;
        wizard.weapon.needAmmo = true;
        reloadBar.minAmount = 0;
        reloadBar.maxAmount = wizard.weapon.ammoMax;
    }

    public void StartCrystalWeapon()
    {
        crystalWeaponText.gameObject.SetActive(true);
        wizard.weapon.crystalText = crystalWeaponText;
        wizard.weapon.needCrystals = true;
        crystalWeaponText.minAmount = 0;
        crystalWeaponText.maxAmount = wizard.weapon.crystalsMax;
    }

    public void StartHeatWeapon()
    {
        heatWeaponBar.gameObject.SetActive(true);
        wizard.weapon.heatBar = heatWeaponBar;
        wizard.weapon.canOverHeat = true;
        wizard.weapon.StartCoroutine(wizard.weapon.HeatManagement());
        heatWeaponBar.minAmount = 0;
        heatWeaponBar.maxAmount = wizard.weapon.heatMax;
    }

    public void StartFuel()
    {
        fuelBar.gameObject.SetActive(true);
        wizard.fuelBar = fuelBar;
        wizard.needFuel = true;
        fuelBar.minAmount = 0;
        fuelBar.maxAmount = wizard.fuelMax;
    }

    public void StartCrystalChair()
    {
        crystalChairText.gameObject.SetActive(true);
        wizard.crystalText = crystalChairText;
        wizard.needCrystals = true;
        crystalChairText.minAmount = 0;
        crystalChairText.maxAmount = wizard.crystalsMax;
    }

    public void StartHeatChair()
    {
        heatChairBar.gameObject.SetActive(true);
        wizard.heatBar = heatChairBar;
        wizard.canOverHeat = true;
        wizard.StartCoroutine(wizard.HeatManagement());
        heatChairBar.minAmount = 0;
        heatChairBar.maxAmount = wizard.heatMax;
    }

    public void Win()
    {
        menuEnd.win = true;
        menuEnd.gameObject.SetActive(true);
    }
}