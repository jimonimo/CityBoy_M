using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePlus : MonoBehaviour
{
    public bool getLife = false;
    void Start()
    {
        
    }
    void Update()
    {
        if (getLife)
        {
            this.gameObject.SetActive(false);
            getLife = false;
        }
    }
}
