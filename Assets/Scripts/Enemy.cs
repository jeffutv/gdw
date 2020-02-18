using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Vector2 direction;
    public GameObject shot;
    public Transform shotSpawn;
    public float minShotDelay;
    public float maxShotDelay;
    public int points;


    Rigidbody2D rb;
    //AudioSource audio;
    BoxCollider2D playArea;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //audio = GetComponent<AudioSource>();
        direction.x = Random.Range(-direction.x, direction.x);
        rb.velocity = direction;

        playArea = GameObject.Find("PlayArea").GetComponent<BoxCollider2D>();

        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.position.x > playArea.bounds.max.x && rb.velocity.x > 0)
        {
            direction.x = -Mathf.Abs(direction.x);
            rb.velocity = direction;
        }
        else if (rb.position.x < playArea.bounds.min.x && rb.velocity.x < 0)
        {
            direction.x = Mathf.Abs(direction.x);
            rb.velocity = direction;
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(Random.Range(minShotDelay, maxShotDelay));

        while (true)
        {
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            yield return new WaitForSeconds(Random.Range(minShotDelay, maxShotDelay));
        }
    }

    public void Damage()
    {
        anim.Play("explosion");
        Destroy(gameObject, 1);
    }

    //private void OnDestroy()
    //{
    //    audio.Play();
    //}
}
