using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.PXR;
using UnityEngine.UI;
using UnityEngine.XR;
using System.IO;

public class AdvancedGuardian : MonoBehaviour
{
    [ Header("UI Panels")]
    public LoadingPanel LoadingPanel;
    public RebootPanel RebootPanel;
    [Header("Boundary Related")]
    public float BoundaryDuration=3f;

    private const string mapPath= "/storage/emulated/0/maps/";

    void Start()
    {
        //Set the boundary visibility on start up
        StartCoroutine("setBoundaryVisible", BoundaryDuration);
    }

    public void CreateMap()
    {
        LoadingPanel.Open();
        PXR_Plugin.System.UPxr_SwitchLargeSpaceScene(true, (callbackValue)=> {
            if (callbackValue.Equals(true))
            {
                DebugHelper.Instance.Log("Create boundary success !");
                SaveMap();
            }
            else
            {
                DebugHelper.Instance.Log("Create boundary failed !");
            }
            LoadingPanel.Close();
        });
    }
    private void SaveMap()
    {
       if(  PXR_Plugin.System.UPxr_SaveLargeSpaceMaps())
        {
            DebugHelper.Instance.Log("Save map success !");
        }
        else
        {
            DebugHelper.Instance.Log("Save map fail !");
        }
    }

    /// <summary>
    /// Import the map, doing this will reboot the device.
    /// </summary>
    public void ImportMap()
    {
        if(Directory.Exists(mapPath))
        {
            DirectoryInfo _dirInfo = new DirectoryInfo(mapPath);
            FileInfo[] _fileInfo = _dirInfo.GetFiles("*.finchz");

            if (_fileInfo.Length > 0)
            {
                DebugHelper.Instance.Log("Detect Map in [internal storage /maps/]");
                LoadingPanel.Open();
                PXR_Plugin.System.UPxr_ImportMaps((callbackValue) => {
                    DebugHelper.Instance.Log("Import Map Success! Please reboot device");
                    LoadingPanel.Close();
                    RebootPanel.Open();
                });
            }
            else
            {
                DebugHelper.Instance.Log("No Maps in [internal storage /maps/] ");
            }
        }
        else
        {
            DebugHelper.Instance.Log(" [internal storage /maps/] Not exist ! Please use export map to create it");
            return;
        }
       
    }

    public void ExportMap()
    {
        LoadingPanel.Open();
        PXR_Plugin.System.UPxr_ExportMaps((callbackValue) => {
            if (callbackValue)
            {
                DebugHelper.Instance.Log("Map is stored in path[ internal storage /maps/export ] ");
            }
            else
            {
                DebugHelper.Instance.Log("Map stored fail ");
            }
            LoadingPanel.Close();
        });
    }

    public void Exit()
    {
        PXR_Plugin.System.UPxr_SetLargeSpaceStatus(false);
        Application.Quit();
    }

    private IEnumerator setBoundaryVisible(float Duration)
    {
        PXR_Boundary.SetVisible(true);
        yield return new WaitForSeconds(Duration);
        PXR_Boundary.SetVisible(false);
    }

}
