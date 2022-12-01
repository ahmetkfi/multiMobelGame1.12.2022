using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeToLive = 2f;
    private PlayerValuesSO _playerValuesSo;
    private GunData _gunData;
    private void Awake()
    {
        StartCoroutine(DestroyBullet());
    }


    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            _playerValuesSo.playerCurrentHealt = -_gunData.damage;

        }
    }
}
