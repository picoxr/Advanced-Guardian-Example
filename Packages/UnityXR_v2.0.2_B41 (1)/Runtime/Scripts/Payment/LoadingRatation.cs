// Copyright © 2015-2021 Pico Technology Co., Ltd. All Rights Reserved.

using UnityEngine;

namespace Unity.XR.PXR
{
    public class LoadingRatation : MonoBehaviour
    {
        void Update()
        {
            gameObject.transform.Rotate(new Vector3(0, 0, -4));
        }
    }
}

