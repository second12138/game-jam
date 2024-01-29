using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pickup : MonoBehaviour
{
private Bag bag;
public int dropChance;
public GameObject popupCanvas;
public float displayTime = 1f;

public GameObject itemButton;
// Start is called before the first frame update
void Start()
{
bag = GameObject.FindGameObjectWithTag("Player").GetComponent<Bag>();
}

// Update is called once per frame

void OnTriggerEnter2D(Collider2D other)
{
if (other.CompareTag("Player"))
{
// 显示弹出画布
// 显示UI
GameObject uiInstance = Instantiate(popupCanvas, transform.position, Quaternion.identity);

// 延迟一段时间后关闭UI和销毁GameObject
Invoke("HideUIAndDisappear", displayTime);
}
}
private void HideUIAndDisappear()
{
// 关闭UI
Destroy(GameObject.FindWithTag("UI"));

// 销毁GameObject
Destroy(gameObject);
}
}
