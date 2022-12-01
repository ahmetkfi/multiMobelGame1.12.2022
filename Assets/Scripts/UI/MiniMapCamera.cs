using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float storedShadowDistance;

    private void OnPreRender()
    {
        storedShadowDistance = QualitySettings.shadowDistance;
        QualitySettings.shadowDistance = 0;
    }

    private void OnPostRender()
    {
        QualitySettings.shadowDistance = storedShadowDistance;
    }

    private void LateUpdate()
    {

        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;



    }
}