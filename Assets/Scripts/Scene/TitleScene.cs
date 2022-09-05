using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : Scene
{

    #region Scene���� ��ӹ��� �Լ���
    // �� ������ �ε尡 �Ϸ�Ǵ� ������ ȣ��Ǵ� �Լ��Դϴ�.
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

    // �� ������ �ٸ� �� ���Ϸ� ������ �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ��Դϴ�.
    public override void Exit()
    {
        UIMng.Instance.Get<UITitle>(UIType.UITitle).PosChange();
        UIMng.Instance.Get<UITitle>(UIType.UITitle).SetActive(false);
    }
    // ������ �ε�ǰ� �������� ��Ȳ�� �����ֱ� ���� �Լ��Դϴ�.
    // �Ʒ��� �Լ����� �ε� ui�� ����Ҽ��� �ֽ��ϴ�.

    public override void Progress(float progress)
    {
        // �Ʒ�ó�� �ڵ带 �ۼ��ϸ� 100% �� ������ �ε�Ǵ��� ui �����̴��� 30%�� ä������ ���°� �˴ϴ�.

    }

    #endregion Scene���� ��ӹ��� �Լ���
 
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
