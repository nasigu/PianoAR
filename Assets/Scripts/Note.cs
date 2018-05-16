using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Note : MonoBehaviour {

    Rigidbody2D rb;
    public float speed;
    TestSequencer seq;
    bool EventHandler;
    public bool statusNote;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        
    }
	void Start () {
        Transform tt = gameObject.GetComponent<Transform>();
        //EventHandler =  seq.GetComponent<TestSequencer>().EventClose;
        rb.velocity = new Vector2(0,-speed);
        
	}

    void Update()
    {
        /*if (seq.GetComponent<TestSequencer>().EventClose == true)
        {
            Vector3 jo = seq.GetComponent<TestSequencer>().vec;
            gameObject.GetComponent<Transform>().localScale = jo;
        }*/
    }

    
}
