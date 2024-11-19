using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiffScaler : MonoBehaviour
{

    [SerializeField] Slider diffSlider;
    [SerializeField] bulletmanager bulletmanager;
    
    void Update()
    {
        bulletmanager.timeBetweenBullet = diffSlider.value;
    }
}
