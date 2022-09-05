using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 1. 신이 시작하면 on hp가 몇개인지 받는다.
// 2. 이상한 위치에 hp 바를 갯수만큼 만든다.
// 3. 투명화 후 캐릭터에 연결한다.
// 4. 연결된 녀석의 hp를 실시간으로 확인한다.
// 5. hp 값이 변화 데미지를 받으면 켜진드 set 데미지가 호출되면 켜진다.
public class UIHpMng : BaseUI
{
    public List<UIHp> hpList = new List<UIHp>();

    public UIHp UIHpCreate(Transform pivot, ShareInfo info)
    {
        
            UIHp hp = Resources.Load<UIHp>("Prefab/UI/UIHp");
        
            if (hp == null)
            return null;
        
        hp = Instantiate(hp, pivot.position, Quaternion.identity,transform);
        hp.Init();
        hp.HpPivot = pivot;
        hp.info = info;
        
        hpList.Add(hp);
        return hp;
    }

    #region BASEUI로부터 상속받은 함수목록
    public override void Init()
    {

    }
    public override void Run()
    {
        
        List<UIHp> temp = new List<UIHp>();

        foreach (var value in hpList)
        {
            if(value !=null)
                temp.Add(value);
        }

        hpList.Clear();
        hpList.AddRange(temp);

        foreach (var value in hpList)
        {
            value.Run();
        }
    }
    public override void Open()
    {
    }
    public override void Close()
    {
    }
    #endregion BASEUI로부터 상속받은 함수목록
}
