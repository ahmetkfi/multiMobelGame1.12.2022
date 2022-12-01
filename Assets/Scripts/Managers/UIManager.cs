using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public void OpenGameobject(GameObject gobj)
    {
        gobj.SetActive(true);
    }

    public void CloseGameobject(GameObject gobj)
    {
        gobj.SetActive(false);
    }
}
