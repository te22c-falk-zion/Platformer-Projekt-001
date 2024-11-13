using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textTimer;
    float floatTimer;
    int timer = 0;
    void Start()
    {
        
    }

    void Update()
    {
        floatTimer += Time.deltaTime;
        timer = (int)floatTimer;
        textTimer.text = timer.ToString();
    }
}
