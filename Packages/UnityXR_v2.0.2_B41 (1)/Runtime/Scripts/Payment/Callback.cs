// Copyright © 2015-2021 Pico Technology Co., Ltd. All Rights Reserved.

#if !UNITY_EDITOR
#if UNITY_ANDROID
#define ANDROID_DEVICE
#elif UNITY_IPHONE
#define IOS_DEVICE
#elif UNITY_STANDALONE_WIN
#define WIN_DEVICE
#endif
#endif

using LitJson;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.XR.PXR
{
    public class Callback : MonoBehaviour
    {

        private static string IS_SUCCESS = "isSuccess";
        private static string MSG = "msg";
        private static string CODE = "code";

        public void LoginCallback(string LoginInfo)
        {
            JsonData jsrr = JsonMapper.ToObject(LoginInfo);
            SetMassage(LoginInfo);
            DemoController.showLoading();

            if (jsrr[IS_SUCCESS] != null)
            {
                CommonDic.getInstance().isSuccess = jsrr[IS_SUCCESS].ToString();
            }
            if (jsrr[MSG] != null)
            {
                CommonDic.getInstance().loginMsg = jsrr[MSG].ToString();
            }

            Debug.Log("PXRLog LoginCallback LoginInfo:" + LoginInfo);
        }

        public void QueryOrPayCallback(string queryOrPayInfo)
        {
            JsonData jsrr = JsonMapper.ToObject(queryOrPayInfo);
            if (jsrr[CODE] != null)
            {
                CommonDic.getInstance().code = jsrr["code"].ToString();
            }
            if (jsrr[MSG] != null)
            {
                CommonDic.getInstance().msg = jsrr["msg"].ToString();
            }
            if (jsrr != null)
            {
                CommonDic.getInstance().order_info = jsrr[1].ToString();
            }

            SetMassage(queryOrPayInfo);
            DemoController.showLoading();
            Debug.Log("PXRLog QueryOrPayCallback queryOrPayInfo:" + queryOrPayInfo);
        }

        public void UserInfoCallback(string userInfo)
        {

            CommonDic.getInstance().user_info = userInfo;

            SetMassage(userInfo);
            DemoController.showLoading();
            Debug.Log("PXRLog UserInfoCallback userInfo:" + userInfo);
        }

        public void SetMassage(string massage)
        {
            if (!GetCurrentGameObject().Equals(null))
            {
                GetCurrentGameObject().GetComponent<Text>().text = massage;
            }
            else
            {
                Debug.LogError("PXRLog There is no gameObject to receive this message");
            }
        }

        public GameObject GetCurrentGameObject()
        {
            return GameObject.Find("MassageInfo");
        }

        public void ActivityForResultCallback(string activity)
        {
            PicoPaymentSDK.jo.Call("authCallback", activity);
        }
    }
}
