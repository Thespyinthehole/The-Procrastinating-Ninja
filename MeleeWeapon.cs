using UnityEngine;
using System.Collections;

public class MeleeWeapon : MonoBehaviour {
    public int damage;
    public Transform weapon;
    public AudioSource emitter;

    public AudioClip attacksound;
    public AudioClip SpecialSound;

    public void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon").transform;
        emitter = GetComponent<AudioSource>();
    }
}
