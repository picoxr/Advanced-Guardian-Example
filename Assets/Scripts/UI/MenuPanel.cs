using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    public Button Connect;
    void Start()
    {
        Connect.onClick.AddListener(()=>{ ServerManager.instance.Connect(); });  
    }
}
