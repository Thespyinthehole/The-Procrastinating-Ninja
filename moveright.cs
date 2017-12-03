using UnityEngine;
using System.Collections;

public class moveright : MonoBehaviour {
    
	void Update () {
        transform.Translate(Vector3.right*0.03f);
	}
}
