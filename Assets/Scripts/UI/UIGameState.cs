using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameState : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GameClear()
    {
        UIMng.Instance.Get<UIIngame>(UIType.UIIngame).Open(true);
    }

    public void GameFail()
    {
        UIMng.Instance.Get<UIIngame>(UIType.UIIngame).Open(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
