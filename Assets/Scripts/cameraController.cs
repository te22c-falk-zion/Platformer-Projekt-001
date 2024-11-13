using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField]
    Transform player;

    Vector3 offset;
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = player.position.x;
        transform.position = player.position + offset;
    }
}
