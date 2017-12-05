using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {
    public bool canMove;
    public Vector2 speed;
    public Vector2 direction;
	
	// Update is called once per frame
	void Update () {
        if (canMove)
        {
            transform.position += new Vector3(speed.x * direction.x, speed.y * direction.y, 0.0f);
        }
	}
}
