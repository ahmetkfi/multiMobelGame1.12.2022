using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MoveJoystickUI : MonoBehaviour
{
    [SerializeField] private Image handleImg;


    public void MoveTap()
    {
        Color handlecolor = handleImg.color;
        handlecolor.a = 1f;
        handleImg.color = handlecolor;
    }
    public void MoveUnTap()
    {
        Color handlecolor1 = handleImg.color;
        handlecolor1.a = .25f;
        handleImg.color = handlecolor1;
    }
}
