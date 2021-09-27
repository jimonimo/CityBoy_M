using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameManager poolObj;
    [SerializeField]
    private int allocateCount;

    private Stack<GameManager> poolStack = new Stack<GameManager>();

    void Start()
    {
        Allocate();
    }
    //오브젝트생성 스택할당
    public void Allocate()
    {

        for (int i = 0; i < allocateCount; i++)
        {
            GameManager allocatObj = Instantiate(poolObj, this.gameObject.transform);
        }
    }

    public GameObject Pop()
    {
        GameManager obj = poolStack.Pop();
        obj.gameObject.SetActive(true);
        return obj.gameObject;
    }

    public void Push(GameManager obj)
    {
        obj.gameObject.SetActive(false);
        poolStack.Push(obj);
    }
}
