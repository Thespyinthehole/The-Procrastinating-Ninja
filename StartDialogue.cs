using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartDialogue : MonoBehaviour {
    public List<string> Dialogue;
    public GameObject ninja;
    public AudioClip ding;

    AudioSource emitter;
    GameObject tospawn;

    int listIndex = 0;
    int stringIndex = 0;

    
    TextMesh text;

    void Start()
    {
        tospawn = transform.GetChild(1).gameObject;
        text = transform.GetChild(0).GetComponent<TextMesh>();
        emitter = GetComponent<AudioSource>();
        StartCoroutine(AddText());
    }
    

    IEnumerator AddText()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            ++stringIndex;
            if (stringIndex > Dialogue[listIndex].Length)
            {
                stringIndex = 0;
                ++listIndex;
                emitter.Stop();
                emitter.clip = ding;
                emitter.Play();
                if (listIndex == Dialogue.Count) Done();
            }

            string todisplay = "";

            for (int i = 0; i < stringIndex; i++)
            {
                todisplay += Dialogue[listIndex][i];
            }

            text.text = todisplay;
        }
    }

    void Done()
    {
        Instantiate(ninja, tospawn.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
