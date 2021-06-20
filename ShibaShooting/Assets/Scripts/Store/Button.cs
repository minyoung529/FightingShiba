using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject IsSelected { get; private set; }

    public string buttonName { get; private set; }

    private void OnMouseDown()
    {
        IsSelected = this.gameObject;
        buttonName = gameObject.name;
    }
}
