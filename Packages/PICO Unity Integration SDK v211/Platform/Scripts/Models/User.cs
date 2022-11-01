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
    public class User
    {
        public readonly string DisplayName;
        public readonly string ImageUrl;
        public readonly string ID;
        public readonly UserPresenceStatus PresenceStatus;
        public readonly Gender Gender;
        public readonly string Presence;
        public readonly string PresenceDeeplinkMessage;
        public readonly string PresenceDestinationApiName;
        public readonly string PresenceLobbySessionId;
        public readonly string PresenceMatchSessionId;
        public readonly string PresenceExtra;
        public readonly string SmallImageUrl;
        public readonly string InviteToken;
        public readonly string StoreRegion;

        public User(IntPtr obj)
        {
            DisplayName = CLIB.ppf_User_GetDisplayName(obj);
            ImageUrl = CLIB.ppf_User_GetImageUrl(obj);
            ID = CLIB.ppf_User_GetID(obj);
            InviteToken = CLIB.ppf_User_GetInviteToken(obj);
            PresenceStatus = CLIB.ppf_User_GetPresenceStatus(obj);
            Gender = CLIB.ppf_User_GetGender(obj);
            Presence = CLIB.ppf_User_GetPresence(obj);
            PresenceDeeplinkMessage = CLIB.ppf_User_GetPresenceDeeplinkMessage(obj);
            PresenceDestinationApiName = CLIB.ppf_User_GetPresenceDestinationApiName(obj);
            PresenceLobbySessionId = CLIB.ppf_User_GetPresenceLobbySessionId(obj);
            PresenceMatchSessionId = CLIB.ppf_User_GetPresenceMatchSessionId(obj);
            PresenceExtra = CLIB.ppf_User_GetPresenceExtra(obj);
            SmallImageUrl = CLIB.ppf_User_GetSmallImageUrl(obj);
            InviteToken = CLIB.ppf_User_GetInviteToken(obj);
            StoreRegion = CLIB.ppf_User_GetStoreRegion(obj);
        }
    }


    public class UserList : MessageArray<User>
    {
        public UserList(IntPtr a)
        {
            var count = (int) CLIB.ppf_UserArray_GetSize(a);
            this.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                this.Add(new User(CLIB.ppf_UserArray_GetElement(a, (UIntPtr) i)));
            }

            NextPageParam = CLIB.ppf_UserArray_GetNextPageParam(a);
        }
    }

    public class LaunchFriendResult
    {
        public readonly bool DidCancel;
        public readonly bool DidSendRequest;

        public LaunchFriendResult(IntPtr obj)
        {
            DidCancel = CLIB.ppf_LaunchFriendRequestFlowResult_GetDidCancel(obj);
            DidSendRequest = CLIB.ppf_LaunchFriendRequestFlowResult_GetDidSendRequest(obj);
        }
    }


    public class UserRoom
    {
        public readonly User User;
        public readonly Room Room;

        public UserRoom(IntPtr o)
        {
            User = new User(CLIB.ppf_UserAndRoom_GetUser(o));
            var ptr = CLIB.ppf_UserAndRoom_GetRoom(o);
            if (ptr != IntPtr.Zero)
            {
                Room = new Room(ptr);
            }
        }
    }


    public class UserRoomList : MessageArray<UserRoom>
    {
        public UserRoomList(IntPtr a)
        {
            var count = (int) CLIB.ppf_UserAndRoomArray_GetSize(a);
            this.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                this.Add(new UserRoom(CLIB.ppf_UserAndRoomArray_GetElement(a, (UIntPtr) i)));
            }

            NextPageParam = CLIB.ppf_UserAndRoomArray_GetNextPageParam(a);
        }
    }


    public class PermissionResult
    {
        public readonly string[] AuthorizedPermissions;
        public readonly string AccessToken;
        public readonly string UserID;

        public PermissionResult(IntPtr o)
        {
            {
                int sz = (int) CLIB.ppf_PermissionResult_GetAuthorizedPermissionsSize(o);
                AuthorizedPermissions = new string[sz];
                for (int i = 0; i < sz; i++)
                {
                    AuthorizedPermissions[i] = CLIB.ppf_PermissionResult_GetAuthorizedPermissions(o, (UIntPtr) i);
                }
            }

            AccessToken = CLIB.ppf_PermissionResult_GetAccessToken(o);
            UserID = CLIB.ppf_PermissionResult_GetUserID(o);
        }
    }
}