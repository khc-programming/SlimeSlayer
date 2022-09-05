using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : Scene
{

    #region Scene에서 상속받은 함수들
    // 신 파일이 로드가 완료되는 시점에 호출되는 함수입니다.
    public override void Enter()
    {

        GameDB.currSceneType = SceneType.TitleScene;
        
        UIMng.Instance.modeChange();
        //UIMng.Instance.SetEventSystme(true);
        UIMng.Instance.Get<UIIngame>(UIType.UIIngame).SetActive(false);
        UIMng.Instance.Get<UIMenu>(UIType.UIMenu).SetActive(false);
        UIMng.Instance.Get<UITitle>(UIType.UITitle).SetActive(true);
        UIMng.Instance.FadeIn(1);
        AudioMng.Instance.PlayBackground("TitleBackground", 0.3f);

        Invoke("delayAction", 1.2f);
    }

    public void delayAction()
    {
        UIMng.Instance.Get<UITitle>(UIType.UITitle).ani.SetTrigger("Action");
        AudioMng.Instance.Play2DEffect("door_lock_slide_04");
      
    }

    // 신 파일이 다른 신 파일로 변경이 완료되었을 때 호출되는 함수입니다.
    public override void Exit()
    {
        UIMng.Instance.Get<UITitle>(UIType.UITitle).PosChange();
        UIMng.Instance.Get<UITitle>(UIType.UITitle).SetActive(false);
    }
    // 파일이 로드되고 있을때의 상황을 보여주기 위한 함수입니다.
    // 아래의 함수에서 로딩 ui를 출력할수도 있습니다.

    public override void Progress(float progress)
    {
        // 아래처럼 코드를 작성하면 100% 신 파일이 로드되더라도 ui 슬라이더가 30%만 채워지는 상태가 됩니다.

    }

    #endregion Scene에서 상속받은 함수들
 
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
