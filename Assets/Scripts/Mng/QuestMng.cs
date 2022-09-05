using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMng : Mng<TargetMng>
{
    #region // 추상 정의부
    public override void Run()
    {

    }

    // 픽스드 업데이트에서 구현할 기능
    public override void FixRun()
    {

    }

    public override void LateRun()
    {

    }

    public override void Init()
    {
        mngType = MngType.QuestMng;

        
    }

    public override void OnActive()
    {

    }
    public override void OnDeactive()
    {

    }
    public override void OnGameEnable()
    {

    }
    public override void OnGameDisable()
    {

    }
    public override void SetActive(bool state)
    {

    }
    public override void SetEnable(bool state)
    {

    }

    #endregion
}
