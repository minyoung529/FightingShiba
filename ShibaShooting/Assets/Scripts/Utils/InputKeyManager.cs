using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyManager : MonoBehaviour
{
    EventParam eventParam;

    private void Update()
    {
        if (Input.GetMouseButton(0) && !GameManager.Instance.isGameOver)
        {
            eventParam.vectorParam = GameManager.Instance.mainCam.cam.ScreenToWorldPoint(Input.mousePosition);
            InputEventManager.TriggerEvent("MOVE", eventParam);
            ResetParam();
        }
    }

    private void ResetParam()
    {
        eventParam.strParam = "";
        eventParam.vectorParam = Vector3.zero;
        eventParam.intParam = 0;
        eventParam.floatParam = 0f;
        eventParam.boolParam = false;
    }
}
