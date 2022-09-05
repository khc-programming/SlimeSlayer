using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class SceneMng : Mng<SceneMng>
{
    // �ε�ǰ� �ִ� ���¸� ����ų ���º���
    [SerializeField]
    private bool loading = false;

    // ���� ����ϱ� ���� ����
    private Dictionary<SceneType, Scene> sceneDic =
        new Dictionary<SceneType, Scene>();

    // ���� ��
    private SceneType current = SceneType.None;

    public SceneType Current
        { get { return current; } }
    // ��ü�� ��ȸ�ϸ鼭 �Ű������� ���� ���� üũ�ڽ���
    // Ȱ��ȭ�ϴ� �Լ��Դϴ�.


    #region // Mng �߻� �޼ҵ� ���Ǻ�


    public override void Run()
    {
        if ((GameDB.MngEnabled & (int)MngType.SceneMng) != (int)MngType.SceneMng)
            return;
    }

    public override void FixRun()
    {

    }

    public override void LateRun()
    {

    }
    public override void Init()
    {
        mngType = MngType.SceneMng;
        
    }

    public override void OnActive()
    {
       // GameDB.MngEnabled += (int)MngType.SceneMng;
    }
    public override void OnDeactive()
    {
       //GameDB.MngEnabled -= (int)MngType.SceneMng;
    }
    public override void OnGameEnable()
    {
       // GameDB.MngEnabled += (int)MngType.SceneMng;
    }
    public override void OnGameDisable()
    {
        //GameDB.MngEnabled -= (int)MngType.SceneMng;
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



    public void Enable(SceneType scene)
    {
        foreach (var pair in sceneDic)
        {
            if (pair.Key != scene)
            {
                pair.Value.enabled = false;
            }
            else
                pair.Value.enabled = true;
        }
    }
    public T AddScene<T>(SceneType sType, bool state = false)
        where T : Scene
    {
        // ���� ��ϵǾ� ���� �ʴٸ� ���� ������Ʈ�� �����,
        // �� ��ũ��Ʈ�� ������ �� �Ŵ����� ����մϴ�.
        if (sceneDic.ContainsKey(sType) == false)
        {
           
            T t = this.CreateObject<T>(transform, true);
            
            t.enabled = state;
            sceneDic.Add(sType, t);
            return t;
        }
        sceneDic[sType].enabled = state;
        // ���� ��ϵǾ� �ִ� ���¶�� ã�Ƽ� �����մϴ�.
        return sceneDic[sType] as T;
    }

    public void Enable(SceneType scene,
                        int idx = 0,
                        bool falseLoading = false,
                        float targetTime = 2.0f)
    {
        // �̵��ϰ����ϴ� ���� ��ϵ� ���¶�� ó���մϴ�.
        if (sceneDic.ContainsKey(scene))
        {
            // �ش�Ǵ� ���� ��ũ��Ʈ�� ���ݴϴ�.
            Enable(scene);

            // ���� �񵿱�� �ε��մϴ�.
            LoadAsync(scene, idx);
        }
    }
    private IEnumerator IEEnableDelay(float delayTime,
                                         SceneType scene,
                                         int idx = 0,
                                        bool falseLoading = false,
                                        float targetTime = 2.0f)
    {
        
        yield return new WaitForSeconds(delayTime);
        Enable(scene,idx ,falseLoading, targetTime);
    }

    public void EnableDelay(float delayTime,
                            SceneType scene,
                            int idx = 0,
                            bool falseLoading = false,
                            float targetTime = 2.0f)
    {
        
        StartCoroutine(IEEnableDelay(delayTime,
                                    scene, idx,falseLoading, targetTime));
    }

    public void LoadAsync(SceneType sceneType , int idx = 0)
    {
        if (loading)
            return;
        loading = true;
        StartCoroutine(IELoadAsync(sceneType, idx));
    }

    private IEnumerator IELoadAsync(SceneType nextScene, int idx = 0)
    {
        // �񵿱�� ���� �ε��մϴ�.
        // ���� �ε��ϴ� �Լ��� ����ϰ� �Ǹ�, 
        // �ſ� ��ġ�Ǿ��ִ� ��ü���� �����˴ϴ�.
        AsyncOperation operation =
                SceneManager.LoadSceneAsync(nextScene.ToString() + idx);

        // �̹��� ������ 300�������Ǵ� �������� - 3������
        //FadeUI ui = Resources.Load<FadeUI>("UI/FadeUI");

        bool state = false;

        while (state == false)
        {
            if (sceneDic.ContainsKey(nextScene))
                sceneDic[nextScene].Progress(operation.progress);

            // ���� ��� �ε�� ���¶�� ó���մϴ�.
            if (operation.isDone)
            {
                state = true;
                // ���� ���� Exit�Լ��� ȣ���մϴ�.
                if (sceneDic.ContainsKey(current))
                    sceneDic[current].Exit();

                // ����Ǵ� ���� Enter�Լ��� ȣ���մϴ�.
                if (sceneDic.ContainsKey(nextScene))
                    sceneDic[nextScene].Enter();
                // ���� ���� �����մϴ�.
                current = nextScene;

                loading = false;
            }

            yield return null;
        }
    }

    // �ȵ���̵��� ��Ŀ���� ������ ��� ȣ��Ǵ� �Լ��Դϴ�.
    private void OnApplicationFocus(bool focus)
    {

    }
    // �ȵ���̵��� ȨŰ�� �������� ȣ��Ǵ� �Լ��Դϴ�.
    private void OnApplicationPause(bool pause)
    {

    }
    // ���α׷��� ����ɶ� Release�Լ��� ȣ���մϴ�.
    private void OnApplicationQuit()
    {
        Release();
    }
}
