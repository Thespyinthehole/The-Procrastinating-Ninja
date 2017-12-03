using UnityEngine;
using System.Collections;

public class SwordAttack : MeleeWeapon {

    float spintime = 0;

    public int spinspeed = 2;
    public bool spinning = false;



    void FixedUpdate()
    {
        if (!spinning && Input.GetButtonDown("Fire1")) spinning = true;
        if (spinning) SpinAttack();

    }

    void WeaponLook()
    {
        weapon.localRotation = Quaternion.identity;
    }

    void SpinAttack()
    {
        weapon.Rotate(0, 0, spinspeed / weapon.GetComponent<WeaponAttached>().timeMultipier);
        spintime += spinspeed / weapon.GetComponent<WeaponAttached>().timeMultipier;
        if (spintime >= 360)
        {
            spintime = 0;
            spinning = false;
            WeaponLook();
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (spinning)
        {
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
                    attached.Cut();
                }
            }
        }
    }
}
