using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 1f;
    public GameObject player;
    public AudioSource impact;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player.transform.position.x < transform.position.x)
        {
            speed = -speed;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        transform.Translate(speed, 0.0f, 0.0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<Player_Character>().GetHit();
            impact.Play();
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Bullet>().enabled = false;
            //Destroy(gameObject);
            //GetComponent<Enemy>().enabled = false;
        }
    }
}
