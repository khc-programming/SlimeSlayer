using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScene : Scene
{

    #region Scene���� ��ӹ��� �Լ���
    // �� ������ �ε尡 �Ϸ�Ǵ� ������ ȣ��Ǵ� �Լ��Դϴ�.
    public override void Enter()
    {
        GameDB.currSceneType = SceneType.BossScene;
        UIMng.Instance.modeChange();
    }

    // �� ������ �ٸ� �� ���Ϸ� ������ �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ��Դϴ�.
    public override void Exit()
    {

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
