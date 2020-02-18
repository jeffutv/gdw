using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public string tagToDestroy;
    public GameObject playerExplosion;
    public GameObject enemyExplosion;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagToDestroy))
        {
            
            Destroy(gameObject);

            if (tagToDestroy == "Enemy")
            {
                // Link to GameController & increase the score by XXX
                int points = other.GetComponent<Enemy>().points;
                GameObject.FindWithTag("GameController").GetComponent<GameController>().AddToScore(points);
                Instantiate(enemyExplosion, other.transform.position, other.transform.rotation);
                Destroy(other.gameObject);
            }
            else if (tagToDestroy == "Player")
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                other.GetComponent<PlayerController>().LoseALife();
                // Drop Lives by 1
            }

            
        }
    }
}
