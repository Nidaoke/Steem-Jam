using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Character : MonoBehaviour {

    public float horizontalSpeed = 1f;
    public float verticalSpeed = 1f;

    public AudioSource deathSound, oofSound, attackSound;

    public int lives = 5;

    public Text livesText;

    public GameObject killThing;

    public Transform enemy;

    public Transform barrierTop, barrierBot, barrierWest, barrierEast;

    public GameObject camera1;

    public bool canGetHit = true;

    public Animator myAnimator;

	// Use this for initialization
	void Start () {
		
	}

    IEnumerator HitTime()
    {
        canGetHit = false;
        yield return new WaitForSeconds(.5f);
        canGetHit = true;
    }

    public void GetHit()
    {
        if (canGetHit)
        {
            Debug.Log("GotHit!");
            lives--;
            livesText.text = "Lives: " + lives;
            if (lives <= 0)
                Die();
            else
                oofSound.Play();
            StartCoroutine(HitTime());
        }
    }

    void Die()
    {
        deathSound.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Player_Character>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        //Destroy(gameObject);
    }

    void StartAttacker()
    {
        killThing.SetActive(true);
        //killThing.GetComponent<BoxCollider2D>().enabled = true;
        
    }

    void EndAttacker()
    {
        killThing.SetActive(false);
        //killThing.GetComponent<BoxCollider2D>().enabled = false;
        attackSound.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!myAnimator.GetBool("Attack"))
            {
                myAnimator.SetBool("Attack", true);
            }
        }

        /*if (myAnimator.GetBool("Attack"))
        {
            if (!killThing.activeSelf)
            {
                killThing.SetActive(true);
            }
        }
        else
        {
            killThing.SetActive(false);
        }*/

        //Debug.Log("Dist: " + (transform.position.y - enemy.transform.position.y)); //7.5!!!
	}
    
    public void AnimAttackEnd()
    {
        myAnimator.SetBool("Attack", false);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool moveCamera = false;

        if (transform.position.y >= barrierTop.position.y && moveVertical > 0)
        {
            moveVertical = 0;
            transform.position = new Vector3(transform.position.x, barrierTop.position.y, transform.position.z);
        }
        if (transform.position.y <= barrierBot.position.y && moveVertical < 0)
        {
            moveVertical = 0;
            transform.position = new Vector3(transform.position.x, barrierBot.position.y, transform.position.z);
        }

        if(transform.position.x <= barrierWest.position.x && moveHorizontal < 0)
        {
            moveHorizontal = 0;
            transform.position = new Vector3(barrierWest.position.x, transform.position.y, transform.position.z);
        }

        if(GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            if(transform.position.x >= barrierEast.position.x && moveHorizontal > 0)
            {
                moveHorizontal = 0;
                transform.position = new Vector3(barrierEast.position.x, transform.position.y, transform.position.z);
            }
        }
        else
        {
            if (transform.position.x >= barrierEast.position.x && moveHorizontal > 0)
            {
                //barrierEast.parent = gameObject.transform;
                //camera1.transform.parent = gameObject.transform;
                moveCamera = true;
            }
            else
            {
                //barrierEast.parent = null;
                //camera1.transform.parent = null;
                moveCamera = false;
            }
        }

        if (moveHorizontal < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (moveHorizontal > 0)
            transform.localScale = new Vector3(1, 1, 1);

        if(moveHorizontal != 0)
        {
            myAnimator.SetBool("Running", true);
        }
        else
        {
            myAnimator.SetBool("Running", false);
        }

        if (moveCamera)
        {
            camera1.transform.Translate(moveHorizontal * horizontalSpeed, 0.0f, 0.0f);
        }

        transform.Translate(moveHorizontal * horizontalSpeed, moveVertical * verticalSpeed, 0.0f);
    }
}
