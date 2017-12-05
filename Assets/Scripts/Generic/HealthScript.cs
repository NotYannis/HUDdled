using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthScript : MonoBehaviour {
    public int hp;
    public bool isInvincible = false;
    public bool isEnemy;
    public int meleeDamage;
	protected Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
	}


	public virtual void Damage(int amount)
    {
        if (!isInvincible)
        {
            hp -= amount;

			DmgFeedback();
            if(hp <= 0)
            {
                Dead();
            }
        }

    }

    public virtual void Dead()
    {
        Destroy(this.gameObject);
    }

	public virtual void DmgFeedback()
	{

	}
}
