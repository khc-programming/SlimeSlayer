using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationListener : MonoBehaviour
{
    // 
    
    public void CallEvent(string functionName)
    {
        // SendMessage계열의 함수를 사용할 경우에는
        // 함수의 이름이 중복되지 않도록 설계하시는 것이
        // 좋습니다.
        transform.SendMessageUpwards(functionName,
            SendMessageOptions.DontRequireReceiver);
    }
  
}
