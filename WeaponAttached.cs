using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponAttached : MonoBehaviour
{
    public float timeMultipier;
    public List<GameObject> weapons;
    public GameObject ActiveWeapon;

    Transform player;
    GameObject spawned;
    bool right = true;
    bool left = false;
    bool up = false;
    int index = -1;

    void Start()
    {
        NextWeapon();
        //Switch();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void NextWeapon()
    {
        ++index;
        Destroy(spawned);
        if (index > weapons.Count - 1) index = 0;
        ActiveWeapon = weapons[index];
        spawned = (GameObject)Instantiate(ActiveWeapon, transform.position, Quaternion.identity);
        spawned.transform.SetParent(transform);
        if (left && transform.localScale.x == 1) LookLeft();
        if (spawned.transform.localScale.x == -1 && spawned.name == "Blowgun(Clone)") Reset();
        if (!right && spawned.name == "Sword(Clone)") Flip();
    }


    void Reset()
    {
        Vector3 size = spawned.transform.localScale;
        size.x = 1;
        spawned.transform.localScale = size;
        size = transform.localScale;
        size.x = 1;
        transform.localScale = size;
    }

    void Flip()
    {
        Vector3 size = spawned.transform.localScale;
        size.x *= -1;
        spawned.transform.localScale = size;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2")) NextWeapon();
        transform.position = player.position;
        if (spawned.name != "Blowgun(Clone)")
        {
            if (Input.GetAxis("Horizontal") < 0 && !left) LookLeft();
            if (Input.GetAxis("Horizontal") > 0 && !right) LookRight();
            if (Input.GetAxis("Vertical") > 0 && !up) LookUp();
        } else
        {
            Debug.Log(spawned.name);
            if (Input.GetAxis("Horizontal") < 0 && !left)
            {
                left = true;
                right = false;
                up = false;
            }
            if (Input.GetAxis("Horizontal") > 0 && !right)
            {
                left = false;
                right = true;
                up = false;
            }
        }
    }

    void LookLeft()
    {
        if (up)
        {
            transform.Rotate(0, 0, -90);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else Switch();
        left = true;
        right = false;
        up = false;
    }

    void LookRight()
    {
        if (up)
        {
            transform.Rotate(0, 0, -90);
            transform.localScale = new Vector3(1, 1, 1);
        } else Switch();
        right = true;
        left = false;
        up = false;
    }

    void LookUp()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.Rotate(0, 0, 90);
        right = false;
        left = false;
        up = true;
    }
    void Switch()
    {
        Vector3 size = transform.localScale;
        size.x *= -1;
        transform.localScale = size;
    }
}