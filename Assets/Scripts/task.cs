using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class task : MonoBehaviour
{
    /*public int seconds;
    float timer = 0.0f;
    private void Update()
    {
        // seconds in float
        timer += Time.deltaTime;
        // turn seconds in float to int
        seconds = (int)(timer % 60);
        print(seconds);

    }*/
    
    /*public GameObject myObject;

    void Update()
    {
        myObject.transform.position = new Vector3(0,myObject.transform.position.y+.1f,0); 
        // Error (Operator ‘+’ cannot be applied to operands of type ‘Vector3’ and ‘float’
    }*/
    private int[] myInts={3,1};
    private List<int> myList=new List<int>();
    private void Start()
    {
        AddArrayValue(4);
        Debug.Log(myList[myList.Count-1]);
        AddArrayValue(3);
        Debug.Log(myList[myList.Count-1]);
        AddArrayValue(5);
        Debug.Log(myList[myList.Count-1]);
    }
    
    public void AddArrayValue(int input)
    {
        myList.Add(input);
    }


}
