using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ����Ƽ���� �������� ������ �÷����ϴ� ����� �ֽ��ϴ�.
// �� ����� �ε�ui�� �ִ´ٸ� ��Ʈ�������μ��� ��ġ�� �ֽ��ϴ�.
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