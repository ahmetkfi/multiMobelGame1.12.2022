using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseVisibleScript : MonoBehaviour
{
    [SerializeField] private GameObject houseTopPart;
    [SerializeField] private Collider houseGround;

    /*HER EVİN BİR ZEMİN OBJESİ OLACAK ZEMİN OBJESİ BU SCRİPTE AİT OLACAK VE OYUNCU BU ZEMİNE GİRDİĞİNDE EVİN ÜST KISMI GÖRÜNMEZ OLUP OYUNCU ZEMİNDEN ÇIKINCA GÖRÜNÜR OLACAK*/

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            StartCoroutine(DisableHouseTop());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            StartCoroutine(ActiveHouseTop());
        }
    }


    IEnumerator DisableHouseTop()
    {
        yield return new WaitForSeconds(.1f);
        houseTopPart.SetActive(false);
    }

    IEnumerator ActiveHouseTop()
    {
        yield return new WaitForSeconds(.1f);
        houseTopPart.SetActive(true);
    }

 
}