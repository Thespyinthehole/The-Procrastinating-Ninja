﻿using UnityEngine;
using System.Collections;

public class Dart : MonoBehaviour {
    int damage = 5;
    Vector3 direction;

    void Start()
    {
        direction = transform.position - GameObject.FindGameObjectWithTag("Blowgun").transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, angle);
    }
	void Update () {
        transform.parent.Translate(transform.right * 0.3f);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy") Hit(col.gameObject);
        Destroy(transform.parent.gameObject);
    }

    void Hit(GameObject col)
    {
        Enemy attached = col.GetComponent<Enemy>();
        if (attached != null)
        {
            attached.TakeDamage(damage);
            if (Random.Range(0, 10) == 0)
            {
                attached.GetPoisoned();
            }
        }
    }
}
