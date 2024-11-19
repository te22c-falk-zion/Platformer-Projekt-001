using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bulletmanager : MonoBehaviour
{
    [SerializeField]
    public static float timeBetweenBullet;
    float timeSinceLastBullet;

    [SerializeField]
    GameObject bulletPrefab;

    private void Awake() 
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        DontDestroyOnLoad(this.gameObject);
        if (sceneName == "Dead")
        {
            Destroy(this.gameObject);
        }
    }

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
