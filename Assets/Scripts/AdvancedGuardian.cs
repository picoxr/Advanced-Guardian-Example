using System.Collections;
using UnityEngine;
using Unity.XR.PXR;
using System.IO;

public class AdvancedGuardian : MonoBehaviour
{
    [Header("UI Panels")]
    public LoadingPanel loadingPanel;
    public RebootPanel rebootPanel;
    [Header("Boundary Related")]
    public float boundaryDuration = 3f;

    private const string mapPath= "/storage/emulated/0/maps/";

    void Start()
    {
        // Set the boundary visibility on start up
        StartCoroutine("SetBoundaryVisible", boundaryDuration);
    }

    public void CreateMap()
    {
        if (loadingPanel != null)
        {
            loadingPanel.Open();
        }

        PXR_Plugin.System.UPxr_SwitchLargeSpaceScene(true, (callbackValue)=> {
            if (callbackValue)
            {
                DebugHelper.Instance.Log("Create boundary success !");
                SaveMap();
            }
            else
            {
                DebugHelper.Instance.Log("Create boundary failed !");
            }

            if (loadingPanel != null)
            {
                loadingPanel.Close();
            }
        });
    }

    private void SaveMap()
    {
        if(PXR_Plugin.System.UPxr_SaveLargeSpaceMaps())
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
            DirectoryInfo dirInfo = new DirectoryInfo(mapPath);
            FileInfo[] fileInfo = dirInfo.GetFiles("*.finchz");
            if (fileInfo != null && fileInfo.Length > 0 && loadingPanel != null && rebootPanel != null)
            {
                DebugHelper.Instance.Log("Detect Map in [internal storage /maps/]");
                loadingPanel.Open();
                PXR_Plugin.System.UPxr_ImportMaps((callbackValue) => {
                    DebugHelper.Instance.Log("Import Map Success! Please reboot device");
                    loadingPanel.Close();
                    rebootPanel.Open();
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
        loadingPanel.Open();
        PXR_Plugin.System.UPxr_ExportMaps((callbackValue) => {
            if (callbackValue)
            {
                DebugHelper.Instance.Log("Map is stored in path[ internal storage /maps/export ] ");
            }
            else
            {
                DebugHelper.Instance.Log("Map stored fail ");
            }
            loadingPanel.Close();
        });
    }

    public void Exit()
    {
        Application.Quit();
    }

    private IEnumerator SetBoundaryVisible(float duration)
    {
        PXR_Boundary.SetVisible(true);
        yield return new WaitForSeconds(duration);
        PXR_Boundary.SetVisible(false);
    }
}
