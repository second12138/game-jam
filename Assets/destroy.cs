using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    // Start is called before the first frame update
    public float delay = 0.6f;

    // Use this for initialization
    void Start()
    {
        // 开始协程
        StartCoroutine(Disappear());
    }

    // 协程来处理延迟和消失
    IEnumerator Disappear()
    {
        // 等待指定的延迟时间
        yield return new WaitForSeconds(delay);

        // 使GameObject不可见
        gameObject.SetActive(false);
    }
}
