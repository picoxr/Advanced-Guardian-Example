using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.XR.PXR;

public class RebootPanel : MonoBehaviour
{
    public void RebootDevice()
    {
        PXR_System.ControlSetDeviceAction(DeviceControlEnum.DEVICE_CONTROL_REBOOT, (value) =>
        {
            if (!value.Equals("0"))
            {
                Debug.LogError("Device Can't Reboot!");
            }
        });
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
