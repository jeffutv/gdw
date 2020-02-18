using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public BoxCollider2D playArea;
    public GameObject shot;
    public Transform shotSpawn;
    public float shotDelay;
    public GameObject gameOverPanel;
    public int lives;
    public float immunityLength;
    public Text livesText;

    bool canFire = true;
    bool canBeHit = true;

    Rigidbody2D rb;
    SpriteRenderer sr;
    //AudioSource audio;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(moveH, moveV);
        rb.velocity = move * speed;

        // Clamp Ship position to playArea
        float clampX = Mathf.Clamp(rb.position.x, playArea.bounds.min.x, playArea.bounds.max.x);
        float clampY = Mathf.Clamp(rb.position.y, playArea.bounds.min.y, playArea.bounds.max.y);
        rb.position = new Vector2(clampX, clampY);

        if (Input.GetButtonDown("Jump"))
        {
            Shoot();
        }

        if (Input.GetButton("Jump") && canFire)
        {
            Shoot();
            StartCoroutine(ShotCooldown());
        }
    }

    void Shoot()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
    }

    IEnumerator ShotCooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(shotDelay);
        canFire = true;
    }

    private void OnDestroy()
    {
        gameOverPanel.SetActive(true);
        //audio.Play();
    }

    public void LoseALife()
    {
        lives--;
        livesText.text = "";

        if (lives <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(ImmunityCooldown());
            for (int i = 0; i < lives; i++)
            {
                livesText.text += "A";
            }
        }
    }

    IEnumerator ImmunityCooldown()
    {
        canBeHit = false;
        sr.color = Color.gray;
        yield return new WaitForSeconds(immunityLength);
        canBeHit = true;
        sr.color = Color.white;
    }
}
