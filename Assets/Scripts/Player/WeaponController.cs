using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {
    public int damage = 1;
    public int ammo = 10;
    public int ammoMax = 10;
    public float fireRate;
    public bool canAttack = true;
    public bool autoAttack;
    public bool canReload = true;


    public bool needAmmo = false;
    public float reloadCooldown = 0.5f;

    public bool needCrystals;
    public int crystalsMax;
    public int crystals;

    public bool canOverHeat;
    public bool overHeat = false;
    public int heatMax;
    public int heat;
    public int shootHeat;
    public float releaseHeatCooldown = 1.0f;
    private bool isReleasingHeat;

    private bool onReloadCooldown;
    protected bool onShootCooldown;

    [System.NonSerialized]
    public float bulletScale = 0.25f;
    public FillBarMonitor shotBar;
    public GameObject bulletPrefab;
    [System.NonSerialized]
    public FillBarMonitor reloadBar;
    [System.NonSerialized]
    public FillBarMonitor heatBar;
    [System.NonSerialized]
    public TextMonitor crystalText;

    private WizardAnimation anim;

    // Use this for initialization
    void Start () {
        anim = GetComponentInChildren<WizardAnimation>();
    }
	
	// Update is called once per frame
	void Update () {
        Attack();
        Reload();
        FarmCrystals();
	}

    protected virtual void Attack()
    {
        canAttack = ammo > 0 && !overHeat;
        bool shoot = Input.GetButtonDown("Shoot");
        if (shoot && ammo <= 0)
        {
            SoundManagerScript.Instance.MakeSoundErrorReload();
            shotBar.ShakeButtonEmpty();
        }
        if(shoot && overHeat)
        {
            SoundManagerScript.Instance.MakeSoundErrorReload();
            shotBar.ShakeButtonFull();
        }
        if (canAttack)
        {
            if(shoot && !onShootCooldown || (autoAttack && !onShootCooldown))
            {
				SoundManagerScript.Instance.MakeSoundAttackCharacter();
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<ShotScript>().damage = this.damage;
                bullet.transform.localScale = new Vector3(1.0f * bulletScale, 1.0f * bulletScale, 1.0f);
                anim.SetAttack();
                if (canOverHeat)
                {
                    heat += shootHeat;
                    if(heat > heatMax)
                    {
                        heat = heatMax;
                        overHeat = true;
						SoundManagerScript.Instance.MakeSoundAlert();
                        heatBar.ChangeColor(Color.red);
                    }
                }
                if (needAmmo)
                {
                    ammo -= 1;
                    reloadBar.UpdateAmount(ammo);
                }
                StartCoroutine(ShootCooldown());
            }
        }

    }

    void Reload()
    {
        if (needAmmo)
        {
            bool reload = Input.GetButtonDown("Reload") && !onReloadCooldown && ((needCrystals && crystals == crystalsMax) || !needCrystals);
            if(Input.GetButtonDown("Reload") && needCrystals && crystals < crystalsMax)
            {
                reloadBar.ShakeButtonEmpty();
                SoundManagerScript.Instance.MakeSoundErrorReload();
            }
            if (reload)
            {
                if (needCrystals)
                {
                    crystals = 0;
                    crystalText.UpdateAmount(crystals);
                }
                anim.SetReload();
				SoundManagerScript.Instance.MakeSoundReload();
                StartCoroutine(ReloadCooldown());
            }
        }
    }

    void FarmCrystals()
    {
        if(needCrystals)
        {
            bool farm = Input.GetButtonDown("Crystals");
            if (farm && crystals < crystalsMax)
            {
                crystals++;
				SoundManagerScript.Instance.MakeSoundCristal();
                crystalText.UpdateAmount(crystals);
            }
        }
    }

    public IEnumerator HeatManagement()
    {
        while (true)
        {
            if (!isReleasingHeat)
            {
                if(heat > 0)
                {
                    heat--;
                }
                else if (overHeat)
                {
                    overHeat = false;
                    heatBar.ResetColor();
                }
            }

            heatBar.UpdateAmount(heat);

            if(!isReleasingHeat && Input.GetKeyDown(KeyCode.H))
            {
                StartCoroutine(ReleaseHeat());
            }

            yield return null;
        }
    }

    protected IEnumerator ShootCooldown()
    {
        float currentCooldown = 0.0f;
        onShootCooldown = true;

        while(currentCooldown < fireRate)
        {
            currentCooldown += Time.deltaTime;
            shotBar.Fill(currentCooldown / fireRate);
            yield return null;
        }
        onShootCooldown = false;

        yield return null;
    }

    IEnumerator ReloadCooldown()
    {
        float currentCooldown = 0.0f;
        onReloadCooldown = true;

        canAttack = false;
        while(currentCooldown < reloadCooldown)
        {
            currentCooldown += Time.deltaTime;
            reloadBar.Fill(currentCooldown / reloadCooldown);

            yield return null;
        }

        ammo = ammoMax;
        onReloadCooldown = false;
        canAttack = true;
        anim.SetIdle();

        yield return null;
    }

    IEnumerator ReleaseHeat()
    {
		SoundManagerScript.Instance.MakeSoundReleaseHeat();
		float currentCooldown = ((float)heat / (float)heatMax) * releaseHeatCooldown;

        isReleasingHeat = true;
        while(currentCooldown > 0.0f)
        {
            currentCooldown -= Time.deltaTime;
            heat = (int)((currentCooldown / releaseHeatCooldown) * heatMax);
            yield return null;
        }

        isReleasingHeat = false;
        yield return null;
    }
}
