using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : HealthScript {

    void Update()
    {
        bool isInsideCamera = RendererExtensions.IsVisibleFrom(this.GetComponent<SpriteRenderer>(), Camera.main);
        if (isInsideCamera && isInvincible)
        {
            isInvincible = false;
        }
    }

    public override void Dead()
    {
		SoundManagerScript.Instance.MakeSoundDeath();
        ExperienceScript.xp.AddExperience(GetComponent<EnemyController>().experienceGiven);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<MoveScript>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyWeaponController>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        Invoke("DestroyEntity", GetComponent<ParticleSystem>().main.duration);
    }

    void DestroyEntity()
    {
        Destroy(this.gameObject);
    }

	public override void DmgFeedback()
	{
		anim.SetTrigger("dmg");
		SoundManagerScript.Instance.MakeSoundEnemyhit();
	}
}
