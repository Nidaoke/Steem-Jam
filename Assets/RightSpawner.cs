using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSpawner : MonoBehaviour {

    //public int spawned = 0;
    public GameObject enemy, enemy2;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Spawn(3));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Spawn(float time)
    {
        yield return new WaitForSeconds(time);
        float num = Random.Range(0.0f, 1.0f);
        if (num > .5f)
            Instantiate(enemy, transform.position, Quaternion.identity);
        else
            Instantiate(enemy2, transform.position, Quaternion.identity);
        StartCoroutine(Spawn(7));
    }
}
