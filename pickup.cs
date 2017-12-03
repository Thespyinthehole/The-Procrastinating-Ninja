using UnityEngine;
using System.Collections;

public class pickup : MonoBehaviour {
    public GameObject pickedup;
    public GameObject prefabninja;

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Pickedup");
        if (col.gameObject.tag == "Player")
        {
            prefabninja.GetComponent<WeaponAttached>().weapons.Add(pickedup);
            Destroy(gameObject);
        }
    }
}
