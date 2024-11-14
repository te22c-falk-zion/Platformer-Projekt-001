using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    [SerializeField]
    float speed = 3f;

    [SerializeField]
    GameObject bulletPrefab;

    void Start()
    {
            Vector2 position = new();
            position.y = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize);
            position.x = Camera.main.orthographicSize + 1;

            transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = Vector2.left * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
