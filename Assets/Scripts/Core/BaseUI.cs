using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Init() Start() 대신 시작을 관리하는 함수
// Run() 업데이트를 대신 실시간 관리하는 함수
// Open() 함수가 생성될 때???
// Close() 함수를 닫을 때 사용

public abstract class BaseUI : MonoBehaviour
{

    public bool isInit = false;

    public abstract void Init();
    public abstract void Run();

    public abstract void Open();

    public abstract void Close();

    public virtual void OnActive()
    {
    }
    public virtual void OnDeactive()
    {

    }
    public virtual void OnUIEnable()
    {
    }
    public virtual void OnUIDisable()
    {

    }
    public virtual void Destroy(float delayTime = 0)
    {
        Object.Destroy(gameObject, delayTime);
    }
    public virtual void SetActive(bool state)
    {
        

        if (state)
        {
            OnActive();
        }
        else
        {
            OnDeactive();
        }
        gameObject.SetActive(state);
    }
    public virtual void SetEnable(bool state)
    {
        if (state)
        {
            OnUIEnable();
        }
        else
        {
            OnUIDisable();
        }
        enabled = state;
    }

    private void Update()
    {
        //Run();
    }
}
