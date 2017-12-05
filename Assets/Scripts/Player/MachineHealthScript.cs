using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineHealthScript : HealthScript {
    public MenuEnd looooooooose;

    public override void Damage(int amount)
    {
        base.Damage(amount);

        UIManager.ui.UpdateHpBar();
    }

	public override void DmgFeedback()
	{
        anim.SetTrigger("dmg");
        SoundManagerScript.Instance.MakeSoundImpactMachine();
	}

    public override void Dead()
    {
		anim.SetTrigger("dead");
        SoundManagerScript.Instance.MakeSoundMachineExplosion();
        GetComponent<Collider2D>().enabled = false;
        Invoke("LooseMenu", 2.0f);
    }

    void LooseMenu()
    {
        looooooooose.gameObject.SetActive(true);
    }
}
