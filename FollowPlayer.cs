using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
    GameObject player;
    public float buffer = 0.02f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 change = player.transform.position - transform.position;
        change.z = 0;
        transform.Translate(change * buffer);
    }
}
