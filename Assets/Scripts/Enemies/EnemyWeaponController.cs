using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : WeaponController {

    void Update()
    {

    }

    protected override void Attack()
    {
        if (canAttack)
        {
            GameObject axe = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            axe.transform.position += new Vector3(-107.0f, 9.0f, 0.0f);
        }
    }
}
