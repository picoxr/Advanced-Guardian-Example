using System;
using UnityEngine;

namespace Unity.XR.PXR
{
    public enum AchievementType : int
    {
        Unknown,
        Simple,
        Bitfield,
        Count,
    }
    public class PXR_AchievementAPI
    {
#if UNITY_ANDROID
        private static AndroidJavaClass achievementAPI = new AndroidJavaClass("com.pico.achievenment.AchievementAPI");
        private static AndroidJavaClass definitionArrayHandle = new AndroidJavaClass("com.picovr.achievement.utils.pvrAchievementDefinitionArrayHandle");
        private static AndroidJavaClass definitionHandle = new AndroidJavaClass("com.picovr.achievement.utils.pvrAchievementDefinitionHandle");
        private static AndroidJavaClass progressArrayHandle = new AndroidJavaClass("com.picovr.achievement.utils.pvrAchievementProgressArrayHandle");
        private static AndroidJavaClass progressHandle = new AndroidJavaClass("com.picovr.achievement.utils.pvrAchievementProgressHandle");
        private static AndroidJavaClass updateHandle = new AndroidJavaClass("com.picovr.achievement.utils.pvrAchievementUpdateHandle");
        private static AndroidJavaObject errorHandle = new AndroidJavaObject("com.picovr.achievement.utils.pvrAchievementErrorHandle");
        private static AndroidJavaObject unityInterface = new AndroidJavaObject("com.pico.loginpaysdk.UnityInterface");

#endif
        private static string openId;
        private static string accessToken;
        private static string appId = PXR_PlatformSetting.Instance.appID;

        public static long UPxr_AchievementInit()
        {
            long returnValue = 0;
#if UNITY_ANDROID
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            unityInterface.Call("init", currentActivity);
            unityInterface.Call("authSSO");

            AndroidJavaClass accessTokenKeeper = new AndroidJavaClass("com.pico.loginpaysdk.utils.PicoAccessTokenKeeper");
            AndroidJavaObject accessInfo = accessTokenKeeper.CallStatic<AndroidJavaObject>("readAccessToken", currentActivity);

            accessToken = accessInfo.Call<string>("getAccessToken");
            openId = accessInfo.Call<string>("getOpenId");
            returnValue = achievementAPI.CallStatic<long>("init", accessToken, openId, currentActivity);
#endif
            return returnValue;
        }
        public static void UPxr_RegisterNetwork()
        {
#if UNITY_ANDROID
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            achievementAPI.CallStatic("registerNetwork", currentActivity);
#endif
        }
        public static void UPxr_UnRegisterNetwork()
        {
#if UNITY_ANDROID
            achievementAPI.CallStatic("unregisterNetwork");
#endif
        }
        public static AndroidJavaObject UPxr_PopMessage()
        {
#if UNITY_ANDROID
            return achievementAPI.CallStatic<AndroidJavaObject>("pvr_PopMessage");
#else
            return null;
#endif
        }
        public static string UPxr_ErrorGetMessage(AndroidJavaObject popMessage)
        {
            string returnValue = "";
#if UNITY_ANDROID
            returnValue = errorHandle.CallStatic<string>("pvr_Error_GetMessage", popMessage);
#endif
            return returnValue;
        }
        public static int UPxr_ErrorGetHttpCode(AndroidJavaObject popMessage)
        {
            int returnValue = 0;
#if UNITY_ANDROID
            returnValue = errorHandle.CallStatic<int>("pvr_Error_GetHttpCode", popMessage);
#endif
            return returnValue;
        }
        public static int UPxr_ErrorGetCode(AndroidJavaObject popMessage)
        {
            int returnValue = 0;
#if UNITY_ANDROID
            returnValue = errorHandle.CallStatic<int>("pvr_Error_GetCode", popMessage);
#endif
            return returnValue;
        }

