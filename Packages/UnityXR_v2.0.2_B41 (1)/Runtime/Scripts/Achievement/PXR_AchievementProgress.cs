using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.XR.PXR
{
  public class PXR_AchievementProgress
  {
    public readonly string bitfield;
    public readonly long count;
    public readonly bool isUnlocked;
    public readonly string name;
    public readonly DateTime unlockTime;


    public PXR_AchievementProgress(AndroidJavaObject msg)
    {
      bitfield = PXR_AchievementAPI.UPxr_AchievementProgressGetBitfield(msg);
      count = PXR_AchievementAPI.UPxr_AchievementProgressGetCount(msg);
      isUnlocked = PXR_AchievementAPI.UPxr_AchievementProgressGetIsUnlocked(msg);
      name = PXR_AchievementAPI.UPxr_AchievementProgressGetName(msg);
      unlockTime = PXR_AchievementAPI.UPxr_AchievementProgressGetUnlockTime(msg);
    }
  }

  public class PXR_AchievementProgressList : PXR_DeserializableList<PXR_AchievementProgress> {
    public PXR_AchievementProgressList(AndroidJavaObject msg) {
      var count = PXR_AchievementAPI.UPxr_AchievementProgressArrayGetSize(msg);
      data = new List<PXR_AchievementProgress>(count);
      for (int i = 0; i < count; i++) {
        data.Add(new PXR_AchievementProgress(PXR_AchievementAPI.UPxr_AchievementProgressArrayGetElement(msg, i)));
      }
      nextUrl = PXR_AchievementAPI.UPxr_AchievementProgressArrayGetNextUrl(msg);
    }
  }
}
