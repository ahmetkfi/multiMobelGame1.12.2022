using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletUI : MonoBehaviour
{
    [SerializeField] private PlayerValuesSO _playerValuesSo;
    [SerializeField] private TextMeshProUGUI _bulletTxt;



    public void Shoot()
    {
        _bulletTxt.text = _playerValuesSo.bulletCount.ToString();
        if(_playerValuesSo.bulletCount==0)
            StartCoroutine(ReloadBullet());
    }
    IEnumerator ReloadBullet()
    {
        yield return new WaitForSeconds(2f);
        _playerValuesSo.bulletCount = 20;
        _bulletTxt.text = _playerValuesSo.bulletCount.ToString();
    }
}
