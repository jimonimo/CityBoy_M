using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickLog : MonoBehaviour
{
    public string str;

    public void ChangeLog()
    {
        ClickImaged.Instance.ChangeLog(str); //로그이름 변경
        ClickImaged.Instance.ChangeSprite(); //로그이미지 변경
    }
}