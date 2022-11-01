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

namespace Pico.Platform.Models
{
    
    public class Destination
    {
        public readonly string ApiName;
        public readonly string DeeplinkMessage;
        public readonly string DisplayName;

        public Destination(IntPtr o)
        {
            ApiName = CLIB.ppf_Destination_GetApiName(o);
            DeeplinkMessage = CLIB.ppf_Destination_GetDeeplinkMessage(o);
            DisplayName = CLIB.ppf_Destination_GetDisplayName(o);
        }
    }


    public class DestinationList : MessageArray<Destination>
    {
        public DestinationList(IntPtr a)
        {
            var count = (int)CLIB.ppf_DestinationArray_GetSize(a);
            this.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                this.Add(new Destination(CLIB.ppf_DestinationArray_GetElement(a,(UIntPtr) i)));
            }

            NextPageParam = CLIB.ppf_DestinationArray_GetNextPageParam(a);
        }
    }
    
    public class ApplicationInvite
    {
        public readonly Destination Destination;
        public readonly User Recipient;
        public readonly UInt64 ID;
        public readonly bool IsActive;
        public readonly string LobbySessionId;
        public readonly string MatchSessionId;

        public ApplicationInvite(IntPtr o)
        {
            Destination = new Destination(CLIB.ppf_ApplicationInvite_GetDestination(o));
            Recipient = new User(CLIB.ppf_ApplicationInvite_GetRecipient(o));
            ID = CLIB.ppf_ApplicationInvite_GetID(o);
            IsActive = CLIB.ppf_ApplicationInvite_GetIsActive(o);
            LobbySessionId = CLIB.ppf_ApplicationInvite_GetLobbySessionId(o);
            MatchSessionId = CLIB.ppf_ApplicationInvite_GetMatchSessionId(o);
        }
    }
    

    public class ApplicationInviteList : MessageArray<ApplicationInvite>
    {
        public ApplicationInviteList(IntPtr a)
        {
            var count = (int)CLIB.ppf_ApplicationInviteArray_GetSize(a);
            this.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                this.Add(new ApplicationInvite(CLIB.ppf_ApplicationInviteArray_GetElement(a, (UIntPtr)i)));
            }

            NextPageParam = CLIB.ppf_ApplicationInviteArray_GetNextPageParam(a);
        }
    }
    
    public class LaunchDetails
    {
        public readonly string DeeplinkMessage;
        public readonly string DestinationApiName;
        public readonly string LaunchSource;
        public readonly string LobbySessionID;
        public readonly string MatchSessionID;
        public readonly string Extra;
        public readonly UInt64 RoomID;
        public readonly string TrackingID;
        public readonly UserList Users;
        public readonly LaunchType LaunchType;

        public LaunchDetails(IntPtr o)
        {
            DeeplinkMessage = CLIB.ppf_LaunchDetails_GetDeeplinkMessage(o);
            DestinationApiName = CLIB.ppf_LaunchDetails_GetDestinationApiName(o);
            LaunchSource = CLIB.ppf_LaunchDetails_GetLaunchSource(o);
            LobbySessionID = CLIB.ppf_LaunchDetails_GetLobbySessionID(o);
            MatchSessionID = CLIB.ppf_LaunchDetails_GetMatchSessionID(o);
            Extra = CLIB.ppf_LaunchDetails_GetExtra(o);
            RoomID = CLIB.ppf_LaunchDetails_GetRoomID(o);
            TrackingID = CLIB.ppf_LaunchDetails_GetTrackingID(o);
            Users = new UserList(CLIB.ppf_LaunchDetails_GetUsers(o));
            LaunchType = CLIB.ppf_LaunchDetails_GetLaunchType(o);
        }
    }
    
    public class SendInvitesResult
    {
        public readonly ApplicationInviteList Invites;

        public SendInvitesResult(IntPtr o)
        {
            Invites = new ApplicationInviteList(CLIB.ppf_SendInvitesResult_GetInvites(o));
        }
    }
}