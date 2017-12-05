using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public float xStop;
    public int experienceGiven;
    private MoveScript movement;
    private WeaponController weapon;

	// Use this for initialization
	void Start () {
        movement = GetComponent<MoveScript>();
        weapon = GetComponent<WeaponController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x < xStop && movement.canMove)
        {
            movement.canMove = false;
            weapon.canAttack = true;
            GetComponent<Animator>().SetTrigger("Attack");
        }
	}
}
