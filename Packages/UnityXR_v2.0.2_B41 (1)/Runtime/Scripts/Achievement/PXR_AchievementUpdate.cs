using UnityEngine;

namespace Unity.XR.PXR
{
  public class PXR_AchievementUpdate
  {
    public readonly bool justUnlocked;
    public readonly string name;

    public PXR_AchievementUpdate(AndroidJavaObject msg)
    {
      justUnlocked = PXR_AchievementAPI.UPxr_AchievementUpdateGetJustUnlocked(msg);
      name = PXR_AchievementAPI.UPxr_AchievementUpdateGetName(msg);
    }
  }
}
