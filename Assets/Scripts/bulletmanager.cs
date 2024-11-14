using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletmanager : MonoBehaviour
{
    [SerializeField]
    float timeBetweenBullet;
    float timeSinceLastBullet;

    [SerializeField]
    GameObject bulletPrefab;

    void Update()
    {
        timeSinceLastBullet += Time.deltaTime;
        if (timeSinceLastBullet > timeBetweenBullet)
        {
            Instantiate(bulletPrefab);
            timeSinceLastBullet = 0f;
        }
    }
}
