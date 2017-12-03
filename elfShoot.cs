using UnityEngine;
using System.Collections;

public class elfShoot : MonoBehaviour {
    Enemy ene;
    GameObject blowgun;
    public int damage = 10;

    void Start()
    {
        ene = GetComponent<Enemy>();
    }


    void Update()
    {
        if (ene.target != null && Random.Range(0, 101) == 0 && !ene.stuned) Explode();
    }

    void Explode()
    {
        ene.target.GetComponent<PlayerMove>().PlayerTakeDamage(damage);
        Vector3 Direction = ene.target.transform.position - transform.position;
        ene.target.GetComponent<Rigidbody2D>().AddForce(Direction * damage * 10);
    }
}
