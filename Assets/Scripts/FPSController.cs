using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSController : MonoBehaviour
{
    private TMP_Text _fpsText; 
    private void Start()
    {
        _fpsText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        float fps = 1.0f / Time.deltaTime;
        _fpsText.text = "" + (int)fps;
    }
}
