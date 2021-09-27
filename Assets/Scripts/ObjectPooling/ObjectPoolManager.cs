using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager _instance;//실제이름
    public static ObjectPoolManager Instance//밖에서 쓰이는 이름
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ObjectPoolManager();
            }
            return _instance;
        }


    }

    public ObjectPool pool;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
}
