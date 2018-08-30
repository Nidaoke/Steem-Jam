using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject player;
    public bool shooting = false;
    public Animator myAnimator;
    public GameObject bullet;

    //public int lives = 2;



    public AudioSource shootSound;

    public float timeToShoot = 1f;
    public bool canShoot = true;

    public float verticalSpeed, horizontalSpeed;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        myAnimator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
        if(player != null)
        {
            //Debug.Log(Mathf.Abs(transform.position.y - transform.position.y));
            if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 7.5f && Mathf.Abs(transform.position.y - player.transform.position.y) <= .5f)
            {
                if(transform.position.x > (GameObject.FindGameObjectWithTag("MainCamera").transform.position.x + 7.5f))
                {
                    myAnimator.SetBool("Move", true);
                    transform.Translate(-horizontalSpeed, 0, 0);
                    transform.localScale = new Vector3(-9.6342f, 9.6342f, 9.6342f);
                }
                else
                {
                    if (!shooting && canShoot)
                    {
                        Shoot();
                    }
                    myAnimator.SetBool("Move", false);
                }
                
            }
            else
            {
                if (transform.position.x > player.transform.position.x && Mathf.Abs(transform.position.x - player.transform.position.x) > 7.5f)
                {
                    myAnimator.SetBool("Move", true);
                    transform.Translate(-horizontalSpeed, 0, 0);
                    transform.localScale = new Vector3(-9.6342f, 9.6342f, 9.6342f);
                }

                if (transform.position.x < player.transform.position.x && Mathf.Abs(transform.position.x - player.transform.position.x) > 7.5f)
                {
                    myAnimator.SetBool("Move", true);
                    transform.Translate(horizontalSpeed, 0, 0);
                    transform.localScale = new Vector3(9.6342f, 9.6342f, 9.6342f);
                }

                if (transform.position.y < player.transform.position.y && Mathf.Abs(transform.position.y - player.transform.position.y) > .5f)
                {
                    myAnimator.SetBool("Move", true);
                    transform.Translate(0, verticalSpeed, 0);
                    //transform.localScale = new Vector3(-9.6342f, -9.6342f, -9.6342f);
                }

                if (transform.position.y > player.transform.position.y && Mathf.Abs(transform.position.y - player.transform.position.y) > .5f)
                {
                    myAnimator.SetBool("Move", true);
                    transform.Translate(0, -verticalSpeed, 0);
                    //transform.localScale = new Vector3(-9.6342f, -9.6342f, -9.6342f);
                }
            }
        }

        
	}

    void Shoot()
    {
        shooting = true;
        canShoot = false;
        myAnimator.SetBool("Attack", true);
        StartCoroutine(WaitToShoot());
    }

    void EndShooting()
    {
        shooting = false;
        myAnimator.SetBool("Attack", false);
    }

    void CastBullet()
    {
        Instantiate(bullet, transform.position + new Vector3(1, .8f, 0), Quaternion.identity);
        shootSound.Play();
    }

    IEnumerator WaitToShoot()
    {
        //Debug.Log("Before");
        yield return new WaitForSeconds(timeToShoot);
        //Debug.Log("After");
        canShoot = true;
    }
}
