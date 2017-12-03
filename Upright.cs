using UnityEngine;
using System.Collections;

public class Upright : MonoBehaviour {
    public bool turnable;
    public bool Uprightable;

    bool right = true;
    bool left = false;
	void Update () {
        if (turnable)
        {
            if (Input.GetAxis("Horizontal") < 0 && !left) LookLeft();
            if (Input.GetAxis("Horizontal") > 0 && !right) LookRight();
        }
        if (Uprightable) transform.rotation = Quaternion.identity;
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


    void Switch()
    {
        Vector3 size = transform.localScale;
        size.x *= -1;
        transform.localScale = size;
    }
}