        public static long UPxr_AchievementsAddCount(string name, long count)
        {
            long returnValue = 0;
#if UNITY_ANDROID
            returnValue = achievementAPI.CallStatic<long>("pvr_Achievements_AddCount", name, count, accessToken);
#endif
            return returnValue;
        }
        public static long UPxr_AchievementsAddFields(string name, string fields)
        {
            long returnValue = 0;
#if UNITY_ANDROID
            returnValue = achievementAPI.CallStatic<long>("pvr_Achievements_AddFields", name, fields, accessToken);
#endif
            return returnValue;
        }
        public static long UPxr_AchievementsGetAllDefinitions()
        {
            long returnValue = 0;
#if UNITY_ANDROID
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            returnValue = achievementAPI.CallStatic<long>("pvr_Achievements_GetAllDefinitions", appId, currentActivity);
#endif
            return returnValue;
        }
        public static long UPxr_AchievementsGetAllProgress()
        {
            long returnValue = 0;
#if UNITY_ANDROID
            returnValue = achievementAPI.CallStatic<long>("pvr_Achievements_GetAllProgress", accessToken);
#endif
            return returnValue;
        }
        public static long UPxr_AchievementsGetDefinitionsByName(string[] names, int v)
        {
            long returnValue = 0;
#if UNITY_ANDROID
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            returnValue = achievementAPI.CallStatic<long>("pvr_Achievements_GetDefinitionsByName", names, currentActivity);
#endif
            return returnValue;
        }
        public static long UPxr_AchievementsGetProgressByName(string[] names, int v)
        {
            long returnValue = 0;
#if UNITY_ANDROID
            returnValue = achievementAPI.CallStatic<long>("pvr_Achievements_GetProgressByName", names, accessToken);
#endif
            return returnValue;
        }
        public static long UPxr_AchievementsUnlock(string name)
        {
            long returnValue = 0;
#if UNITY_ANDROID
            returnValue = achievementAPI.CallStatic<long>("pvr_Achievements_Unlock", name, accessToken);
#endif
            return returnValue;
        }
        public static long UPxr_GetWithMessageType(string nextUrl, PXR_Message.MessageType messageType)
        {
            long returnValue = 0;
#if UNITY_ANDROID
            switch (messageType)
            {
                case PXR_Message.MessageType.AchievementsGetNextAchievementDefinitionArrayPage:
                    AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                    AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                    returnValue = achievementAPI.CallStatic<long>("pvr_Achievements_GetAllDefinitionsByUrl", nextUrl, currentActivity);
                    break;
                case PXR_Message.MessageType.AchievementsGetNextAchievementProgressArrayPage:
                    returnValue = achievementAPI.CallStatic<long>("pvr_Achievements_GetAllProgressByUrl", nextUrl);
                    break;
                default:
                    break;
            }
#endif
            return returnValue;
        }

        public static long UPxr_MessageGetType(AndroidJavaObject popMessage)
        {
            long returnValue = 0;
#if UNITY_ANDROID
            returnValue = popMessage.Call<AndroidJavaObject>("getHandleType").Call<long>("getIndex");
#endif
            return returnValue;
        }

        public static bool UPxr_MessageIsError(AndroidJavaObject popMessage)
        {
            return popMessage.Call<bool>("isMessage_IsError");
        }

        public static long UPxr_MessageGetRequestID(AndroidJavaObject popMessage)
        {
            long returnValue = 0;
#if UNITY_ANDROID
            returnValue = popMessage.Call<long>("getId");
#endif
            return returnValue;
        }

        public static string UPxr_MessageGetString(AndroidJavaObject popMessage)
        {
            string returnValue = "";
#if UNITY_ANDROID
            returnValue = popMessage.Call<string>("getContent");
#endif
            return returnValue;
        }

        public static bool UPxr_AchievementUpdateGetJustUnlocked(AndroidJavaObject popMessage)
        {
            bool returnValue = true;
#if UNITY_ANDROID
            returnValue = updateHandle.CallStatic<bool>("pvr_AchievementUpdate_GetJustUnlocked", popMessage);
#endif
            return returnValue;
        }

        public static string UPxr_AchievementUpdateGetName(AndroidJavaObject popMessage)
        {
            string returnValue = "";
#if UNITY_ANDROID
            returnValue = updateHandle.CallStatic<string>("pvr_AchievementUpdate_GetName", popMessage);
#endif
            return returnValue;
        }

        public static int UPxr_AchievementProgressArrayGetSize(AndroidJavaObject msg)
        {
            int returnValue = 0;
#if UNITY_ANDROID
            returnValue = progressArrayHandle.CallStatic<int>("pvr_AchievementProgressArray_GetSize", msg);
#endif
            return returnValue;
        }

        public static AndroidJavaObject UPxr_AchievementProgressArrayGetElement(AndroidJavaObject msg, int index)
        {
#if UNITY_ANDROID
            return progressArrayHandle.CallStatic<AndroidJavaObject>("pvr_AchievementProgressArray_GetElement", msg, index);
#else
            return null;
#endif
        }

        public static string UPxr_AchievementProgressArrayGetNextUrl(AndroidJavaObject msg)
        {
            string returnValue = "";
#if UNITY_ANDROID
            returnValue = progressArrayHandle.CallStatic<string>("pvr_AchievementProgressArray_GetNextUrl", msg);
#endif
            return returnValue;
        }

        public static string UPxr_AchievementProgressGetBitfield(AndroidJavaObject msg)
        {
            string returnValue = "";
#if UNITY_ANDROID
            returnValue = progressHandle.CallStatic<string>("pvr_AchievementProgress_GetBitfield", msg);
#endif
            return returnValue;
        }

        public static long UPxr_AchievementProgressGetCount(AndroidJavaObject msg)
        {
            long returnValue = 0;
#if UNITY_ANDROID
            returnValue = progressHandle.CallStatic<long>("pvr_AchievementProgress_GetCount", msg);
#endif
            return returnValue;
        }

        public static bool UPxr_AchievementProgressGetIsUnlocked(AndroidJavaObject msg)
        {
            bool returnValue = true;
#if UNITY_ANDROID
            returnValue = progressHandle.CallStatic<bool>("pvr_AchievementProgress_GetIsUnlocked", msg);
#endif
            return returnValue;
        }

