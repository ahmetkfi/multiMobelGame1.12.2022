using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform _transformPlayer;
    public GameObject _bullet;
    public Transform _firePoint;
    float _fireSpeed= 1800f;
    
    float timeSinceLastShot;
    
    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    public void Shoot() {
        if (gunData.currentAmmo > 0) {
            if (CanShoot()) {
                
                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                
                GameObject newBullet = Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
                Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(_transformPlayer.forward*_fireSpeed);
            }
        }
    }
    
    
    private void Update() {
        timeSinceLastShot += Time.deltaTime;
        
    }
}
