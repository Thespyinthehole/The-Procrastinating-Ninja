using UnityEngine;
using System.Collections;

public class DoorDamage : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Fist") if (col.GetComponent<Punch>().punching) PopOff((transform.position -  GameObject.FindGameObjectWithTag("Player").transform.position));
    }

    void PopOff(Vector3 direction)
    {
        gameObject.AddComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().AddForce(direction*250);
    }
}
