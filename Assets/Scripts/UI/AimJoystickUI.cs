using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimJoystickUI : MonoBehaviour
{
    [SerializeField] private Image handleImg;


    public void AimTap()
    {
        Color handlecolor = handleImg.color;
        handlecolor.a = 1f;
        handleImg.color = handlecolor;
    }
    public void AimUnTap()
    {
        Color handlecolor1 = handleImg.color;
        handlecolor1.a = .25f;
        handleImg.color = handlecolor1;
    }
}
