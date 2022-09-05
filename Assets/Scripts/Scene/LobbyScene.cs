using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : Scene
{
    // 비동기로 생성할 캐릭터 집합
    List<SpawnPoint> spawnList = new List<SpawnPoint>();

    // 1. 생성할 캐릭터 리스트를 취합
    // 2. 비동기로 캐릭터 리스트를 생성
    // 3. 생성이된 캐릭터가 있다면 로딩 ui에 적용해줘야 합니다.
    public IEnumerator IELoadAll()
    {
        SpawnPoint[] spawnArr = GameObject.FindObjectsOfType<SpawnPoint>();

        // spawnList는 현재 생성이 완료되지 않은 캐릭터 리스트를 가리킨다.!
        spawnList.AddRange(spawnArr);

        // 생성할 캐릭터의 개수를 받습니다.
        int totalCount = spawnList.Count;

        // 비동기 함수를 호출합니다.
        for (int i = 0; i < spawnList.Count; ++i)
            spawnList[i].Create();

        List<SpawnPoint> temp = new List<SpawnPoint>();

        float ratio = 0.3f;

        // 생성할 캐릭터가 있을때까지 계속 순회하겠다.!
        while (spawnList.Count > 0)
        {
            temp.Clear();
            foreach (SpawnPoint s in spawnArr)
            {
                // 아직 로드된 상태가 아니라면 temp값에 저장합니다.
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

        
        // 로딩 ui를 꺼줍니다.
        UIMng.Instance.SetActive(UIType.LoadingUI, false);

        UnitMng.isUnitcreateComplete = true;
        
        UIMng.Instance.SetActive(UIType.UIIngame, true);
        ControlMng.Instance.SetEnable(true);

        foreach(var value in GameDB.charDic)
        {
            value.Value.currHp = value.Value.lastState[2];
        }

        // 페이드 인 처리를 합니다.
        UIMng.Instance.FadeIn(1);
        AudioMng.Instance.PlayBackground("LobbyBackground", 0.3f);
        UIMng.Instance.SetEventSystme(true);
        UnitMng.Instance.ResumeAll();


        
    }

    public void LoadAll()
    {

        StartCoroutine(IELoadAll());
    }


    #region Scene에서 상속받은 함수들
    // 신 파일이 로드가 완료되는 시점에 호출되는 함수입니다.
    public override void Enter()
    {

        // 게임신 초기화
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

    // 신 파일이 다른 신 파일로 변경이 완료되었을 때 호출되는 함수입니다.
    public override void Exit()
    {
        UIMng.Instance.Get<UIDungeon>(UIType.UIDungeon).SetActive(false);
        AIPathMng.Instance.CallEvent(AIPathType.AIGrid2DRenderer, "WallDestory");

        
    }
    // 파일이 로드되고 있을때의 상황을 보여주기 위한 함수입니다.
    // 아래의 함수에서 로딩 ui를 출력할수도 있습니다.

    public override void Progress(float progress)
    {

    }
    #endregion Scene에서 상속받은 함수들

    private void OnGUI()
    {
        //// 버튼을 클릭하면 다음 신으로 변경합니다.
        //if (GUI.Button(new Rect(0, 0, 100, 100), "NextScene"))
        //{

        //    SceneMng.Instance.EnableDelay(1.0f, SceneType.GameScene);

        //    // 1초동안 화면을 검게 만든다.
        //    UIMng.Instance.FadeOut(1);

        //    // 1초뒤에 로딩 ui를 보여준다.
        //    UIMng.Instance.ShowDelay(1.0f, UIType.LoadingUI);
        //    UnitMng.Instance.Clear();


        //}
    }

}
