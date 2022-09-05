using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : Scene
{
    // �񵿱�� ������ ĳ���� ����
    List<SpawnPoint> spawnList = new List<SpawnPoint>();

    // 1. ������ ĳ���� ����Ʈ�� ����
    // 2. �񵿱�� ĳ���� ����Ʈ�� ����
    // 3. �����̵� ĳ���Ͱ� �ִٸ� �ε� ui�� ��������� �մϴ�.
    public IEnumerator IELoadAll()
    {
        SpawnPoint[] spawnArr = GameObject.FindObjectsOfType<SpawnPoint>();

        // spawnList�� ���� ������ �Ϸ���� ���� ĳ���� ����Ʈ�� ����Ų��.!
        spawnList.AddRange(spawnArr);

        // ������ ĳ������ ������ �޽��ϴ�.
        int totalCount = spawnList.Count;

        // �񵿱� �Լ��� ȣ���մϴ�.
        for (int i = 0; i < spawnList.Count; ++i)
            spawnList[i].Create();

        List<SpawnPoint> temp = new List<SpawnPoint>();

        float ratio = 0.3f;

        // ������ ĳ���Ͱ� ���������� ��� ��ȸ�ϰڴ�.!
        while (spawnList.Count > 0)
        {
            temp.Clear();
            foreach (SpawnPoint s in spawnArr)
            {
                // ���� �ε�� ���°� �ƴ϶�� temp���� �����մϴ�.
                if (!s.IsCreated)
                    temp.Add(s);
                else
                {
                    ratio += (0.7f) / totalCount;
                    UIMng.Instance.CallEvent(UIType.LoadingUI, "SetValue", ratio);
                }
            }
            spawnList.Clear();
            spawnList.AddRange(temp);
            yield return null;

        }

        UnitMng.Instance.GetPlayer();
        yield return new WaitForSeconds(1.0f);

        
        // �ε� ui�� ���ݴϴ�.
        UIMng.Instance.SetActive(UIType.LoadingUI, false);

        UnitMng.isUnitcreateComplete = true;
        
        UIMng.Instance.SetActive(UIType.UIIngame, true);
        ControlMng.Instance.SetEnable(true);

        foreach(var value in GameDB.charDic)
        {
            value.Value.currHp = value.Value.lastState[2];
        }

        // ���̵� �� ó���� �մϴ�.
        UIMng.Instance.FadeIn(1);
        AudioMng.Instance.PlayBackground("LobbyBackground", 0.3f);
        UIMng.Instance.SetEventSystme(true);
        UnitMng.Instance.ResumeAll();


        
    }

    public void LoadAll()
    {

        StartCoroutine(IELoadAll());
    }


    #region Scene���� ��ӹ��� �Լ���
    // �� ������ �ε尡 �Ϸ�Ǵ� ������ ȣ��Ǵ� �Լ��Դϴ�.
    public override void Enter()
    {

        // ���ӽ� �ʱ�ȭ
        GameScene.isClear = false;
        //GameScene.firstPlayer = false;
        GameScene.stageStart = false;
        //

        GameDB.currSceneType = SceneType.LobbyScene;
        AIPathMng.Instance.CallEvent(AIPathType.AIGrid2DRenderer, "CreateGrid2D");
        AIPathMng.Instance.CallEvent(AIPathType.AIGrid2DRenderer, "WallCreate");
        LoadAll();
        UIMng.Instance.modeChange();
        
    }

    // �� ������ �ٸ� �� ���Ϸ� ������ �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ��Դϴ�.
    public override void Exit()
    {
        UIMng.Instance.Get<UIDungeon>(UIType.UIDungeon).SetActive(false);
        AIPathMng.Instance.CallEvent(AIPathType.AIGrid2DRenderer, "WallDestory");

        
    }
    // ������ �ε�ǰ� �������� ��Ȳ�� �����ֱ� ���� �Լ��Դϴ�.
    // �Ʒ��� �Լ����� �ε� ui�� ����Ҽ��� �ֽ��ϴ�.

    public override void Progress(float progress)
    {

    }
    #endregion Scene���� ��ӹ��� �Լ���

    private void OnGUI()
    {
        //// ��ư�� Ŭ���ϸ� ���� ������ �����մϴ�.
        //if (GUI.Button(new Rect(0, 0, 100, 100), "NextScene"))
        //{

        //    SceneMng.Instance.EnableDelay(1.0f, SceneType.GameScene);

        //    // 1�ʵ��� ȭ���� �˰� �����.
        //    UIMng.Instance.FadeOut(1);

        //    // 1�ʵڿ� �ε� ui�� �����ش�.
        //    UIMng.Instance.ShowDelay(1.0f, UIType.LoadingUI);
        //    UnitMng.Instance.Clear();


        //}
    }

}
