using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    public AudioSource win;
    public GameObject player, right, left;
    public GameObject textBox;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            EndTheGame();
        }
    }

    IEnumerator Ender()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }

    void EndTheGame()
    {
        win.Play();
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<Player_Character>().enabled = false;
        right.SetActive(false);
        left.SetActive(false);
        textBox.SetActive(true);
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject mons in monsters)
        {
            Destroy(mons);
        }
        StartCoroutine(Ender());
    }
}
