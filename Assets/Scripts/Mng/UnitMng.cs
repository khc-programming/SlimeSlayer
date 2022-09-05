using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LoaderAsync))]
public class UnitMng : Mng<UnitMng>
{
    private CameraMove cameraMove;
    private LoaderAsync loader;
    private Dictionary<int, Unit> unitDic =
        new Dictionary<int, Unit>();

    

    private Dictionary<int, Unit> tempDic =
        new Dictionary<int, Unit>();

    // 게인신 로드 완료 후 1초 동안 플레이어가 공격할 수 없게 하는 변수

    public static bool pause = false;

    // 신의 기본 유닛이 모두 생성되었는지 체크한다.
   
    public static bool isUnitcreateComplete = false;

    public Dictionary<int, Unit> getUnitDic
    {
        get { return unitDic; }
    }

    // 프리팸 경로
    private readonly string path = "Prefab/Unit/";

    // 고유 아이디를 지정하기 위해 추가하였습니다.
    //private static int uniqueCount = 10;

    // 고유 아이디를 지정하기 위해 추가하였습니다.
    private static int spawnCount = 10;




    // 1. 주변 반경 몇미터내에 있는 캐릭터를 알려주세요.!
    // 2. 상성이 있을때 그 상성에 맞는 캐릭터를 알려주세요.!
    // 3. 2D 게임일경우 SpriteDepth값을 조정하는 역할을 수행해야 합니다.
    // 4. 특정 상황에서 주변 캐릭터의 멈춤현상, 전체 캐릭터의 멈춤 현상등을 제어하는 코드가 있어야 합니다.
    // 5. 캐릭터 AI의 이동 우선 순위를 정할 수 있는 코드를 지원해야 합니다.

    #region // Mng 추상 메소드 정의부


