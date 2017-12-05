using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour {
    public bool canMove;
    public Vector2 speed;
    public float yMaxLimit;
    public float yMinLimit;

    [Header("Fuel")]
    public bool needFuel;
    public int fuel;
    public int fuelMax;
    public float fuelFillCooldown;
    private bool isRefillingFuel;

    [Header("Crystal")]
    public bool needCrystals;
    public int crystals;
    public int crystalsMax;

    [Header("Heat")]
    public bool canOverHeat;
    public int heat;
    public int heatMax;
    public bool overHeating;
    public bool isReleasingHeat;
    public float releaseHeatCooldown;

    [Header("Reparation")]
    public bool isBroken;
    public int repairAmount;
    public int repairMax;
    public FillBarMonitor repairBar;


    [System.NonSerialized]
    public FillBarMonitor fuelBar;
    [System.NonSerialized]
    public TextMonitor crystalText;
    [System.NonSerialized]
    public FillBarMonitor heatBar;
    public MovementKeysMonitor arrowKeys;

    [System.NonSerialized]
    public WeaponController weapon;
	// Use this for initialization
	void Start () {
        weapon = GetComponent<WeaponController>();
        repairBar.maxAmount = repairMax;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        RefillFuel();
        FarmCrystals();
        Repair();
    }

    void Move()
    {
        canMove = fuel > 0 && !isRefillingFuel && !overHeating && !isReleasingHeat &&!isBroken; 
        float verticalMove = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(fuel <= 0)
            {
                arrowKeys.ShakeButtonEmpty();
                SoundManagerScript.Instance.MakeSoundNoFuel();
            }
            if (overHeating)
            {
                arrowKeys.ShakeButtonFull();
            }
            if (isBroken)
            {
                arrowKeys.ShakeButtonRepair();
            }
        }
        if (canMove)
        {
            Vector3 pos = transform.position;
            pos.y += verticalMove * speed.y;
            pos.y = Mathf.Clamp(pos.y, yMinLimit, yMaxLimit);
            transform.position = pos;

            if(verticalMove != 0.0f)
            {
                if (needFuel)
                {
                    fuel--;
                    fuelBar.UpdateAmount(fuel);
                }

                if (canOverHeat)
                {
                    heat += 5;
                    if (heat > heatMax)
                    {
                        heat = heatMax;
                        overHeating = true;
                        repairAmount = 0;
                        heatBar.ChangeColor(Color.red);

                        isBroken = true;
                        repairBar.gameObject.SetActive(true);
                        repairAmount = 0;
                        repairBar.UpdateAmount(repairAmount);
                    }
                }
            }
        }

    }

    void RefillFuel()
    {
        if (needFuel)
        {
            if ((Input.GetKeyDown(KeyCode.G) && needCrystals && crystals < crystalsMax))
            {
                fuelBar.ShakeButtonEmpty();
                SoundManagerScript.Instance.MakeSoundErrorReload();
            }
            if (Input.GetKeyDown(KeyCode.G) && !isRefillingFuel && ((needCrystals && crystals == crystalsMax) || !needCrystals))
            {
			    SoundManagerScript.Instance.MakeSoundRefill();
                isRefillingFuel = true;
                StartCoroutine(RefillFuelCooldown());

                if (needCrystals)
                {
                    crystals = 0;
                    crystalText.UpdateAmount(crystals);
                }
            }
        }

    }

    void FarmCrystals()
    {
        if (needCrystals)
        {
            if (Input.GetKeyDown(KeyCode.D) && crystals < crystalsMax)
            {
                crystals++;
				SoundManagerScript.Instance.MakeSoundCristal();
                crystalText.UpdateAmount(crystals);
            }
        }
    }

    void Repair()
    {
        if (isBroken)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                repairAmount++;
                repairBar.UpdateAmount(repairAmount);
                if(repairAmount > repairMax)
                {
                    isBroken = false;
                    repairBar.gameObject.SetActive(false);
                }
            }
        }
    }

    IEnumerator RefillFuelCooldown()
    {
        float currentCooldown = 0.0f;

        while (currentCooldown < fuelFillCooldown)
        {
            currentCooldown += Time.deltaTime;
            fuelBar.Fill(currentCooldown / fuelFillCooldown);

            yield return null;
        }

        fuel = fuelMax;
        isRefillingFuel = false;
        yield return null;
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
                else if (overHeating)
                {
                    overHeating = false;
                    heatBar.ResetColor();
                }
            }

            heatBar.UpdateAmount(heat);

            if (!isReleasingHeat && Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(ReleaseHeat());
            }

            yield return null;
        }
    }

    IEnumerator ReleaseHeat()
    {
        float currentCooldown = ((float)heat / (float)heatMax) * releaseHeatCooldown;

        isReleasingHeat = true;
        while (currentCooldown > 0.0f)
        {
            currentCooldown -= Time.deltaTime;
            heat = (int)((currentCooldown / releaseHeatCooldown) * heatMax);
            yield return null;
        }

        isReleasingHeat = false;
        yield return null;
    }

}
