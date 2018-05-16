using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicActivator : MonoBehaviour {

    AudioSource seq ;

	// Use this for initialization
	void Awake () {
        seq = GameObject.Find("Sequencer").GetComponent<AudioSource>();

    }
	
	// Update is called once per frame

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!seq.isPlaying && col.gameObject.tag == "Redline")
        {
            seq.Play();
        }
        
    }
}
