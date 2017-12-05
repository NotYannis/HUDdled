using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour {
    public bool hasTouchedSomething = false;
    public bool isEnemy;
    public int damage;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        bool isInsideCamera = RendererExtensions.IsVisibleFrom(this.GetComponent<SpriteRenderer>(), mainCamera);
        if (!isInsideCamera)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTouchedSomething)
        {
            HealthScript otherHealth = other.GetComponent<HealthScript>();
            if(otherHealth != null)
            {
                if (otherHealth.isEnemy != isEnemy)
                {
                    hasTouchedSomething = true;
                    otherHealth.Damage(damage);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
