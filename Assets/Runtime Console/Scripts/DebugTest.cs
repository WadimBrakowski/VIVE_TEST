using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuntimeConsole;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class DebugTest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugTextField;
    [SerializeField] private InputActionProperty actionProperty;
    // Start is called before the first frame update
    void Start()
    {
        DebugRT.TextMeshProUGUI = debugTextField;
        
    }

    // Update is called once per frame
    void Update()
    {
        DebugRT.Log(actionProperty.action.IsPressed().ToString());
        Debug.Log(actionProperty.action.IsPressed());
        
    }

}
