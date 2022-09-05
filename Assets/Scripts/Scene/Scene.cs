using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Scene : MonoBehaviour
{
    // �� ������ �ε尡 �Ϸ�Ǵ� ������ ȣ��Ǵ� �Լ��Դϴ�.
    public abstract void Enter();

    // �� ������ �ٸ� �� ���Ϸ� ������ �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ��Դϴ�.
    public abstract void Exit();


    // ������ �ε�ǰ� �������� ��Ȳ�� �����ֱ� ���� �Լ��Դϴ�.
    // �Ʒ��� �Լ����� �ε� ui�� ����Ҽ��� �ֽ��ϴ�.
    public abstract void Progress(float progress);

 
}
