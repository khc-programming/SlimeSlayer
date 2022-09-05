using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 유니티에도 동영상을 간단히 플레이하는 기능이 있습니다.
// 이 기능을 로딩ui에 넣는다면 포트폴리오로서의 가치가 있습니다.
public class LoadingUI : BaseUI
{
    private Slider slider;

    public void SetValue( float delta )
    {
        slider.value = delta;
    }
    public override void Init()
    {
        slider = GetComponentInChildren<Slider>();
    }
    public override void Run()
    {

    }

    public override void Open()
    {

    }

    public override void Close()
    {

    }
}
