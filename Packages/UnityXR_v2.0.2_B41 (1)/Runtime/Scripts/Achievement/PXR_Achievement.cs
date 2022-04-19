using UnityEngine;

namespace Unity.XR.PXR
{
    public sealed class PXR_AchievementCore
    {
        private static bool IsPlatformInitialized = true;
        public static bool IsInitialized()
        {
            return IsPlatformInitialized;
        }

        public static void RegisterNetwork()
        {
            PXR_AchievementAPI.UPxr_RegisterNetwork();
        }

        public static void UnRegisterNetwork()
        {
            PXR_AchievementAPI.UPxr_UnRegisterNetwork();
        }
        
        public static bool LogMessages = false;

    }

    public static partial class PXR_Achievement
    {
        /// <summary>
        /// Achievement system initialization.
        /// </summary>
        /// <returns></returns>
        public static PXR_Request<PXR_AchievementUpdate> Init()
        {

            if (PXR_AchievementCore.IsInitialized())
            {
                return new PXR_Request<PXR_AchievementUpdate>(PXR_AchievementAPI.UPxr_AchievementInit());
            }
            return null;
        }

        /// <summary>
        ///  Add count achievements.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static PXR_Request<PXR_AchievementUpdate> AddCount(string name, long count)
        {
            if (PXR_AchievementCore.IsInitialized())
            {
                return new PXR_Request<PXR_AchievementUpdate>(PXR_AchievementAPI.UPxr_AchievementsAddCount(name, count));
            }

            return null;
        }

        /// <summary>
        /// Add bitfield achievements.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static PXR_Request<PXR_AchievementUpdate> AddFields(string name, string fields)
        {
            if (PXR_AchievementCore.IsInitialized())
            {
                return new PXR_Request<PXR_AchievementUpdate>(PXR_AchievementAPI.UPxr_AchievementsAddFields(name, fields));
            }

            return null;
        }

        /// <summary>
        /// Get all definitions of achievements.
        /// </summary>
        /// <returns></returns>
        public static PXR_Request<PXR_AchievementDefinitionList> GetAllDefinitions()
        {
            if (PXR_AchievementCore.IsInitialized())
            {
                return new PXR_Request<PXR_AchievementDefinitionList>(PXR_AchievementAPI.UPxr_AchievementsGetAllDefinitions());
            }

            return null;
        }

        /// <summary>
        /// Get progress of all modified achievements.
        /// </summary>
        /// <returns></returns>
        public static PXR_Request<PXR_AchievementProgressList> GetAllProgress()
        {
            if (PXR_AchievementCore.IsInitialized())
            {
                return new PXR_Request<PXR_AchievementProgressList>(PXR_AchievementAPI.UPxr_AchievementsGetAllProgress());
            }

            return null;
        }

        /// <summary>
        /// Get definitions by name.
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static PXR_Request<PXR_AchievementDefinitionList> GetDefinitionsByName(string[] names)
        {
            if (PXR_AchievementCore.IsInitialized())
            {
                return new PXR_Request<PXR_AchievementDefinitionList>(PXR_AchievementAPI.UPxr_AchievementsGetDefinitionsByName(names, (names != null ? names.Length : 0)));
            }

            return null;
        }

        /// <summary>
        /// Get achievement progress by name.
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static PXR_Request<PXR_AchievementProgressList> GetProgressByName(string[] names)
        {
            if (PXR_AchievementCore.IsInitialized())
            {
                return new PXR_Request<PXR_AchievementProgressList>(PXR_AchievementAPI.UPxr_AchievementsGetProgressByName(names, (names != null ? names.Length : 0)));
            }

            return null;
        }

        /// <summary>
        /// Unlock achievement according to apiname.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static PXR_Request<PXR_AchievementUpdate> Unlock(string name)
        {
            if (PXR_AchievementCore.IsInitialized())
            {
                return new PXR_Request<PXR_AchievementUpdate>(PXR_AchievementAPI.UPxr_AchievementsUnlock(name));
            }

            return null;
        }

        /// <summary>
        /// When getting all the achievement definitions, if there are multiple pages, get the next page.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static PXR_Request<PXR_AchievementDefinitionList> GetNextAchievementDefinitionListPage(PXR_AchievementDefinitionList list)
        {
            if (!list.HasNextPage)
            {
                Debug.LogWarning("PXRLog Platform.GetNextAchievementDefinitionListPage: List has no next page");
                return null;
            }

            if (PXR_AchievementCore.IsInitialized())
            {
                return new PXR_Request<PXR_AchievementDefinitionList>(
                  PXR_AchievementAPI.UPxr_GetWithMessageType(
                    list.NextUrl,
                    PXR_Message.MessageType.AchievementsGetNextAchievementDefinitionArrayPage
                  )
                );
            }

            return null;
        }

        /// <summary>
        /// When getting all modified achievement progress information, if there are multiple pages, get the next page.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static PXR_Request<PXR_AchievementProgressList> GetNextAchievementProgressListPage(PXR_AchievementProgressList list)
        {
            if (!list.HasNextPage)
            {
                Debug.LogWarning("PXRLog Platform.GetNextAchievementProgressListPage: List has no next page");
                return null;
            }

            if (PXR_AchievementCore.IsInitialized())
            {
                return new PXR_Request<PXR_AchievementProgressList>(
                  PXR_AchievementAPI.UPxr_GetWithMessageType(
                    list.NextUrl,
                    PXR_Message.MessageType.AchievementsGetNextAchievementProgressArrayPage
                  )
                );
            }

            return null;
        }

    }
}
