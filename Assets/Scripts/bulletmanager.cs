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
    string sceneName;

    private void Awake() 
    {

        DontDestroyOnLoad(this.gameObject);

    }

    void Update()
    {        
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        timeSinceLastBullet += Time.deltaTime;
        if (timeSinceLastBullet > timeBetweenBullet && sceneName == "Main")
        {
            Instantiate(bulletPrefab);
            timeSinceLastBullet = 0f;
        }
    }
}
