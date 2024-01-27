using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInputHandler : MonoBehaviour
{
    public GameObject[] uiSlots; // 存储UI Slot对象的数组，这些对象将通过Inspector手动赋值

    void Update()
    {
        // 检测按键1到6是否被按下
        for (int i = 0; i < uiSlots.Length; i++)
        {
            int keyIndex = i;
            KeyCode keyCode = (KeyCode)(49 + i); // 49对应键盘上的数字1的KeyCode，依次类推

            if (Input.GetKeyDown(keyCode))
            {
                // 获取对应UI Slot上的Button组件
                Button button = uiSlots[keyIndex].GetComponentInChildren<Button>();

                // 视为点击Button
                if (button != null)
                {
                    button.onClick.Invoke();
                }
            }
        }
    }
}
