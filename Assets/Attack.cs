using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bullet" || other.gameObject.tag == "Enemy")
        {
            //if(other.gameObject.GetComponent<Enemy2> () != null)
            //{//
              //  other.gameObject.GetComponent<Enemy2>().GetHit();
            //}
            //else
            //{
                Destroy(other.gameObject);
            //}
            //Destroy(other.gameObject);
            GetComponent<AudioSource>().Play();
        }
    }
}
