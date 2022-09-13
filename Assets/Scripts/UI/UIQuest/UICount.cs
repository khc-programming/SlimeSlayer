using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UICount : MonoBehaviour
{
    TMP_Text rewardText;

    public void Init()
    {
        Transform t = transform.Find("CountText");
        if (t != null) rewardText = t.GetComponent<TMP_Text>();
    }

    public void SetText(string reward)
    {
        if (rewardText != null) rewardText.text = reward;
    }

    public void Close()
    {
        Destroy(gameObject);
    }

}

