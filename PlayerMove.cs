using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    WeaponAttached weapon;
    AudioSource emitter;

    int jumps = 0;
    float timeMultiplier = 1;
    bool crawling = false;

    public List<string> screams;
    public int maxjumps = 1;
    public int health;
    public int maxHealth;
    public float jumpPower = 250;
    public float horizonalSpeed = 1;
    public GameObject text;
    public AudioClip jumpNoise;
    public AudioClip wallJumpNoise;
    public AudioClip walking;

    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponAttached>();
        emitter = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumps > 0) Jump(jumpPower);
        HorizionalMove(Input.GetAxis("Horizontal"));
        if (Input.GetAxis("Vertical") < 0 && !crawling) StartCrawl();
        if (Input.GetAxis("Vertical")>= 0 && crawling) EndCrawl();
        if (Input.GetButtonDown("Cancel")) SceneManager.LoadScene("Menu");
    }

    void HorizionalMove(float speed)
    {
        rb.AddForce(Vector2.right * speed * horizonalSpeed / timeMultiplier * 2);

        emitter.clip = walking;
        if (!emitter.isPlaying && (speed <-0.2 || speed > 0.2))
            emitter.Play();
    }

    void StartCrawl()
    {
        crawling = true;
        transform.localScale /= 2;
    }

    void EndCrawl()
    {
        crawling = false;
        transform.localScale *= 2;
    }

    void Jump(float speed)
    {
        rb.AddForce(Vector2.up * speed / timeMultiplier /2);
        --jumps;
        emitter.clip = jumpNoise;
        emitter.Play();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Floor") jumps = maxjumps;
        if (col.gameObject.tag == "Wall") WallJump(col.contacts[0].normal, jumpPower);
    }

    void WallJump(Vector2 direction, float speed)
    {
        if (Input.GetButton("Jump") && !crawling)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(direction * speed / timeMultiplier);
            rb.AddForce(Vector2.up * speed / timeMultiplier);

            emitter.clip = wallJumpNoise;
            emitter.Play();
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
        (Instantiate(text, transform.position, Quaternion.identity) as GameObject).GetComponent<TextMesh>().text = screams[Random.Range(0, screams.Count)];
    }

    public void Die()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ChangeMultiplier(float time, float maxTime)
    {
        if (timeMultiplier > 0.25) timeMultiplier = time / maxTime;
        weapon.timeMultipier = timeMultiplier;
    }
}
