using UnityEngine;
using System.Collections;

public class StarDie : MonoBehaviour {
    int time = 0;

	void Update () {
        transform.Translate(Vector3.up * 0.02f);
        ++time;
        if (time == 100) Destroy(gameObject);
	}
}
