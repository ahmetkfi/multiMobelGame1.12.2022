using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public float fireSpeed;
    [SerializeField] private FixedJoystick _aimJoystick;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;
    private Vector3 aimVelocity;
    public float fireRate = 0.5F;
    [SerializeField] private TextMeshProUGUI bulletTxt;
    private float nextFire = 0.0F;
    public int bulletCount = 20;
    public Vector3 aimDirection;
    private void FixedUpdate()
    {
        if (_aimJoystick.Horizontal >= 0.6f || _aimJoystick.Vertical >= 0.6f)
        {
            GetValue();
            Vector3 lookAtPoint = playerTransform.position + aimDirection;
            playerTransform.LookAt(lookAtPoint);
            nextFire += Time.deltaTime;
            if(nextFire>=fireRate && bulletCount!=0)
            {
                nextFire = 0;
                bulletCount--;
                bulletTxt.text = bulletCount.ToString();
                Shoot();
            }
        }
        else if (_aimJoystick.Horizontal <= -0.6f || _aimJoystick.Vertical <= -0.6f)
        {
            GetValue();
            Vector3 lookAtPoint = playerTransform.position + aimDirection;
            playerTransform.LookAt(lookAtPoint);
            nextFire += Time.deltaTime;
            if (nextFire >= fireRate && bulletCount!=0)
            {
                nextFire = 0;
                bulletCount--;
                bulletTxt.text = bulletCount.ToString();
                Shoot();
            }
                
        }
    }

    public void GetValue()
    {
        //Get the input direction
        float inputX = _aimJoystick.Horizontal;
        float inputY = _aimJoystick.Vertical;
        Vector3 inputDirection = new Vector3(inputX, 0, inputY);

        //Get the camera horizontal rotation
        Vector3 faceDirection = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z);

        //Get the angle between world forward and camera
        float cameraAngle = Vector3.SignedAngle(Vector3.forward, faceDirection, Vector3.up);

        //Finally rotate the input direction horizontally by the cameraAngle
        aimDirection = Quaternion.Euler(0, cameraAngle, 0) * inputDirection;
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(playerTransform.forward*fireSpeed);
        if (bulletCount == 0)
        {
            StartCoroutine(ReloadBullet());
        }
    }

    IEnumerator ReloadBullet()
    {
        yield return new WaitForSeconds(2f);
        bulletCount = 20;
        bulletTxt.text = bulletCount.ToString();
    }
}
