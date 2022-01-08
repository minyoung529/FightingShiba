using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyManager : MonoBehaviour
{
    EventParam eventParam;
    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !GameManager.Instance.isGameOver)
        {
            //LATER:: Save Main Camera As Various
            eventParam.vectorParam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
