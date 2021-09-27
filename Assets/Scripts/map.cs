using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * scrollSpeed;//옵셋설정시간*스크롤 스피드
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));

    }
}
