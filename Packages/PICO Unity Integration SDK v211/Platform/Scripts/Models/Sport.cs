/*******************************************************************************
Copyright © 2015-2022 Pico Technology Co., Ltd.All rights reserved.

NOTICE：All information contained herein is, and remains the property of
Pico Technology Co., Ltd. The intellectual and technical concepts
contained herein are proprietary to Pico Technology Co., Ltd. and may be
covered by patents, patents in process, and are protected by trade secret or
copyright law. Dissemination of this information or reproduction of this
material is strictly forbidden unless prior written permission is obtained from
Pico Technology Co., Ltd.
*******************************************************************************/

using System;
using UnityEngine;

namespace Pico.Platform.Models
{
    // 运动时长和时间单位默认都是毫秒
    public class SportDailySummary
    {
        public readonly long Id;
        public readonly DateTime Date;
        public readonly int DurationInSeconds;
        public readonly int PlanDurationInMinutes;
        public readonly double Calorie;
        public readonly double PlanCalorie;

        public SportDailySummary(IntPtr o)
        {
            Id = CLIB.ppf_SportDailySummary_GetId(o);
            Date = Util.MilliSecondsToDateTime(CLIB.ppf_SportDailySummary_GetDate(o));
            DurationInSeconds = CLIB.ppf_SportDailySummary_GetDurationInSeconds(o);
            PlanDurationInMinutes = CLIB.ppf_SportDailySummary_GetPlanDurationInMinutes(o);
            Calorie = CLIB.ppf_SportDailySummary_GetCalorie(o);
            PlanCalorie = CLIB.ppf_SportDailySummary_GetPlanCalorie(o);
        }
    }


    public class SportDailySummaryList : MessageArray<SportDailySummary>
    {
        public SportDailySummaryList(IntPtr a)
        {
            var count = (int) CLIB.ppf_SportDailySummaryArray_GetSize(a);
            this.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                this.Add(new SportDailySummary(CLIB.ppf_SportDailySummaryArray_GetElement(a, (UIntPtr) i)));
            }
        }
    }

    public class SportSummary
    {
        public readonly long DurationInSeconds;
        public readonly double Calorie;
        public readonly DateTime StartTime;
        public readonly DateTime EndTime;

        public SportSummary(IntPtr o)
        {
            DurationInSeconds = CLIB.ppf_SportSummary_GetDurationInSeconds(o);
            Calorie = CLIB.ppf_SportSummary_GetCalorie(o);
            StartTime = Util.MilliSecondsToDateTime(CLIB.ppf_SportSummary_GetStartTime(o));
            EndTime = Util.MilliSecondsToDateTime(CLIB.ppf_SportSummary_GetEndTime(o));
        }
    }

    public class SportUserInfo
    {
        public readonly Gender Gender;
        public readonly DateTime Birthday;
        public readonly int Stature;
        public readonly int Weight;
        public readonly int SportLevel;
        public readonly int DailyDurationInMinutes;
        public readonly int DaysPerWeek;
        public readonly SportTarget SportTarget;

        public SportUserInfo(IntPtr o)
        {
            Gender = CLIB.ppf_SportUserInfo_GetGender(o);
            Birthday = Util.MilliSecondsToDateTime(CLIB.ppf_SportUserInfo_GetBirthday(o));
            Stature = CLIB.ppf_SportUserInfo_GetStature(o);
            Weight = CLIB.ppf_SportUserInfo_GetWeight(o);
            SportLevel = CLIB.ppf_SportUserInfo_GetSportLevel(o);
            DailyDurationInMinutes = CLIB.ppf_SportUserInfo_GetDailyDurationInMinutes(o);
            DaysPerWeek = CLIB.ppf_SportUserInfo_GetDaysPerWeek(o);
            SportTarget = CLIB.ppf_SportUserInfo_GetSportTarget(o);
        }
    }
}