using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagPoint : MonoBehaviour
{
    [SerializeField] private PlayerValuesSO _playerValuesSo;
    public Image circleImg;
    public bool playerTrigger=false;
    public float takeTime = 7;
    public float waitTime = 40;
    public bool alreadyTaken = false;
    public Renderer flagMaterial;

    private void Update()
    {
        ControlPoint();
    }


    private void ControlPoint()
    {
        if (playerTrigger)
        {
            if (!alreadyTaken)
            {
                if (circleImg.fillAmount != 0)
                {
                    circleImg.fillAmount -= 1.0f / takeTime * Time.deltaTime;
                }
                else
                {
                    flagMaterial.material = _playerValuesSo.playerTeamMaterial;   
                    alreadyTaken = true;
                }
            }
            else
            {
                if (circleImg.fillAmount <1f)
                {
                    circleImg.fillAmount += 1.0f / waitTime * Time.deltaTime;
                }
                else
                {
                    alreadyTaken = false;
                }
                
            }
        }
        else
        {
            if (!alreadyTaken)
            {
                if (circleImg.fillAmount < 1.0f)
                {
                    circleImg.fillAmount += 1.0f / takeTime * Time.deltaTime;
                }
            }
            else
            {
                if (circleImg.fillAmount <1f)
                {
                    circleImg.fillAmount += 1.0f / waitTime * Time.deltaTime;
                }
                else
                {
                    alreadyTaken = false;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTrigger = false;
        }
        
    }
}
