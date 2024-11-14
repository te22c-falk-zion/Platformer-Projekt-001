using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{

    bool spawnLeft;
    PlayerController playerController;

    [SerializeField]
    float speed = 3f;



    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        int spawn = Random.Range(1,3);
        if (spawn == 1)
        {
            spawnLeft = true;
        }
        else
        {
            spawnLeft = false;
        }

        if(spawnLeft == false)
        {
        Vector2 position = new();
        position.y = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize + 4);
        position.x = Camera.main.orthographicSize + 3;

        transform.position = position;
        }
        if(spawnLeft == true)
        {
        Vector2 position = new();
            position.y = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize+4);
            position.x = -Camera.main.orthographicSize - 3;

            transform.position = position;
        }
    }


    void Update()
    {
        if (spawnLeft == false)
        {
            Vector2 movement = Vector2.left * speed * Time.deltaTime;
            transform.Translate(movement);

            if (transform.position.x < (-Camera.main.orthographicSize -3))
            {
                Destroy(this.gameObject);
            }
        }
        if (spawnLeft == true)
        {
            Vector2 movement = Vector2.right * speed * Time.deltaTime;
            transform.Translate(movement);

            if (transform.position.x > (Camera.main.orthographicSize +3))
            {
                Destroy(this.gameObject);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            playerController.TakeDamage(10);
        }
        
    }
}