    public override void Run()
    {
        if ((GameDB.MngEnabled & (int)MngType.UnitMng) != (int)MngType.UnitMng)
            return;

        if (Input.GetKeyDown(KeyCode.T))
        {


            foreach (var value in unitDic)
            {
               
                Monster m = value.Value.GetComponent<Monster>();
                if (m != null)
                    m.currState = UnitState.Chase;
                

            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            
            foreach (var value in unitDic)
            {
                Monster m = value.Value.GetComponent<Monster>();
                if (m != null) m.currState = UnitState.Idle;

            }
        }

        

    }

    public override void FixRun()
    {

    }

    public override void LateRun()
    {

    }

    public override void Init()
    {
        mngType = MngType.UnitMng;
        loader = GetComponent<LoaderAsync>();
        cameraMove = GameObject.FindObjectOfType<CameraMove>();
    }

    public override void OnActive()
    {
      //  GameDB.MngEnabled += (int)MngType.UnitMng;
    }
    public override void OnDeactive()
    {
      //  GameDB.MngEnabled -= (int)MngType.UnitMng;
    }
    public override void OnGameEnable()
    {
      //  GameDB.MngEnabled += (int)MngType.UnitMng;
    }
    public override void OnGameDisable()
    {
      //  GameDB.MngEnabled -= (int)MngType.UnitMng;
    }

    public override void SetActive(bool state)
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
    public override void SetEnable(bool state)
    {
        if (state)
        {
            OnGameEnable();
        }
        else
        {
            OnGameDisable();
        }
        enabled = state;
    }

    #endregion


    public  void Pause()
    {
        GameDB.isSkillOn = false;
        foreach (var value in unitDic)
        {
            value.Value.Pause();
            
        }
        pause = true;
    }
    public  void Resume()
    {
        GameDB.isSkillOn = true;
        foreach (var value in unitDic)
        {
            value.Value.Resume();
        }

        pause = false;
    }

    public int getCount
    {
        get { return unitDic.Count; }
    }


    public void ChangeUnit(int spawnID , Unit newUnit )
    {
        if(unitDic.ContainsKey(spawnID))
        {
            unitDic[spawnID] = newUnit;
        }
    }


    public int AddAsync(UnitType unitType ,int tableID, int LV, Vector3 position, Quaternion rotation)
    {
        // 테이블 아이디에 맞는 모델링의 이름을 찾습니다.
        // 그 이름값에 맞도록 로딩할 예정입니다.
        // ----- 정보 세팅 ------

        // 차후 수정될 사항

        // -----------------------

        TableType loadTable = TableType.None;

        switch((int)unitType)
        {
            case 0:
                {
                    loadTable = TableType.NONETABLE;
                }
                break;
            case 6:
                {
                    loadTable = TableType.PLAYERTABLE;
                }
                break;
            case 7:
                {
                    loadTable = TableType.MONSTERTABLE;
                }
                break;
            case 8:
                {
                    loadTable = TableType.NPCTABLE;
                }
                break;
            case 9:
                {
                    loadTable = TableType.ITEMTABLE;
                }
                break;
            case 10:
                {
                    loadTable = TableType.SKILLTABLE;
                }
                break;
        }
        int spawnID = 0;
        if (unitType == UnitType.Player)
        {
            spawnID = 1;
            AddAsync(unitType, spawnID, tableID, LV, path + unitType + DataManager.ToS(loadTable, tableID, "MODEL"), position, rotation);
        }
        //else if(unitType == UnitType.None)
        //{
        //    spawnID = spawnCount;
        //    AddAsync(unitType, spawnID, tableID, LV, path + unitType + "/Portal", position, rotation);
        //    spawnCount++;
        //}
        else
        {
            spawnID = spawnCount;
            AddAsync(unitType, spawnID, tableID, LV, path + unitType + DataManager.ToS(loadTable, tableID, "MODEL"), position, rotation);
            spawnCount++;
        }
        return spawnID;
    }



    void Finished(int spawnID, int tableID, int LV, Unit unit, Vector3 position, Quaternion rotation)
    {

        
        // 비동기 로딩이 완료되었다면 캐릭터를 생성하고, 저장소에 저장합니다.
        unit = Instantiate(unit, position, rotation);
        
        unit.Init();
        unit.setInfo(spawnID, tableID, LV);
        unit.setUpdate();
     


        // 생성한 캐릭터를 등록합니다
        if (unitDic.ContainsKey(spawnID) == false)
            unitDic.Add(spawnID, unit);


    }
    public void AddAsync(UnitType unitType, int spawnID, int tableID, int LV, string fileName, Vector3 position, Quaternion rotation)
    {
        loader.LoadAsync<Unit>(spawnID, tableID, LV, fileName, position, rotation, Finished);
    }

    // 캐릭터를 멈추도록 합니다.
    public void PauseAll()
    {
        foreach (var kValue in unitDic)
        {
            kValue.Value.Pause();
        }
    }
    // 캐릭터를 다시 움직이도록 합니다.
    public void ResumeAll()
    {
        foreach (var kValue in unitDic)
        {
            kValue.Value.Resume();
        }
    }


    public void Del(int spawnID)
    {
        if (unitDic.ContainsKey(spawnID))
        {
            unitDic[spawnID].Destroy();
            unitDic.Remove(spawnID);
        }
    }

    void RemoveAll()
    {
        
        tempDic.Clear();

        foreach (var value in unitDic)
        {
            if (value.Value != null)
                tempDic.Add(value.Key, value.Value);
        }

        unitDic.Clear();

        foreach (var value in tempDic)
        {
            if (value.Value != null)
                unitDic.Add(value.Key, value.Value);
        }

    }

    public List<Unit> GetMinimapIcons()
    {
        RemoveAll();
        List<Unit> targetList = new List<Unit>();


        foreach (var value in unitDic)
        {
            if(value.Value.tag != "Player")
                targetList.Add(value.Value);
        }
        return targetList;
    }



    public void Clear()
    {
        spawnCount = 10;
        unitDic.Clear();
    }


    public bool Contains(int spawnID)
    {
        if (unitDic.ContainsKey(spawnID))
        {
            return true;
        }

        return false;
    }

    public Unit GetPlayer()
    {
        RemoveAll();

        GameDB.playerPos = null;
        foreach( var value in unitDic)
        {
            if(value.Value.unitType == UnitType.Player)
            {
                GameDB.playerPos = value.Value.getModel.getTarget.getCenter;
                cameraMove.setTarget = GameDB.playerPos;
                return value.Value;
            }
        }
        return null;
    }




    public bool targetInScreen(Vector3 tarPos)
    {
        if (Camera.main != null)
        {
            Vector3 viewPos = Camera.main.WorldToScreenPoint(tarPos);

            if (viewPos.x >= 0 && viewPos.x <= Camera.main.scaledPixelWidth && viewPos.y >= 0 && viewPos.y <= Camera.main.scaledPixelHeight)
                return true;
        }

        return false;


    }

    public List<Target> GetTargetList()
    {

        RemoveAll();
        List<Target> targetList = new List<Target>();


        foreach (var value in unitDic)
        {

            
            if (targetInScreen(value.Value.getModel.getTarget.getCenter.position) && value.Value.targetType == TargetType.On)
            {
                value.Value.getModel.getTarget.enabled = true;
                targetList.Add(value.Value.getModel.getTarget);
                value.Value.screenIn = true;
            }

            else
            {
                value.Value.getModel.getTarget.setActive(false);
                value.Value.getModel.getTarget.enabled = false;
                value.Value.screenIn = false;
            }


            //print(value.Value.tag);

        }

        
            
       
        return targetList;
    }

  


    // Update is called once per frame
    void Update()
    {

    }
}
