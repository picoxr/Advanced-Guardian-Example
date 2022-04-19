using System.Collections.Generic;
using UnityEngine;

namespace Unity.XR.PXR
{
    public class PXR_AchievementDefinition
    {
        public readonly AchievementType type;
        public readonly string name;
        public readonly int bitfieldLength;
        public readonly long target;
        public readonly string title;
        public readonly string description;
        public readonly string unlockedDescription;
        public readonly string unlockedIcon;
        public readonly string lockedIcon;
        public readonly bool isSecrect;

        public PXR_AchievementDefinition(AndroidJavaObject msg)
        {
            type = PXR_AchievementAPI.UPxr_AchievementDefinitionGetType(msg);
            name = PXR_AchievementAPI.UPxr_AchievementDefinitionGetName(msg);
            bitfieldLength = PXR_AchievementAPI.UPxr_AchievementDefinitionGetBitfieldLength(msg);
            target = PXR_AchievementAPI.UPxr_AchievementDefinitionGetTarget(msg);
            title = PXR_AchievementAPI.UPxr_AchievementDefinitionGetTitle(msg);
            description = PXR_AchievementAPI.UPxr_AchievementDefinitionGetDescription(msg);
            unlockedDescription = PXR_AchievementAPI.UPxr_AchievementDefinitionGetUnlockedDescription(msg);
            unlockedIcon = PXR_AchievementAPI.UPxr_AchievementDefinitionGetUnlockedIcon(msg);
            lockedIcon = PXR_AchievementAPI.UPxr_AchievementDefinitionGetLockedIcon(msg);
            isSecrect = PXR_AchievementAPI.UPxr_AchievementDefinitionGetIsSecrect(msg);
        }
    }

    public class PXR_AchievementDefinitionList : PXR_DeserializableList<PXR_AchievementDefinition>
    {
        public PXR_AchievementDefinitionList(AndroidJavaObject msg)
        {
            var count = PXR_AchievementAPI.UPxr_AchievementDefinitionArrayGetSize(msg);
            data = new List<PXR_AchievementDefinition>(count);
            for (int i = 0; i < count; i++)
            {
                data.Add(new PXR_AchievementDefinition(PXR_AchievementAPI.UPxr_AchievementDefinitionArrayGetElement(msg, i)));
            }

            nextUrl = PXR_AchievementAPI.UPxr_AchievementDefinitionArrayGetNextUrl(msg);
        }

    }
}