        public static string UPxr_AchievementProgressGetName(AndroidJavaObject msg)
        {
            string returnValue = "";
#if UNITY_ANDROID
            returnValue = progressHandle.CallStatic<string>("pvr_AchievementProgress_GetName", msg);
#endif
            return returnValue;
        }

        public static DateTime UPxr_AchievementProgressGetUnlockTime(AndroidJavaObject msg)
        {
            DateTime returnValue = new DateTime(1970, 1, 1, 0, 0, 0, 0);
#if UNITY_ANDROID
            returnValue = UPxr_DateTimeFromNative(progressHandle.CallStatic<long>("pvr_AchievementProgress_GetUnlockTime", msg));
#endif
            return returnValue;
        }
        public static DateTime UPxr_DateTimeFromNative(long dateTime)
        {
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return dt.AddSeconds(dateTime).ToLocalTime();
        }

        public static int UPxr_AchievementDefinitionArrayGetSize(AndroidJavaObject msg)
        {
            int returnValue = 0;
#if UNITY_ANDROID
            returnValue = definitionArrayHandle.CallStatic<int>("pvr_AchievementDefinitionArray_GetSize", msg);
#endif
            return returnValue;
        }

        public static AndroidJavaObject UPxr_AchievementDefinitionArrayGetElement(AndroidJavaObject msg, int index)
        {
#if UNITY_ANDROID
            return definitionArrayHandle.CallStatic<AndroidJavaObject>("pvr_AchievementDefinitionArray_GetElement", msg, index);
#else
            return null;
#endif
        }

        public static string UPxr_AchievementDefinitionArrayGetNextUrl(AndroidJavaObject msg)
        {
            string returnValue = "";
#if UNITY_ANDROID
            returnValue = definitionArrayHandle.CallStatic<string>("pvr_AchievementDefinitionArray_GetNextUrl", msg);
#endif
            return returnValue;
        }

        public static AchievementType UPxr_AchievementDefinitionGetType(AndroidJavaObject msg)
        {
            AchievementType returnValue = AchievementType.Bitfield;
#if UNITY_ANDROID
            AndroidJavaObject ajo = definitionHandle.CallStatic<AndroidJavaObject>("pvr_AchievementDefinition_GetType", msg);
            returnValue = (AchievementType)ajo.Call<int>("getIndex");
#endif
            return returnValue;
        }

        public static string UPxr_AchievementDefinitionGetName(AndroidJavaObject msg)
        {
            string returnValue = "";
#if UNITY_ANDROID
            returnValue = definitionHandle.CallStatic<string>("pvr_AchievementDefinition_GetName", msg);
#endif
            return returnValue;
        }

        public static int UPxr_AchievementDefinitionGetBitfieldLength(AndroidJavaObject msg)
        {
            int returnValue = 0;
#if UNITY_ANDROID
            returnValue = definitionHandle.CallStatic<int>("pvr_AchievementDefinition_GetBitfieldLength", msg);
#endif
            return returnValue;
        }

        public static long UPxr_AchievementDefinitionGetTarget(AndroidJavaObject msg)
        {
            long returnValue = 0;
#if UNITY_ANDROID
            returnValue = definitionHandle.CallStatic<long>("pvr_AchievementDefinition_GetTarget", msg);
#endif
            return returnValue;
        }

        public static string UPxr_AchievementDefinitionGetTitle(AndroidJavaObject msg)
        {
            string returnValue = "";
#if UNITY_ANDROID
            returnValue = definitionHandle.CallStatic<string>("pvr_AchievementDefinition_GetTitle", msg);
#endif
            return returnValue;
        }

        public static string UPxr_AchievementDefinitionGetUnlockedDescription(AndroidJavaObject msg)
        {
            string returnValue = "";
#if UNITY_ANDROID
            returnValue = definitionHandle.CallStatic<string>("pvr_AchievementDefinition_GetUnlocked_description", msg);
#endif
            return returnValue;
        }

        public static string UPxr_AchievementDefinitionGetLockedIcon(AndroidJavaObject msg)
        {
            string returnValue = "";
#if UNITY_ANDROID
            returnValue = definitionHandle.CallStatic<string>("pvr_AchievementDefinition_GetLocked_image", msg);
#endif
            return returnValue;
        }

        public static bool UPxr_AchievementDefinitionGetIsSecrect(AndroidJavaObject msg)
        {
            bool returnValue = false;
#if UNITY_ANDROID
            returnValue = definitionHandle.CallStatic<bool>("pvr_AchievementDefinition_GetIs_secret", msg);
#endif
            return returnValue;
        }

        public static string UPxr_AchievementDefinitionGetUnlockedIcon(AndroidJavaObject msg)
        {
            string returnValue = "";
#if UNITY_ANDROID
            returnValue = definitionHandle.CallStatic<string>("pvr_AchievementDefinition_GetUnlocked_image", msg);
#endif
            return returnValue;
        }

        public static string UPxr_AchievementDefinitionGetDescription(AndroidJavaObject msg)
        {
            string returnValue = "";
#if UNITY_ANDROID
            returnValue = definitionHandle.CallStatic<string>("pvr_AchievementDefinition_GetDescription", msg);
#endif
            return returnValue;
        }
    }
}