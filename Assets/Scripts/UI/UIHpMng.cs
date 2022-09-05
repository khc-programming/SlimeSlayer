using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 1. ���� �����ϸ� on hp�� ����� �޴´�.
// 2. �̻��� ��ġ�� hp �ٸ� ������ŭ �����.
// 3. ����ȭ �� ĳ���Ϳ� �����Ѵ�.
// 4. ����� �༮�� hp�� �ǽð����� Ȯ���Ѵ�.
// 5. hp ���� ��ȭ �������� ������ ������ set �������� ȣ��Ǹ� ������.
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

    #region BASEUI�κ��� ��ӹ��� �Լ����
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
    #endregion BASEUI�κ��� ��ӹ��� �Լ����
}
