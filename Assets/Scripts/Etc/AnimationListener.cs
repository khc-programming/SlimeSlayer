using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationListener : MonoBehaviour
{
    // 
    
    public void CallEvent(string functionName)
    {
        // SendMessage�迭�� �Լ��� ����� ��쿡��
        // �Լ��� �̸��� �ߺ����� �ʵ��� �����Ͻô� ����
        // �����ϴ�.
        transform.SendMessageUpwards(functionName,
            SendMessageOptions.DontRequireReceiver);
    }
  
}
