using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int health;
    public int maxhealth;
    public LayerMask mask = -1;
    public int sight;
    public float speed;
    public GameObject loot;
    public int damage;

    TextMesh text;
    public Transform target = null;
    Vector3 lastSeenPos;
    Rigidbody2D rb;

    bool jumped = false;
    bool left = false;
    bool right = false;
    bool tracking = false;
    bool hunting = false;
    public bool found = false;
    public bool stuned = false;
    bool poisioned = false;
    bool cut = false;
    int Stuntime = 0;
    int cuttime = 0;
    int poisontime = 0;

    float startspeed;

    void Start()
    {
        startspeed = speed;
        health = maxhealth;
        text = transform.GetChild(0).GetChild(0).GetComponent<TextMesh>();
        text.text = health.ToString();
        transform.GetChild(1).GetComponent<CircleCollider2D>().radius = sight;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
        text.text = health.ToString();
    }

    void Die()
    {
        if (loot != null) loot.transform.position = transform.position;
        if (GameObject.Find("QuestSystem") != null) GameObject.Find("QuestSystem").GetComponent<Quests>().EnemyKilled();
        if (GameObject.Find("ArenaManager") != null) GameObject.Find("ArenaManager").GetComponent<SpawnEnemies>().RemoveEnemy(gameObject);
        Destroy(gameObject);
    }

    public void GetStunned()
    {
        stuned = true;
        Stuntime = 0;
        rb.velocity = Vector3.zero;
    }

    public void GetPoisoned()
    {
        poisioned = true;
        poisontime = 0;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") SeenPlayer(col.transform);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player") LostPlayer();
    }

    void SeenPlayer(Transform player)
    {
        target = player;
        hunting = true;
        tracking = false;
    }

    void LostPlayer()
    {
        target = null;
        hunting = false;
        tracking = true;
        rb.velocity = Vector2.zero;
    }

    void Update()
    {
        if (!stuned)
        {
            if (hunting) LookForPlayer();
            if (found)
            {
                if (hunting) Hunt();
                if (tracking) Track();
            }
        }
        else
        {
            ++Stuntime;
            if (Stuntime == 100) stuned = false;
        }

        if (cut)
        {
            ++cuttime;
            if (cuttime == 100) Healed();
        }

        if (poisioned)
        {
            ++poisontime;
            TakeDamage(1);
            if (poisontime == 100) Cured();
        }
        if (target != null)
        {
            if (target.position.x - transform.position.x > 0 && !left) LookLeft();
            if (target.position.x - transform.position.x < 0 && !right) LookRight();
        }
    }

    void Cured()
    {
        poisioned = false;
    }

    void LookLeft()
    {
        Switch();
        left = true;
        right = false;
    }

    void LookRight()
    {
        Switch();
        right = true;
        left = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Floor") jumped = false;
        if (col.gameObject.tag == "Player" && !stuned) Attack(col.gameObject);
    }

    void Attack(GameObject player)
    {
        player.GetComponent<PlayerMove>().PlayerTakeDamage(damage);
    }

    void Switch()
    {
        Vector3 size = transform.localScale;
        size.x *= -1;
        transform.localScale = size;


        size = text.gameObject.transform.parent.localScale;
        size.x *= -1;
        text.gameObject.transform.parent.localScale = size;
    }

    public void Cut()
    {
        cut = true;
        speed = startspeed / 2;
        cuttime = 0;
    }

    void Healed()
    {
        cut = false;
        speed = startspeed;
    }

    void LookForPlayer()
    {
        Vector3 direction = target.position - transform.position;
        Vector2 vdir = new Vector2(direction.x, direction.y);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vdir, sight, mask.value);
        if (hit.collider != null) if (hit.collider.transform == target) found = true;

    }

    void Hunt()
    {
        Vector3 direction = target.position - transform.position;
        lastSeenPos = target.position;
        MoveTowardsPoint(direction);
        if (target.position.y > transform.position.y && !jumped) jump();
    }

    void jump()
    {
        rb.AddForce(Vector3.up * 250);
        jumped = true;
    }

    void Track()
    {
        Vector3 direction = lastSeenPos - transform.position;
        MoveTowardsPoint(direction);
        if (Vector3.Distance(transform.position, lastSeenPos) < 0.5f)
        {
            found = false;
            tracking = false;
            hunting = false;
            rb.velocity = Vector2.zero;
        }
    }

    void MoveTowardsPoint(Vector3 direction)
    {
        if (rb.velocity.magnitude < 3)
            rb.AddForce(direction * speed);
    }
}
