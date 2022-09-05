using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    // Start is called before the first frame update
    public void Init()
    {
        Destroy(gameObject, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
