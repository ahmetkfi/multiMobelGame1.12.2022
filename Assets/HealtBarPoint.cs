using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealtBarPoint : MonoBehaviour
{
    [SerializeField] private PlayerValuesSO _playerValuesSo;
    public bool playerTrigger = false;
    [SerializeField] private float takeTime = 2f;
    public Image circleImg;
    [SerializeField] private GameObject healtKit;
    public bool isHealing = false;
    [SerializeField] private Transform AxeJumpPosition;
    public float jumpPower=.0101f;
    public int numJumps=0;
    public float duration=.1f;
    private void Update()
    {
        if (isHealing)
        {
            healtKit.transform.DOJump(Vector3.up, jumpPower, numJumps, duration).SetRelative();
        }
        else
        {
            healtKit.transform.DORotate(new Vector3(0, 0, 90), 5f, RotateMode.LocalAxisAdd).SetLoops(-1).SetRelative()
                .SetEase(Ease.Linear);
            ControlPoint();
        }
        
    }

    public void ControlPoint()
    {
        if (playerTrigger)
        {
            circleImg.fillAmount -=  1.0f / takeTime * Time.deltaTime;
            if (circleImg.fillAmount==0)
            {
                isHealing = true;
                TakeHeal();
                
            }
        }
        else
        {
            circleImg.fillAmount +=1.0f/ takeTime * Time.deltaTime;
            isHealing = false;
        }
    }

    private void TakeHeal()
    {
        //Destroy(transform.root.gameObject);
        StartCoroutine(DestoryParent());
        if (_playerValuesSo.playerCurrentHealt is < 100 and >= 86)
        {
            _playerValuesSo.playerCurrentHealt = 100;
            
        }
        else
        {
            _playerValuesSo.playerCurrentHealt += 50;//15 olarak dğeişecek
            
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

    IEnumerator DestoryParent()
    {
        yield return new WaitForSeconds(.25f);
        Destroy(transform.root.gameObject);
    }
}
