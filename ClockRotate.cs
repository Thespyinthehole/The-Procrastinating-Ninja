using UnityEngine;
using System.Collections;

public class ClockRotate : MonoBehaviour {
	void Update () {
        transform.Rotate(0, 0, -10*Time.deltaTime);
	}
}
