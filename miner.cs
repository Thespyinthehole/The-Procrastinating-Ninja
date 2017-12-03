using UnityEngine;
using System.Collections;

public class miner : MonoBehaviour {
    Enemy ene;
    public AudioClip explosion;
    public int damage = 10;

    void Start()
    {
        ene = GetComponent<Enemy>();
    }
	

	void Update () {
        if (ene.target != null && Random.Range(0, 101) == 0 && !ene.stuned) Explode(); 
	}

    void Explode()
    {
        GetComponent<AudioSource>().clip = explosion;
        GetComponent<AudioSource>().Play();
        ene.target.GetComponent<PlayerMove>().PlayerTakeDamage(damage);
        Vector3 Direction = ene.target.transform.position - transform.position;
        ene.target.GetComponent<Rigidbody2D>().AddForce(Direction * damage*10);
    }
}
