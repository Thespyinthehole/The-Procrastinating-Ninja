using UnityEngine;
using System.Collections;

public class Punch : MeleeWeapon {
    public bool punching = false;
    public GameObject stars;

    bool hitobject = false;
    bool insideObject = false;
    Transform player;
    Vector3 start;
    int time = 10;
    int times = 0;
    int maxtime = 0;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        start = transform.localPosition;
        base.Start();
    }
    void FixedUpdate()
    {
        if (!punching && Input.GetButtonDown("Fire1") && !insideObject) { punching = true; times = 0; }
        if (punching) Punched();
    }

    void Punched()
    {
        Vector3 difference = transform.position - player.position;
        if (!hitobject)
        {
            if (times < time / 2)
            {
                transform.Translate(difference.normalized / 2);
            }
            else transform.Translate(-difference.normalized / 2);

            if (times == time)
            {
                transform.localPosition = start;
                punching = false;
                hitobject = false;
            }
        } else
        {
            if (times == maxtime || Vector3.Distance(transform.localPosition, start) < 0.1f)
            {
                transform.localPosition = start;
                punching = false;
                hitobject = false;
            } else transform.Translate(-difference.normalized / 2);
        }
        ++times;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Invisible") { 
            if (punching)
            {
                maxtime = times * 2;
                hitobject = true;
                emitter.clip = attacksound;
                emitter.Play();
                Enemy attached = col.GetComponent<Enemy>();
                if (attached != null)
                {
                    attached.TakeDamage(damage);
                    if (Random.Range(0, 10) == 0)
                    {
                        emitter.clip = SpecialSound;
                        emitter.Play();
                        attached.GetStunned();
                        Instantiate(stars, transform.position, Quaternion.identity);
                    }
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag != "Invisible") if (col.tag != "Enemy") insideObject = true;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag != "Invisible") insideObject = false;
    }
}
