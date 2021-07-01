using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Image progressBar;
    
    private float staticPosition;

    private void Start()
    {
        
    }
    private void FixedUpdate()
    {

        if (staticPosition != player.transform.position.z)
        {
            progressBar.fillAmount += (player.position.z / 650) * 0.004f;
        }
        staticPosition = player.transform.position.z;

       

    }
}
