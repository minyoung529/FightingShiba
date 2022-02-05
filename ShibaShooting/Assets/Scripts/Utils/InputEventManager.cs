using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventManager : MonoBehaviour
{
    private static Dictionary<string, Action<EventParam>> eventDictionary = new Dictionary<string, Action<EventParam>>();

    public static void StartListening(string eventName, Action<EventParam> listener)
    {
        Action<EventParam> thisEvent;

        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            eventDictionary[eventName] = thisEvent;
        }
        else
        {
            eventDictionary.Add(eventName, listener);
        }
    }

    public static void StopListening(string eventName, Action<EventParam> listener)
    {
        Action<EventParam> thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            eventDictionary[eventName] = thisEvent;
        }
        else
        {
            eventDictionary.Remove(eventName);
        }
    }

    public static void TriggerEvent(string eventName, EventParam param)
    {
        Action<EventParam> thisEvent;

        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent?.Invoke(param);
        }
    }
}

public struct EventParam
{
    public string strParam;
    public int intParam;
    public float floatParam;
    public bool boolParam;
    public Vector3 vectorParam;
}