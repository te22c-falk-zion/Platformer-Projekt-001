using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiffDisplay : MonoBehaviour
{
    [SerializeField]
    TMP_Text pointsText;
    [SerializeField] Slider diffSlider;
    void Update()
    {
        pointsText.text = "BulletTimer: " + diffSlider.value;
    }
}
