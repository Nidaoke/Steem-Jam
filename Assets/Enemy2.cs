using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour {

    public GameObject player;
    public bool shooting = false;
    public Animator myAnimator;
    public GameObject bullet;

    public AudioSource getHitSound;

    public int lives = 2;

    public float timeToShoot = 1f;
    public bool canShoot = true;

    public GameObject strikeZone;

    public float verticalSpeed, horizontalSpeed;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myAnimator = GetComponent<Animator>();
    }

    public void GetHit()
    {
        lives--;
        getHitSound.Play();
        if(lives <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            //Debug.Log(Mathf.Abs(transform.position.y - transform.position.y));
            if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 2f && Mathf.Abs(transform.position.y - player.transform.position.y) <= .4f)
            {
                if (!shooting && canShoot)
                {
                    Shoot();
                }
                myAnimator.SetBool("Move", false);
            }
            else
            {
                if (transform.position.x > player.transform.position.x && Mathf.Abs(transform.position.x - player.transform.position.x) > 2f)
                {
                    myAnimator.SetBool("Move", true);
                    transform.Translate(-horizontalSpeed, 0, 0);
                    transform.localScale = new Vector3(-10f, 10f, 10f);
                }

                if (transform.position.x < player.transform.position.x && Mathf.Abs(transform.position.x - player.transform.position.x) > 2f)
                {
                    myAnimator.SetBool("Move", true);
                    transform.Translate(horizontalSpeed, 0, 0);
                    transform.localScale = new Vector3(10f, 10f, 10f);
                }

                if (transform.position.y < player.transform.position.y && Mathf.Abs(transform.position.y - player.transform.position.y) > .4f)
                {
                    myAnimator.SetBool("Move", true);
                    transform.Translate(0, verticalSpeed, 0);
                    //transform.localScale = new Vector3(-9.6342f, -9.6342f, -9.6342f);
                }

                if (transform.position.y > player.transform.position.y && Mathf.Abs(transform.position.y - player.transform.position.y) > .4f)
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
        strikeZone.SetActive(true);
        myAnimator.SetBool("Attack", false);
    }

    void CloseStrike()
    {
        strikeZone.SetActive(false);
    }

    void CastBullet()
    {
        Instantiate(bullet, transform.position + new Vector3(1, .8f, 0), Quaternion.identity);
    }

    IEnumerator WaitToShoot()
    {
        //Debug.Log("Before");
        yield return new WaitForSeconds(timeToShoot);
        //Debug.Log("After");
        canShoot = true;
    }
}
