using UnityEngine;

namespace Unity.XR.PXR
{
    public abstract class PXR_Message<T> : PXR_Message
    {
        public new delegate void Callback(PXR_Message<T> message);
        public PXR_Message(AndroidJavaObject msg) : base(msg)
        {
            if (!IsError)
            {
                data = GetDataFromMessage(msg);
            }
        }

        public T Data { get { return data; } }
        protected abstract T GetDataFromMessage(AndroidJavaObject msg);
        private T data;
    }

    public class PXR_Message
    {
        public delegate void Callback(PXR_Message message);
        public PXR_Message(AndroidJavaObject msg)
        {
            type = (MessageType)PXR_AchievementAPI.UPxr_MessageGetType(msg);
            var isError = PXR_AchievementAPI.UPxr_MessageIsError(msg);
            requestID = PXR_AchievementAPI.UPxr_MessageGetRequestID(msg);
            if (isError)
            {
                error = new Error(
                  PXR_AchievementAPI.UPxr_ErrorGetCode(msg),
                  PXR_AchievementAPI.UPxr_ErrorGetMessage(msg),
                  PXR_AchievementAPI.UPxr_ErrorGetHttpCode(msg));
            }
            else if (PXR_AchievementCore.LogMessages)
            {
                var message = PXR_AchievementAPI.UPxr_MessageGetString(msg);
                if (message != null)
                {
                    Debug.Log(message);
                }
                else
                {
                    Debug.Log(string.Format("PXRLog null message string {0}", msg));
                }
            }
        }
        public enum MessageType : uint
        {
            Unknown,
            AchievementsAddCount = 0x03E76231,
            AchievementsAddFields = 0x14AA2129,
            AchievementsGetAllDefinitions = 0x03D3458D,
            AchievementsGetAllProgress = 0x4F9FDE1D,
            AchievementsGetDefinitionsByName = 0x629101BC,
            AchievementsGetNextAchievementDefinitionArrayPage = 0x2A7DD255,
            AchievementsGetNextAchievementProgressArrayPage = 0x2F42E727,
            AchievementsGetProgressByName = 0x152663B1,
            AchievementsUnlock = 0x593CCBDD,
            AchievementsWriteAchievementProgress = 0x736BBDD,
            AchievementsVerifyAccessToken = 0x032D103C
        };

        public MessageType Type { get { return type; } }
        public bool IsError { get { return error != null; } }
        public long RequestID { get { return requestID; } }

        private MessageType type;
        private long requestID;
        private Error error;

        public virtual Error GetError() { return error; }
        public virtual PXR_AchievementDefinitionList GetAchievementDefinitions() { return null; }
        public virtual PXR_AchievementProgressList GetAchievementProgressList() { return null; }
        public virtual PXR_AchievementUpdate GetAchievementUpdate() { return null; }
        public virtual string GetString() { return null; }

        internal static PXR_Message ParseMessageHandle(AndroidJavaObject messageHandle)
        {
            if (messageHandle == null)
            {
                return null;
            }

            PXR_Message message = null;
            MessageType message_type = (MessageType)PXR_AchievementAPI.UPxr_MessageGetType(messageHandle);

            switch (message_type)
            {
                case MessageType.AchievementsGetAllDefinitions:
                case MessageType.AchievementsGetDefinitionsByName:
                case MessageType.AchievementsGetNextAchievementDefinitionArrayPage:
                    message = new MessageWithAchievementDefinitions(messageHandle);
                    break;

                case MessageType.AchievementsGetAllProgress:
                case MessageType.AchievementsGetNextAchievementProgressArrayPage:
                case MessageType.AchievementsGetProgressByName:
                    message = new MessageWithAchievementProgressList(messageHandle);
                    break;

                case MessageType.AchievementsAddCount:
                case MessageType.AchievementsAddFields:
                case MessageType.AchievementsUnlock:
                case MessageType.AchievementsVerifyAccessToken:
                    message = new MessageWithAchievementUpdate(messageHandle);
                    break;

            }

            return message;
        }

        public static PXR_Message PopMessage()
        {
            var messageHandle = PXR_AchievementAPI.UPxr_PopMessage();

            PXR_Message message = ParseMessageHandle(messageHandle);

            return message;
        }

        internal delegate PXR_Message ExtraMessageTypesHandler(AndroidJavaObject messageHandle, MessageType messageType);
        internal static ExtraMessageTypesHandler HandleExtraMessageTypes { set; private get; }
    }


    public class MessageWithAchievementDefinitions : PXR_Message<PXR_AchievementDefinitionList>
    {
        public MessageWithAchievementDefinitions(AndroidJavaObject msg) : base(msg) { }
        public override PXR_AchievementDefinitionList GetAchievementDefinitions() { return Data; }
        protected override PXR_AchievementDefinitionList GetDataFromMessage(AndroidJavaObject msg)
        {
            return new PXR_AchievementDefinitionList(msg);
        }

    }
    public class MessageWithAchievementProgressList : PXR_Message<PXR_AchievementProgressList>
    {
        public MessageWithAchievementProgressList(AndroidJavaObject msg) : base(msg) { }
        public override PXR_AchievementProgressList GetAchievementProgressList() { return Data; }
        protected override PXR_AchievementProgressList GetDataFromMessage(AndroidJavaObject msg)
        {
            return new PXR_AchievementProgressList(msg);
        }

    }
    public class MessageWithAchievementUpdate : PXR_Message<PXR_AchievementUpdate>
    {
        public MessageWithAchievementUpdate(AndroidJavaObject msg) : base(msg) { }
        public override PXR_AchievementUpdate GetAchievementUpdate() { return Data; }
        protected override PXR_AchievementUpdate GetDataFromMessage(AndroidJavaObject msg)
        {
            return new PXR_AchievementUpdate(msg);
        }

    }
    public class MessageWithString : PXR_Message<string>
    {
        public MessageWithString(AndroidJavaObject msg) : base(msg) { }
        public override string GetString() { return Data; }
        protected override string GetDataFromMessage(AndroidJavaObject msg)
        {
            return PXR_AchievementAPI.UPxr_MessageGetString(msg);
        }
    }
    public class Error
    {
        public Error(int code, string message, int httpCode)
        {
            this.message = message;
            this.code = code;
            this.httpCode = httpCode;
        }

        public readonly int code;
        public readonly int httpCode;
        public readonly string message;
    }

}
