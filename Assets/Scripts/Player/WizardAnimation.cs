using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WizardAnimation : MonoBehaviour {
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetIdle()
    {
        anim.SetTrigger("Idle");
    }

    public void SetAttack()
    {
        anim.SetTrigger("Attack");
    }

    public void SetReload()
    {
        anim.SetTrigger("Reload");
    }
}
