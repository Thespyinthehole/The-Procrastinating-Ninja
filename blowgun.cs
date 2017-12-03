using UnityEngine;
using System.Collections;

public class blowgun : MonoBehaviour {
    float lastx;
    bool shot = false;
    public int reloadtimer = 0;
    float angle;
    Transform end;
    public GameObject dart;

    void Start()
    {
        transform.Rotate(0, 0, 90);
        lastx = Input.mousePosition.x;
        end = transform.GetChild(1);
    }

	void Update ()
    {
        LookMouse();
        if (Input.GetButtonDown("Fire1") && !shot) Shoot();
        if (shot)
        {
            ++reloadtimer;
            if (reloadtimer == 25) shot = false;
        }
	}

    void Shoot()
    {
        Instantiate(dart, end.position, Quaternion.identity);
        shot = true;
        reloadtimer = 0;
    }

    void LookMouse()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = transform.position.z;
        Vector3 direction = mousepos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.identity;
        transform.Rotate(0, 0, angle);
    }
}
