using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TLSharp.Core.MTProto
{
    #region Abstract types

    public abstract class TLObject
    {
        public abstract uint ConstructorCode { get; }
        public abstract void Write(TBinaryWriter writer);
        public abstract void Read(TBinaryReader reader);
    }

    public abstract class MTProtoRequest
    {
        public long MessageId { get; set; }
        public int Sequence { get; set; }
        public abstract uint ConstructorCode { get; }

        public bool Dirty { get; set; }

        public bool Sent { get; private set; }
        public DateTime SendTime { get; private set; }
        public bool ConfirmReceived { get; set; }
        public abstract void OnSend(TBinaryWriter writer);
        public abstract void OnResponse(TBinaryReader reader);
        public abstract void OnException(Exception exception);
        public abstract bool Confirmed { get; }
        public abstract bool Responded { get; }

        public virtual void OnSendSuccess()
        {
            SendTime = DateTime.Now;
            Sent = true;
        }

        public virtual void OnConfirm()
        {
            ConfirmReceived = true;
        }

        public bool NeedResend => Dirty || (Confirmed && !ConfirmReceived && DateTime.Now - SendTime > TimeSpan.FromSeconds(3));
    }

    public abstract class MsgsAck : TLObject { }
    public abstract class BadMsgNotification : TLObject { }
    public abstract class MsgsStateReq : TLObject { }
    public abstract class MsgsStateInfo : TLObject { }
    public abstract class MsgsAllInfo : TLObject { }
    public abstract class MsgDetailedInfo : TLObject { }
    public abstract class MsgResendReq : TLObject { }
    public abstract class RpcError : TLObject { }
    public abstract class RpcDropAnswer : TLObject { }
    public abstract class FutureSalt : TLObject { }
    public abstract class FutureSalts : TLObject { }
    public abstract class Pong : TLObject { }
    public abstract class DestroySessionRes : TLObject { }
    public abstract class NewSession : TLObject { }
    public abstract class HttpWait : TLObject { }
    public abstract class True : TLObject { }
    public abstract class Error : TLObject { }
    public abstract class Null : TLObject { }
    public abstract class InputPeer : TLObject { }
    public abstract class InputUser : TLObject { }
    public abstract class InputContact : TLObject { }
    public abstract class InputFile : TLObject { }
    public abstract class InputMedia : TLObject { }
    public abstract class InputChatPhoto : TLObject { }
    public abstract class InputGeoPoint : TLObject { }
    public abstract class InputPhoto : TLObject { }
    public abstract class InputFileLocation : TLObject { }
    public abstract class InputPhotoCrop : TLObject { }
    public abstract class InputAppEvent : TLObject { }
    public abstract class Peer : TLObject { }
    public abstract class StorageFileType : TLObject { }
    public abstract class FileLocation : TLObject { }
    public abstract class User : TLObject { }
    public abstract class UserProfilePhoto : TLObject { }
    public abstract class UserStatus : TLObject { }
    public abstract class Chat : TLObject { }
    public abstract class ChatFull : TLObject { }
    public abstract class ChatParticipant : TLObject { }
    public abstract class ChatParticipants : TLObject { }
    public abstract class ChatPhoto : TLObject { }
    public abstract class Message : TLObject { }
    public abstract class MessageMedia : TLObject { }
    public abstract class MessageAction : TLObject { }
    public abstract class Dialog : TLObject { }
    public abstract class Photo : TLObject { }
    public abstract class PhotoSize : TLObject { }
    public abstract class GeoPoint : TLObject { }
    public abstract class AuthCheckedPhone : TLObject { }
    public abstract class AuthSentCode : TLObject { }
    public abstract class AuthAuthorization : TLObject { }
    public abstract class AuthExportedAuthorization : TLObject { }
    public abstract class InputNotifyPeer : TLObject { }
    public abstract class InputPeerNotifyEvents : TLObject { }
    public abstract class InputPeerNotifySettings : TLObject { }
    public abstract class PeerNotifyEvents : TLObject { }
    public abstract class PeerNotifySettings : TLObject { }
    public abstract class WallPaper : TLObject { }
    public abstract class ReportReason : TLObject { }
    public abstract class UserFull : TLObject { }
    public abstract class Contact : TLObject { }
    public abstract class ImportedContact : TLObject { }
    public abstract class ContactBlocked : TLObject { }
    public abstract class ContactStatus : TLObject { }
    public abstract class ContactsLink : TLObject { }
    public abstract class ContactsContacts : TLObject { }
    public abstract class ContactsImportedContacts : TLObject { }
    public abstract class ContactsBlocked : TLObject { }
    public abstract class MessagesDialogs : TLObject { }
    public abstract class MessagesMessages : TLObject { }
    public abstract class MessagesChats : TLObject { }
    public abstract class MessagesChatFull : TLObject { }
    public abstract class MessagesAffectedHistory : TLObject { }
    public abstract class MessagesFilter : TLObject { }
    public abstract class Update : TLObject { }
    public abstract class UpdatesState : TLObject { }
    public abstract class UpdatesDifference : TLObject { }
    public abstract class Updates : TLObject { }
    public abstract class PhotosPhotos : TLObject { }
    public abstract class PhotosPhoto : TLObject { }
    public abstract class UploadFile : TLObject { }
    public abstract class DcOption : TLObject { }
    public abstract class Config : TLObject { }
    public abstract class NearestDc : TLObject { }
    public abstract class HelpAppUpdate : TLObject { }
    public abstract class HelpInviteText : TLObject { }
    public abstract class EncryptedChat : TLObject { }
    public abstract class InputEncryptedChat : TLObject { }
    public abstract class EncryptedFile : TLObject { }
    public abstract class InputEncryptedFile : TLObject { }
    public abstract class EncryptedMessage : TLObject { }
    public abstract class MessagesDhConfig : TLObject { }
    public abstract class MessagesSentEncryptedMessage : TLObject { }
    public abstract class InputDocument : TLObject { }
    public abstract class Document : TLObject { }
    public abstract class HelpSupport : TLObject { }
    public abstract class NotifyPeer : TLObject { }
    public abstract class SendMessageAction : TLObject { }
    public abstract class ContactsFound : TLObject { }
    public abstract class InputPrivacyKey : TLObject { }
    public abstract class PrivacyKey : TLObject { }
    public abstract class InputPrivacyRule : TLObject { }
    public abstract class PrivacyRule : TLObject { }
    public abstract class AccountPrivacyRules : TLObject { }
    public abstract class AccountDaysTTL : TLObject { }
    public abstract class AccountSentChangePhoneCode : TLObject { }
    public abstract class DocumentAttribute : TLObject { }
    public abstract class MessagesStickers : TLObject { }
    public abstract class StickerPack : TLObject { }
    public abstract class MessagesAllStickers : TLObject { }
    public abstract class DisabledFeature : TLObject { }
    public abstract class MessagesAffectedMessages : TLObject { }
    public abstract class ContactLink : TLObject { }
    public abstract class WebPage : TLObject { }
    public abstract class Authorization : TLObject { }
    public abstract class AccountAuthorizations : TLObject { }
    public abstract class AccountPassword : TLObject { }
    public abstract class AccountPasswordSettings : TLObject { }
    public abstract class AccountPasswordInputSettings : TLObject { }
    public abstract class AuthPasswordRecovery : TLObject { }
    public abstract class ReceivedNotifyMessage : TLObject { }
    public abstract class ExportedChatInvite : TLObject { }
    public abstract class ChatInvite : TLObject { }
    public abstract class InputStickerSet : TLObject { }
    public abstract class StickerSet : TLObject { }
    public abstract class MessagesStickerSet : TLObject { }
    public abstract class BotCommand : TLObject { }
    public abstract class BotInfo : TLObject { }
    public abstract class KeyboardButton : TLObject { }
    public abstract class KeyboardButtonRow : TLObject { }
    public abstract class ReplyMarkup : TLObject { }
    public abstract class HelpAppChangelog : TLObject { }
    public abstract class MessageEntity : TLObject { }
    public abstract class InputChannel : TLObject { }
    public abstract class ContactsResolvedPeer : TLObject { }
    public abstract class MessageRange : TLObject { }
    public abstract class MessageGroup : TLObject { }
    public abstract class UpdatesChannelDifference : TLObject { }
    public abstract class ChannelMessagesFilter : TLObject { }
    public abstract class ChannelParticipant : TLObject { }
    public abstract class ChannelParticipantsFilter : TLObject { }
    public abstract class ChannelParticipantRole : TLObject { }
    public abstract class ChannelsChannelParticipants : TLObject { }
    public abstract class ChannelsChannelParticipant : TLObject { }
    public abstract class HelpTermsOfService : TLObject { }
    public abstract class FoundGif : TLObject { }
    public abstract class MessagesFoundGifs : TLObject { }
    public abstract class MessagesSavedGifs : TLObject { }
    public abstract class InputBotInlineMessage : TLObject { }
    public abstract class InputBotInlineResult : TLObject { }
    public abstract class BotInlineMessage : TLObject { }
    public abstract class BotInlineResult : TLObject { }
    public abstract class MessagesBotResults : TLObject { }

    #endregion

    #region TBinaryReader and TBinaryWriter

    public class TBinaryReader : BinaryReader
    {
        public TBinaryReader(Stream stream) : base(stream) { }
        public TBinaryReader(Stream stream, Encoding encoding) : base(stream, encoding) { }
        public TBinaryReader(Stream stream, Encoding encoding, bool leaveOpen) : base(stream, encoding, leaveOpen) { }

        public byte[] ReadBytes()
        {
            byte firstByte = ReadByte();
            int len, padding;
            if (firstByte == 254)
            {
                len = ReadByte() | (ReadByte() << 8) | (ReadByte() << 16);
                padding = len % 4;
            }
            else
            {
                len = firstByte;
                padding = (len + 1) % 4;
            }

            byte[] data = ReadBytes(len);
            if (padding > 0)
            {
                padding = 4 - padding;
                ReadBytes(padding);
            }

            return data;
        }

        public override string ReadString()
        {
            byte[] data = ReadBytes();
            return Encoding.UTF8.GetString(data, 0, data.Length);
        }

        public override bool ReadBoolean()
        {
            return ReadUInt32() == 0x997275b5; // uint == true code ? true : false
        }

        public True ReadTrue()
        {
            // true's don't require to be read, they're only used in flags
            return new TL.TrueType();
        }

        public T Read<T>()
        {
            return (T)(object)ReadTLObject();
        }

        public TLObject ReadTLObject()
        {
            var code = ReadUInt32();
            return ReadTLObject(code);
        }

        public TLObject ReadTLObject(uint code)
        {
            TLObject obj = (TLObject)Activator.CreateInstance(TL.Constructors[code]);
            obj.Read(this);
            return obj;
        }
    }

    public class TBinaryWriter : BinaryWriter
    {
        public TBinaryWriter() { }
        public TBinaryWriter(Stream stream): base(stream) { }
        public TBinaryWriter(Stream stream, Encoding encoding): base(stream, encoding) { }
        public TBinaryWriter(Stream stream, Encoding encoding, bool leaveOpen): base(stream, encoding, leaveOpen) { }

        public override void Write(byte[] data)
        {
            int padding;
            if (data.Length < 254)
            {
                padding = (data.Length + 1) % 4;
                if (padding != 0)
                {
                    padding = 4 - padding;
                }

                base.Write((byte)data.Length);
                base.Write(data);
            }
            else
            {
                padding = (data.Length) % 4;
                if (padding != 0)
                {
                    padding = 4 - padding;
                }
                base.Write((byte)254);
                base.Write((byte)(data.Length));
                base.Write((byte)(data.Length >> 8));
                base.Write((byte)(data.Length >> 16));
                base.Write(data);
            }

            for (int i = 0; i < padding; i++)
            {
                base.Write((byte)0);
            }
        }
        public void WriteBase(byte[] data) => base.Write(data);

        public override void Write(string value)
        {
            Write(Encoding.UTF8.GetBytes(value));
        }

        public override void Write(bool value)
        {
            //            true         false
            Write(value ? 0x997275b5 : 0xbc799737);
        }

    }

    #endregion

    public class TL
    {
        #region Constructors dictionary

        public static readonly Dictionary<uint, Type> Constructors = new Dictionary<uint, Type>()
        {
            { 0x62d6b459, typeof(MsgsAckType) },
            { 0xa7eff811, typeof(BadMsgNotificationType) },
            { 0xedab447b, typeof(BadServerSaltType) },
            { 0xda69fb52, typeof(MsgsStateReqType) },
            { 0x04deb57d, typeof(MsgsStateInfoType) },
            { 0x8cc0d131, typeof(MsgsAllInfoType) },
            { 0x276d3ec6, typeof(MsgDetailedInfoType) },
            { 0x809db6df, typeof(MsgNewDetailedInfoType) },
            { 0x7d861a08, typeof(MsgResendReqType) },
            { 0x2144ca19, typeof(RpcErrorType) },
            { 0x5e2ad36e, typeof(RpcAnswerUnknownType) },
            { 0xcd78e586, typeof(RpcAnswerDroppedRunningType) },
            { 0xa43ad8b7, typeof(RpcAnswerDroppedType) },
            { 0x0949d9dc, typeof(FutureSaltType) },
            { 0xae500895, typeof(FutureSaltsType) },
            { 0x347773c5, typeof(PongType) },
            { 0xe22045fc, typeof(DestroySessionOkType) },
            { 0x62d350c9, typeof(DestroySessionNoneType) },
            { 0x9ec20908, typeof(NewSessionCreatedType) },
            { 0x9299359f, typeof(HttpWaitType) },
            { 0x3fedd339, typeof(TrueType) },
            { 0xc4b9f9bb, typeof(ErrorType) },
            { 0x56730bcc, typeof(NullType) },
            { 0x7f3b18ea, typeof(InputPeerEmptyType) },
            { 0x7da07ec9, typeof(InputPeerSelfType) },
            { 0x179be863, typeof(InputPeerChatType) },
            { 0x7b8e7de6, typeof(InputPeerUserType) },
            { 0x20adaef8, typeof(InputPeerChannelType) },
            { 0xb98886cf, typeof(InputUserEmptyType) },
            { 0xf7c1b13f, typeof(InputUserSelfType) },
            { 0xd8292816, typeof(InputUserType) },
            { 0xf392b7f4, typeof(InputPhoneContactType) },
            { 0xf52ff27f, typeof(InputFileType) },
            { 0xfa4f0bb5, typeof(InputFileBigType) },
            { 0x9664f57f, typeof(InputMediaEmptyType) },
            { 0xf7aff1c0, typeof(InputMediaUploadedPhotoType) },
            { 0xe9bfb4f3, typeof(InputMediaPhotoType) },
            { 0xf9c44144, typeof(InputMediaGeoPointType) },
            { 0xa6e45987, typeof(InputMediaContactType) },
            { 0x1d89306d, typeof(InputMediaUploadedDocumentType) },
            { 0xad613491, typeof(InputMediaUploadedThumbDocumentType) },
            { 0x1a77f29c, typeof(InputMediaDocumentType) },
            { 0x2827a81a, typeof(InputMediaVenueType) },
            { 0x4843b0fd, typeof(InputMediaGifExternalType) },
            { 0x1ca48f57, typeof(InputChatPhotoEmptyType) },
            { 0x94254732, typeof(InputChatUploadedPhotoType) },
            { 0xb2e1bf08, typeof(InputChatPhotoType) },
            { 0xe4c123d6, typeof(InputGeoPointEmptyType) },
            { 0xf3b7acc9, typeof(InputGeoPointType) },
            { 0x1cd7bf0d, typeof(InputPhotoEmptyType) },
            { 0xfb95c6c4, typeof(InputPhotoType) },
            { 0x14637196, typeof(InputFileLocationType) },
            { 0xf5235d55, typeof(InputEncryptedFileLocationType) },
            { 0x4e45abe9, typeof(InputDocumentFileLocationType) },
            { 0xade6b004, typeof(InputPhotoCropAutoType) },
            { 0xd9915325, typeof(InputPhotoCropType) },
            { 0x770656a8, typeof(InputAppEventType) },
            { 0x9db1bc6d, typeof(PeerUserType) },
            { 0xbad0e5bb, typeof(PeerChatType) },
            { 0xbddde532, typeof(PeerChannelType) },
            { 0xaa963b05, typeof(StorageFileUnknownType) },
            { 0x007efe0e, typeof(StorageFileJpegType) },
            { 0xcae1aadf, typeof(StorageFileGifType) },
            { 0x0a4f63c0, typeof(StorageFilePngType) },
            { 0xae1e508d, typeof(StorageFilePdfType) },
            { 0x528a0677, typeof(StorageFileMp3Type) },
            { 0x4b09ebbc, typeof(StorageFileMovType) },
            { 0x40bc6f52, typeof(StorageFilePartialType) },
            { 0xb3cea0e4, typeof(StorageFileMp4Type) },
            { 0x1081464c, typeof(StorageFileWebpType) },
            { 0x7c596b46, typeof(FileLocationUnavailableType) },
            { 0x53d69076, typeof(FileLocationType) },
            { 0x200250ba, typeof(UserEmptyType) },
            { 0xd10d979a, typeof(UserType) },
            { 0x4f11bae1, typeof(UserProfilePhotoEmptyType) },
            { 0xd559d8c8, typeof(UserProfilePhotoType) },
            { 0x09d05049, typeof(UserStatusEmptyType) },
            { 0xedb93949, typeof(UserStatusOnlineType) },
            { 0x008c703f, typeof(UserStatusOfflineType) },
            { 0xe26f42f1, typeof(UserStatusRecentlyType) },
            { 0x07bf09fc, typeof(UserStatusLastWeekType) },
            { 0x77ebc742, typeof(UserStatusLastMonthType) },
            { 0x9ba2d800, typeof(ChatEmptyType) },
            { 0xd91cdd54, typeof(ChatType) },
            { 0x07328bdb, typeof(ChatForbiddenType) },
            { 0x4b1b7506, typeof(ChannelType) },
            { 0x2d85832c, typeof(ChannelForbiddenType) },
            { 0x2e02a614, typeof(ChatFullType) },
            { 0x9e341ddf, typeof(ChannelFullType) },
            { 0xc8d7493e, typeof(ChatParticipantType) },
            { 0xda13538a, typeof(ChatParticipantCreatorType) },
            { 0xe2d6e436, typeof(ChatParticipantAdminType) },
            { 0xfc900c2b, typeof(ChatParticipantsForbiddenType) },
            { 0x3f460fed, typeof(ChatParticipantsType) },
            { 0x37c1011c, typeof(ChatPhotoEmptyType) },
            { 0x6153276a, typeof(ChatPhotoType) },
            { 0x83e5de54, typeof(MessageEmptyType) },
            { 0xc992e15c, typeof(MessageType) },
            { 0xc06b9607, typeof(MessageServiceType) },
            { 0x3ded6320, typeof(MessageMediaEmptyType) },
            { 0x3d8ce53d, typeof(MessageMediaPhotoType) },
            { 0x56e0d474, typeof(MessageMediaGeoType) },
            { 0x5e7d2f39, typeof(MessageMediaContactType) },
            { 0x9f84f49e, typeof(MessageMediaUnsupportedType) },
            { 0xf3e02ea8, typeof(MessageMediaDocumentType) },
            { 0xa32dd600, typeof(MessageMediaWebPageType) },
            { 0x7912b71f, typeof(MessageMediaVenueType) },
            { 0xb6aef7b0, typeof(MessageActionEmptyType) },
            { 0xa6638b9a, typeof(MessageActionChatCreateType) },
            { 0xb5a1ce5a, typeof(MessageActionChatEditTitleType) },
            { 0x7fcb13a8, typeof(MessageActionChatEditPhotoType) },
            { 0x95e3fbef, typeof(MessageActionChatDeletePhotoType) },
            { 0x488a7337, typeof(MessageActionChatAddUserType) },
            { 0xb2ae9b0c, typeof(MessageActionChatDeleteUserType) },
            { 0xf89cf5e8, typeof(MessageActionChatJoinedByLinkType) },
            { 0x95d2ac92, typeof(MessageActionChannelCreateType) },
            { 0x51bdb021, typeof(MessageActionChatMigrateToType) },
            { 0xb055eaee, typeof(MessageActionChannelMigrateFromType) },
            { 0xc1dd804a, typeof(DialogType) },
            { 0x5b8496b2, typeof(DialogChannelType) },
            { 0x2331b22d, typeof(PhotoEmptyType) },
            { 0xcded42fe, typeof(PhotoType) },
            { 0x0e17e23c, typeof(PhotoSizeEmptyType) },
            { 0x77bfb61b, typeof(PhotoSizeType) },
            { 0xe9a734fa, typeof(PhotoCachedSizeType) },
            { 0x1117dd5f, typeof(GeoPointEmptyType) },
            { 0x2049d70c, typeof(GeoPointType) },
            { 0x811ea28e, typeof(AuthCheckedPhoneType) },
            { 0xefed51d9, typeof(AuthSentCodeType) },
            { 0xe325edcf, typeof(AuthSentAppCodeType) },
            { 0xff036af1, typeof(AuthAuthorizationType) },
            { 0xdf969c2d, typeof(AuthExportedAuthorizationType) },
            { 0xb8bc5b0c, typeof(InputNotifyPeerType) },
            { 0x193b4417, typeof(InputNotifyUsersType) },
            { 0x4a95e84e, typeof(InputNotifyChatsType) },
            { 0xa429b886, typeof(InputNotifyAllType) },
            { 0xf03064d8, typeof(InputPeerNotifyEventsEmptyType) },
            { 0xe86a2c74, typeof(InputPeerNotifyEventsAllType) },
            { 0x46a2ce98, typeof(InputPeerNotifySettingsType) },
            { 0xadd53cb3, typeof(PeerNotifyEventsEmptyType) },
            { 0x6d1ded88, typeof(PeerNotifyEventsAllType) },
            { 0x70a68512, typeof(PeerNotifySettingsEmptyType) },
            { 0x8d5e11ee, typeof(PeerNotifySettingsType) },
            { 0xccb03657, typeof(WallPaperType) },
            { 0x63117f24, typeof(WallPaperSolidType) },
            { 0x58dbcab8, typeof(InputReportReasonSpamType) },
            { 0x1e22c78d, typeof(InputReportReasonViolenceType) },
            { 0x2e59d922, typeof(InputReportReasonPornographyType) },
            { 0xe1746d0a, typeof(InputReportReasonOtherType) },
            { 0x5a89ac5b, typeof(UserFullType) },
            { 0xf911c994, typeof(ContactType) },
            { 0xd0028438, typeof(ImportedContactType) },
            { 0x561bc879, typeof(ContactBlockedType) },
            { 0xd3680c61, typeof(ContactStatusType) },
            { 0x3ace484c, typeof(ContactsLinkType) },
            { 0xb74ba9d2, typeof(ContactsContactsNotModifiedType) },
            { 0x6f8b8cb2, typeof(ContactsContactsType) },
            { 0xad524315, typeof(ContactsImportedContactsType) },
            { 0x1c138d15, typeof(ContactsBlockedType) },
            { 0x900802a1, typeof(ContactsBlockedSliceType) },
            { 0x15ba6c40, typeof(MessagesDialogsType) },
            { 0x71e094f3, typeof(MessagesDialogsSliceType) },
            { 0x8c718e87, typeof(MessagesMessagesType) },
            { 0x0b446ae3, typeof(MessagesMessagesSliceType) },
            { 0xbc0f17bc, typeof(MessagesChannelMessagesType) },
            { 0x64ff9fd5, typeof(MessagesChatsType) },
            { 0xe5d7d19c, typeof(MessagesChatFullType) },
            { 0xb45c69d1, typeof(MessagesAffectedHistoryType) },
            { 0x57e2f66c, typeof(InputMessagesFilterEmptyType) },
            { 0x9609a51c, typeof(InputMessagesFilterPhotosType) },
            { 0x9fc00e65, typeof(InputMessagesFilterVideoType) },
            { 0x56e9f0e4, typeof(InputMessagesFilterPhotoVideoType) },
            { 0xd95e73bb, typeof(InputMessagesFilterPhotoVideoDocumentsType) },
            { 0x9eddf188, typeof(InputMessagesFilterDocumentType) },
            { 0x7ef0dd87, typeof(InputMessagesFilterUrlType) },
            { 0xffc86587, typeof(InputMessagesFilterGifType) },
            { 0x50f5c392, typeof(InputMessagesFilterVoiceType) },
            { 0x3751b49e, typeof(InputMessagesFilterMusicType) },
            { 0x1f2b0afd, typeof(UpdateNewMessageType) },
            { 0x4e90bfd6, typeof(UpdateMessageIDType) },
            { 0xa20db0e5, typeof(UpdateDeleteMessagesType) },
            { 0x5c486927, typeof(UpdateUserTypingType) },
            { 0x9a65ea1f, typeof(UpdateChatUserTypingType) },
            { 0x07761198, typeof(UpdateChatParticipantsType) },
            { 0x1bfbd823, typeof(UpdateUserStatusType) },
            { 0xa7332b73, typeof(UpdateUserNameType) },
            { 0x95313b0c, typeof(UpdateUserPhotoType) },
            { 0x2575bbb9, typeof(UpdateContactRegisteredType) },
            { 0x9d2e67c5, typeof(UpdateContactLinkType) },
            { 0x8f06529a, typeof(UpdateNewAuthorizationType) },
            { 0x12bcbd9a, typeof(UpdateNewEncryptedMessageType) },
            { 0x1710f156, typeof(UpdateEncryptedChatTypingType) },
            { 0xb4a2e88d, typeof(UpdateEncryptionType) },
            { 0x38fe25b7, typeof(UpdateEncryptedMessagesReadType) },
            { 0xea4b0e5c, typeof(UpdateChatParticipantAddType) },
            { 0x6e5f8c22, typeof(UpdateChatParticipantDeleteType) },
            { 0x8e5e9873, typeof(UpdateDcOptionsType) },
            { 0x80ece81a, typeof(UpdateUserBlockedType) },
            { 0xbec268ef, typeof(UpdateNotifySettingsType) },
            { 0x382dd3e4, typeof(UpdateServiceNotificationType) },
            { 0xee3b272a, typeof(UpdatePrivacyType) },
            { 0x12b9417b, typeof(UpdateUserPhoneType) },
            { 0x9961fd5c, typeof(UpdateReadHistoryInboxType) },
            { 0x2f2f21bf, typeof(UpdateReadHistoryOutboxType) },
            { 0x7f891213, typeof(UpdateWebPageType) },
            { 0x68c13933, typeof(UpdateReadMessagesContentsType) },
            { 0x60946422, typeof(UpdateChannelTooLongType) },
            { 0xb6d45656, typeof(UpdateChannelType) },
            { 0xc36c1e3c, typeof(UpdateChannelGroupType) },
            { 0x62ba04d9, typeof(UpdateNewChannelMessageType) },
            { 0x4214f37f, typeof(UpdateReadChannelInboxType) },
            { 0xc37521c9, typeof(UpdateDeleteChannelMessagesType) },
            { 0x98a12b4b, typeof(UpdateChannelMessageViewsType) },
            { 0x6e947941, typeof(UpdateChatAdminsType) },
            { 0xb6901959, typeof(UpdateChatParticipantAdminType) },
            { 0x688a30aa, typeof(UpdateNewStickerSetType) },
            { 0xf0dfb451, typeof(UpdateStickerSetsOrderType) },
            { 0x43ae3dec, typeof(UpdateStickerSetsType) },
            { 0x9375341e, typeof(UpdateSavedGifsType) },
            { 0xc01eea08, typeof(UpdateBotInlineQueryType) },
            { 0x0f69e113, typeof(UpdateBotInlineSendType) },
            { 0xa56c2a3e, typeof(UpdatesStateType) },
            { 0x5d75a138, typeof(UpdatesDifferenceEmptyType) },
            { 0x00f49ca0, typeof(UpdatesDifferenceType) },
            { 0xa8fb1981, typeof(UpdatesDifferenceSliceType) },
            { 0xe317af7e, typeof(UpdatesTooLongType) },
            { 0x13e4deaa, typeof(UpdateShortMessageType) },
            { 0x248afa62, typeof(UpdateShortChatMessageType) },
            { 0x78d4dec1, typeof(UpdateShortType) },
            { 0x725b04c3, typeof(UpdatesCombinedType) },
            { 0x74ae4240, typeof(UpdatesType) },
            { 0x11f1331c, typeof(UpdateShortSentMessageType) },
            { 0x8dca6aa5, typeof(PhotosPhotosType) },
            { 0x15051f54, typeof(PhotosPhotosSliceType) },
            { 0x20212ca8, typeof(PhotosPhotoType) },
            { 0x096a18d5, typeof(UploadFileType) },
            { 0x05d8c6cc, typeof(DcOptionType) },
            { 0x06bbc5f8, typeof(ConfigType) },
            { 0x8e1a1775, typeof(NearestDcType) },
            { 0x8987f311, typeof(HelpAppUpdateType) },
            { 0xc45a6536, typeof(HelpNoAppUpdateType) },
            { 0x18cb9f78, typeof(HelpInviteTextType) },
            { 0xab7ec0a0, typeof(EncryptedChatEmptyType) },
            { 0x3bf703dc, typeof(EncryptedChatWaitingType) },
            { 0xc878527e, typeof(EncryptedChatRequestedType) },
            { 0xfa56ce36, typeof(EncryptedChatType) },
            { 0x13d6dd27, typeof(EncryptedChatDiscardedType) },
            { 0xf141b5e1, typeof(InputEncryptedChatType) },
            { 0xc21f497e, typeof(EncryptedFileEmptyType) },
            { 0x4a70994c, typeof(EncryptedFileType) },
            { 0x1837c364, typeof(InputEncryptedFileEmptyType) },
            { 0x64bd0306, typeof(InputEncryptedFileUploadedType) },
            { 0x5a17b5e5, typeof(InputEncryptedFileType) },
            { 0x2dc173c8, typeof(InputEncryptedFileBigUploadedType) },
            { 0xed18c118, typeof(EncryptedMessageType) },
            { 0x23734b06, typeof(EncryptedMessageServiceType) },
            { 0xc0e24635, typeof(MessagesDhConfigNotModifiedType) },
            { 0x2c221edd, typeof(MessagesDhConfigType) },
            { 0x560f8935, typeof(MessagesSentEncryptedMessageType) },
            { 0x9493ff32, typeof(MessagesSentEncryptedFileType) },
            { 0x72f0eaae, typeof(InputDocumentEmptyType) },
            { 0x18798952, typeof(InputDocumentType) },
            { 0x36f8c871, typeof(DocumentEmptyType) },
            { 0xf9a39f4f, typeof(DocumentType) },
            { 0x17c6b5f6, typeof(HelpSupportType) },
            { 0x9fd40bd8, typeof(NotifyPeerType) },
            { 0xb4c83b4c, typeof(NotifyUsersType) },
            { 0xc007cec3, typeof(NotifyChatsType) },
            { 0x74d07c60, typeof(NotifyAllType) },
            { 0x16bf744e, typeof(SendMessageTypingActionType) },
            { 0xfd5ec8f5, typeof(SendMessageCancelActionType) },
            { 0xa187d66f, typeof(SendMessageRecordVideoActionType) },
            { 0xe9763aec, typeof(SendMessageUploadVideoActionType) },
            { 0xd52f73f7, typeof(SendMessageRecordAudioActionType) },
            { 0xf351d7ab, typeof(SendMessageUploadAudioActionType) },
            { 0xd1d34a26, typeof(SendMessageUploadPhotoActionType) },
            { 0xaa0cd9e4, typeof(SendMessageUploadDocumentActionType) },
            { 0x176f8ba1, typeof(SendMessageGeoLocationActionType) },
            { 0x628cbc6f, typeof(SendMessageChooseContactActionType) },
            { 0x1aa1f784, typeof(ContactsFoundType) },
            { 0x4f96cb18, typeof(InputPrivacyKeyStatusTimestampType) },
            { 0xbdfb0426, typeof(InputPrivacyKeyChatInviteType) },
            { 0xbc2eab30, typeof(PrivacyKeyStatusTimestampType) },
            { 0x500e6dfa, typeof(PrivacyKeyChatInviteType) },
            { 0x0d09e07b, typeof(InputPrivacyValueAllowContactsType) },
            { 0x184b35ce, typeof(InputPrivacyValueAllowAllType) },
            { 0x131cc67f, typeof(InputPrivacyValueAllowUsersType) },
            { 0x0ba52007, typeof(InputPrivacyValueDisallowContactsType) },
            { 0xd66b66c9, typeof(InputPrivacyValueDisallowAllType) },
            { 0x90110467, typeof(InputPrivacyValueDisallowUsersType) },
            { 0xfffe1bac, typeof(PrivacyValueAllowContactsType) },
            { 0x65427b82, typeof(PrivacyValueAllowAllType) },
            { 0x4d5bbe0c, typeof(PrivacyValueAllowUsersType) },
            { 0xf888fa1a, typeof(PrivacyValueDisallowContactsType) },
            { 0x8b73e763, typeof(PrivacyValueDisallowAllType) },
            { 0x0c7f49b7, typeof(PrivacyValueDisallowUsersType) },
            { 0x554abb6f, typeof(AccountPrivacyRulesType) },
            { 0xb8d0afdf, typeof(AccountDaysTTLType) },
            { 0xa4f58c4c, typeof(AccountSentChangePhoneCodeType) },
            { 0x6c37c15c, typeof(DocumentAttributeImageSizeType) },
            { 0x11b58939, typeof(DocumentAttributeAnimatedType) },
            { 0x3a556302, typeof(DocumentAttributeStickerType) },
            { 0x5910cccb, typeof(DocumentAttributeVideoType) },
            { 0x9852f9c6, typeof(DocumentAttributeAudioType) },
            { 0x15590068, typeof(DocumentAttributeFilenameType) },
            { 0xf1749a22, typeof(MessagesStickersNotModifiedType) },
            { 0x8a8ecd32, typeof(MessagesStickersType) },
            { 0x12b299d4, typeof(StickerPackType) },
            { 0xe86602c3, typeof(MessagesAllStickersNotModifiedType) },
            { 0xedfd405f, typeof(MessagesAllStickersType) },
            { 0xae636f24, typeof(DisabledFeatureType) },
            { 0x84d19185, typeof(MessagesAffectedMessagesType) },
            { 0x5f4f9247, typeof(ContactLinkUnknownType) },
            { 0xfeedd3ad, typeof(ContactLinkNoneType) },
            { 0x268f3f59, typeof(ContactLinkHasPhoneType) },
            { 0xd502c2d0, typeof(ContactLinkContactType) },
            { 0xeb1477e8, typeof(WebPageEmptyType) },
            { 0xc586da1c, typeof(WebPagePendingType) },
            { 0xca820ed7, typeof(WebPageType) },
            { 0x7bf2e6f6, typeof(AuthorizationType) },
            { 0x1250abde, typeof(AccountAuthorizationsType) },
            { 0x96dabc18, typeof(AccountNoPasswordType) },
            { 0x7c18141c, typeof(AccountPasswordType) },
            { 0xb7b72ab3, typeof(AccountPasswordSettingsType) },
            { 0x86916deb, typeof(AccountPasswordInputSettingsType) },
            { 0x137948a5, typeof(AuthPasswordRecoveryType) },
            { 0xa384b779, typeof(ReceivedNotifyMessageType) },
            { 0x69df3769, typeof(ChatInviteEmptyType) },
            { 0xfc2e05bc, typeof(ChatInviteExportedType) },
            { 0x5a686d7c, typeof(ChatInviteAlreadyType) },
            { 0x93e99b60, typeof(ChatInviteType) },
            { 0xffb62b95, typeof(InputStickerSetEmptyType) },
            { 0x9de7a269, typeof(InputStickerSetIDType) },
            { 0x861cc8a0, typeof(InputStickerSetShortNameType) },
            { 0xcd303b41, typeof(StickerSetType) },
            { 0xb60a24a6, typeof(MessagesStickerSetType) },
            { 0xc27ac8c7, typeof(BotCommandType) },
            { 0xbb2e37ce, typeof(BotInfoEmptyType) },
            { 0x09cf585d, typeof(BotInfoType) },
            { 0xa2fa4880, typeof(KeyboardButtonType) },
            { 0x77608b83, typeof(KeyboardButtonRowType) },
            { 0xa03e5b85, typeof(ReplyKeyboardHideType) },
            { 0xf4108aa0, typeof(ReplyKeyboardForceReplyType) },
            { 0x3502758c, typeof(ReplyKeyboardMarkupType) },
            { 0xaf7e0394, typeof(HelpAppChangelogEmptyType) },
            { 0x4668e6bd, typeof(HelpAppChangelogType) },
            { 0xbb92ba95, typeof(MessageEntityUnknownType) },
            { 0xfa04579d, typeof(MessageEntityMentionType) },
            { 0x6f635b0d, typeof(MessageEntityHashtagType) },
            { 0x6cef8ac7, typeof(MessageEntityBotCommandType) },
            { 0x6ed02538, typeof(MessageEntityUrlType) },
            { 0x64e475c2, typeof(MessageEntityEmailType) },
            { 0xbd610bc9, typeof(MessageEntityBoldType) },
            { 0x826f8b60, typeof(MessageEntityItalicType) },
            { 0x28a20571, typeof(MessageEntityCodeType) },
            { 0x73924be0, typeof(MessageEntityPreType) },
            { 0x76a6d327, typeof(MessageEntityTextUrlType) },
            { 0xee8c1e86, typeof(InputChannelEmptyType) },
            { 0xafeb712e, typeof(InputChannelType) },
            { 0x7f077ad9, typeof(ContactsResolvedPeerType) },
            { 0x0ae30253, typeof(MessageRangeType) },
            { 0xe8346f53, typeof(MessageGroupType) },
            { 0x3e11affb, typeof(UpdatesChannelDifferenceEmptyType) },
            { 0x5e167646, typeof(UpdatesChannelDifferenceTooLongType) },
            { 0x2064674e, typeof(UpdatesChannelDifferenceType) },
            { 0x94d42ee7, typeof(ChannelMessagesFilterEmptyType) },
            { 0xcd77d957, typeof(ChannelMessagesFilterType) },
            { 0xfa01232e, typeof(ChannelMessagesFilterCollapsedType) },
            { 0x15ebac1d, typeof(ChannelParticipantType) },
            { 0xa3289a6d, typeof(ChannelParticipantSelfType) },
            { 0x91057fef, typeof(ChannelParticipantModeratorType) },
            { 0x98192d61, typeof(ChannelParticipantEditorType) },
            { 0x8cc5e69a, typeof(ChannelParticipantKickedType) },
            { 0xe3e2e1f9, typeof(ChannelParticipantCreatorType) },
            { 0xde3f3c79, typeof(ChannelParticipantsRecentType) },
            { 0xb4608969, typeof(ChannelParticipantsAdminsType) },
            { 0x3c37bb7a, typeof(ChannelParticipantsKickedType) },
            { 0xb0d1865b, typeof(ChannelParticipantsBotsType) },
            { 0xb285a0c6, typeof(ChannelRoleEmptyType) },
            { 0x9618d975, typeof(ChannelRoleModeratorType) },
            { 0x820bfe8c, typeof(ChannelRoleEditorType) },
            { 0xf56ee2a8, typeof(ChannelsChannelParticipantsType) },
            { 0xd0d9b163, typeof(ChannelsChannelParticipantType) },
            { 0xf1ee3e90, typeof(HelpTermsOfServiceType) },
            { 0x162ecc1f, typeof(FoundGifType) },
            { 0x9c750409, typeof(FoundGifCachedType) },
            { 0x450a1c0a, typeof(MessagesFoundGifsType) },
            { 0xe8025ca2, typeof(MessagesSavedGifsNotModifiedType) },
            { 0x2e0709a5, typeof(MessagesSavedGifsType) },
            { 0x2e43e587, typeof(InputBotInlineMessageMediaAutoType) },
            { 0xadf0df71, typeof(InputBotInlineMessageTextType) },
            { 0x2cbbe15a, typeof(InputBotInlineResultType) },
            { 0xfc56e87d, typeof(BotInlineMessageMediaAutoType) },
            { 0xa56197a9, typeof(BotInlineMessageTextType) },
            { 0xf897d33e, typeof(BotInlineMediaResultDocumentType) },
            { 0xc5528587, typeof(BotInlineMediaResultPhotoType) },
            { 0x9bebaeb9, typeof(BotInlineResultType) },
            { 0x1170b0a3, typeof(MessagesBotResultsType) }
        };

        #endregion

        #region Functions (requests)

        public class MsgsAckRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x62d6b459;

            public List<long> MsgIds;

            public MsgsAck Result;

            public MsgsAckRequest() { }

            public MsgsAckRequest(List<long> MsgIds)
            {
                this.MsgIds = MsgIds;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(MsgIds.Count);
                foreach (long MsgIdsElement in MsgIds)
                    writer.Write(MsgIdsElement);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MsgsAck>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MsgsAckRequest MsgIds:{0})", MsgIds);
            }
        }

        public class BadMsgNotificationRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xa7eff811;

            public long BadMsgId;
            public int BadMsgSeqno;
            public int ErrorCode;

            public BadMsgNotification Result;

            public BadMsgNotificationRequest() { }

            public BadMsgNotificationRequest(long BadMsgId, int BadMsgSeqno, int ErrorCode)
            {
                this.BadMsgId = BadMsgId;
                this.BadMsgSeqno = BadMsgSeqno;
                this.ErrorCode = ErrorCode;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(BadMsgId);
                writer.Write(BadMsgSeqno);
                writer.Write(ErrorCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<BadMsgNotification>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(BadMsgNotificationRequest BadMsgId:{0} BadMsgSeqno:{1} ErrorCode:{2})", BadMsgId, BadMsgSeqno, ErrorCode);
            }
        }

        public class RpcDropAnswerRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x58e4a740;

            public long ReqMsgId;

            public RpcDropAnswer Result;

            public RpcDropAnswerRequest() { }

            public RpcDropAnswerRequest(long ReqMsgId)
            {
                this.ReqMsgId = ReqMsgId;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ReqMsgId);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<RpcDropAnswer>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(RpcDropAnswerRequest ReqMsgId:{0})", ReqMsgId);
            }
        }

        public class GetFutureSaltsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xb921bd04;

            public int Num;

            public FutureSalts Result;

            public GetFutureSaltsRequest() { }

            public GetFutureSaltsRequest(int Num)
            {
                this.Num = Num;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Num);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<FutureSalts>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(GetFutureSaltsRequest Num:{0})", Num);
            }
        }

        public class PingRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x7abe77ec;

            public long PingId;

            public Pong Result;

            public PingRequest() { }

            public PingRequest(long PingId)
            {
                this.PingId = PingId;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PingId);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Pong>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(PingRequest PingId:{0})", PingId);
            }
        }

        public class PingDelayDisconnectRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xf3427b8c;

            public long PingId;
            public int DisconnectDelay;

            public Pong Result;

            public PingDelayDisconnectRequest() { }

            public PingDelayDisconnectRequest(long PingId, int DisconnectDelay)
            {
                this.PingId = PingId;
                this.DisconnectDelay = DisconnectDelay;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PingId);
                writer.Write(DisconnectDelay);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Pong>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(PingDelayDisconnectRequest PingId:{0} DisconnectDelay:{1})", PingId, DisconnectDelay);
            }
        }

        public class DestroySessionRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xe7512126;

            public long SessionId;

            public DestroySessionRes Result;

            public DestroySessionRequest() { }

            public DestroySessionRequest(long SessionId)
            {
                this.SessionId = SessionId;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(SessionId);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<DestroySessionRes>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(DestroySessionRequest SessionId:{0})", SessionId);
            }
        }

        public class RegisterSaveDeveloperInfoRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x9a5f6e95;

            public string Name;
            public string Email;
            public string PhoneNumber;
            public int Age;
            public string City;

            public bool Result;

            public RegisterSaveDeveloperInfoRequest() { }

            public RegisterSaveDeveloperInfoRequest(string Name, string Email, string PhoneNumber, int Age, string City)
            {
                this.Name = Name;
                this.Email = Email;
                this.PhoneNumber = PhoneNumber;
                this.Age = Age;
                this.City = City;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Name);
                writer.Write(Email);
                writer.Write(PhoneNumber);
                writer.Write(Age);
                writer.Write(City);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(RegisterSaveDeveloperInfoRequest Name:{0} Email:{1} PhoneNumber:{2} Age:{3} City:{4})", Name, Email, PhoneNumber, Age, City);
            }
        }

        public class InvokeAfterMsgRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xcb9f372d;

            public long MsgId;
            public MTProtoRequest Query;

            public TLObject Result;

            public InvokeAfterMsgRequest() { }

            public InvokeAfterMsgRequest(long MsgId, MTProtoRequest Query)
            {
                this.MsgId = MsgId;
                this.Query = Query;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(MsgId);
                Query.OnSend(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<TLObject>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(InvokeAfterMsgRequest MsgId:{0} Query:{1})", MsgId, Query);
            }
        }

        public class InvokeAfterMsgsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x3dc4b4f0;

            public List<long> MsgIds;
            public MTProtoRequest Query;

            public TLObject Result;

            public InvokeAfterMsgsRequest() { }

            public InvokeAfterMsgsRequest(List<long> MsgIds, MTProtoRequest Query)
            {
                this.MsgIds = MsgIds;
                this.Query = Query;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(MsgIds.Count);
                foreach (long MsgIdsElement in MsgIds)
                    writer.Write(MsgIdsElement);
                Query.OnSend(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<TLObject>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(InvokeAfterMsgsRequest MsgIds:{0} Query:{1})", MsgIds, Query);
            }
        }

        public class InitConnectionRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x69796de9;

            public int ApiId;
            public string DeviceModel;
            public string SystemVersion;
            public string AppVersion;
            public string LangCode;
            public MTProtoRequest Query;

            public TLObject Result;

            public InitConnectionRequest() { }

            public InitConnectionRequest(int ApiId, string DeviceModel, string SystemVersion, string AppVersion, string LangCode, MTProtoRequest Query)
            {
                this.ApiId = ApiId;
                this.DeviceModel = DeviceModel;
                this.SystemVersion = SystemVersion;
                this.AppVersion = AppVersion;
                this.LangCode = LangCode;
                this.Query = Query;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ApiId);
                writer.Write(DeviceModel);
                writer.Write(SystemVersion);
                writer.Write(AppVersion);
                writer.Write(LangCode);
                Query.OnSend(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<TLObject>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(InitConnectionRequest ApiId:{0} DeviceModel:{1} SystemVersion:{2} AppVersion:{3} LangCode:{4} Query:{5})", ApiId, DeviceModel, SystemVersion, AppVersion, LangCode, Query);
            }
        }

        public class InvokeWithLayerRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xda9b0d0d;

            public int Layer;
            public MTProtoRequest Query;

            public TLObject Result;

            public InvokeWithLayerRequest() { }

            public InvokeWithLayerRequest(int Layer, MTProtoRequest Query)
            {
                this.Layer = Layer;
                this.Query = Query;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Layer);
                Query.OnSend(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<TLObject>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(InvokeWithLayerRequest Layer:{0} Query:{1})", Layer, Query);
            }
        }

        public class InvokeWithoutUpdatesRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xbf9459b7;

            public MTProtoRequest Query;

            public TLObject Result;

            public InvokeWithoutUpdatesRequest() { }

            public InvokeWithoutUpdatesRequest(MTProtoRequest Query)
            {
                this.Query = Query;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Query.OnSend(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<TLObject>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(InvokeWithoutUpdatesRequest Query:{0})", Query);
            }
        }

        public class AuthCheckPhoneRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x6fe51dfb;

            public string PhoneNumber;

            public AuthCheckedPhone Result;

            public AuthCheckPhoneRequest() { }

            public AuthCheckPhoneRequest(string PhoneNumber)
            {
                this.PhoneNumber = PhoneNumber;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PhoneNumber);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<AuthCheckedPhone>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AuthCheckPhoneRequest PhoneNumber:{0})", PhoneNumber);
            }
        }

        public class AuthSendCodeRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x768d5f4d;

            public string PhoneNumber;
            public int SmsType;
            public int ApiId;
            public string ApiHash;
            public string LangCode;

            public AuthSentCode Result;

            public AuthSendCodeRequest() { }

            public AuthSendCodeRequest(string PhoneNumber, int SmsType, int ApiId, string ApiHash, string LangCode)
            {
                this.PhoneNumber = PhoneNumber;
                this.SmsType = SmsType;
                this.ApiId = ApiId;
                this.ApiHash = ApiHash;
                this.LangCode = LangCode;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PhoneNumber);
                writer.Write(SmsType);
                writer.Write(ApiId);
                writer.Write(ApiHash);
                writer.Write(LangCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<AuthSentCode>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AuthSendCodeRequest PhoneNumber:{0} SmsType:{1} ApiId:{2} ApiHash:{3} LangCode:{4})", PhoneNumber, SmsType, ApiId, ApiHash, LangCode);
            }
        }

        public class AuthSendCallRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x03c51564;

            public string PhoneNumber;
            public string PhoneCodeHash;

            public bool Result;

            public AuthSendCallRequest() { }

            public AuthSendCallRequest(string PhoneNumber, string PhoneCodeHash)
            {
                this.PhoneNumber = PhoneNumber;
                this.PhoneCodeHash = PhoneCodeHash;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PhoneNumber);
                writer.Write(PhoneCodeHash);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AuthSendCallRequest PhoneNumber:{0} PhoneCodeHash:{1})", PhoneNumber, PhoneCodeHash);
            }
        }

        public class AuthSignUpRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x1b067634;

            public string PhoneNumber;
            public string PhoneCodeHash;
            public string PhoneCode;
            public string FirstName;
            public string LastName;

            public AuthAuthorization Result;

            public AuthSignUpRequest() { }

            public AuthSignUpRequest(string PhoneNumber, string PhoneCodeHash, string PhoneCode, string FirstName, string LastName)
            {
                this.PhoneNumber = PhoneNumber;
                this.PhoneCodeHash = PhoneCodeHash;
                this.PhoneCode = PhoneCode;
                this.FirstName = FirstName;
                this.LastName = LastName;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PhoneNumber);
                writer.Write(PhoneCodeHash);
                writer.Write(PhoneCode);
                writer.Write(FirstName);
                writer.Write(LastName);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<AuthAuthorization>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AuthSignUpRequest PhoneNumber:{0} PhoneCodeHash:{1} PhoneCode:{2} FirstName:{3} LastName:{4})", PhoneNumber, PhoneCodeHash, PhoneCode, FirstName, LastName);
            }
        }

        public class AuthSignInRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xbcd51581;

            public string PhoneNumber;
            public string PhoneCodeHash;
            public string PhoneCode;

            public AuthAuthorization Result;

            public AuthSignInRequest() { }

            public AuthSignInRequest(string PhoneNumber, string PhoneCodeHash, string PhoneCode)
            {
                this.PhoneNumber = PhoneNumber;
                this.PhoneCodeHash = PhoneCodeHash;
                this.PhoneCode = PhoneCode;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PhoneNumber);
                writer.Write(PhoneCodeHash);
                writer.Write(PhoneCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<AuthAuthorization>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AuthSignInRequest PhoneNumber:{0} PhoneCodeHash:{1} PhoneCode:{2})", PhoneNumber, PhoneCodeHash, PhoneCode);
            }
        }

        public class AuthLogOutRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x5717da40;

            public bool Result;

            public AuthLogOutRequest() { }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return "(AuthLogOutRequest)";
            }
        }

        public class AuthResetAuthorizationsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x9fab0d1a;

            public bool Result;

            public AuthResetAuthorizationsRequest() { }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return "(AuthResetAuthorizationsRequest)";
            }
        }

        public class AuthSendInvitesRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x771c1d97;

            public List<string> PhoneNumbers;
            public string Message;

            public bool Result;

            public AuthSendInvitesRequest() { }

            public AuthSendInvitesRequest(List<string> PhoneNumbers, string Message)
            {
                this.PhoneNumbers = PhoneNumbers;
                this.Message = Message;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(PhoneNumbers.Count);
                foreach (string PhoneNumbersElement in PhoneNumbers)
                    writer.Write(PhoneNumbersElement);
                writer.Write(Message);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AuthSendInvitesRequest PhoneNumbers:{0} Message:{1})", PhoneNumbers, Message);
            }
        }

        public class AuthExportAuthorizationRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xe5bfffcd;

            public int DcId;

            public AuthExportedAuthorization Result;

            public AuthExportAuthorizationRequest() { }

            public AuthExportAuthorizationRequest(int DcId)
            {
                this.DcId = DcId;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(DcId);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<AuthExportedAuthorization>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AuthExportAuthorizationRequest DcId:{0})", DcId);
            }
        }

        public class AuthImportAuthorizationRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xe3ef9613;

            public int Id;
            public byte[] Bytes;

            public AuthAuthorization Result;

            public AuthImportAuthorizationRequest() { }

            public AuthImportAuthorizationRequest(int Id, byte[] Bytes)
            {
                this.Id = Id;
                this.Bytes = Bytes;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(Bytes);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<AuthAuthorization>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AuthImportAuthorizationRequest Id:{0} Bytes:{1})", Id, Bytes);
            }
        }

        public class AuthBindTempAuthKeyRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xcdd42a05;

            public long PermAuthKeyId;
            public long Nonce;
            public int ExpiresAt;
            public byte[] EncryptedMessage;

            public bool Result;

            public AuthBindTempAuthKeyRequest() { }

            public AuthBindTempAuthKeyRequest(long PermAuthKeyId, long Nonce, int ExpiresAt, byte[] EncryptedMessage)
            {
                this.PermAuthKeyId = PermAuthKeyId;
                this.Nonce = Nonce;
                this.ExpiresAt = ExpiresAt;
                this.EncryptedMessage = EncryptedMessage;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PermAuthKeyId);
                writer.Write(Nonce);
                writer.Write(ExpiresAt);
                writer.Write(EncryptedMessage);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AuthBindTempAuthKeyRequest PermAuthKeyId:{0} Nonce:{1} ExpiresAt:{2} EncryptedMessage:{3})", PermAuthKeyId, Nonce, ExpiresAt, EncryptedMessage);
            }
        }

        public class AuthSendSmsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x0da9f3e8;

            public string PhoneNumber;
            public string PhoneCodeHash;

            public bool Result;

            public AuthSendSmsRequest() { }

            public AuthSendSmsRequest(string PhoneNumber, string PhoneCodeHash)
            {
                this.PhoneNumber = PhoneNumber;
                this.PhoneCodeHash = PhoneCodeHash;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PhoneNumber);
                writer.Write(PhoneCodeHash);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AuthSendSmsRequest PhoneNumber:{0} PhoneCodeHash:{1})", PhoneNumber, PhoneCodeHash);
            }
        }

        public class AuthImportBotAuthorizationRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x67a3ff2c;

            public int Flags;
            public int ApiId;
            public string ApiHash;
            public string BotAuthToken;

            public AuthAuthorization Result;

            public AuthImportBotAuthorizationRequest() { }

            public AuthImportBotAuthorizationRequest(int Flags, int ApiId, string ApiHash, string BotAuthToken)
            {
                this.Flags = Flags;
                this.ApiId = ApiId;
                this.ApiHash = ApiHash;
                this.BotAuthToken = BotAuthToken;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Flags);
                writer.Write(ApiId);
                writer.Write(ApiHash);
                writer.Write(BotAuthToken);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<AuthAuthorization>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AuthImportBotAuthorizationRequest Flags:{0} ApiId:{1} ApiHash:{2} BotAuthToken:{3})", Flags, ApiId, ApiHash, BotAuthToken);
            }
        }

        public class AuthCheckPasswordRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x0a63011e;

            public byte[] PasswordHash;

            public AuthAuthorization Result;

            public AuthCheckPasswordRequest() { }

            public AuthCheckPasswordRequest(byte[] PasswordHash)
            {
                this.PasswordHash = PasswordHash;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PasswordHash);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<AuthAuthorization>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AuthCheckPasswordRequest PasswordHash:{0})", PasswordHash);
            }
        }

        public class AuthRequestPasswordRecoveryRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xd897bc66;

            public AuthPasswordRecovery Result;

            public AuthRequestPasswordRecoveryRequest() { }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<AuthPasswordRecovery>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return "(AuthRequestPasswordRecoveryRequest)";
            }
        }

        public class AuthRecoverPasswordRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x4ea56e92;

            public string Code;

            public AuthAuthorization Result;

            public AuthRecoverPasswordRequest() { }

            public AuthRecoverPasswordRequest(string Code)
            {
                this.Code = Code;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Code);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<AuthAuthorization>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AuthRecoverPasswordRequest Code:{0})", Code);
            }
        }

        public class AccountRegisterDeviceRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x446c712c;

            public int TokenType;
            public string Token;
            public string DeviceModel;
            public string SystemVersion;
            public string AppVersion;
            public bool AppSandbox;
            public string LangCode;

            public bool Result;

            public AccountRegisterDeviceRequest() { }

            public AccountRegisterDeviceRequest(int TokenType, string Token, string DeviceModel, string SystemVersion, string AppVersion, bool AppSandbox, string LangCode)
            {
                this.TokenType = TokenType;
                this.Token = Token;
                this.DeviceModel = DeviceModel;
                this.SystemVersion = SystemVersion;
                this.AppVersion = AppVersion;
                this.AppSandbox = AppSandbox;
                this.LangCode = LangCode;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(TokenType);
                writer.Write(Token);
                writer.Write(DeviceModel);
                writer.Write(SystemVersion);
                writer.Write(AppVersion);
                writer.Write(AppSandbox);
                writer.Write(LangCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountRegisterDeviceRequest TokenType:{0} Token:{1} DeviceModel:{2} SystemVersion:{3} AppVersion:{4} AppSandbox:{5} LangCode:{6})", TokenType, Token, DeviceModel, SystemVersion, AppVersion, AppSandbox, LangCode);
            }
        }

        public class AccountUnregisterDeviceRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x65c55b40;

            public int TokenType;
            public string Token;

            public bool Result;

            public AccountUnregisterDeviceRequest() { }

            public AccountUnregisterDeviceRequest(int TokenType, string Token)
            {
                this.TokenType = TokenType;
                this.Token = Token;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(TokenType);
                writer.Write(Token);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountUnregisterDeviceRequest TokenType:{0} Token:{1})", TokenType, Token);
            }
        }

        public class AccountUpdateNotifySettingsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x84be5b93;

            public InputNotifyPeer Peer;
            public InputPeerNotifySettings Settings;

            public bool Result;

            public AccountUpdateNotifySettingsRequest() { }

            public AccountUpdateNotifySettingsRequest(InputNotifyPeer Peer, InputPeerNotifySettings Settings)
            {
                this.Peer = Peer;
                this.Settings = Settings;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                Settings.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountUpdateNotifySettingsRequest Peer:{0} Settings:{1})", Peer, Settings);
            }
        }

        public class AccountGetNotifySettingsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x12b3ad31;

            public InputNotifyPeer Peer;

            public PeerNotifySettings Result;

            public AccountGetNotifySettingsRequest() { }

            public AccountGetNotifySettingsRequest(InputNotifyPeer Peer)
            {
                this.Peer = Peer;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<PeerNotifySettings>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountGetNotifySettingsRequest Peer:{0})", Peer);
            }
        }

        public class AccountResetNotifySettingsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xdb7e1747;

            public bool Result;

            public AccountResetNotifySettingsRequest() { }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return "(AccountResetNotifySettingsRequest)";
            }
        }

        public class AccountUpdateProfileRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xf0888d68;

            public string FirstName;
            public string LastName;

            public User Result;

            public AccountUpdateProfileRequest() { }

            public AccountUpdateProfileRequest(string FirstName, string LastName)
            {
                this.FirstName = FirstName;
                this.LastName = LastName;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(FirstName);
                writer.Write(LastName);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<User>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountUpdateProfileRequest FirstName:{0} LastName:{1})", FirstName, LastName);
            }
        }

        public class AccountUpdateStatusRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x6628562c;

            public bool Offline;

            public bool Result;

            public AccountUpdateStatusRequest() { }

            public AccountUpdateStatusRequest(bool Offline)
            {
                this.Offline = Offline;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Offline);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountUpdateStatusRequest Offline:{0})", Offline);
            }
        }

        public class AccountGetWallPapersRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xc04cfac2;

            public List<WallPaper> Result;

            public AccountGetWallPapersRequest() { }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<List<WallPaper>>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return "(AccountGetWallPapersRequest)";
            }
        }

        public class AccountReportPeerRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xae189d5f;

            public InputPeer Peer;
            public ReportReason Reason;

            public bool Result;

            public AccountReportPeerRequest() { }

            public AccountReportPeerRequest(InputPeer Peer, ReportReason Reason)
            {
                this.Peer = Peer;
                this.Reason = Reason;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                Reason.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountReportPeerRequest Peer:{0} Reason:{1})", Peer, Reason);
            }
        }

        public class AccountCheckUsernameRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x2714d86c;

            public string Username;

            public bool Result;

            public AccountCheckUsernameRequest() { }

            public AccountCheckUsernameRequest(string Username)
            {
                this.Username = Username;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Username);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountCheckUsernameRequest Username:{0})", Username);
            }
        }

        public class AccountUpdateUsernameRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x3e0bdd7c;

            public string Username;

            public User Result;

            public AccountUpdateUsernameRequest() { }

            public AccountUpdateUsernameRequest(string Username)
            {
                this.Username = Username;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Username);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<User>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountUpdateUsernameRequest Username:{0})", Username);
            }
        }

        public class AccountGetPrivacyRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xdadbc950;

            public InputPrivacyKey Key;

            public AccountPrivacyRules Result;

            public AccountGetPrivacyRequest() { }

            public AccountGetPrivacyRequest(InputPrivacyKey Key)
            {
                this.Key = Key;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Key.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<AccountPrivacyRules>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountGetPrivacyRequest Key:{0})", Key);
            }
        }

        public class AccountSetPrivacyRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xc9f81ce8;

            public InputPrivacyKey Key;
            public List<InputPrivacyRule> Rules;

            public AccountPrivacyRules Result;

            public AccountSetPrivacyRequest() { }

            public AccountSetPrivacyRequest(InputPrivacyKey Key, List<InputPrivacyRule> Rules)
            {
                this.Key = Key;
                this.Rules = Rules;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Key.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Rules.Count);
                foreach (InputPrivacyRule RulesElement in Rules)
                    RulesElement.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<AccountPrivacyRules>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountSetPrivacyRequest Key:{0} Rules:{1})", Key, Rules);
            }
        }

        public class AccountDeleteAccountRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x418d4e0b;

            public string Reason;

            public bool Result;

            public AccountDeleteAccountRequest() { }

            public AccountDeleteAccountRequest(string Reason)
            {
                this.Reason = Reason;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Reason);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountDeleteAccountRequest Reason:{0})", Reason);
            }
        }

        public class AccountGetAccountTTLRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x08fc711d;

            public AccountDaysTTL Result;

            public AccountGetAccountTTLRequest() { }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<AccountDaysTTL>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return "(AccountGetAccountTTLRequest)";
            }
        }

        public class AccountSetAccountTTLRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x2442485e;

            public AccountDaysTTL Ttl;

            public bool Result;

            public AccountSetAccountTTLRequest() { }

            public AccountSetAccountTTLRequest(AccountDaysTTL Ttl)
            {
                this.Ttl = Ttl;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Ttl.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountSetAccountTTLRequest Ttl:{0})", Ttl);
            }
        }

        public class AccountSendChangePhoneCodeRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xa407a8f4;

            public string PhoneNumber;

            public AccountSentChangePhoneCode Result;

            public AccountSendChangePhoneCodeRequest() { }

            public AccountSendChangePhoneCodeRequest(string PhoneNumber)
            {
                this.PhoneNumber = PhoneNumber;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PhoneNumber);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<AccountSentChangePhoneCode>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountSendChangePhoneCodeRequest PhoneNumber:{0})", PhoneNumber);
            }
        }

        public class AccountChangePhoneRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x70c32edb;

            public string PhoneNumber;
            public string PhoneCodeHash;
            public string PhoneCode;

            public User Result;

            public AccountChangePhoneRequest() { }

            public AccountChangePhoneRequest(string PhoneNumber, string PhoneCodeHash, string PhoneCode)
            {
                this.PhoneNumber = PhoneNumber;
                this.PhoneCodeHash = PhoneCodeHash;
                this.PhoneCode = PhoneCode;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PhoneNumber);
                writer.Write(PhoneCodeHash);
                writer.Write(PhoneCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<User>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountChangePhoneRequest PhoneNumber:{0} PhoneCodeHash:{1} PhoneCode:{2})", PhoneNumber, PhoneCodeHash, PhoneCode);
            }
        }

        public class AccountUpdateDeviceLockedRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x38df3532;

            public int Period;

            public bool Result;

            public AccountUpdateDeviceLockedRequest() { }

            public AccountUpdateDeviceLockedRequest(int Period)
            {
                this.Period = Period;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Period);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountUpdateDeviceLockedRequest Period:{0})", Period);
            }
        }

        public class AccountGetAuthorizationsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xe320c158;

            public AccountAuthorizations Result;

            public AccountGetAuthorizationsRequest() { }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<AccountAuthorizations>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return "(AccountGetAuthorizationsRequest)";
            }
        }

        public class AccountResetAuthorizationRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xdf77f3bc;

            public long Hash;

            public bool Result;

            public AccountResetAuthorizationRequest() { }

            public AccountResetAuthorizationRequest(long Hash)
            {
                this.Hash = Hash;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Hash);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountResetAuthorizationRequest Hash:{0})", Hash);
            }
        }

        public class AccountGetPasswordRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x548a30f5;

            public AccountPassword Result;

            public AccountGetPasswordRequest() { }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<AccountPassword>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return "(AccountGetPasswordRequest)";
            }
        }

        public class AccountGetPasswordSettingsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xbc8d11bb;

            public byte[] CurrentPasswordHash;

            public AccountPasswordSettings Result;

            public AccountGetPasswordSettingsRequest() { }

            public AccountGetPasswordSettingsRequest(byte[] CurrentPasswordHash)
            {
                this.CurrentPasswordHash = CurrentPasswordHash;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(CurrentPasswordHash);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<AccountPasswordSettings>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountGetPasswordSettingsRequest CurrentPasswordHash:{0})", CurrentPasswordHash);
            }
        }

        public class AccountUpdatePasswordSettingsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xfa7c4b86;

            public byte[] CurrentPasswordHash;
            public AccountPasswordInputSettings NewSettings;

            public bool Result;

            public AccountUpdatePasswordSettingsRequest() { }

            public AccountUpdatePasswordSettingsRequest(byte[] CurrentPasswordHash, AccountPasswordInputSettings NewSettings)
            {
                this.CurrentPasswordHash = CurrentPasswordHash;
                this.NewSettings = NewSettings;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(CurrentPasswordHash);
                NewSettings.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(AccountUpdatePasswordSettingsRequest CurrentPasswordHash:{0} NewSettings:{1})", CurrentPasswordHash, NewSettings);
            }
        }

        public class UsersGetUsersRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x0d91a548;

            public List<InputUser> Id;

            public List<User> Result;

            public UsersGetUsersRequest() { }

            public UsersGetUsersRequest(List<InputUser> Id)
            {
                this.Id = Id;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Id.Count);
                foreach (InputUser IdElement in Id)
                    IdElement.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<List<User>>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(UsersGetUsersRequest Id:{0})", Id);
            }
        }

        public class UsersGetFullUserRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xca30a5b1;

            public InputUser Id;

            public UserFull Result;

            public UsersGetFullUserRequest() { }

            public UsersGetFullUserRequest(InputUser Id)
            {
                this.Id = Id;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Id.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<UserFull>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(UsersGetFullUserRequest Id:{0})", Id);
            }
        }

        public class ContactsGetStatusesRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xc4a353ee;

            public List<ContactStatus> Result;

            public ContactsGetStatusesRequest() { }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<List<ContactStatus>>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return "(ContactsGetStatusesRequest)";
            }
        }

        public class ContactsGetContactsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x22c6aa08;

            public string Hash;

            public ContactsContacts Result;

            public ContactsGetContactsRequest() { }

            public ContactsGetContactsRequest(string Hash)
            {
                this.Hash = Hash;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Hash);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<ContactsContacts>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ContactsGetContactsRequest Hash:{0})", Hash);
            }
        }

        public class ContactsImportContactsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xda30b32d;

            public List<InputContact> Contacts;
            public bool Replace;

            public ContactsImportedContacts Result;

            public ContactsImportContactsRequest() { }

            public ContactsImportContactsRequest(List<InputContact> Contacts, bool Replace)
            {
                this.Contacts = Contacts;
                this.Replace = Replace;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Contacts.Count);
                foreach (InputContact ContactsElement in Contacts)
                    ContactsElement.Write(writer);
                writer.Write(Replace);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<ContactsImportedContacts>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ContactsImportContactsRequest Contacts:{0} Replace:{1})", Contacts, Replace);
            }
        }

        public class ContactsDeleteContactRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x8e953744;

            public InputUser Id;

            public ContactsLink Result;

            public ContactsDeleteContactRequest() { }

            public ContactsDeleteContactRequest(InputUser Id)
            {
                this.Id = Id;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Id.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<ContactsLink>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ContactsDeleteContactRequest Id:{0})", Id);
            }
        }

        public class ContactsDeleteContactsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x59ab389e;

            public List<InputUser> Id;

            public bool Result;

            public ContactsDeleteContactsRequest() { }

            public ContactsDeleteContactsRequest(List<InputUser> Id)
            {
                this.Id = Id;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Id.Count);
                foreach (InputUser IdElement in Id)
                    IdElement.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ContactsDeleteContactsRequest Id:{0})", Id);
            }
        }

        public class ContactsBlockRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x332b49fc;

            public InputUser Id;

            public bool Result;

            public ContactsBlockRequest() { }

            public ContactsBlockRequest(InputUser Id)
            {
                this.Id = Id;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Id.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ContactsBlockRequest Id:{0})", Id);
            }
        }

        public class ContactsUnblockRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xe54100bd;

            public InputUser Id;

            public bool Result;

            public ContactsUnblockRequest() { }

            public ContactsUnblockRequest(InputUser Id)
            {
                this.Id = Id;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Id.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ContactsUnblockRequest Id:{0})", Id);
            }
        }

        public class ContactsGetBlockedRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xf57c350f;

            public int Offset;
            public int Limit;

            public ContactsBlocked Result;

            public ContactsGetBlockedRequest() { }

            public ContactsGetBlockedRequest(int Offset, int Limit)
            {
                this.Offset = Offset;
                this.Limit = Limit;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Offset);
                writer.Write(Limit);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<ContactsBlocked>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ContactsGetBlockedRequest Offset:{0} Limit:{1})", Offset, Limit);
            }
        }

        public class ContactsExportCardRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x84e53737;

            public List<int> Result;

            public ContactsExportCardRequest() { }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<List<int>>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return "(ContactsExportCardRequest)";
            }
        }

        public class ContactsImportCardRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x4fe196fe;

            public List<int> ExportCard;

            public User Result;

            public ContactsImportCardRequest() { }

            public ContactsImportCardRequest(List<int> ExportCard)
            {
                this.ExportCard = ExportCard;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(ExportCard.Count);
                foreach (int ExportCardElement in ExportCard)
                    writer.Write(ExportCardElement);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<User>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ContactsImportCardRequest ExportCard:{0})", ExportCard);
            }
        }

        public class ContactsSearchRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x11f812d8;

            public string Q;
            public int Limit;

            public ContactsFound Result;

            public ContactsSearchRequest() { }

            public ContactsSearchRequest(string Q, int Limit)
            {
                this.Q = Q;
                this.Limit = Limit;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Q);
                writer.Write(Limit);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<ContactsFound>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ContactsSearchRequest Q:{0} Limit:{1})", Q, Limit);
            }
        }

        public class ContactsResolveUsernameRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xf93ccba3;

            public string Username;

            public ContactsResolvedPeer Result;

            public ContactsResolveUsernameRequest() { }

            public ContactsResolveUsernameRequest(string Username)
            {
                this.Username = Username;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Username);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<ContactsResolvedPeer>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ContactsResolveUsernameRequest Username:{0})", Username);
            }
        }

        public class MessagesGetMessagesRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x4222fa74;

            public List<int> Id;

            public MessagesMessages Result;

            public MessagesGetMessagesRequest() { }

            public MessagesGetMessagesRequest(List<int> Id)
            {
                this.Id = Id;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Id.Count);
                foreach (int IdElement in Id)
                    writer.Write(IdElement);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesMessages>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesGetMessagesRequest Id:{0})", Id);
            }
        }

        public class MessagesGetDialogsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x6b47f94d;

            public int OffsetDate;
            public int OffsetId;
            public InputPeer OffsetPeer;
            public int Limit;

            public MessagesDialogs Result;

            public MessagesGetDialogsRequest() { }

            public MessagesGetDialogsRequest(int OffsetDate, int OffsetId, InputPeer OffsetPeer, int Limit)
            {
                this.OffsetDate = OffsetDate;
                this.OffsetId = OffsetId;
                this.OffsetPeer = OffsetPeer;
                this.Limit = Limit;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(OffsetDate);
                writer.Write(OffsetId);
                OffsetPeer.Write(writer);
                writer.Write(Limit);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesDialogs>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesGetDialogsRequest OffsetDate:{0} OffsetId:{1} OffsetPeer:{2} Limit:{3})", OffsetDate, OffsetId, OffsetPeer, Limit);
            }
        }

        public class MessagesGetHistoryRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xafa92846;

            public InputPeer Peer;
            public int OffsetId;
            public int OffsetDate;
            public int AddOffset;
            public int Limit;
            public int MaxId;
            public int MinId;

            public MessagesMessages Result;

            public MessagesGetHistoryRequest() { }

            public MessagesGetHistoryRequest(InputPeer Peer, int OffsetId, int OffsetDate, int AddOffset, int Limit, int MaxId, int MinId)
            {
                this.Peer = Peer;
                this.OffsetId = OffsetId;
                this.OffsetDate = OffsetDate;
                this.AddOffset = AddOffset;
                this.Limit = Limit;
                this.MaxId = MaxId;
                this.MinId = MinId;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                writer.Write(OffsetId);
                writer.Write(OffsetDate);
                writer.Write(AddOffset);
                writer.Write(Limit);
                writer.Write(MaxId);
                writer.Write(MinId);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesMessages>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesGetHistoryRequest Peer:{0} OffsetId:{1} OffsetDate:{2} AddOffset:{3} Limit:{4} MaxId:{5} MinId:{6})", Peer, OffsetId, OffsetDate, AddOffset, Limit, MaxId, MinId);
            }
        }

        public class MessagesSearchRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xd4569248;

            public True ImportantOnly;
            public InputPeer Peer;
            public string Q;
            public MessagesFilter Filter;
            public int MinDate;
            public int MaxDate;
            public int Offset;
            public int MaxId;
            public int Limit;

            public MessagesMessages Result;

            public MessagesSearchRequest() { }

            /// <summary>
            /// The following arguments can be null: ImportantOnly
            /// </summary>
            /// <param name="ImportantOnly">Can be null</param>
            /// <param name="Peer">Can NOT be null</param>
            /// <param name="Q">Can NOT be null</param>
            /// <param name="Filter">Can NOT be null</param>
            /// <param name="MinDate">Can NOT be null</param>
            /// <param name="MaxDate">Can NOT be null</param>
            /// <param name="Offset">Can NOT be null</param>
            /// <param name="MaxId">Can NOT be null</param>
            /// <param name="Limit">Can NOT be null</param>
            public MessagesSearchRequest(True ImportantOnly, InputPeer Peer, string Q, MessagesFilter Filter, int MinDate, int MaxDate, int Offset, int MaxId, int Limit)
            {
                this.ImportantOnly = ImportantOnly;
                this.Peer = Peer;
                this.Q = Q;
                this.Filter = Filter;
                this.MinDate = MinDate;
                this.MaxDate = MaxDate;
                this.Offset = Offset;
                this.MaxId = MaxId;
                this.Limit = Limit;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                int flags =
                    (ImportantOnly != null ? 1 << 0 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (ImportantOnly != null) {

                }

                Peer.Write(writer);
                writer.Write(Q);
                Filter.Write(writer);
                writer.Write(MinDate);
                writer.Write(MaxDate);
                writer.Write(Offset);
                writer.Write(MaxId);
                writer.Write(Limit);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesMessages>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesSearchRequest ImportantOnly:{0} Peer:{1} Q:{2} Filter:{3} MinDate:{4} MaxDate:{5} Offset:{6} MaxId:{7} Limit:{8})", ImportantOnly, Peer, Q, Filter, MinDate, MaxDate, Offset, MaxId, Limit);
            }
        }

        public class MessagesReadHistoryRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x0e306d3a;

            public InputPeer Peer;
            public int MaxId;

            public MessagesAffectedMessages Result;

            public MessagesReadHistoryRequest() { }

            public MessagesReadHistoryRequest(InputPeer Peer, int MaxId)
            {
                this.Peer = Peer;
                this.MaxId = MaxId;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                writer.Write(MaxId);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesAffectedMessages>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesReadHistoryRequest Peer:{0} MaxId:{1})", Peer, MaxId);
            }
        }

        public class MessagesDeleteHistoryRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xb7c13bd9;

            public InputPeer Peer;
            public int MaxId;

            public MessagesAffectedHistory Result;

            public MessagesDeleteHistoryRequest() { }

            public MessagesDeleteHistoryRequest(InputPeer Peer, int MaxId)
            {
                this.Peer = Peer;
                this.MaxId = MaxId;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                writer.Write(MaxId);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesAffectedHistory>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesDeleteHistoryRequest Peer:{0} MaxId:{1})", Peer, MaxId);
            }
        }

        public class MessagesDeleteMessagesRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xa5f18925;

            public List<int> Id;

            public MessagesAffectedMessages Result;

            public MessagesDeleteMessagesRequest() { }

            public MessagesDeleteMessagesRequest(List<int> Id)
            {
                this.Id = Id;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Id.Count);
                foreach (int IdElement in Id)
                    writer.Write(IdElement);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesAffectedMessages>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesDeleteMessagesRequest Id:{0})", Id);
            }
        }

        public class MessagesReceivedMessagesRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x05a954c0;

            public int MaxId;

            public List<ReceivedNotifyMessage> Result;

            public MessagesReceivedMessagesRequest() { }

            public MessagesReceivedMessagesRequest(int MaxId)
            {
                this.MaxId = MaxId;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(MaxId);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<List<ReceivedNotifyMessage>>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesReceivedMessagesRequest MaxId:{0})", MaxId);
            }
        }

        public class MessagesSetTypingRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xa3825e50;

            public InputPeer Peer;
            public SendMessageAction Action;

            public bool Result;

            public MessagesSetTypingRequest() { }

            public MessagesSetTypingRequest(InputPeer Peer, SendMessageAction Action)
            {
                this.Peer = Peer;
                this.Action = Action;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                Action.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesSetTypingRequest Peer:{0} Action:{1})", Peer, Action);
            }
        }

        public class MessagesSendMessageRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xfa88427a;

            public True NoWebpage;
            public True Broadcast;
            public InputPeer Peer;
            public int? ReplyToMsgId;
            public string Message;
            public long RandomId;
            public ReplyMarkup ReplyMarkup;
            public List<MessageEntity> Entities;

            public Updates Result;

            public MessagesSendMessageRequest() { }

            /// <summary>
            /// The following arguments can be null: NoWebpage, Broadcast, ReplyToMsgId, ReplyMarkup, Entities
            /// </summary>
            /// <param name="NoWebpage">Can be null</param>
            /// <param name="Broadcast">Can be null</param>
            /// <param name="Peer">Can NOT be null</param>
            /// <param name="ReplyToMsgId">Can be null</param>
            /// <param name="Message">Can NOT be null</param>
            /// <param name="RandomId">Can NOT be null</param>
            /// <param name="ReplyMarkup">Can be null</param>
            /// <param name="Entities">Can be null</param>
            public MessagesSendMessageRequest(True NoWebpage, True Broadcast, InputPeer Peer, int? ReplyToMsgId, string Message, long RandomId, ReplyMarkup ReplyMarkup, List<MessageEntity> Entities)
            {
                this.NoWebpage = NoWebpage;
                this.Broadcast = Broadcast;
                this.Peer = Peer;
                this.ReplyToMsgId = ReplyToMsgId;
                this.Message = Message;
                this.RandomId = RandomId;
                this.ReplyMarkup = ReplyMarkup;
                this.Entities = Entities;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                int flags =
                    (NoWebpage != null ? 1 << 1 : 0) |
                    (Broadcast != null ? 1 << 4 : 0) |
                    (ReplyToMsgId != null ? 1 << 0 : 0) |
                    (ReplyMarkup != null ? 1 << 2 : 0) |
                    (Entities != null ? 1 << 3 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (NoWebpage != null) {

                }

                if (Broadcast != null) {

                }

                Peer.Write(writer);
                if (ReplyToMsgId != null) {
                    writer.Write(ReplyToMsgId.Value);
                }

                writer.Write(Message);
                writer.Write(RandomId);
                if (ReplyMarkup != null) {
                    ReplyMarkup.Write(writer);
                }

                if (Entities != null) {
                    writer.Write(0x1cb5c415); // vector code
                    writer.Write(Entities.Count);
                    foreach (MessageEntity EntitiesElement in Entities)
                        EntitiesElement.Write(writer);
                }

            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesSendMessageRequest NoWebpage:{0} Broadcast:{1} Peer:{2} ReplyToMsgId:{3} Message:{4} RandomId:{5} ReplyMarkup:{6} Entities:{7})", NoWebpage, Broadcast, Peer, ReplyToMsgId, Message, RandomId, ReplyMarkup, Entities);
            }
        }

        public class MessagesSendMediaRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xc8f16791;

            public True Broadcast;
            public InputPeer Peer;
            public int? ReplyToMsgId;
            public InputMedia Media;
            public long RandomId;
            public ReplyMarkup ReplyMarkup;

            public Updates Result;

            public MessagesSendMediaRequest() { }

            /// <summary>
            /// The following arguments can be null: Broadcast, ReplyToMsgId, ReplyMarkup
            /// </summary>
            /// <param name="Broadcast">Can be null</param>
            /// <param name="Peer">Can NOT be null</param>
            /// <param name="ReplyToMsgId">Can be null</param>
            /// <param name="Media">Can NOT be null</param>
            /// <param name="RandomId">Can NOT be null</param>
            /// <param name="ReplyMarkup">Can be null</param>
            public MessagesSendMediaRequest(True Broadcast, InputPeer Peer, int? ReplyToMsgId, InputMedia Media, long RandomId, ReplyMarkup ReplyMarkup)
            {
                this.Broadcast = Broadcast;
                this.Peer = Peer;
                this.ReplyToMsgId = ReplyToMsgId;
                this.Media = Media;
                this.RandomId = RandomId;
                this.ReplyMarkup = ReplyMarkup;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                int flags =
                    (Broadcast != null ? 1 << 4 : 0) |
                    (ReplyToMsgId != null ? 1 << 0 : 0) |
                    (ReplyMarkup != null ? 1 << 2 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Broadcast != null) {

                }

                Peer.Write(writer);
                if (ReplyToMsgId != null) {
                    writer.Write(ReplyToMsgId.Value);
                }

                Media.Write(writer);
                writer.Write(RandomId);
                if (ReplyMarkup != null) {
                    ReplyMarkup.Write(writer);
                }

            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesSendMediaRequest Broadcast:{0} Peer:{1} ReplyToMsgId:{2} Media:{3} RandomId:{4} ReplyMarkup:{5})", Broadcast, Peer, ReplyToMsgId, Media, RandomId, ReplyMarkup);
            }
        }

        public class MessagesForwardMessagesRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x708e0195;

            public True Broadcast;
            public InputPeer FromPeer;
            public List<int> Id;
            public List<long> RandomId;
            public InputPeer ToPeer;

            public Updates Result;

            public MessagesForwardMessagesRequest() { }

            /// <summary>
            /// The following arguments can be null: Broadcast
            /// </summary>
            /// <param name="Broadcast">Can be null</param>
            /// <param name="FromPeer">Can NOT be null</param>
            /// <param name="Id">Can NOT be null</param>
            /// <param name="RandomId">Can NOT be null</param>
            /// <param name="ToPeer">Can NOT be null</param>
            public MessagesForwardMessagesRequest(True Broadcast, InputPeer FromPeer, List<int> Id, List<long> RandomId, InputPeer ToPeer)
            {
                this.Broadcast = Broadcast;
                this.FromPeer = FromPeer;
                this.Id = Id;
                this.RandomId = RandomId;
                this.ToPeer = ToPeer;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                int flags =
                    (Broadcast != null ? 1 << 4 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Broadcast != null) {

                }

                FromPeer.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Id.Count);
                foreach (int IdElement in Id)
                    writer.Write(IdElement);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(RandomId.Count);
                foreach (long RandomIdElement in RandomId)
                    writer.Write(RandomIdElement);
                ToPeer.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesForwardMessagesRequest Broadcast:{0} FromPeer:{1} Id:{2} RandomId:{3} ToPeer:{4})", Broadcast, FromPeer, Id, RandomId, ToPeer);
            }
        }

        public class MessagesReportSpamRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xcf1592db;

            public InputPeer Peer;

            public bool Result;

            public MessagesReportSpamRequest() { }

            public MessagesReportSpamRequest(InputPeer Peer)
            {
                this.Peer = Peer;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesReportSpamRequest Peer:{0})", Peer);
            }
        }

        public class MessagesGetChatsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x3c6aa187;

            public List<int> Id;

            public MessagesChats Result;

            public MessagesGetChatsRequest() { }

            public MessagesGetChatsRequest(List<int> Id)
            {
                this.Id = Id;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Id.Count);
                foreach (int IdElement in Id)
                    writer.Write(IdElement);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesChats>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesGetChatsRequest Id:{0})", Id);
            }
        }

        public class MessagesGetFullChatRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x3b831c66;

            public int ChatId;

            public MessagesChatFull Result;

            public MessagesGetFullChatRequest() { }

            public MessagesGetFullChatRequest(int ChatId)
            {
                this.ChatId = ChatId;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesChatFull>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesGetFullChatRequest ChatId:{0})", ChatId);
            }
        }

        public class MessagesEditChatTitleRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xdc452855;

            public int ChatId;
            public string Title;

            public Updates Result;

            public MessagesEditChatTitleRequest() { }

            public MessagesEditChatTitleRequest(int ChatId, string Title)
            {
                this.ChatId = ChatId;
                this.Title = Title;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
                writer.Write(Title);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesEditChatTitleRequest ChatId:{0} Title:{1})", ChatId, Title);
            }
        }

        public class MessagesEditChatPhotoRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xca4c79d8;

            public int ChatId;
            public InputChatPhoto Photo;

            public Updates Result;

            public MessagesEditChatPhotoRequest() { }

            public MessagesEditChatPhotoRequest(int ChatId, InputChatPhoto Photo)
            {
                this.ChatId = ChatId;
                this.Photo = Photo;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
                Photo.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesEditChatPhotoRequest ChatId:{0} Photo:{1})", ChatId, Photo);
            }
        }

        public class MessagesAddChatUserRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xf9a0aa09;

            public int ChatId;
            public InputUser UserId;
            public int FwdLimit;

            public Updates Result;

            public MessagesAddChatUserRequest() { }

            public MessagesAddChatUserRequest(int ChatId, InputUser UserId, int FwdLimit)
            {
                this.ChatId = ChatId;
                this.UserId = UserId;
                this.FwdLimit = FwdLimit;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
                UserId.Write(writer);
                writer.Write(FwdLimit);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesAddChatUserRequest ChatId:{0} UserId:{1} FwdLimit:{2})", ChatId, UserId, FwdLimit);
            }
        }

        public class MessagesDeleteChatUserRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xe0611f16;

            public int ChatId;
            public InputUser UserId;

            public Updates Result;

            public MessagesDeleteChatUserRequest() { }

            public MessagesDeleteChatUserRequest(int ChatId, InputUser UserId)
            {
                this.ChatId = ChatId;
                this.UserId = UserId;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
                UserId.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesDeleteChatUserRequest ChatId:{0} UserId:{1})", ChatId, UserId);
            }
        }

        public class MessagesCreateChatRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x09cb126e;

            public List<InputUser> Users;
            public string Title;

            public Updates Result;

            public MessagesCreateChatRequest() { }

            public MessagesCreateChatRequest(List<InputUser> Users, string Title)
            {
                this.Users = Users;
                this.Title = Title;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (InputUser UsersElement in Users)
                    UsersElement.Write(writer);
                writer.Write(Title);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesCreateChatRequest Users:{0} Title:{1})", Users, Title);
            }
        }

        public class MessagesForwardMessageRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x33963bf9;

            public InputPeer Peer;
            public int Id;
            public long RandomId;

            public Updates Result;

            public MessagesForwardMessageRequest() { }

            public MessagesForwardMessageRequest(InputPeer Peer, int Id, long RandomId)
            {
                this.Peer = Peer;
                this.Id = Id;
                this.RandomId = RandomId;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                writer.Write(Id);
                writer.Write(RandomId);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesForwardMessageRequest Peer:{0} Id:{1} RandomId:{2})", Peer, Id, RandomId);
            }
        }

        public class MessagesSendBroadcastRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xbf73f4da;

            public List<InputUser> Contacts;
            public List<long> RandomId;
            public string Message;
            public InputMedia Media;

            public Updates Result;

            public MessagesSendBroadcastRequest() { }

            public MessagesSendBroadcastRequest(List<InputUser> Contacts, List<long> RandomId, string Message, InputMedia Media)
            {
                this.Contacts = Contacts;
                this.RandomId = RandomId;
                this.Message = Message;
                this.Media = Media;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Contacts.Count);
                foreach (InputUser ContactsElement in Contacts)
                    ContactsElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(RandomId.Count);
                foreach (long RandomIdElement in RandomId)
                    writer.Write(RandomIdElement);
                writer.Write(Message);
                Media.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesSendBroadcastRequest Contacts:{0} RandomId:{1} Message:{2} Media:{3})", Contacts, RandomId, Message, Media);
            }
        }

        public class MessagesGetDhConfigRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x26cf8950;

            public int Version;
            public int RandomLength;

            public MessagesDhConfig Result;

            public MessagesGetDhConfigRequest() { }

            public MessagesGetDhConfigRequest(int Version, int RandomLength)
            {
                this.Version = Version;
                this.RandomLength = RandomLength;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Version);
                writer.Write(RandomLength);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesDhConfig>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesGetDhConfigRequest Version:{0} RandomLength:{1})", Version, RandomLength);
            }
        }

        public class MessagesRequestEncryptionRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xf64daf43;

            public InputUser UserId;
            public int RandomId;
            public byte[] GA;

            public EncryptedChat Result;

            public MessagesRequestEncryptionRequest() { }

            public MessagesRequestEncryptionRequest(InputUser UserId, int RandomId, byte[] GA)
            {
                this.UserId = UserId;
                this.RandomId = RandomId;
                this.GA = GA;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                UserId.Write(writer);
                writer.Write(RandomId);
                writer.Write(GA);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<EncryptedChat>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesRequestEncryptionRequest UserId:{0} RandomId:{1} GA:{2})", UserId, RandomId, GA);
            }
        }

        public class MessagesAcceptEncryptionRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x3dbc0415;

            public InputEncryptedChat Peer;
            public byte[] GB;
            public long KeyFingerprint;

            public EncryptedChat Result;

            public MessagesAcceptEncryptionRequest() { }

            public MessagesAcceptEncryptionRequest(InputEncryptedChat Peer, byte[] GB, long KeyFingerprint)
            {
                this.Peer = Peer;
                this.GB = GB;
                this.KeyFingerprint = KeyFingerprint;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                writer.Write(GB);
                writer.Write(KeyFingerprint);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<EncryptedChat>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesAcceptEncryptionRequest Peer:{0} GB:{1} KeyFingerprint:{2})", Peer, GB, KeyFingerprint);
            }
        }

        public class MessagesDiscardEncryptionRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xedd923c5;

            public int ChatId;

            public bool Result;

            public MessagesDiscardEncryptionRequest() { }

            public MessagesDiscardEncryptionRequest(int ChatId)
            {
                this.ChatId = ChatId;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesDiscardEncryptionRequest ChatId:{0})", ChatId);
            }
        }

        public class MessagesSetEncryptedTypingRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x791451ed;

            public InputEncryptedChat Peer;
            public bool Typing;

            public bool Result;

            public MessagesSetEncryptedTypingRequest() { }

            public MessagesSetEncryptedTypingRequest(InputEncryptedChat Peer, bool Typing)
            {
                this.Peer = Peer;
                this.Typing = Typing;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                writer.Write(Typing);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesSetEncryptedTypingRequest Peer:{0} Typing:{1})", Peer, Typing);
            }
        }

        public class MessagesReadEncryptedHistoryRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x7f4b690a;

            public InputEncryptedChat Peer;
            public int MaxDate;

            public bool Result;

            public MessagesReadEncryptedHistoryRequest() { }

            public MessagesReadEncryptedHistoryRequest(InputEncryptedChat Peer, int MaxDate)
            {
                this.Peer = Peer;
                this.MaxDate = MaxDate;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                writer.Write(MaxDate);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesReadEncryptedHistoryRequest Peer:{0} MaxDate:{1})", Peer, MaxDate);
            }
        }

        public class MessagesSendEncryptedRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xa9776773;

            public InputEncryptedChat Peer;
            public long RandomId;
            public byte[] Data;

            public MessagesSentEncryptedMessage Result;

            public MessagesSendEncryptedRequest() { }

            public MessagesSendEncryptedRequest(InputEncryptedChat Peer, long RandomId, byte[] Data)
            {
                this.Peer = Peer;
                this.RandomId = RandomId;
                this.Data = Data;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                writer.Write(RandomId);
                writer.Write(Data);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesSentEncryptedMessage>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesSendEncryptedRequest Peer:{0} RandomId:{1} Data:{2})", Peer, RandomId, Data);
            }
        }

        public class MessagesSendEncryptedFileRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x9a901b66;

            public InputEncryptedChat Peer;
            public long RandomId;
            public byte[] Data;
            public InputEncryptedFile File;

            public MessagesSentEncryptedMessage Result;

            public MessagesSendEncryptedFileRequest() { }

            public MessagesSendEncryptedFileRequest(InputEncryptedChat Peer, long RandomId, byte[] Data, InputEncryptedFile File)
            {
                this.Peer = Peer;
                this.RandomId = RandomId;
                this.Data = Data;
                this.File = File;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                writer.Write(RandomId);
                writer.Write(Data);
                File.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesSentEncryptedMessage>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesSendEncryptedFileRequest Peer:{0} RandomId:{1} Data:{2} File:{3})", Peer, RandomId, Data, File);
            }
        }

        public class MessagesSendEncryptedServiceRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x32d439a4;

            public InputEncryptedChat Peer;
            public long RandomId;
            public byte[] Data;

            public MessagesSentEncryptedMessage Result;

            public MessagesSendEncryptedServiceRequest() { }

            public MessagesSendEncryptedServiceRequest(InputEncryptedChat Peer, long RandomId, byte[] Data)
            {
                this.Peer = Peer;
                this.RandomId = RandomId;
                this.Data = Data;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                writer.Write(RandomId);
                writer.Write(Data);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesSentEncryptedMessage>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesSendEncryptedServiceRequest Peer:{0} RandomId:{1} Data:{2})", Peer, RandomId, Data);
            }
        }

        public class MessagesReceivedQueueRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x55a5bb66;

            public int MaxQts;

            public List<long> Result;

            public MessagesReceivedQueueRequest() { }

            public MessagesReceivedQueueRequest(int MaxQts)
            {
                this.MaxQts = MaxQts;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(MaxQts);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<List<long>>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesReceivedQueueRequest MaxQts:{0})", MaxQts);
            }
        }

        public class MessagesReadMessageContentsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x36a73f77;

            public List<int> Id;

            public MessagesAffectedMessages Result;

            public MessagesReadMessageContentsRequest() { }

            public MessagesReadMessageContentsRequest(List<int> Id)
            {
                this.Id = Id;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Id.Count);
                foreach (int IdElement in Id)
                    writer.Write(IdElement);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesAffectedMessages>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesReadMessageContentsRequest Id:{0})", Id);
            }
        }

        public class MessagesGetStickersRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xae22e045;

            public string Emoticon;
            public string Hash;

            public MessagesStickers Result;

            public MessagesGetStickersRequest() { }

            public MessagesGetStickersRequest(string Emoticon, string Hash)
            {
                this.Emoticon = Emoticon;
                this.Hash = Hash;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Emoticon);
                writer.Write(Hash);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesStickers>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesGetStickersRequest Emoticon:{0} Hash:{1})", Emoticon, Hash);
            }
        }

        public class MessagesGetAllStickersRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x1c9618b1;

            public int Hash;

            public MessagesAllStickers Result;

            public MessagesGetAllStickersRequest() { }

            public MessagesGetAllStickersRequest(int Hash)
            {
                this.Hash = Hash;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Hash);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesAllStickers>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesGetAllStickersRequest Hash:{0})", Hash);
            }
        }

        public class MessagesGetWebPagePreviewRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x25223e24;

            public string Message;

            public MessageMedia Result;

            public MessagesGetWebPagePreviewRequest() { }

            public MessagesGetWebPagePreviewRequest(string Message)
            {
                this.Message = Message;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Message);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessageMedia>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesGetWebPagePreviewRequest Message:{0})", Message);
            }
        }

        public class MessagesExportChatInviteRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x7d885289;

            public int ChatId;

            public ExportedChatInvite Result;

            public MessagesExportChatInviteRequest() { }

            public MessagesExportChatInviteRequest(int ChatId)
            {
                this.ChatId = ChatId;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<ExportedChatInvite>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesExportChatInviteRequest ChatId:{0})", ChatId);
            }
        }

        public class MessagesCheckChatInviteRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x3eadb1bb;

            public string Hash;

            public ChatInvite Result;

            public MessagesCheckChatInviteRequest() { }

            public MessagesCheckChatInviteRequest(string Hash)
            {
                this.Hash = Hash;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Hash);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<ChatInvite>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesCheckChatInviteRequest Hash:{0})", Hash);
            }
        }

        public class MessagesImportChatInviteRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x6c50051c;

            public string Hash;

            public Updates Result;

            public MessagesImportChatInviteRequest() { }

            public MessagesImportChatInviteRequest(string Hash)
            {
                this.Hash = Hash;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Hash);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesImportChatInviteRequest Hash:{0})", Hash);
            }
        }

        public class MessagesGetStickerSetRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x2619a90e;

            public InputStickerSet Stickerset;

            public MessagesStickerSet Result;

            public MessagesGetStickerSetRequest() { }

            public MessagesGetStickerSetRequest(InputStickerSet Stickerset)
            {
                this.Stickerset = Stickerset;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Stickerset.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesStickerSet>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesGetStickerSetRequest Stickerset:{0})", Stickerset);
            }
        }

        public class MessagesInstallStickerSetRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x7b30c3a6;

            public InputStickerSet Stickerset;
            public bool Disabled;

            public bool Result;

            public MessagesInstallStickerSetRequest() { }

            public MessagesInstallStickerSetRequest(InputStickerSet Stickerset, bool Disabled)
            {
                this.Stickerset = Stickerset;
                this.Disabled = Disabled;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Stickerset.Write(writer);
                writer.Write(Disabled);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesInstallStickerSetRequest Stickerset:{0} Disabled:{1})", Stickerset, Disabled);
            }
        }

        public class MessagesUninstallStickerSetRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xf96e55de;

            public InputStickerSet Stickerset;

            public bool Result;

            public MessagesUninstallStickerSetRequest() { }

            public MessagesUninstallStickerSetRequest(InputStickerSet Stickerset)
            {
                this.Stickerset = Stickerset;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Stickerset.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesUninstallStickerSetRequest Stickerset:{0})", Stickerset);
            }
        }

        public class MessagesStartBotRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xe6df7378;

            public InputUser Bot;
            public InputPeer Peer;
            public long RandomId;
            public string StartParam;

            public Updates Result;

            public MessagesStartBotRequest() { }

            public MessagesStartBotRequest(InputUser Bot, InputPeer Peer, long RandomId, string StartParam)
            {
                this.Bot = Bot;
                this.Peer = Peer;
                this.RandomId = RandomId;
                this.StartParam = StartParam;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Bot.Write(writer);
                Peer.Write(writer);
                writer.Write(RandomId);
                writer.Write(StartParam);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesStartBotRequest Bot:{0} Peer:{1} RandomId:{2} StartParam:{3})", Bot, Peer, RandomId, StartParam);
            }
        }

        public class MessagesGetMessagesViewsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xc4c8a55d;

            public InputPeer Peer;
            public List<int> Id;
            public bool Increment;

            public List<int> Result;

            public MessagesGetMessagesViewsRequest() { }

            public MessagesGetMessagesViewsRequest(InputPeer Peer, List<int> Id, bool Increment)
            {
                this.Peer = Peer;
                this.Id = Id;
                this.Increment = Increment;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Id.Count);
                foreach (int IdElement in Id)
                    writer.Write(IdElement);
                writer.Write(Increment);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<List<int>>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesGetMessagesViewsRequest Peer:{0} Id:{1} Increment:{2})", Peer, Id, Increment);
            }
        }

        public class MessagesToggleChatAdminsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xec8bd9e1;

            public int ChatId;
            public bool Enabled;

            public Updates Result;

            public MessagesToggleChatAdminsRequest() { }

            public MessagesToggleChatAdminsRequest(int ChatId, bool Enabled)
            {
                this.ChatId = ChatId;
                this.Enabled = Enabled;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
                writer.Write(Enabled);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesToggleChatAdminsRequest ChatId:{0} Enabled:{1})", ChatId, Enabled);
            }
        }

        public class MessagesEditChatAdminRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xa9e69f2e;

            public int ChatId;
            public InputUser UserId;
            public bool IsAdmin;

            public bool Result;

            public MessagesEditChatAdminRequest() { }

            public MessagesEditChatAdminRequest(int ChatId, InputUser UserId, bool IsAdmin)
            {
                this.ChatId = ChatId;
                this.UserId = UserId;
                this.IsAdmin = IsAdmin;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
                UserId.Write(writer);
                writer.Write(IsAdmin);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesEditChatAdminRequest ChatId:{0} UserId:{1} IsAdmin:{2})", ChatId, UserId, IsAdmin);
            }
        }

        public class MessagesMigrateChatRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x15a3b8e3;

            public int ChatId;

            public Updates Result;

            public MessagesMigrateChatRequest() { }

            public MessagesMigrateChatRequest(int ChatId)
            {
                this.ChatId = ChatId;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesMigrateChatRequest ChatId:{0})", ChatId);
            }
        }

        public class MessagesSearchGlobalRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x9e3cacb0;

            public string Q;
            public int OffsetDate;
            public InputPeer OffsetPeer;
            public int OffsetId;
            public int Limit;

            public MessagesMessages Result;

            public MessagesSearchGlobalRequest() { }

            public MessagesSearchGlobalRequest(string Q, int OffsetDate, InputPeer OffsetPeer, int OffsetId, int Limit)
            {
                this.Q = Q;
                this.OffsetDate = OffsetDate;
                this.OffsetPeer = OffsetPeer;
                this.OffsetId = OffsetId;
                this.Limit = Limit;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Q);
                writer.Write(OffsetDate);
                OffsetPeer.Write(writer);
                writer.Write(OffsetId);
                writer.Write(Limit);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesMessages>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesSearchGlobalRequest Q:{0} OffsetDate:{1} OffsetPeer:{2} OffsetId:{3} Limit:{4})", Q, OffsetDate, OffsetPeer, OffsetId, Limit);
            }
        }

        public class MessagesReorderStickerSetsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x9fcfbc30;

            public List<long> Order;

            public bool Result;

            public MessagesReorderStickerSetsRequest() { }

            public MessagesReorderStickerSetsRequest(List<long> Order)
            {
                this.Order = Order;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Order.Count);
                foreach (long OrderElement in Order)
                    writer.Write(OrderElement);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesReorderStickerSetsRequest Order:{0})", Order);
            }
        }

        public class MessagesGetDocumentByHashRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x338e2464;

            public byte[] Sha256;
            public int Size;
            public string MimeType;

            public Document Result;

            public MessagesGetDocumentByHashRequest() { }

            public MessagesGetDocumentByHashRequest(byte[] Sha256, int Size, string MimeType)
            {
                this.Sha256 = Sha256;
                this.Size = Size;
                this.MimeType = MimeType;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Sha256);
                writer.Write(Size);
                writer.Write(MimeType);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Document>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesGetDocumentByHashRequest Sha256:{0} Size:{1} MimeType:{2})", Sha256, Size, MimeType);
            }
        }

        public class MessagesSearchGifsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xbf9a776b;

            public string Q;
            public int Offset;

            public MessagesFoundGifs Result;

            public MessagesSearchGifsRequest() { }

            public MessagesSearchGifsRequest(string Q, int Offset)
            {
                this.Q = Q;
                this.Offset = Offset;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Q);
                writer.Write(Offset);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesFoundGifs>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesSearchGifsRequest Q:{0} Offset:{1})", Q, Offset);
            }
        }

        public class MessagesGetSavedGifsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x83bf3d52;

            public int Hash;

            public MessagesSavedGifs Result;

            public MessagesGetSavedGifsRequest() { }

            public MessagesGetSavedGifsRequest(int Hash)
            {
                this.Hash = Hash;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Hash);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesSavedGifs>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesGetSavedGifsRequest Hash:{0})", Hash);
            }
        }

        public class MessagesSaveGifRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x327a30cb;

            public InputDocument Id;
            public bool Unsave;

            public bool Result;

            public MessagesSaveGifRequest() { }

            public MessagesSaveGifRequest(InputDocument Id, bool Unsave)
            {
                this.Id = Id;
                this.Unsave = Unsave;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Id.Write(writer);
                writer.Write(Unsave);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesSaveGifRequest Id:{0} Unsave:{1})", Id, Unsave);
            }
        }

        public class MessagesGetInlineBotResultsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x9324600d;

            public InputUser Bot;
            public string Query;
            public string Offset;

            public MessagesBotResults Result;

            public MessagesGetInlineBotResultsRequest() { }

            public MessagesGetInlineBotResultsRequest(InputUser Bot, string Query, string Offset)
            {
                this.Bot = Bot;
                this.Query = Query;
                this.Offset = Offset;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Bot.Write(writer);
                writer.Write(Query);
                writer.Write(Offset);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesBotResults>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesGetInlineBotResultsRequest Bot:{0} Query:{1} Offset:{2})", Bot, Query, Offset);
            }
        }

        public class MessagesSetInlineBotResultsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x3f23ec12;

            public True Gallery;
            public True Private;
            public long QueryId;
            public List<InputBotInlineResult> Results;
            public int CacheTime;
            public string NextOffset;

            public bool Result;

            public MessagesSetInlineBotResultsRequest() { }

            /// <summary>
            /// The following arguments can be null: Gallery, Private, NextOffset
            /// </summary>
            /// <param name="Gallery">Can be null</param>
            /// <param name="Private">Can be null</param>
            /// <param name="QueryId">Can NOT be null</param>
            /// <param name="Results">Can NOT be null</param>
            /// <param name="CacheTime">Can NOT be null</param>
            /// <param name="NextOffset">Can be null</param>
            public MessagesSetInlineBotResultsRequest(True Gallery, True Private, long QueryId, List<InputBotInlineResult> Results, int CacheTime, string NextOffset)
            {
                this.Gallery = Gallery;
                this.Private = Private;
                this.QueryId = QueryId;
                this.Results = Results;
                this.CacheTime = CacheTime;
                this.NextOffset = NextOffset;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                int flags =
                    (Gallery != null ? 1 << 0 : 0) |
                    (Private != null ? 1 << 1 : 0) |
                    (NextOffset != null ? 1 << 2 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Gallery != null) {

                }

                if (Private != null) {

                }

                writer.Write(QueryId);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Results.Count);
                foreach (InputBotInlineResult ResultsElement in Results)
                    ResultsElement.Write(writer);
                writer.Write(CacheTime);
                if (NextOffset != null) {
                    writer.Write(NextOffset);
                }

            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesSetInlineBotResultsRequest Gallery:{0} Private:{1} QueryId:{2} Results:{3} CacheTime:{4} NextOffset:{5})", Gallery, Private, QueryId, Results, CacheTime, NextOffset);
            }
        }

        public class MessagesSendInlineBotResultRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xb16e06fe;

            public True Broadcast;
            public InputPeer Peer;
            public int? ReplyToMsgId;
            public long RandomId;
            public long QueryId;
            public string Id;

            public Updates Result;

            public MessagesSendInlineBotResultRequest() { }

            /// <summary>
            /// The following arguments can be null: Broadcast, ReplyToMsgId
            /// </summary>
            /// <param name="Broadcast">Can be null</param>
            /// <param name="Peer">Can NOT be null</param>
            /// <param name="ReplyToMsgId">Can be null</param>
            /// <param name="RandomId">Can NOT be null</param>
            /// <param name="QueryId">Can NOT be null</param>
            /// <param name="Id">Can NOT be null</param>
            public MessagesSendInlineBotResultRequest(True Broadcast, InputPeer Peer, int? ReplyToMsgId, long RandomId, long QueryId, string Id)
            {
                this.Broadcast = Broadcast;
                this.Peer = Peer;
                this.ReplyToMsgId = ReplyToMsgId;
                this.RandomId = RandomId;
                this.QueryId = QueryId;
                this.Id = Id;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                int flags =
                    (Broadcast != null ? 1 << 4 : 0) |
                    (ReplyToMsgId != null ? 1 << 0 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Broadcast != null) {

                }

                Peer.Write(writer);
                if (ReplyToMsgId != null) {
                    writer.Write(ReplyToMsgId.Value);
                }

                writer.Write(RandomId);
                writer.Write(QueryId);
                writer.Write(Id);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(MessagesSendInlineBotResultRequest Broadcast:{0} Peer:{1} ReplyToMsgId:{2} RandomId:{3} QueryId:{4} Id:{5})", Broadcast, Peer, ReplyToMsgId, RandomId, QueryId, Id);
            }
        }

        public class UpdatesGetStateRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xedd4882a;

            public UpdatesState Result;

            public UpdatesGetStateRequest() { }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<UpdatesState>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return "(UpdatesGetStateRequest)";
            }
        }

        public class UpdatesGetDifferenceRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x0a041495;

            public int Pts;
            public int Date;
            public int Qts;

            public UpdatesDifference Result;

            public UpdatesGetDifferenceRequest() { }

            public UpdatesGetDifferenceRequest(int Pts, int Date, int Qts)
            {
                this.Pts = Pts;
                this.Date = Date;
                this.Qts = Qts;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Pts);
                writer.Write(Date);
                writer.Write(Qts);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<UpdatesDifference>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(UpdatesGetDifferenceRequest Pts:{0} Date:{1} Qts:{2})", Pts, Date, Qts);
            }
        }

        public class UpdatesGetChannelDifferenceRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xbb32d7c0;

            public InputChannel Channel;
            public ChannelMessagesFilter Filter;
            public int Pts;
            public int Limit;

            public UpdatesChannelDifference Result;

            public UpdatesGetChannelDifferenceRequest() { }

            public UpdatesGetChannelDifferenceRequest(InputChannel Channel, ChannelMessagesFilter Filter, int Pts, int Limit)
            {
                this.Channel = Channel;
                this.Filter = Filter;
                this.Pts = Pts;
                this.Limit = Limit;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                Filter.Write(writer);
                writer.Write(Pts);
                writer.Write(Limit);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<UpdatesChannelDifference>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(UpdatesGetChannelDifferenceRequest Channel:{0} Filter:{1} Pts:{2} Limit:{3})", Channel, Filter, Pts, Limit);
            }
        }

        public class PhotosUpdateProfilePhotoRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xeef579a0;

            public InputPhoto Id;
            public InputPhotoCrop Crop;

            public UserProfilePhoto Result;

            public PhotosUpdateProfilePhotoRequest() { }

            public PhotosUpdateProfilePhotoRequest(InputPhoto Id, InputPhotoCrop Crop)
            {
                this.Id = Id;
                this.Crop = Crop;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Id.Write(writer);
                Crop.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<UserProfilePhoto>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(PhotosUpdateProfilePhotoRequest Id:{0} Crop:{1})", Id, Crop);
            }
        }

        public class PhotosUploadProfilePhotoRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xd50f9c88;

            public InputFile File;
            public string Caption;
            public InputGeoPoint GeoPoint;
            public InputPhotoCrop Crop;

            public PhotosPhoto Result;

            public PhotosUploadProfilePhotoRequest() { }

            public PhotosUploadProfilePhotoRequest(InputFile File, string Caption, InputGeoPoint GeoPoint, InputPhotoCrop Crop)
            {
                this.File = File;
                this.Caption = Caption;
                this.GeoPoint = GeoPoint;
                this.Crop = Crop;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                File.Write(writer);
                writer.Write(Caption);
                GeoPoint.Write(writer);
                Crop.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<PhotosPhoto>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(PhotosUploadProfilePhotoRequest File:{0} Caption:{1} GeoPoint:{2} Crop:{3})", File, Caption, GeoPoint, Crop);
            }
        }

        public class PhotosDeletePhotosRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x87cf7f2f;

            public List<InputPhoto> Id;

            public List<long> Result;

            public PhotosDeletePhotosRequest() { }

            public PhotosDeletePhotosRequest(List<InputPhoto> Id)
            {
                this.Id = Id;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Id.Count);
                foreach (InputPhoto IdElement in Id)
                    IdElement.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<List<long>>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(PhotosDeletePhotosRequest Id:{0})", Id);
            }
        }

        public class PhotosGetUserPhotosRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x91cd32a8;

            public InputUser UserId;
            public int Offset;
            public long MaxId;
            public int Limit;

            public PhotosPhotos Result;

            public PhotosGetUserPhotosRequest() { }

            public PhotosGetUserPhotosRequest(InputUser UserId, int Offset, long MaxId, int Limit)
            {
                this.UserId = UserId;
                this.Offset = Offset;
                this.MaxId = MaxId;
                this.Limit = Limit;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                UserId.Write(writer);
                writer.Write(Offset);
                writer.Write(MaxId);
                writer.Write(Limit);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<PhotosPhotos>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(PhotosGetUserPhotosRequest UserId:{0} Offset:{1} MaxId:{2} Limit:{3})", UserId, Offset, MaxId, Limit);
            }
        }

        public class UploadSaveFilePartRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xb304a621;

            public long FileId;
            public int FilePart;
            public byte[] Bytes;

            public bool Result;

            public UploadSaveFilePartRequest() { }

            public UploadSaveFilePartRequest(long FileId, int FilePart, byte[] Bytes)
            {
                this.FileId = FileId;
                this.FilePart = FilePart;
                this.Bytes = Bytes;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(FileId);
                writer.Write(FilePart);
                writer.Write(Bytes);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(UploadSaveFilePartRequest FileId:{0} FilePart:{1} Bytes:{2})", FileId, FilePart, Bytes);
            }
        }

        public class UploadGetFileRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xe3a6cfb5;

            public InputFileLocation Location;
            public int Offset;
            public int Limit;

            public UploadFile Result;

            public UploadGetFileRequest() { }

            public UploadGetFileRequest(InputFileLocation Location, int Offset, int Limit)
            {
                this.Location = Location;
                this.Offset = Offset;
                this.Limit = Limit;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Location.Write(writer);
                writer.Write(Offset);
                writer.Write(Limit);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<UploadFile>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(UploadGetFileRequest Location:{0} Offset:{1} Limit:{2})", Location, Offset, Limit);
            }
        }

        public class UploadSaveBigFilePartRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xde7b673d;

            public long FileId;
            public int FilePart;
            public int FileTotalParts;
            public byte[] Bytes;

            public bool Result;

            public UploadSaveBigFilePartRequest() { }

            public UploadSaveBigFilePartRequest(long FileId, int FilePart, int FileTotalParts, byte[] Bytes)
            {
                this.FileId = FileId;
                this.FilePart = FilePart;
                this.FileTotalParts = FileTotalParts;
                this.Bytes = Bytes;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(FileId);
                writer.Write(FilePart);
                writer.Write(FileTotalParts);
                writer.Write(Bytes);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(UploadSaveBigFilePartRequest FileId:{0} FilePart:{1} FileTotalParts:{2} Bytes:{3})", FileId, FilePart, FileTotalParts, Bytes);
            }
        }

        public class HelpGetConfigRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xc4f9186b;

            public Config Result;

            public HelpGetConfigRequest() { }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Config>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return "(HelpGetConfigRequest)";
            }
        }

        public class HelpGetNearestDcRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x1fb33026;

            public NearestDc Result;

            public HelpGetNearestDcRequest() { }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<NearestDc>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return "(HelpGetNearestDcRequest)";
            }
        }

        public class HelpGetAppUpdateRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xc812ac7e;

            public string DeviceModel;
            public string SystemVersion;
            public string AppVersion;
            public string LangCode;

            public HelpAppUpdate Result;

            public HelpGetAppUpdateRequest() { }

            public HelpGetAppUpdateRequest(string DeviceModel, string SystemVersion, string AppVersion, string LangCode)
            {
                this.DeviceModel = DeviceModel;
                this.SystemVersion = SystemVersion;
                this.AppVersion = AppVersion;
                this.LangCode = LangCode;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(DeviceModel);
                writer.Write(SystemVersion);
                writer.Write(AppVersion);
                writer.Write(LangCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<HelpAppUpdate>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(HelpGetAppUpdateRequest DeviceModel:{0} SystemVersion:{1} AppVersion:{2} LangCode:{3})", DeviceModel, SystemVersion, AppVersion, LangCode);
            }
        }

        public class HelpSaveAppLogRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x6f02f748;

            public List<InputAppEvent> Events;

            public bool Result;

            public HelpSaveAppLogRequest() { }

            public HelpSaveAppLogRequest(List<InputAppEvent> Events)
            {
                this.Events = Events;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Events.Count);
                foreach (InputAppEvent EventsElement in Events)
                    EventsElement.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(HelpSaveAppLogRequest Events:{0})", Events);
            }
        }

        public class HelpGetInviteTextRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xa4a95186;

            public string LangCode;

            public HelpInviteText Result;

            public HelpGetInviteTextRequest() { }

            public HelpGetInviteTextRequest(string LangCode)
            {
                this.LangCode = LangCode;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(LangCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<HelpInviteText>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(HelpGetInviteTextRequest LangCode:{0})", LangCode);
            }
        }

        public class HelpGetSupportRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x9cdf08cd;

            public HelpSupport Result;

            public HelpGetSupportRequest() { }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<HelpSupport>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return "(HelpGetSupportRequest)";
            }
        }

        public class HelpGetAppChangelogRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x5bab7fb2;

            public string DeviceModel;
            public string SystemVersion;
            public string AppVersion;
            public string LangCode;

            public HelpAppChangelog Result;

            public HelpGetAppChangelogRequest() { }

            public HelpGetAppChangelogRequest(string DeviceModel, string SystemVersion, string AppVersion, string LangCode)
            {
                this.DeviceModel = DeviceModel;
                this.SystemVersion = SystemVersion;
                this.AppVersion = AppVersion;
                this.LangCode = LangCode;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(DeviceModel);
                writer.Write(SystemVersion);
                writer.Write(AppVersion);
                writer.Write(LangCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<HelpAppChangelog>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(HelpGetAppChangelogRequest DeviceModel:{0} SystemVersion:{1} AppVersion:{2} LangCode:{3})", DeviceModel, SystemVersion, AppVersion, LangCode);
            }
        }

        public class HelpGetTermsOfServiceRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x37d78f83;

            public string LangCode;

            public HelpTermsOfService Result;

            public HelpGetTermsOfServiceRequest() { }

            public HelpGetTermsOfServiceRequest(string LangCode)
            {
                this.LangCode = LangCode;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(LangCode);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<HelpTermsOfService>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(HelpGetTermsOfServiceRequest LangCode:{0})", LangCode);
            }
        }

        public class ChannelsGetDialogsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xa9d3d249;

            public int Offset;
            public int Limit;

            public MessagesDialogs Result;

            public ChannelsGetDialogsRequest() { }

            public ChannelsGetDialogsRequest(int Offset, int Limit)
            {
                this.Offset = Offset;
                this.Limit = Limit;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Offset);
                writer.Write(Limit);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesDialogs>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsGetDialogsRequest Offset:{0} Limit:{1})", Offset, Limit);
            }
        }

        public class ChannelsGetImportantHistoryRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x8f494bb2;

            public InputChannel Channel;
            public int OffsetId;
            public int OffsetDate;
            public int AddOffset;
            public int Limit;
            public int MaxId;
            public int MinId;

            public MessagesMessages Result;

            public ChannelsGetImportantHistoryRequest() { }

            public ChannelsGetImportantHistoryRequest(InputChannel Channel, int OffsetId, int OffsetDate, int AddOffset, int Limit, int MaxId, int MinId)
            {
                this.Channel = Channel;
                this.OffsetId = OffsetId;
                this.OffsetDate = OffsetDate;
                this.AddOffset = AddOffset;
                this.Limit = Limit;
                this.MaxId = MaxId;
                this.MinId = MinId;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                writer.Write(OffsetId);
                writer.Write(OffsetDate);
                writer.Write(AddOffset);
                writer.Write(Limit);
                writer.Write(MaxId);
                writer.Write(MinId);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesMessages>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsGetImportantHistoryRequest Channel:{0} OffsetId:{1} OffsetDate:{2} AddOffset:{3} Limit:{4} MaxId:{5} MinId:{6})", Channel, OffsetId, OffsetDate, AddOffset, Limit, MaxId, MinId);
            }
        }

        public class ChannelsReadHistoryRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xcc104937;

            public InputChannel Channel;
            public int MaxId;

            public bool Result;

            public ChannelsReadHistoryRequest() { }

            public ChannelsReadHistoryRequest(InputChannel Channel, int MaxId)
            {
                this.Channel = Channel;
                this.MaxId = MaxId;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                writer.Write(MaxId);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsReadHistoryRequest Channel:{0} MaxId:{1})", Channel, MaxId);
            }
        }

        public class ChannelsDeleteMessagesRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x84c1fd4e;

            public InputChannel Channel;
            public List<int> Id;

            public MessagesAffectedMessages Result;

            public ChannelsDeleteMessagesRequest() { }

            public ChannelsDeleteMessagesRequest(InputChannel Channel, List<int> Id)
            {
                this.Channel = Channel;
                this.Id = Id;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Id.Count);
                foreach (int IdElement in Id)
                    writer.Write(IdElement);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesAffectedMessages>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsDeleteMessagesRequest Channel:{0} Id:{1})", Channel, Id);
            }
        }

        public class ChannelsDeleteUserHistoryRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xd10dd71b;

            public InputChannel Channel;
            public InputUser UserId;

            public MessagesAffectedHistory Result;

            public ChannelsDeleteUserHistoryRequest() { }

            public ChannelsDeleteUserHistoryRequest(InputChannel Channel, InputUser UserId)
            {
                this.Channel = Channel;
                this.UserId = UserId;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                UserId.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesAffectedHistory>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsDeleteUserHistoryRequest Channel:{0} UserId:{1})", Channel, UserId);
            }
        }

        public class ChannelsReportSpamRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xfe087810;

            public InputChannel Channel;
            public InputUser UserId;
            public List<int> Id;

            public bool Result;

            public ChannelsReportSpamRequest() { }

            public ChannelsReportSpamRequest(InputChannel Channel, InputUser UserId, List<int> Id)
            {
                this.Channel = Channel;
                this.UserId = UserId;
                this.Id = Id;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                UserId.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Id.Count);
                foreach (int IdElement in Id)
                    writer.Write(IdElement);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsReportSpamRequest Channel:{0} UserId:{1} Id:{2})", Channel, UserId, Id);
            }
        }

        public class ChannelsGetMessagesRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x93d7b347;

            public InputChannel Channel;
            public List<int> Id;

            public MessagesMessages Result;

            public ChannelsGetMessagesRequest() { }

            public ChannelsGetMessagesRequest(InputChannel Channel, List<int> Id)
            {
                this.Channel = Channel;
                this.Id = Id;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Id.Count);
                foreach (int IdElement in Id)
                    writer.Write(IdElement);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesMessages>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsGetMessagesRequest Channel:{0} Id:{1})", Channel, Id);
            }
        }

        public class ChannelsGetParticipantsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x24d98f92;

            public InputChannel Channel;
            public ChannelParticipantsFilter Filter;
            public int Offset;
            public int Limit;

            public ChannelsChannelParticipants Result;

            public ChannelsGetParticipantsRequest() { }

            public ChannelsGetParticipantsRequest(InputChannel Channel, ChannelParticipantsFilter Filter, int Offset, int Limit)
            {
                this.Channel = Channel;
                this.Filter = Filter;
                this.Offset = Offset;
                this.Limit = Limit;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                Filter.Write(writer);
                writer.Write(Offset);
                writer.Write(Limit);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<ChannelsChannelParticipants>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsGetParticipantsRequest Channel:{0} Filter:{1} Offset:{2} Limit:{3})", Channel, Filter, Offset, Limit);
            }
        }

        public class ChannelsGetParticipantRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x546dd7a6;

            public InputChannel Channel;
            public InputUser UserId;

            public ChannelsChannelParticipant Result;

            public ChannelsGetParticipantRequest() { }

            public ChannelsGetParticipantRequest(InputChannel Channel, InputUser UserId)
            {
                this.Channel = Channel;
                this.UserId = UserId;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                UserId.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<ChannelsChannelParticipant>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsGetParticipantRequest Channel:{0} UserId:{1})", Channel, UserId);
            }
        }

        public class ChannelsGetChannelsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x0a7f6bbb;

            public List<InputChannel> Id;

            public MessagesChats Result;

            public ChannelsGetChannelsRequest() { }

            public ChannelsGetChannelsRequest(List<InputChannel> Id)
            {
                this.Id = Id;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Id.Count);
                foreach (InputChannel IdElement in Id)
                    IdElement.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesChats>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsGetChannelsRequest Id:{0})", Id);
            }
        }

        public class ChannelsGetFullChannelRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x08736a09;

            public InputChannel Channel;

            public MessagesChatFull Result;

            public ChannelsGetFullChannelRequest() { }

            public ChannelsGetFullChannelRequest(InputChannel Channel)
            {
                this.Channel = Channel;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<MessagesChatFull>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsGetFullChannelRequest Channel:{0})", Channel);
            }
        }

        public class ChannelsCreateChannelRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xf4893d7f;

            public True Broadcast;
            public True Megagroup;
            public string Title;
            public string About;

            public Updates Result;

            public ChannelsCreateChannelRequest() { }

            /// <summary>
            /// The following arguments can be null: Broadcast, Megagroup
            /// </summary>
            /// <param name="Broadcast">Can be null</param>
            /// <param name="Megagroup">Can be null</param>
            /// <param name="Title">Can NOT be null</param>
            /// <param name="About">Can NOT be null</param>
            public ChannelsCreateChannelRequest(True Broadcast, True Megagroup, string Title, string About)
            {
                this.Broadcast = Broadcast;
                this.Megagroup = Megagroup;
                this.Title = Title;
                this.About = About;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                int flags =
                    (Broadcast != null ? 1 << 0 : 0) |
                    (Megagroup != null ? 1 << 1 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Broadcast != null) {

                }

                if (Megagroup != null) {

                }

                writer.Write(Title);
                writer.Write(About);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsCreateChannelRequest Broadcast:{0} Megagroup:{1} Title:{2} About:{3})", Broadcast, Megagroup, Title, About);
            }
        }

        public class ChannelsEditAboutRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x13e27f1e;

            public InputChannel Channel;
            public string About;

            public bool Result;

            public ChannelsEditAboutRequest() { }

            public ChannelsEditAboutRequest(InputChannel Channel, string About)
            {
                this.Channel = Channel;
                this.About = About;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                writer.Write(About);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsEditAboutRequest Channel:{0} About:{1})", Channel, About);
            }
        }

        public class ChannelsEditAdminRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xeb7611d0;

            public InputChannel Channel;
            public InputUser UserId;
            public ChannelParticipantRole Role;

            public Updates Result;

            public ChannelsEditAdminRequest() { }

            public ChannelsEditAdminRequest(InputChannel Channel, InputUser UserId, ChannelParticipantRole Role)
            {
                this.Channel = Channel;
                this.UserId = UserId;
                this.Role = Role;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                UserId.Write(writer);
                Role.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsEditAdminRequest Channel:{0} UserId:{1} Role:{2})", Channel, UserId, Role);
            }
        }

        public class ChannelsEditTitleRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x566decd0;

            public InputChannel Channel;
            public string Title;

            public Updates Result;

            public ChannelsEditTitleRequest() { }

            public ChannelsEditTitleRequest(InputChannel Channel, string Title)
            {
                this.Channel = Channel;
                this.Title = Title;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                writer.Write(Title);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsEditTitleRequest Channel:{0} Title:{1})", Channel, Title);
            }
        }

        public class ChannelsEditPhotoRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xf12e57c9;

            public InputChannel Channel;
            public InputChatPhoto Photo;

            public Updates Result;

            public ChannelsEditPhotoRequest() { }

            public ChannelsEditPhotoRequest(InputChannel Channel, InputChatPhoto Photo)
            {
                this.Channel = Channel;
                this.Photo = Photo;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                Photo.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsEditPhotoRequest Channel:{0} Photo:{1})", Channel, Photo);
            }
        }

        public class ChannelsToggleCommentsRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xaaa29e88;

            public InputChannel Channel;
            public bool Enabled;

            public Updates Result;

            public ChannelsToggleCommentsRequest() { }

            public ChannelsToggleCommentsRequest(InputChannel Channel, bool Enabled)
            {
                this.Channel = Channel;
                this.Enabled = Enabled;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                writer.Write(Enabled);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsToggleCommentsRequest Channel:{0} Enabled:{1})", Channel, Enabled);
            }
        }

        public class ChannelsCheckUsernameRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x10e6bd2c;

            public InputChannel Channel;
            public string Username;

            public bool Result;

            public ChannelsCheckUsernameRequest() { }

            public ChannelsCheckUsernameRequest(InputChannel Channel, string Username)
            {
                this.Channel = Channel;
                this.Username = Username;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                writer.Write(Username);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsCheckUsernameRequest Channel:{0} Username:{1})", Channel, Username);
            }
        }

        public class ChannelsUpdateUsernameRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x3514b3de;

            public InputChannel Channel;
            public string Username;

            public bool Result;

            public ChannelsUpdateUsernameRequest() { }

            public ChannelsUpdateUsernameRequest(InputChannel Channel, string Username)
            {
                this.Channel = Channel;
                this.Username = Username;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                writer.Write(Username);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.ReadBoolean();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsUpdateUsernameRequest Channel:{0} Username:{1})", Channel, Username);
            }
        }

        public class ChannelsJoinChannelRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x24b524c5;

            public InputChannel Channel;

            public Updates Result;

            public ChannelsJoinChannelRequest() { }

            public ChannelsJoinChannelRequest(InputChannel Channel)
            {
                this.Channel = Channel;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsJoinChannelRequest Channel:{0})", Channel);
            }
        }

        public class ChannelsLeaveChannelRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xf836aa95;

            public InputChannel Channel;

            public Updates Result;

            public ChannelsLeaveChannelRequest() { }

            public ChannelsLeaveChannelRequest(InputChannel Channel)
            {
                this.Channel = Channel;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsLeaveChannelRequest Channel:{0})", Channel);
            }
        }

        public class ChannelsInviteToChannelRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x199f3a6c;

            public InputChannel Channel;
            public List<InputUser> Users;

            public Updates Result;

            public ChannelsInviteToChannelRequest() { }

            public ChannelsInviteToChannelRequest(InputChannel Channel, List<InputUser> Users)
            {
                this.Channel = Channel;
                this.Users = Users;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (InputUser UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsInviteToChannelRequest Channel:{0} Users:{1})", Channel, Users);
            }
        }

        public class ChannelsKickFromChannelRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xa672de14;

            public InputChannel Channel;
            public InputUser UserId;
            public bool Kicked;

            public Updates Result;

            public ChannelsKickFromChannelRequest() { }

            public ChannelsKickFromChannelRequest(InputChannel Channel, InputUser UserId, bool Kicked)
            {
                this.Channel = Channel;
                this.UserId = UserId;
                this.Kicked = Kicked;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                UserId.Write(writer);
                writer.Write(Kicked);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsKickFromChannelRequest Channel:{0} UserId:{1} Kicked:{2})", Channel, UserId, Kicked);
            }
        }

        public class ChannelsExportInviteRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xc7560885;

            public InputChannel Channel;

            public ExportedChatInvite Result;

            public ChannelsExportInviteRequest() { }

            public ChannelsExportInviteRequest(InputChannel Channel)
            {
                this.Channel = Channel;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<ExportedChatInvite>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsExportInviteRequest Channel:{0})", Channel);
            }
        }

        public class ChannelsDeleteChannelRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0xc0111fe3;

            public InputChannel Channel;

            public Updates Result;

            public ChannelsDeleteChannelRequest() { }

            public ChannelsDeleteChannelRequest(InputChannel Channel)
            {
                this.Channel = Channel;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsDeleteChannelRequest Channel:{0})", Channel);
            }
        }

        public class ChannelsToggleInvitesRequest : MTProtoRequest
        {
            public override uint ConstructorCode => 0x49609307;

            public InputChannel Channel;
            public bool Enabled;

            public Updates Result;

            public ChannelsToggleInvitesRequest() { }

            public ChannelsToggleInvitesRequest(InputChannel Channel, bool Enabled)
            {
                this.Channel = Channel;
                this.Enabled = Enabled;
            }

            public override void OnSend(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Channel.Write(writer);
                writer.Write(Enabled);
            }

            public override void OnResponse(TBinaryReader reader)
            {
                Result = reader.Read<Updates>();
            }

            public override void OnException(Exception exception)
            {
                throw exception;
            }

            public override bool Confirmed => true;
            public override bool Responded { get; }

            public override string ToString()
            {
                return string.Format("(ChannelsToggleInvitesRequest Channel:{0} Enabled:{1})", Channel, Enabled);
            }
        }

        #endregion

        #region Types

        public class MsgsAckType : MsgsAck
        {
            public override uint ConstructorCode => 0x62d6b459;

            public List<long> MsgIds;

            public MsgsAckType() { }

            public MsgsAckType(List<long> MsgIds)
            {
                this.MsgIds = MsgIds;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(MsgIds.Count);
                foreach (long MsgIdsElement in MsgIds)
                    writer.Write(MsgIdsElement);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int MsgIdsLength = reader.ReadInt32();
                MsgIds = new List<long>(MsgIdsLength);
                for (int MsgIdsIndex = 0; MsgIdsIndex < MsgIdsLength; MsgIdsIndex++)
                    MsgIds.Add(reader.ReadInt64());
            }

            public override string ToString()
            {
                return string.Format("(MsgsAckType MsgIds:{0})", MsgIds);
            }
        }

        public class BadMsgNotificationType : BadMsgNotification
        {
            public override uint ConstructorCode => 0xa7eff811;

            public long BadMsgId;
            public int BadMsgSeqno;
            public int ErrorCode;

            public BadMsgNotificationType() { }

            public BadMsgNotificationType(long BadMsgId, int BadMsgSeqno, int ErrorCode)
            {
                this.BadMsgId = BadMsgId;
                this.BadMsgSeqno = BadMsgSeqno;
                this.ErrorCode = ErrorCode;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(BadMsgId);
                writer.Write(BadMsgSeqno);
                writer.Write(ErrorCode);
            }

            public override void Read(TBinaryReader reader)
            {
                BadMsgId = reader.ReadInt64();
                BadMsgSeqno = reader.ReadInt32();
                ErrorCode = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(BadMsgNotificationType BadMsgId:{0} BadMsgSeqno:{1} ErrorCode:{2})", BadMsgId, BadMsgSeqno, ErrorCode);
            }
        }

        public class BadServerSaltType : BadMsgNotification
        {
            public override uint ConstructorCode => 0xedab447b;

            public long BadMsgId;
            public int BadMsgSeqno;
            public int ErrorCode;
            public long NewServerSalt;

            public BadServerSaltType() { }

            public BadServerSaltType(long BadMsgId, int BadMsgSeqno, int ErrorCode, long NewServerSalt)
            {
                this.BadMsgId = BadMsgId;
                this.BadMsgSeqno = BadMsgSeqno;
                this.ErrorCode = ErrorCode;
                this.NewServerSalt = NewServerSalt;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(BadMsgId);
                writer.Write(BadMsgSeqno);
                writer.Write(ErrorCode);
                writer.Write(NewServerSalt);
            }

            public override void Read(TBinaryReader reader)
            {
                BadMsgId = reader.ReadInt64();
                BadMsgSeqno = reader.ReadInt32();
                ErrorCode = reader.ReadInt32();
                NewServerSalt = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(BadServerSaltType BadMsgId:{0} BadMsgSeqno:{1} ErrorCode:{2} NewServerSalt:{3})", BadMsgId, BadMsgSeqno, ErrorCode, NewServerSalt);
            }
        }

        public class MsgsStateReqType : MsgsStateReq
        {
            public override uint ConstructorCode => 0xda69fb52;

            public List<long> MsgIds;

            public MsgsStateReqType() { }

            public MsgsStateReqType(List<long> MsgIds)
            {
                this.MsgIds = MsgIds;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(MsgIds.Count);
                foreach (long MsgIdsElement in MsgIds)
                    writer.Write(MsgIdsElement);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int MsgIdsLength = reader.ReadInt32();
                MsgIds = new List<long>(MsgIdsLength);
                for (int MsgIdsIndex = 0; MsgIdsIndex < MsgIdsLength; MsgIdsIndex++)
                    MsgIds.Add(reader.ReadInt64());
            }

            public override string ToString()
            {
                return string.Format("(MsgsStateReqType MsgIds:{0})", MsgIds);
            }
        }

        public class MsgsStateInfoType : MsgsStateInfo
        {
            public override uint ConstructorCode => 0x04deb57d;

            public long ReqMsgId;
            public string Info;

            public MsgsStateInfoType() { }

            public MsgsStateInfoType(long ReqMsgId, string Info)
            {
                this.ReqMsgId = ReqMsgId;
                this.Info = Info;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ReqMsgId);
                writer.Write(Info);
            }

            public override void Read(TBinaryReader reader)
            {
                ReqMsgId = reader.ReadInt64();
                Info = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(MsgsStateInfoType ReqMsgId:{0} Info:{1})", ReqMsgId, Info);
            }
        }

        public class MsgsAllInfoType : MsgsAllInfo
        {
            public override uint ConstructorCode => 0x8cc0d131;

            public List<long> MsgIds;
            public string Info;

            public MsgsAllInfoType() { }

            public MsgsAllInfoType(List<long> MsgIds, string Info)
            {
                this.MsgIds = MsgIds;
                this.Info = Info;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(MsgIds.Count);
                foreach (long MsgIdsElement in MsgIds)
                    writer.Write(MsgIdsElement);
                writer.Write(Info);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int MsgIdsLength = reader.ReadInt32();
                MsgIds = new List<long>(MsgIdsLength);
                for (int MsgIdsIndex = 0; MsgIdsIndex < MsgIdsLength; MsgIdsIndex++)
                    MsgIds.Add(reader.ReadInt64());
                Info = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(MsgsAllInfoType MsgIds:{0} Info:{1})", MsgIds, Info);
            }
        }

        public class MsgDetailedInfoType : MsgDetailedInfo
        {
            public override uint ConstructorCode => 0x276d3ec6;

            public long MsgId;
            public long AnswerMsgId;
            public int Bytes;
            public int Status;

            public MsgDetailedInfoType() { }

            public MsgDetailedInfoType(long MsgId, long AnswerMsgId, int Bytes, int Status)
            {
                this.MsgId = MsgId;
                this.AnswerMsgId = AnswerMsgId;
                this.Bytes = Bytes;
                this.Status = Status;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(MsgId);
                writer.Write(AnswerMsgId);
                writer.Write(Bytes);
                writer.Write(Status);
            }

            public override void Read(TBinaryReader reader)
            {
                MsgId = reader.ReadInt64();
                AnswerMsgId = reader.ReadInt64();
                Bytes = reader.ReadInt32();
                Status = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MsgDetailedInfoType MsgId:{0} AnswerMsgId:{1} Bytes:{2} Status:{3})", MsgId, AnswerMsgId, Bytes, Status);
            }
        }

        public class MsgNewDetailedInfoType : MsgDetailedInfo
        {
            public override uint ConstructorCode => 0x809db6df;

            public long AnswerMsgId;
            public int Bytes;
            public int Status;

            public MsgNewDetailedInfoType() { }

            public MsgNewDetailedInfoType(long AnswerMsgId, int Bytes, int Status)
            {
                this.AnswerMsgId = AnswerMsgId;
                this.Bytes = Bytes;
                this.Status = Status;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(AnswerMsgId);
                writer.Write(Bytes);
                writer.Write(Status);
            }

            public override void Read(TBinaryReader reader)
            {
                AnswerMsgId = reader.ReadInt64();
                Bytes = reader.ReadInt32();
                Status = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MsgNewDetailedInfoType AnswerMsgId:{0} Bytes:{1} Status:{2})", AnswerMsgId, Bytes, Status);
            }
        }

        public class MsgResendReqType : MsgResendReq
        {
            public override uint ConstructorCode => 0x7d861a08;

            public List<long> MsgIds;

            public MsgResendReqType() { }

            public MsgResendReqType(List<long> MsgIds)
            {
                this.MsgIds = MsgIds;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(MsgIds.Count);
                foreach (long MsgIdsElement in MsgIds)
                    writer.Write(MsgIdsElement);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int MsgIdsLength = reader.ReadInt32();
                MsgIds = new List<long>(MsgIdsLength);
                for (int MsgIdsIndex = 0; MsgIdsIndex < MsgIdsLength; MsgIdsIndex++)
                    MsgIds.Add(reader.ReadInt64());
            }

            public override string ToString()
            {
                return string.Format("(MsgResendReqType MsgIds:{0})", MsgIds);
            }
        }

        public class RpcErrorType : RpcError
        {
            public override uint ConstructorCode => 0x2144ca19;

            public int ErrorCode;
            public string ErrorMessage;

            public RpcErrorType() { }

            public RpcErrorType(int ErrorCode, string ErrorMessage)
            {
                this.ErrorCode = ErrorCode;
                this.ErrorMessage = ErrorMessage;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ErrorCode);
                writer.Write(ErrorMessage);
            }

            public override void Read(TBinaryReader reader)
            {
                ErrorCode = reader.ReadInt32();
                ErrorMessage = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(RpcErrorType ErrorCode:{0} ErrorMessage:{1})", ErrorCode, ErrorMessage);
            }
        }

        public class RpcAnswerUnknownType : RpcDropAnswer
        {
            public override uint ConstructorCode => 0x5e2ad36e;

            public RpcAnswerUnknownType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(RpcAnswerUnknownType)";
            }
        }

        public class RpcAnswerDroppedRunningType : RpcDropAnswer
        {
            public override uint ConstructorCode => 0xcd78e586;

            public RpcAnswerDroppedRunningType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(RpcAnswerDroppedRunningType)";
            }
        }

        public class RpcAnswerDroppedType : RpcDropAnswer
        {
            public override uint ConstructorCode => 0xa43ad8b7;

            public long MsgId;
            public int SeqNo;
            public int Bytes;

            public RpcAnswerDroppedType() { }

            public RpcAnswerDroppedType(long MsgId, int SeqNo, int Bytes)
            {
                this.MsgId = MsgId;
                this.SeqNo = SeqNo;
                this.Bytes = Bytes;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(MsgId);
                writer.Write(SeqNo);
                writer.Write(Bytes);
            }

            public override void Read(TBinaryReader reader)
            {
                MsgId = reader.ReadInt64();
                SeqNo = reader.ReadInt32();
                Bytes = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(RpcAnswerDroppedType MsgId:{0} SeqNo:{1} Bytes:{2})", MsgId, SeqNo, Bytes);
            }
        }

        public class FutureSaltType : FutureSalt
        {
            public override uint ConstructorCode => 0x0949d9dc;

            public int ValidSince;
            public int ValidUntil;
            public long Salt;

            public FutureSaltType() { }

            public FutureSaltType(int ValidSince, int ValidUntil, long Salt)
            {
                this.ValidSince = ValidSince;
                this.ValidUntil = ValidUntil;
                this.Salt = Salt;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ValidSince);
                writer.Write(ValidUntil);
                writer.Write(Salt);
            }

            public override void Read(TBinaryReader reader)
            {
                ValidSince = reader.ReadInt32();
                ValidUntil = reader.ReadInt32();
                Salt = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(FutureSaltType ValidSince:{0} ValidUntil:{1} Salt:{2})", ValidSince, ValidUntil, Salt);
            }
        }

        public class FutureSaltsType : FutureSalts
        {
            public override uint ConstructorCode => 0xae500895;

            public long ReqMsgId;
            public int Now;
            public List<FutureSalt> Salts;

            public FutureSaltsType() { }

            public FutureSaltsType(long ReqMsgId, int Now, List<FutureSalt> Salts)
            {
                this.ReqMsgId = ReqMsgId;
                this.Now = Now;
                this.Salts = Salts;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ReqMsgId);
                writer.Write(Now);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Salts.Count);
                foreach (FutureSalt SaltsElement in Salts)
                    SaltsElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                ReqMsgId = reader.ReadInt64();
                Now = reader.ReadInt32();
                reader.ReadInt32(); // vector code
                int SaltsLength = reader.ReadInt32();
                Salts = new List<FutureSalt>(SaltsLength);
                for (int SaltsIndex = 0; SaltsIndex < SaltsLength; SaltsIndex++)
                    Salts.Add(reader.Read<FutureSalt>());
            }

            public override string ToString()
            {
                return string.Format("(FutureSaltsType ReqMsgId:{0} Now:{1} Salts:{2})", ReqMsgId, Now, Salts);
            }
        }

        public class PongType : Pong
        {
            public override uint ConstructorCode => 0x347773c5;

            public long MsgId;
            public long PingId;

            public PongType() { }

            public PongType(long MsgId, long PingId)
            {
                this.MsgId = MsgId;
                this.PingId = PingId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(MsgId);
                writer.Write(PingId);
            }

            public override void Read(TBinaryReader reader)
            {
                MsgId = reader.ReadInt64();
                PingId = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(PongType MsgId:{0} PingId:{1})", MsgId, PingId);
            }
        }

        public class DestroySessionOkType : DestroySessionRes
        {
            public override uint ConstructorCode => 0xe22045fc;

            public long SessionId;

            public DestroySessionOkType() { }

            public DestroySessionOkType(long SessionId)
            {
                this.SessionId = SessionId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(SessionId);
            }

            public override void Read(TBinaryReader reader)
            {
                SessionId = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(DestroySessionOkType SessionId:{0})", SessionId);
            }
        }

        public class DestroySessionNoneType : DestroySessionRes
        {
            public override uint ConstructorCode => 0x62d350c9;

            public long SessionId;

            public DestroySessionNoneType() { }

            public DestroySessionNoneType(long SessionId)
            {
                this.SessionId = SessionId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(SessionId);
            }

            public override void Read(TBinaryReader reader)
            {
                SessionId = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(DestroySessionNoneType SessionId:{0})", SessionId);
            }
        }

        public class NewSessionCreatedType : NewSession
        {
            public override uint ConstructorCode => 0x9ec20908;

            public long FirstMsgId;
            public long UniqueId;
            public long ServerSalt;

            public NewSessionCreatedType() { }

            public NewSessionCreatedType(long FirstMsgId, long UniqueId, long ServerSalt)
            {
                this.FirstMsgId = FirstMsgId;
                this.UniqueId = UniqueId;
                this.ServerSalt = ServerSalt;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(FirstMsgId);
                writer.Write(UniqueId);
                writer.Write(ServerSalt);
            }

            public override void Read(TBinaryReader reader)
            {
                FirstMsgId = reader.ReadInt64();
                UniqueId = reader.ReadInt64();
                ServerSalt = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(NewSessionCreatedType FirstMsgId:{0} UniqueId:{1} ServerSalt:{2})", FirstMsgId, UniqueId, ServerSalt);
            }
        }

        public class HttpWaitType : HttpWait
        {
            public override uint ConstructorCode => 0x9299359f;

            public int MaxDelay;
            public int WaitAfter;
            public int MaxWait;

            public HttpWaitType() { }

            public HttpWaitType(int MaxDelay, int WaitAfter, int MaxWait)
            {
                this.MaxDelay = MaxDelay;
                this.WaitAfter = WaitAfter;
                this.MaxWait = MaxWait;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(MaxDelay);
                writer.Write(WaitAfter);
                writer.Write(MaxWait);
            }

            public override void Read(TBinaryReader reader)
            {
                MaxDelay = reader.ReadInt32();
                WaitAfter = reader.ReadInt32();
                MaxWait = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(HttpWaitType MaxDelay:{0} WaitAfter:{1} MaxWait:{2})", MaxDelay, WaitAfter, MaxWait);
            }
        }

        public class TrueType : True
        {
            public override uint ConstructorCode => 0x3fedd339;

            public TrueType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(TrueType)";
            }
        }

        public class ErrorType : Error
        {
            public override uint ConstructorCode => 0xc4b9f9bb;

            public int Code;
            public string Text;

            public ErrorType() { }

            public ErrorType(int Code, string Text)
            {
                this.Code = Code;
                this.Text = Text;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Code);
                writer.Write(Text);
            }

            public override void Read(TBinaryReader reader)
            {
                Code = reader.ReadInt32();
                Text = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(ErrorType Code:{0} Text:{1})", Code, Text);
            }
        }

        public class NullType : Null
        {
            public override uint ConstructorCode => 0x56730bcc;

            public NullType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(NullType)";
            }
        }

        public class InputPeerEmptyType : InputPeer
        {
            public override uint ConstructorCode => 0x7f3b18ea;

            public InputPeerEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputPeerEmptyType)";
            }
        }

        public class InputPeerSelfType : InputPeer
        {
            public override uint ConstructorCode => 0x7da07ec9;

            public InputPeerSelfType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputPeerSelfType)";
            }
        }

        public class InputPeerChatType : InputPeer
        {
            public override uint ConstructorCode => 0x179be863;

            public int ChatId;

            public InputPeerChatType() { }

            public InputPeerChatType(int ChatId)
            {
                this.ChatId = ChatId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
            }

            public override void Read(TBinaryReader reader)
            {
                ChatId = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(InputPeerChatType ChatId:{0})", ChatId);
            }
        }

        public class InputPeerUserType : InputPeer
        {
            public override uint ConstructorCode => 0x7b8e7de6;

            public int UserId;
            public long AccessHash;

            public InputPeerUserType() { }

            public InputPeerUserType(int UserId, long AccessHash)
            {
                this.UserId = UserId;
                this.AccessHash = AccessHash;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(AccessHash);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                AccessHash = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(InputPeerUserType UserId:{0} AccessHash:{1})", UserId, AccessHash);
            }
        }

        public class InputPeerChannelType : InputPeer
        {
            public override uint ConstructorCode => 0x20adaef8;

            public int ChannelId;
            public long AccessHash;

            public InputPeerChannelType() { }

            public InputPeerChannelType(int ChannelId, long AccessHash)
            {
                this.ChannelId = ChannelId;
                this.AccessHash = AccessHash;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChannelId);
                writer.Write(AccessHash);
            }

            public override void Read(TBinaryReader reader)
            {
                ChannelId = reader.ReadInt32();
                AccessHash = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(InputPeerChannelType ChannelId:{0} AccessHash:{1})", ChannelId, AccessHash);
            }
        }

        public class InputUserEmptyType : InputUser
        {
            public override uint ConstructorCode => 0xb98886cf;

            public InputUserEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputUserEmptyType)";
            }
        }

        public class InputUserSelfType : InputUser
        {
            public override uint ConstructorCode => 0xf7c1b13f;

            public InputUserSelfType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputUserSelfType)";
            }
        }

        public class InputUserType : InputUser
        {
            public override uint ConstructorCode => 0xd8292816;

            public int UserId;
            public long AccessHash;

            public InputUserType() { }

            public InputUserType(int UserId, long AccessHash)
            {
                this.UserId = UserId;
                this.AccessHash = AccessHash;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(AccessHash);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                AccessHash = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(InputUserType UserId:{0} AccessHash:{1})", UserId, AccessHash);
            }
        }

        public class InputPhoneContactType : InputContact
        {
            public override uint ConstructorCode => 0xf392b7f4;

            public long ClientId;
            public string Phone;
            public string FirstName;
            public string LastName;

            public InputPhoneContactType() { }

            public InputPhoneContactType(long ClientId, string Phone, string FirstName, string LastName)
            {
                this.ClientId = ClientId;
                this.Phone = Phone;
                this.FirstName = FirstName;
                this.LastName = LastName;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ClientId);
                writer.Write(Phone);
                writer.Write(FirstName);
                writer.Write(LastName);
            }

            public override void Read(TBinaryReader reader)
            {
                ClientId = reader.ReadInt64();
                Phone = reader.ReadString();
                FirstName = reader.ReadString();
                LastName = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(InputPhoneContactType ClientId:{0} Phone:{1} FirstName:{2} LastName:{3})", ClientId, Phone, FirstName, LastName);
            }
        }

        public class InputFileType : InputFile
        {
            public override uint ConstructorCode => 0xf52ff27f;

            public long Id;
            public int Parts;
            public string Name;
            public string Md5Checksum;

            public InputFileType() { }

            public InputFileType(long Id, int Parts, string Name, string Md5Checksum)
            {
                this.Id = Id;
                this.Parts = Parts;
                this.Name = Name;
                this.Md5Checksum = Md5Checksum;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(Parts);
                writer.Write(Name);
                writer.Write(Md5Checksum);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt64();
                Parts = reader.ReadInt32();
                Name = reader.ReadString();
                Md5Checksum = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(InputFileType Id:{0} Parts:{1} Name:{2} Md5Checksum:{3})", Id, Parts, Name, Md5Checksum);
            }
        }

        public class InputFileBigType : InputFile
        {
            public override uint ConstructorCode => 0xfa4f0bb5;

            public long Id;
            public int Parts;
            public string Name;

            public InputFileBigType() { }

            public InputFileBigType(long Id, int Parts, string Name)
            {
                this.Id = Id;
                this.Parts = Parts;
                this.Name = Name;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(Parts);
                writer.Write(Name);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt64();
                Parts = reader.ReadInt32();
                Name = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(InputFileBigType Id:{0} Parts:{1} Name:{2})", Id, Parts, Name);
            }
        }

        public class InputMediaEmptyType : InputMedia
        {
            public override uint ConstructorCode => 0x9664f57f;

            public InputMediaEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputMediaEmptyType)";
            }
        }

        public class InputMediaUploadedPhotoType : InputMedia
        {
            public override uint ConstructorCode => 0xf7aff1c0;

            public InputFile File;
            public string Caption;

            public InputMediaUploadedPhotoType() { }

            public InputMediaUploadedPhotoType(InputFile File, string Caption)
            {
                this.File = File;
                this.Caption = Caption;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                File.Write(writer);
                writer.Write(Caption);
            }

            public override void Read(TBinaryReader reader)
            {
                File = reader.Read<InputFile>();
                Caption = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(InputMediaUploadedPhotoType File:{0} Caption:{1})", File, Caption);
            }
        }

        public class InputMediaPhotoType : InputMedia
        {
            public override uint ConstructorCode => 0xe9bfb4f3;

            public InputPhoto Id;
            public string Caption;

            public InputMediaPhotoType() { }

            public InputMediaPhotoType(InputPhoto Id, string Caption)
            {
                this.Id = Id;
                this.Caption = Caption;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Id.Write(writer);
                writer.Write(Caption);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.Read<InputPhoto>();
                Caption = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(InputMediaPhotoType Id:{0} Caption:{1})", Id, Caption);
            }
        }

        public class InputMediaGeoPointType : InputMedia
        {
            public override uint ConstructorCode => 0xf9c44144;

            public InputGeoPoint GeoPoint;

            public InputMediaGeoPointType() { }

            public InputMediaGeoPointType(InputGeoPoint GeoPoint)
            {
                this.GeoPoint = GeoPoint;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                GeoPoint.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                GeoPoint = reader.Read<InputGeoPoint>();
            }

            public override string ToString()
            {
                return string.Format("(InputMediaGeoPointType GeoPoint:{0})", GeoPoint);
            }
        }

        public class InputMediaContactType : InputMedia
        {
            public override uint ConstructorCode => 0xa6e45987;

            public string PhoneNumber;
            public string FirstName;
            public string LastName;

            public InputMediaContactType() { }

            public InputMediaContactType(string PhoneNumber, string FirstName, string LastName)
            {
                this.PhoneNumber = PhoneNumber;
                this.FirstName = FirstName;
                this.LastName = LastName;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PhoneNumber);
                writer.Write(FirstName);
                writer.Write(LastName);
            }

            public override void Read(TBinaryReader reader)
            {
                PhoneNumber = reader.ReadString();
                FirstName = reader.ReadString();
                LastName = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(InputMediaContactType PhoneNumber:{0} FirstName:{1} LastName:{2})", PhoneNumber, FirstName, LastName);
            }
        }

        public class InputMediaUploadedDocumentType : InputMedia
        {
            public override uint ConstructorCode => 0x1d89306d;

            public InputFile File;
            public string MimeType;
            public List<DocumentAttribute> Attributes;
            public string Caption;

            public InputMediaUploadedDocumentType() { }

            public InputMediaUploadedDocumentType(InputFile File, string MimeType, List<DocumentAttribute> Attributes, string Caption)
            {
                this.File = File;
                this.MimeType = MimeType;
                this.Attributes = Attributes;
                this.Caption = Caption;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                File.Write(writer);
                writer.Write(MimeType);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Attributes.Count);
                foreach (DocumentAttribute AttributesElement in Attributes)
                    AttributesElement.Write(writer);
                writer.Write(Caption);
            }

            public override void Read(TBinaryReader reader)
            {
                File = reader.Read<InputFile>();
                MimeType = reader.ReadString();
                reader.ReadInt32(); // vector code
                int AttributesLength = reader.ReadInt32();
                Attributes = new List<DocumentAttribute>(AttributesLength);
                for (int AttributesIndex = 0; AttributesIndex < AttributesLength; AttributesIndex++)
                    Attributes.Add(reader.Read<DocumentAttribute>());
                Caption = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(InputMediaUploadedDocumentType File:{0} MimeType:{1} Attributes:{2} Caption:{3})", File, MimeType, Attributes, Caption);
            }
        }

        public class InputMediaUploadedThumbDocumentType : InputMedia
        {
            public override uint ConstructorCode => 0xad613491;

            public InputFile File;
            public InputFile Thumb;
            public string MimeType;
            public List<DocumentAttribute> Attributes;
            public string Caption;

            public InputMediaUploadedThumbDocumentType() { }

            public InputMediaUploadedThumbDocumentType(InputFile File, InputFile Thumb, string MimeType, List<DocumentAttribute> Attributes, string Caption)
            {
                this.File = File;
                this.Thumb = Thumb;
                this.MimeType = MimeType;
                this.Attributes = Attributes;
                this.Caption = Caption;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                File.Write(writer);
                Thumb.Write(writer);
                writer.Write(MimeType);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Attributes.Count);
                foreach (DocumentAttribute AttributesElement in Attributes)
                    AttributesElement.Write(writer);
                writer.Write(Caption);
            }

            public override void Read(TBinaryReader reader)
            {
                File = reader.Read<InputFile>();
                Thumb = reader.Read<InputFile>();
                MimeType = reader.ReadString();
                reader.ReadInt32(); // vector code
                int AttributesLength = reader.ReadInt32();
                Attributes = new List<DocumentAttribute>(AttributesLength);
                for (int AttributesIndex = 0; AttributesIndex < AttributesLength; AttributesIndex++)
                    Attributes.Add(reader.Read<DocumentAttribute>());
                Caption = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(InputMediaUploadedThumbDocumentType File:{0} Thumb:{1} MimeType:{2} Attributes:{3} Caption:{4})", File, Thumb, MimeType, Attributes, Caption);
            }
        }

        public class InputMediaDocumentType : InputMedia
        {
            public override uint ConstructorCode => 0x1a77f29c;

            public InputDocument Id;
            public string Caption;

            public InputMediaDocumentType() { }

            public InputMediaDocumentType(InputDocument Id, string Caption)
            {
                this.Id = Id;
                this.Caption = Caption;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Id.Write(writer);
                writer.Write(Caption);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.Read<InputDocument>();
                Caption = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(InputMediaDocumentType Id:{0} Caption:{1})", Id, Caption);
            }
        }

        public class InputMediaVenueType : InputMedia
        {
            public override uint ConstructorCode => 0x2827a81a;

            public InputGeoPoint GeoPoint;
            public string Title;
            public string Address;
            public string Provider;
            public string VenueId;

            public InputMediaVenueType() { }

            public InputMediaVenueType(InputGeoPoint GeoPoint, string Title, string Address, string Provider, string VenueId)
            {
                this.GeoPoint = GeoPoint;
                this.Title = Title;
                this.Address = Address;
                this.Provider = Provider;
                this.VenueId = VenueId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                GeoPoint.Write(writer);
                writer.Write(Title);
                writer.Write(Address);
                writer.Write(Provider);
                writer.Write(VenueId);
            }

            public override void Read(TBinaryReader reader)
            {
                GeoPoint = reader.Read<InputGeoPoint>();
                Title = reader.ReadString();
                Address = reader.ReadString();
                Provider = reader.ReadString();
                VenueId = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(InputMediaVenueType GeoPoint:{0} Title:{1} Address:{2} Provider:{3} VenueId:{4})", GeoPoint, Title, Address, Provider, VenueId);
            }
        }

        public class InputMediaGifExternalType : InputMedia
        {
            public override uint ConstructorCode => 0x4843b0fd;

            public string Url;
            public string Q;

            public InputMediaGifExternalType() { }

            public InputMediaGifExternalType(string Url, string Q)
            {
                this.Url = Url;
                this.Q = Q;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Url);
                writer.Write(Q);
            }

            public override void Read(TBinaryReader reader)
            {
                Url = reader.ReadString();
                Q = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(InputMediaGifExternalType Url:{0} Q:{1})", Url, Q);
            }
        }

        public class InputChatPhotoEmptyType : InputChatPhoto
        {
            public override uint ConstructorCode => 0x1ca48f57;

            public InputChatPhotoEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputChatPhotoEmptyType)";
            }
        }

        public class InputChatUploadedPhotoType : InputChatPhoto
        {
            public override uint ConstructorCode => 0x94254732;

            public InputFile File;
            public InputPhotoCrop Crop;

            public InputChatUploadedPhotoType() { }

            public InputChatUploadedPhotoType(InputFile File, InputPhotoCrop Crop)
            {
                this.File = File;
                this.Crop = Crop;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                File.Write(writer);
                Crop.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                File = reader.Read<InputFile>();
                Crop = reader.Read<InputPhotoCrop>();
            }

            public override string ToString()
            {
                return string.Format("(InputChatUploadedPhotoType File:{0} Crop:{1})", File, Crop);
            }
        }

        public class InputChatPhotoType : InputChatPhoto
        {
            public override uint ConstructorCode => 0xb2e1bf08;

            public InputPhoto Id;
            public InputPhotoCrop Crop;

            public InputChatPhotoType() { }

            public InputChatPhotoType(InputPhoto Id, InputPhotoCrop Crop)
            {
                this.Id = Id;
                this.Crop = Crop;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Id.Write(writer);
                Crop.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.Read<InputPhoto>();
                Crop = reader.Read<InputPhotoCrop>();
            }

            public override string ToString()
            {
                return string.Format("(InputChatPhotoType Id:{0} Crop:{1})", Id, Crop);
            }
        }

        public class InputGeoPointEmptyType : InputGeoPoint
        {
            public override uint ConstructorCode => 0xe4c123d6;

            public InputGeoPointEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputGeoPointEmptyType)";
            }
        }

        public class InputGeoPointType : InputGeoPoint
        {
            public override uint ConstructorCode => 0xf3b7acc9;

            public double Lat;
            public double Long;

            public InputGeoPointType() { }

            public InputGeoPointType(double Lat, double Long)
            {
                this.Lat = Lat;
                this.Long = Long;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Lat);
                writer.Write(Long);
            }

            public override void Read(TBinaryReader reader)
            {
                Lat = reader.ReadDouble();
                Long = reader.ReadDouble();
            }

            public override string ToString()
            {
                return string.Format("(InputGeoPointType Lat:{0} Long:{1})", Lat, Long);
            }
        }

        public class InputPhotoEmptyType : InputPhoto
        {
            public override uint ConstructorCode => 0x1cd7bf0d;

            public InputPhotoEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputPhotoEmptyType)";
            }
        }

        public class InputPhotoType : InputPhoto
        {
            public override uint ConstructorCode => 0xfb95c6c4;

            public long Id;
            public long AccessHash;

            public InputPhotoType() { }

            public InputPhotoType(long Id, long AccessHash)
            {
                this.Id = Id;
                this.AccessHash = AccessHash;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(AccessHash);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt64();
                AccessHash = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(InputPhotoType Id:{0} AccessHash:{1})", Id, AccessHash);
            }
        }

        public class InputFileLocationType : InputFileLocation
        {
            public override uint ConstructorCode => 0x14637196;

            public long VolumeId;
            public int LocalId;
            public long Secret;

            public InputFileLocationType() { }

            public InputFileLocationType(long VolumeId, int LocalId, long Secret)
            {
                this.VolumeId = VolumeId;
                this.LocalId = LocalId;
                this.Secret = Secret;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(VolumeId);
                writer.Write(LocalId);
                writer.Write(Secret);
            }

            public override void Read(TBinaryReader reader)
            {
                VolumeId = reader.ReadInt64();
                LocalId = reader.ReadInt32();
                Secret = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(InputFileLocationType VolumeId:{0} LocalId:{1} Secret:{2})", VolumeId, LocalId, Secret);
            }
        }

        public class InputEncryptedFileLocationType : InputFileLocation
        {
            public override uint ConstructorCode => 0xf5235d55;

            public long Id;
            public long AccessHash;

            public InputEncryptedFileLocationType() { }

            public InputEncryptedFileLocationType(long Id, long AccessHash)
            {
                this.Id = Id;
                this.AccessHash = AccessHash;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(AccessHash);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt64();
                AccessHash = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(InputEncryptedFileLocationType Id:{0} AccessHash:{1})", Id, AccessHash);
            }
        }

        public class InputDocumentFileLocationType : InputFileLocation
        {
            public override uint ConstructorCode => 0x4e45abe9;

            public long Id;
            public long AccessHash;

            public InputDocumentFileLocationType() { }

            public InputDocumentFileLocationType(long Id, long AccessHash)
            {
                this.Id = Id;
                this.AccessHash = AccessHash;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(AccessHash);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt64();
                AccessHash = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(InputDocumentFileLocationType Id:{0} AccessHash:{1})", Id, AccessHash);
            }
        }

        public class InputPhotoCropAutoType : InputPhotoCrop
        {
            public override uint ConstructorCode => 0xade6b004;

            public InputPhotoCropAutoType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputPhotoCropAutoType)";
            }
        }

        public class InputPhotoCropType : InputPhotoCrop
        {
            public override uint ConstructorCode => 0xd9915325;

            public double CropLeft;
            public double CropTop;
            public double CropWidth;

            public InputPhotoCropType() { }

            public InputPhotoCropType(double CropLeft, double CropTop, double CropWidth)
            {
                this.CropLeft = CropLeft;
                this.CropTop = CropTop;
                this.CropWidth = CropWidth;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(CropLeft);
                writer.Write(CropTop);
                writer.Write(CropWidth);
            }

            public override void Read(TBinaryReader reader)
            {
                CropLeft = reader.ReadDouble();
                CropTop = reader.ReadDouble();
                CropWidth = reader.ReadDouble();
            }

            public override string ToString()
            {
                return string.Format("(InputPhotoCropType CropLeft:{0} CropTop:{1} CropWidth:{2})", CropLeft, CropTop, CropWidth);
            }
        }

        public class InputAppEventType : InputAppEvent
        {
            public override uint ConstructorCode => 0x770656a8;

            public double Time;
            public string Type;
            public long Peer;
            public string Data;

            public InputAppEventType() { }

            public InputAppEventType(double Time, string Type, long Peer, string Data)
            {
                this.Time = Time;
                this.Type = Type;
                this.Peer = Peer;
                this.Data = Data;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Time);
                writer.Write(Type);
                writer.Write(Peer);
                writer.Write(Data);
            }

            public override void Read(TBinaryReader reader)
            {
                Time = reader.ReadDouble();
                Type = reader.ReadString();
                Peer = reader.ReadInt64();
                Data = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(InputAppEventType Time:{0} Type:{1} Peer:{2} Data:{3})", Time, Type, Peer, Data);
            }
        }

        public class PeerUserType : Peer
        {
            public override uint ConstructorCode => 0x9db1bc6d;

            public int UserId;

            public PeerUserType() { }

            public PeerUserType(int UserId)
            {
                this.UserId = UserId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(PeerUserType UserId:{0})", UserId);
            }
        }

        public class PeerChatType : Peer
        {
            public override uint ConstructorCode => 0xbad0e5bb;

            public int ChatId;

            public PeerChatType() { }

            public PeerChatType(int ChatId)
            {
                this.ChatId = ChatId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
            }

            public override void Read(TBinaryReader reader)
            {
                ChatId = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(PeerChatType ChatId:{0})", ChatId);
            }
        }

        public class PeerChannelType : Peer
        {
            public override uint ConstructorCode => 0xbddde532;

            public int ChannelId;

            public PeerChannelType() { }

            public PeerChannelType(int ChannelId)
            {
                this.ChannelId = ChannelId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChannelId);
            }

            public override void Read(TBinaryReader reader)
            {
                ChannelId = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(PeerChannelType ChannelId:{0})", ChannelId);
            }
        }

        public class StorageFileUnknownType : StorageFileType
        {
            public override uint ConstructorCode => 0xaa963b05;

            public StorageFileUnknownType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(StorageFileUnknownType)";
            }
        }

        public class StorageFileJpegType : StorageFileType
        {
            public override uint ConstructorCode => 0x007efe0e;

            public StorageFileJpegType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(StorageFileJpegType)";
            }
        }

        public class StorageFileGifType : StorageFileType
        {
            public override uint ConstructorCode => 0xcae1aadf;

            public StorageFileGifType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(StorageFileGifType)";
            }
        }

        public class StorageFilePngType : StorageFileType
        {
            public override uint ConstructorCode => 0x0a4f63c0;

            public StorageFilePngType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(StorageFilePngType)";
            }
        }

        public class StorageFilePdfType : StorageFileType
        {
            public override uint ConstructorCode => 0xae1e508d;

            public StorageFilePdfType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(StorageFilePdfType)";
            }
        }

        public class StorageFileMp3Type : StorageFileType
        {
            public override uint ConstructorCode => 0x528a0677;

            public StorageFileMp3Type() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(StorageFileMp3Type)";
            }
        }

        public class StorageFileMovType : StorageFileType
        {
            public override uint ConstructorCode => 0x4b09ebbc;

            public StorageFileMovType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(StorageFileMovType)";
            }
        }

        public class StorageFilePartialType : StorageFileType
        {
            public override uint ConstructorCode => 0x40bc6f52;

            public StorageFilePartialType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(StorageFilePartialType)";
            }
        }

        public class StorageFileMp4Type : StorageFileType
        {
            public override uint ConstructorCode => 0xb3cea0e4;

            public StorageFileMp4Type() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(StorageFileMp4Type)";
            }
        }

        public class StorageFileWebpType : StorageFileType
        {
            public override uint ConstructorCode => 0x1081464c;

            public StorageFileWebpType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(StorageFileWebpType)";
            }
        }

        public class FileLocationUnavailableType : FileLocation
        {
            public override uint ConstructorCode => 0x7c596b46;

            public long VolumeId;
            public int LocalId;
            public long Secret;

            public FileLocationUnavailableType() { }

            public FileLocationUnavailableType(long VolumeId, int LocalId, long Secret)
            {
                this.VolumeId = VolumeId;
                this.LocalId = LocalId;
                this.Secret = Secret;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(VolumeId);
                writer.Write(LocalId);
                writer.Write(Secret);
            }

            public override void Read(TBinaryReader reader)
            {
                VolumeId = reader.ReadInt64();
                LocalId = reader.ReadInt32();
                Secret = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(FileLocationUnavailableType VolumeId:{0} LocalId:{1} Secret:{2})", VolumeId, LocalId, Secret);
            }
        }

        public class FileLocationType : FileLocation
        {
            public override uint ConstructorCode => 0x53d69076;

            public int DcId;
            public long VolumeId;
            public int LocalId;
            public long Secret;

            public FileLocationType() { }

            public FileLocationType(int DcId, long VolumeId, int LocalId, long Secret)
            {
                this.DcId = DcId;
                this.VolumeId = VolumeId;
                this.LocalId = LocalId;
                this.Secret = Secret;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(DcId);
                writer.Write(VolumeId);
                writer.Write(LocalId);
                writer.Write(Secret);
            }

            public override void Read(TBinaryReader reader)
            {
                DcId = reader.ReadInt32();
                VolumeId = reader.ReadInt64();
                LocalId = reader.ReadInt32();
                Secret = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(FileLocationType DcId:{0} VolumeId:{1} LocalId:{2} Secret:{3})", DcId, VolumeId, LocalId, Secret);
            }
        }

        public class UserEmptyType : User
        {
            public override uint ConstructorCode => 0x200250ba;

            public int Id;

            public UserEmptyType() { }

            public UserEmptyType(int Id)
            {
                this.Id = Id;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UserEmptyType Id:{0})", Id);
            }
        }

        public class UserType : User
        {
            public override uint ConstructorCode => 0xd10d979a;

            public True Self;
            public True Contact;
            public True MutualContact;
            public True Deleted;
            public True Bot;
            public True BotChatHistory;
            public True BotNochats;
            public True Verified;
            public True Restricted;
            public int Id;
            public long? AccessHash;
            public string FirstName;
            public string LastName;
            public string Username;
            public string Phone;
            public UserProfilePhoto Photo;
            public UserStatus Status;
            public int? BotInfoVersion;
            public string RestrictionReason;
            public string BotInlinePlaceholder;

            public UserType() { }

            /// <summary>
            /// The following arguments can be null: Self, Contact, MutualContact, Deleted, Bot, BotChatHistory, BotNochats, Verified, Restricted, AccessHash, FirstName, LastName, Username, Phone, Photo, Status, BotInfoVersion, RestrictionReason, BotInlinePlaceholder
            /// </summary>
            /// <param name="Self">Can be null</param>
            /// <param name="Contact">Can be null</param>
            /// <param name="MutualContact">Can be null</param>
            /// <param name="Deleted">Can be null</param>
            /// <param name="Bot">Can be null</param>
            /// <param name="BotChatHistory">Can be null</param>
            /// <param name="BotNochats">Can be null</param>
            /// <param name="Verified">Can be null</param>
            /// <param name="Restricted">Can be null</param>
            /// <param name="Id">Can NOT be null</param>
            /// <param name="AccessHash">Can be null</param>
            /// <param name="FirstName">Can be null</param>
            /// <param name="LastName">Can be null</param>
            /// <param name="Username">Can be null</param>
            /// <param name="Phone">Can be null</param>
            /// <param name="Photo">Can be null</param>
            /// <param name="Status">Can be null</param>
            /// <param name="BotInfoVersion">Can be null</param>
            /// <param name="RestrictionReason">Can be null</param>
            /// <param name="BotInlinePlaceholder">Can be null</param>
            public UserType(True Self, True Contact, True MutualContact, True Deleted, True Bot, True BotChatHistory, True BotNochats, True Verified, True Restricted, int Id, long? AccessHash, string FirstName, string LastName, string Username, string Phone, UserProfilePhoto Photo, UserStatus Status, int? BotInfoVersion, string RestrictionReason, string BotInlinePlaceholder)
            {
                this.Self = Self;
                this.Contact = Contact;
                this.MutualContact = MutualContact;
                this.Deleted = Deleted;
                this.Bot = Bot;
                this.BotChatHistory = BotChatHistory;
                this.BotNochats = BotNochats;
                this.Verified = Verified;
                this.Restricted = Restricted;
                this.Id = Id;
                this.AccessHash = AccessHash;
                this.FirstName = FirstName;
                this.LastName = LastName;
                this.Username = Username;
                this.Phone = Phone;
                this.Photo = Photo;
                this.Status = Status;
                this.BotInfoVersion = BotInfoVersion;
                this.RestrictionReason = RestrictionReason;
                this.BotInlinePlaceholder = BotInlinePlaceholder;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Self != null ? 1 << 10 : 0) |
                    (Contact != null ? 1 << 11 : 0) |
                    (MutualContact != null ? 1 << 12 : 0) |
                    (Deleted != null ? 1 << 13 : 0) |
                    (Bot != null ? 1 << 14 : 0) |
                    (BotChatHistory != null ? 1 << 15 : 0) |
                    (BotNochats != null ? 1 << 16 : 0) |
                    (Verified != null ? 1 << 17 : 0) |
                    (Restricted != null ? 1 << 18 : 0) |
                    (AccessHash != null ? 1 << 0 : 0) |
                    (FirstName != null ? 1 << 1 : 0) |
                    (LastName != null ? 1 << 2 : 0) |
                    (Username != null ? 1 << 3 : 0) |
                    (Phone != null ? 1 << 4 : 0) |
                    (Photo != null ? 1 << 5 : 0) |
                    (Status != null ? 1 << 6 : 0) |
                    (BotInfoVersion != null ? 1 << 14 : 0) |
                    (RestrictionReason != null ? 1 << 18 : 0) |
                    (BotInlinePlaceholder != null ? 1 << 19 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Self != null) {

                }

                if (Contact != null) {

                }

                if (MutualContact != null) {

                }

                if (Deleted != null) {

                }

                if (Bot != null) {

                }

                if (BotChatHistory != null) {

                }

                if (BotNochats != null) {

                }

                if (Verified != null) {

                }

                if (Restricted != null) {

                }

                writer.Write(Id);
                if (AccessHash != null) {
                    writer.Write(AccessHash.Value);
                }

                if (FirstName != null) {
                    writer.Write(FirstName);
                }

                if (LastName != null) {
                    writer.Write(LastName);
                }

                if (Username != null) {
                    writer.Write(Username);
                }

                if (Phone != null) {
                    writer.Write(Phone);
                }

                if (Photo != null) {
                    Photo.Write(writer);
                }

                if (Status != null) {
                    Status.Write(writer);
                }

                if (BotInfoVersion != null) {
                    writer.Write(BotInfoVersion.Value);
                }

                if (RestrictionReason != null) {
                    writer.Write(RestrictionReason);
                }

                if (BotInlinePlaceholder != null) {
                    writer.Write(BotInlinePlaceholder);
                }

            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 10)) != 0) {
                    Self = reader.ReadTrue();
                }

                if ((flags & (1 << 11)) != 0) {
                    Contact = reader.ReadTrue();
                }

                if ((flags & (1 << 12)) != 0) {
                    MutualContact = reader.ReadTrue();
                }

                if ((flags & (1 << 13)) != 0) {
                    Deleted = reader.ReadTrue();
                }

                if ((flags & (1 << 14)) != 0) {
                    Bot = reader.ReadTrue();
                }

                if ((flags & (1 << 15)) != 0) {
                    BotChatHistory = reader.ReadTrue();
                }

                if ((flags & (1 << 16)) != 0) {
                    BotNochats = reader.ReadTrue();
                }

                if ((flags & (1 << 17)) != 0) {
                    Verified = reader.ReadTrue();
                }

                if ((flags & (1 << 18)) != 0) {
                    Restricted = reader.ReadTrue();
                }

                Id = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    AccessHash = reader.ReadInt64();
                }

                if ((flags & (1 << 1)) != 0) {
                    FirstName = reader.ReadString();
                }

                if ((flags & (1 << 2)) != 0) {
                    LastName = reader.ReadString();
                }

                if ((flags & (1 << 3)) != 0) {
                    Username = reader.ReadString();
                }

                if ((flags & (1 << 4)) != 0) {
                    Phone = reader.ReadString();
                }

                if ((flags & (1 << 5)) != 0) {
                    Photo = reader.Read<UserProfilePhoto>();
                }

                if ((flags & (1 << 6)) != 0) {
                    Status = reader.Read<UserStatus>();
                }

                if ((flags & (1 << 14)) != 0) {
                    BotInfoVersion = reader.ReadInt32();
                }

                if ((flags & (1 << 18)) != 0) {
                    RestrictionReason = reader.ReadString();
                }

                if ((flags & (1 << 19)) != 0) {
                    BotInlinePlaceholder = reader.ReadString();
                }

            }

            public override string ToString()
            {
                return string.Format("(UserType Self:{0} Contact:{1} MutualContact:{2} Deleted:{3} Bot:{4} BotChatHistory:{5} BotNochats:{6} Verified:{7} Restricted:{8} Id:{9} AccessHash:{10} FirstName:{11} LastName:{12} Username:{13} Phone:{14} Photo:{15} Status:{16} BotInfoVersion:{17} RestrictionReason:{18} BotInlinePlaceholder:{19})", Self, Contact, MutualContact, Deleted, Bot, BotChatHistory, BotNochats, Verified, Restricted, Id, AccessHash, FirstName, LastName, Username, Phone, Photo, Status, BotInfoVersion, RestrictionReason, BotInlinePlaceholder);
            }
        }

        public class UserProfilePhotoEmptyType : UserProfilePhoto
        {
            public override uint ConstructorCode => 0x4f11bae1;

            public UserProfilePhotoEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(UserProfilePhotoEmptyType)";
            }
        }

        public class UserProfilePhotoType : UserProfilePhoto
        {
            public override uint ConstructorCode => 0xd559d8c8;

            public long PhotoId;
            public FileLocation PhotoSmall;
            public FileLocation PhotoBig;

            public UserProfilePhotoType() { }

            public UserProfilePhotoType(long PhotoId, FileLocation PhotoSmall, FileLocation PhotoBig)
            {
                this.PhotoId = PhotoId;
                this.PhotoSmall = PhotoSmall;
                this.PhotoBig = PhotoBig;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PhotoId);
                PhotoSmall.Write(writer);
                PhotoBig.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                PhotoId = reader.ReadInt64();
                PhotoSmall = reader.Read<FileLocation>();
                PhotoBig = reader.Read<FileLocation>();
            }

            public override string ToString()
            {
                return string.Format("(UserProfilePhotoType PhotoId:{0} PhotoSmall:{1} PhotoBig:{2})", PhotoId, PhotoSmall, PhotoBig);
            }
        }

        public class UserStatusEmptyType : UserStatus
        {
            public override uint ConstructorCode => 0x09d05049;

            public UserStatusEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(UserStatusEmptyType)";
            }
        }

        public class UserStatusOnlineType : UserStatus
        {
            public override uint ConstructorCode => 0xedb93949;

            public int Expires;

            public UserStatusOnlineType() { }

            public UserStatusOnlineType(int Expires)
            {
                this.Expires = Expires;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Expires);
            }

            public override void Read(TBinaryReader reader)
            {
                Expires = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UserStatusOnlineType Expires:{0})", Expires);
            }
        }

        public class UserStatusOfflineType : UserStatus
        {
            public override uint ConstructorCode => 0x008c703f;

            public int WasOnline;

            public UserStatusOfflineType() { }

            public UserStatusOfflineType(int WasOnline)
            {
                this.WasOnline = WasOnline;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(WasOnline);
            }

            public override void Read(TBinaryReader reader)
            {
                WasOnline = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UserStatusOfflineType WasOnline:{0})", WasOnline);
            }
        }

        public class UserStatusRecentlyType : UserStatus
        {
            public override uint ConstructorCode => 0xe26f42f1;

            public UserStatusRecentlyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(UserStatusRecentlyType)";
            }
        }

        public class UserStatusLastWeekType : UserStatus
        {
            public override uint ConstructorCode => 0x07bf09fc;

            public UserStatusLastWeekType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(UserStatusLastWeekType)";
            }
        }

        public class UserStatusLastMonthType : UserStatus
        {
            public override uint ConstructorCode => 0x77ebc742;

            public UserStatusLastMonthType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(UserStatusLastMonthType)";
            }
        }

        public class ChatEmptyType : Chat
        {
            public override uint ConstructorCode => 0x9ba2d800;

            public int Id;

            public ChatEmptyType() { }

            public ChatEmptyType(int Id)
            {
                this.Id = Id;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(ChatEmptyType Id:{0})", Id);
            }
        }

        public class ChatType : Chat
        {
            public override uint ConstructorCode => 0xd91cdd54;

            public True Creator;
            public True Kicked;
            public True Left;
            public True AdminsEnabled;
            public True Admin;
            public True Deactivated;
            public int Id;
            public string Title;
            public ChatPhoto Photo;
            public int ParticipantsCount;
            public int Date;
            public int Version;
            public InputChannel MigratedTo;

            public ChatType() { }

            /// <summary>
            /// The following arguments can be null: Creator, Kicked, Left, AdminsEnabled, Admin, Deactivated, MigratedTo
            /// </summary>
            /// <param name="Creator">Can be null</param>
            /// <param name="Kicked">Can be null</param>
            /// <param name="Left">Can be null</param>
            /// <param name="AdminsEnabled">Can be null</param>
            /// <param name="Admin">Can be null</param>
            /// <param name="Deactivated">Can be null</param>
            /// <param name="Id">Can NOT be null</param>
            /// <param name="Title">Can NOT be null</param>
            /// <param name="Photo">Can NOT be null</param>
            /// <param name="ParticipantsCount">Can NOT be null</param>
            /// <param name="Date">Can NOT be null</param>
            /// <param name="Version">Can NOT be null</param>
            /// <param name="MigratedTo">Can be null</param>
            public ChatType(True Creator, True Kicked, True Left, True AdminsEnabled, True Admin, True Deactivated, int Id, string Title, ChatPhoto Photo, int ParticipantsCount, int Date, int Version, InputChannel MigratedTo)
            {
                this.Creator = Creator;
                this.Kicked = Kicked;
                this.Left = Left;
                this.AdminsEnabled = AdminsEnabled;
                this.Admin = Admin;
                this.Deactivated = Deactivated;
                this.Id = Id;
                this.Title = Title;
                this.Photo = Photo;
                this.ParticipantsCount = ParticipantsCount;
                this.Date = Date;
                this.Version = Version;
                this.MigratedTo = MigratedTo;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Creator != null ? 1 << 0 : 0) |
                    (Kicked != null ? 1 << 1 : 0) |
                    (Left != null ? 1 << 2 : 0) |
                    (AdminsEnabled != null ? 1 << 3 : 0) |
                    (Admin != null ? 1 << 4 : 0) |
                    (Deactivated != null ? 1 << 5 : 0) |
                    (MigratedTo != null ? 1 << 6 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Creator != null) {

                }

                if (Kicked != null) {

                }

                if (Left != null) {

                }

                if (AdminsEnabled != null) {

                }

                if (Admin != null) {

                }

                if (Deactivated != null) {

                }

                writer.Write(Id);
                writer.Write(Title);
                Photo.Write(writer);
                writer.Write(ParticipantsCount);
                writer.Write(Date);
                writer.Write(Version);
                if (MigratedTo != null) {
                    MigratedTo.Write(writer);
                }

            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    Creator = reader.ReadTrue();
                }

                if ((flags & (1 << 1)) != 0) {
                    Kicked = reader.ReadTrue();
                }

                if ((flags & (1 << 2)) != 0) {
                    Left = reader.ReadTrue();
                }

                if ((flags & (1 << 3)) != 0) {
                    AdminsEnabled = reader.ReadTrue();
                }

                if ((flags & (1 << 4)) != 0) {
                    Admin = reader.ReadTrue();
                }

                if ((flags & (1 << 5)) != 0) {
                    Deactivated = reader.ReadTrue();
                }

                Id = reader.ReadInt32();
                Title = reader.ReadString();
                Photo = reader.Read<ChatPhoto>();
                ParticipantsCount = reader.ReadInt32();
                Date = reader.ReadInt32();
                Version = reader.ReadInt32();
                if ((flags & (1 << 6)) != 0) {
                    MigratedTo = reader.Read<InputChannel>();
                }

            }

            public override string ToString()
            {
                return string.Format("(ChatType Creator:{0} Kicked:{1} Left:{2} AdminsEnabled:{3} Admin:{4} Deactivated:{5} Id:{6} Title:{7} Photo:{8} ParticipantsCount:{9} Date:{10} Version:{11} MigratedTo:{12})", Creator, Kicked, Left, AdminsEnabled, Admin, Deactivated, Id, Title, Photo, ParticipantsCount, Date, Version, MigratedTo);
            }
        }

        public class ChatForbiddenType : Chat
        {
            public override uint ConstructorCode => 0x07328bdb;

            public int Id;
            public string Title;

            public ChatForbiddenType() { }

            public ChatForbiddenType(int Id, string Title)
            {
                this.Id = Id;
                this.Title = Title;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(Title);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt32();
                Title = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(ChatForbiddenType Id:{0} Title:{1})", Id, Title);
            }
        }

        public class ChannelType : Chat
        {
            public override uint ConstructorCode => 0x4b1b7506;

            public True Creator;
            public True Kicked;
            public True Left;
            public True Editor;
            public True Moderator;
            public True Broadcast;
            public True Verified;
            public True Megagroup;
            public True Restricted;
            public True InvitesEnabled;
            public int Id;
            public long AccessHash;
            public string Title;
            public string Username;
            public ChatPhoto Photo;
            public int Date;
            public int Version;
            public string RestrictionReason;

            public ChannelType() { }

            /// <summary>
            /// The following arguments can be null: Creator, Kicked, Left, Editor, Moderator, Broadcast, Verified, Megagroup, Restricted, InvitesEnabled, Username, RestrictionReason
            /// </summary>
            /// <param name="Creator">Can be null</param>
            /// <param name="Kicked">Can be null</param>
            /// <param name="Left">Can be null</param>
            /// <param name="Editor">Can be null</param>
            /// <param name="Moderator">Can be null</param>
            /// <param name="Broadcast">Can be null</param>
            /// <param name="Verified">Can be null</param>
            /// <param name="Megagroup">Can be null</param>
            /// <param name="Restricted">Can be null</param>
            /// <param name="InvitesEnabled">Can be null</param>
            /// <param name="Id">Can NOT be null</param>
            /// <param name="AccessHash">Can NOT be null</param>
            /// <param name="Title">Can NOT be null</param>
            /// <param name="Username">Can be null</param>
            /// <param name="Photo">Can NOT be null</param>
            /// <param name="Date">Can NOT be null</param>
            /// <param name="Version">Can NOT be null</param>
            /// <param name="RestrictionReason">Can be null</param>
            public ChannelType(True Creator, True Kicked, True Left, True Editor, True Moderator, True Broadcast, True Verified, True Megagroup, True Restricted, True InvitesEnabled, int Id, long AccessHash, string Title, string Username, ChatPhoto Photo, int Date, int Version, string RestrictionReason)
            {
                this.Creator = Creator;
                this.Kicked = Kicked;
                this.Left = Left;
                this.Editor = Editor;
                this.Moderator = Moderator;
                this.Broadcast = Broadcast;
                this.Verified = Verified;
                this.Megagroup = Megagroup;
                this.Restricted = Restricted;
                this.InvitesEnabled = InvitesEnabled;
                this.Id = Id;
                this.AccessHash = AccessHash;
                this.Title = Title;
                this.Username = Username;
                this.Photo = Photo;
                this.Date = Date;
                this.Version = Version;
                this.RestrictionReason = RestrictionReason;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Creator != null ? 1 << 0 : 0) |
                    (Kicked != null ? 1 << 1 : 0) |
                    (Left != null ? 1 << 2 : 0) |
                    (Editor != null ? 1 << 3 : 0) |
                    (Moderator != null ? 1 << 4 : 0) |
                    (Broadcast != null ? 1 << 5 : 0) |
                    (Verified != null ? 1 << 7 : 0) |
                    (Megagroup != null ? 1 << 8 : 0) |
                    (Restricted != null ? 1 << 9 : 0) |
                    (InvitesEnabled != null ? 1 << 10 : 0) |
                    (Username != null ? 1 << 6 : 0) |
                    (RestrictionReason != null ? 1 << 9 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Creator != null) {

                }

                if (Kicked != null) {

                }

                if (Left != null) {

                }

                if (Editor != null) {

                }

                if (Moderator != null) {

                }

                if (Broadcast != null) {

                }

                if (Verified != null) {

                }

                if (Megagroup != null) {

                }

                if (Restricted != null) {

                }

                if (InvitesEnabled != null) {

                }

                writer.Write(Id);
                writer.Write(AccessHash);
                writer.Write(Title);
                if (Username != null) {
                    writer.Write(Username);
                }

                Photo.Write(writer);
                writer.Write(Date);
                writer.Write(Version);
                if (RestrictionReason != null) {
                    writer.Write(RestrictionReason);
                }

            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    Creator = reader.ReadTrue();
                }

                if ((flags & (1 << 1)) != 0) {
                    Kicked = reader.ReadTrue();
                }

                if ((flags & (1 << 2)) != 0) {
                    Left = reader.ReadTrue();
                }

                if ((flags & (1 << 3)) != 0) {
                    Editor = reader.ReadTrue();
                }

                if ((flags & (1 << 4)) != 0) {
                    Moderator = reader.ReadTrue();
                }

                if ((flags & (1 << 5)) != 0) {
                    Broadcast = reader.ReadTrue();
                }

                if ((flags & (1 << 7)) != 0) {
                    Verified = reader.ReadTrue();
                }

                if ((flags & (1 << 8)) != 0) {
                    Megagroup = reader.ReadTrue();
                }

                if ((flags & (1 << 9)) != 0) {
                    Restricted = reader.ReadTrue();
                }

                if ((flags & (1 << 10)) != 0) {
                    InvitesEnabled = reader.ReadTrue();
                }

                Id = reader.ReadInt32();
                AccessHash = reader.ReadInt64();
                Title = reader.ReadString();
                if ((flags & (1 << 6)) != 0) {
                    Username = reader.ReadString();
                }

                Photo = reader.Read<ChatPhoto>();
                Date = reader.ReadInt32();
                Version = reader.ReadInt32();
                if ((flags & (1 << 9)) != 0) {
                    RestrictionReason = reader.ReadString();
                }

            }

            public override string ToString()
            {
                return string.Format("(ChannelType Creator:{0} Kicked:{1} Left:{2} Editor:{3} Moderator:{4} Broadcast:{5} Verified:{6} Megagroup:{7} Restricted:{8} InvitesEnabled:{9} Id:{10} AccessHash:{11} Title:{12} Username:{13} Photo:{14} Date:{15} Version:{16} RestrictionReason:{17})", Creator, Kicked, Left, Editor, Moderator, Broadcast, Verified, Megagroup, Restricted, InvitesEnabled, Id, AccessHash, Title, Username, Photo, Date, Version, RestrictionReason);
            }
        }

        public class ChannelForbiddenType : Chat
        {
            public override uint ConstructorCode => 0x2d85832c;

            public int Id;
            public long AccessHash;
            public string Title;

            public ChannelForbiddenType() { }

            public ChannelForbiddenType(int Id, long AccessHash, string Title)
            {
                this.Id = Id;
                this.AccessHash = AccessHash;
                this.Title = Title;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(AccessHash);
                writer.Write(Title);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt32();
                AccessHash = reader.ReadInt64();
                Title = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(ChannelForbiddenType Id:{0} AccessHash:{1} Title:{2})", Id, AccessHash, Title);
            }
        }

        public class ChatFullType : ChatFull
        {
            public override uint ConstructorCode => 0x2e02a614;

            public int Id;
            public ChatParticipants Participants;
            public Photo ChatPhoto;
            public PeerNotifySettings NotifySettings;
            public ExportedChatInvite ExportedInvite;
            public List<BotInfo> BotInfo;

            public ChatFullType() { }

            public ChatFullType(int Id, ChatParticipants Participants, Photo ChatPhoto, PeerNotifySettings NotifySettings, ExportedChatInvite ExportedInvite, List<BotInfo> BotInfo)
            {
                this.Id = Id;
                this.Participants = Participants;
                this.ChatPhoto = ChatPhoto;
                this.NotifySettings = NotifySettings;
                this.ExportedInvite = ExportedInvite;
                this.BotInfo = BotInfo;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                Participants.Write(writer);
                ChatPhoto.Write(writer);
                NotifySettings.Write(writer);
                ExportedInvite.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(BotInfo.Count);
                foreach (BotInfo BotInfoElement in BotInfo)
                    BotInfoElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt32();
                Participants = reader.Read<ChatParticipants>();
                ChatPhoto = reader.Read<Photo>();
                NotifySettings = reader.Read<PeerNotifySettings>();
                ExportedInvite = reader.Read<ExportedChatInvite>();
                reader.ReadInt32(); // vector code
                int BotInfoLength = reader.ReadInt32();
                BotInfo = new List<BotInfo>(BotInfoLength);
                for (int BotInfoIndex = 0; BotInfoIndex < BotInfoLength; BotInfoIndex++)
                    BotInfo.Add(reader.Read<BotInfo>());
            }

            public override string ToString()
            {
                return string.Format("(ChatFullType Id:{0} Participants:{1} ChatPhoto:{2} NotifySettings:{3} ExportedInvite:{4} BotInfo:{5})", Id, Participants, ChatPhoto, NotifySettings, ExportedInvite, BotInfo);
            }
        }

        public class ChannelFullType : ChatFull
        {
            public override uint ConstructorCode => 0x9e341ddf;

            public True CanViewParticipants;
            public int Id;
            public string About;
            public int? ParticipantsCount;
            public int? AdminsCount;
            public int? KickedCount;
            public int ReadInboxMaxId;
            public int UnreadCount;
            public int UnreadImportantCount;
            public Photo ChatPhoto;
            public PeerNotifySettings NotifySettings;
            public ExportedChatInvite ExportedInvite;
            public List<BotInfo> BotInfo;
            public int? MigratedFromChatId;
            public int? MigratedFromMaxId;

            public ChannelFullType() { }

            /// <summary>
            /// The following arguments can be null: CanViewParticipants, ParticipantsCount, AdminsCount, KickedCount, MigratedFromChatId, MigratedFromMaxId
            /// </summary>
            /// <param name="CanViewParticipants">Can be null</param>
            /// <param name="Id">Can NOT be null</param>
            /// <param name="About">Can NOT be null</param>
            /// <param name="ParticipantsCount">Can be null</param>
            /// <param name="AdminsCount">Can be null</param>
            /// <param name="KickedCount">Can be null</param>
            /// <param name="ReadInboxMaxId">Can NOT be null</param>
            /// <param name="UnreadCount">Can NOT be null</param>
            /// <param name="UnreadImportantCount">Can NOT be null</param>
            /// <param name="ChatPhoto">Can NOT be null</param>
            /// <param name="NotifySettings">Can NOT be null</param>
            /// <param name="ExportedInvite">Can NOT be null</param>
            /// <param name="BotInfo">Can NOT be null</param>
            /// <param name="MigratedFromChatId">Can be null</param>
            /// <param name="MigratedFromMaxId">Can be null</param>
            public ChannelFullType(True CanViewParticipants, int Id, string About, int? ParticipantsCount, int? AdminsCount, int? KickedCount, int ReadInboxMaxId, int UnreadCount, int UnreadImportantCount, Photo ChatPhoto, PeerNotifySettings NotifySettings, ExportedChatInvite ExportedInvite, List<BotInfo> BotInfo, int? MigratedFromChatId, int? MigratedFromMaxId)
            {
                this.CanViewParticipants = CanViewParticipants;
                this.Id = Id;
                this.About = About;
                this.ParticipantsCount = ParticipantsCount;
                this.AdminsCount = AdminsCount;
                this.KickedCount = KickedCount;
                this.ReadInboxMaxId = ReadInboxMaxId;
                this.UnreadCount = UnreadCount;
                this.UnreadImportantCount = UnreadImportantCount;
                this.ChatPhoto = ChatPhoto;
                this.NotifySettings = NotifySettings;
                this.ExportedInvite = ExportedInvite;
                this.BotInfo = BotInfo;
                this.MigratedFromChatId = MigratedFromChatId;
                this.MigratedFromMaxId = MigratedFromMaxId;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (CanViewParticipants != null ? 1 << 3 : 0) |
                    (ParticipantsCount != null ? 1 << 0 : 0) |
                    (AdminsCount != null ? 1 << 1 : 0) |
                    (KickedCount != null ? 1 << 2 : 0) |
                    (MigratedFromChatId != null ? 1 << 4 : 0) |
                    (MigratedFromMaxId != null ? 1 << 4 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (CanViewParticipants != null) {

                }

                writer.Write(Id);
                writer.Write(About);
                if (ParticipantsCount != null) {
                    writer.Write(ParticipantsCount.Value);
                }

                if (AdminsCount != null) {
                    writer.Write(AdminsCount.Value);
                }

                if (KickedCount != null) {
                    writer.Write(KickedCount.Value);
                }

                writer.Write(ReadInboxMaxId);
                writer.Write(UnreadCount);
                writer.Write(UnreadImportantCount);
                ChatPhoto.Write(writer);
                NotifySettings.Write(writer);
                ExportedInvite.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(BotInfo.Count);
                foreach (BotInfo BotInfoElement in BotInfo)
                    BotInfoElement.Write(writer);
                if (MigratedFromChatId != null) {
                    writer.Write(MigratedFromChatId.Value);
                }

                if (MigratedFromMaxId != null) {
                    writer.Write(MigratedFromMaxId.Value);
                }

            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 3)) != 0) {
                    CanViewParticipants = reader.ReadTrue();
                }

                Id = reader.ReadInt32();
                About = reader.ReadString();
                if ((flags & (1 << 0)) != 0) {
                    ParticipantsCount = reader.ReadInt32();
                }

                if ((flags & (1 << 1)) != 0) {
                    AdminsCount = reader.ReadInt32();
                }

                if ((flags & (1 << 2)) != 0) {
                    KickedCount = reader.ReadInt32();
                }

                ReadInboxMaxId = reader.ReadInt32();
                UnreadCount = reader.ReadInt32();
                UnreadImportantCount = reader.ReadInt32();
                ChatPhoto = reader.Read<Photo>();
                NotifySettings = reader.Read<PeerNotifySettings>();
                ExportedInvite = reader.Read<ExportedChatInvite>();
                reader.ReadInt32(); // vector code
                int BotInfoLength = reader.ReadInt32();
                BotInfo = new List<BotInfo>(BotInfoLength);
                for (int BotInfoIndex = 0; BotInfoIndex < BotInfoLength; BotInfoIndex++)
                    BotInfo.Add(reader.Read<BotInfo>());
                if ((flags & (1 << 4)) != 0) {
                    MigratedFromChatId = reader.ReadInt32();
                }

                if ((flags & (1 << 4)) != 0) {
                    MigratedFromMaxId = reader.ReadInt32();
                }

            }

            public override string ToString()
            {
                return string.Format("(ChannelFullType CanViewParticipants:{0} Id:{1} About:{2} ParticipantsCount:{3} AdminsCount:{4} KickedCount:{5} ReadInboxMaxId:{6} UnreadCount:{7} UnreadImportantCount:{8} ChatPhoto:{9} NotifySettings:{10} ExportedInvite:{11} BotInfo:{12} MigratedFromChatId:{13} MigratedFromMaxId:{14})", CanViewParticipants, Id, About, ParticipantsCount, AdminsCount, KickedCount, ReadInboxMaxId, UnreadCount, UnreadImportantCount, ChatPhoto, NotifySettings, ExportedInvite, BotInfo, MigratedFromChatId, MigratedFromMaxId);
            }
        }

        public class ChatParticipantType : ChatParticipant
        {
            public override uint ConstructorCode => 0xc8d7493e;

            public int UserId;
            public int InviterId;
            public int Date;

            public ChatParticipantType() { }

            public ChatParticipantType(int UserId, int InviterId, int Date)
            {
                this.UserId = UserId;
                this.InviterId = InviterId;
                this.Date = Date;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(InviterId);
                writer.Write(Date);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                InviterId = reader.ReadInt32();
                Date = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(ChatParticipantType UserId:{0} InviterId:{1} Date:{2})", UserId, InviterId, Date);
            }
        }

        public class ChatParticipantCreatorType : ChatParticipant
        {
            public override uint ConstructorCode => 0xda13538a;

            public int UserId;

            public ChatParticipantCreatorType() { }

            public ChatParticipantCreatorType(int UserId)
            {
                this.UserId = UserId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(ChatParticipantCreatorType UserId:{0})", UserId);
            }
        }

        public class ChatParticipantAdminType : ChatParticipant
        {
            public override uint ConstructorCode => 0xe2d6e436;

            public int UserId;
            public int InviterId;
            public int Date;

            public ChatParticipantAdminType() { }

            public ChatParticipantAdminType(int UserId, int InviterId, int Date)
            {
                this.UserId = UserId;
                this.InviterId = InviterId;
                this.Date = Date;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(InviterId);
                writer.Write(Date);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                InviterId = reader.ReadInt32();
                Date = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(ChatParticipantAdminType UserId:{0} InviterId:{1} Date:{2})", UserId, InviterId, Date);
            }
        }

        public class ChatParticipantsForbiddenType : ChatParticipants
        {
            public override uint ConstructorCode => 0xfc900c2b;

            public int ChatId;
            public ChatParticipant SelfParticipant;

            public ChatParticipantsForbiddenType() { }

            /// <summary>
            /// The following arguments can be null: SelfParticipant
            /// </summary>
            /// <param name="ChatId">Can NOT be null</param>
            /// <param name="SelfParticipant">Can be null</param>
            public ChatParticipantsForbiddenType(int ChatId, ChatParticipant SelfParticipant)
            {
                this.ChatId = ChatId;
                this.SelfParticipant = SelfParticipant;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (SelfParticipant != null ? 1 << 0 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                writer.Write(ChatId);
                if (SelfParticipant != null) {
                    SelfParticipant.Write(writer);
                }

            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                ChatId = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    SelfParticipant = reader.Read<ChatParticipant>();
                }

            }

            public override string ToString()
            {
                return string.Format("(ChatParticipantsForbiddenType ChatId:{0} SelfParticipant:{1})", ChatId, SelfParticipant);
            }
        }

        public class ChatParticipantsType : ChatParticipants
        {
            public override uint ConstructorCode => 0x3f460fed;

            public int ChatId;
            public List<ChatParticipant> Participants;
            public int Version;

            public ChatParticipantsType() { }

            public ChatParticipantsType(int ChatId, List<ChatParticipant> Participants, int Version)
            {
                this.ChatId = ChatId;
                this.Participants = Participants;
                this.Version = Version;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Participants.Count);
                foreach (ChatParticipant ParticipantsElement in Participants)
                    ParticipantsElement.Write(writer);
                writer.Write(Version);
            }

            public override void Read(TBinaryReader reader)
            {
                ChatId = reader.ReadInt32();
                reader.ReadInt32(); // vector code
                int ParticipantsLength = reader.ReadInt32();
                Participants = new List<ChatParticipant>(ParticipantsLength);
                for (int ParticipantsIndex = 0; ParticipantsIndex < ParticipantsLength; ParticipantsIndex++)
                    Participants.Add(reader.Read<ChatParticipant>());
                Version = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(ChatParticipantsType ChatId:{0} Participants:{1} Version:{2})", ChatId, Participants, Version);
            }
        }

        public class ChatPhotoEmptyType : ChatPhoto
        {
            public override uint ConstructorCode => 0x37c1011c;

            public ChatPhotoEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(ChatPhotoEmptyType)";
            }
        }

        public class ChatPhotoType : ChatPhoto
        {
            public override uint ConstructorCode => 0x6153276a;

            public FileLocation PhotoSmall;
            public FileLocation PhotoBig;

            public ChatPhotoType() { }

            public ChatPhotoType(FileLocation PhotoSmall, FileLocation PhotoBig)
            {
                this.PhotoSmall = PhotoSmall;
                this.PhotoBig = PhotoBig;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                PhotoSmall.Write(writer);
                PhotoBig.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                PhotoSmall = reader.Read<FileLocation>();
                PhotoBig = reader.Read<FileLocation>();
            }

            public override string ToString()
            {
                return string.Format("(ChatPhotoType PhotoSmall:{0} PhotoBig:{1})", PhotoSmall, PhotoBig);
            }
        }

        public class MessageEmptyType : Message
        {
            public override uint ConstructorCode => 0x83e5de54;

            public int Id;

            public MessageEmptyType() { }

            public MessageEmptyType(int Id)
            {
                this.Id = Id;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessageEmptyType Id:{0})", Id);
            }
        }

        public class MessageType : Message
        {
            public override uint ConstructorCode => 0xc992e15c;

            public True Unread;
            public True Out;
            public True Mentioned;
            public True MediaUnread;
            public int Id;
            public int? FromId;
            public Peer ToId;
            public Peer FwdFromId;
            public int? FwdDate;
            public int? ViaBotId;
            public int? ReplyToMsgId;
            public int Date;
            public string Message;
            public MessageMedia Media;
            public ReplyMarkup ReplyMarkup;
            public List<MessageEntity> Entities;
            public int? Views;

            public MessageType() { }

            /// <summary>
            /// The following arguments can be null: Unread, Out, Mentioned, MediaUnread, FromId, FwdFromId, FwdDate, ViaBotId, ReplyToMsgId, Media, ReplyMarkup, Entities, Views
            /// </summary>
            /// <param name="Unread">Can be null</param>
            /// <param name="Out">Can be null</param>
            /// <param name="Mentioned">Can be null</param>
            /// <param name="MediaUnread">Can be null</param>
            /// <param name="Id">Can NOT be null</param>
            /// <param name="FromId">Can be null</param>
            /// <param name="ToId">Can NOT be null</param>
            /// <param name="FwdFromId">Can be null</param>
            /// <param name="FwdDate">Can be null</param>
            /// <param name="ViaBotId">Can be null</param>
            /// <param name="ReplyToMsgId">Can be null</param>
            /// <param name="Date">Can NOT be null</param>
            /// <param name="Message">Can NOT be null</param>
            /// <param name="Media">Can be null</param>
            /// <param name="ReplyMarkup">Can be null</param>
            /// <param name="Entities">Can be null</param>
            /// <param name="Views">Can be null</param>
            public MessageType(True Unread, True Out, True Mentioned, True MediaUnread, int Id, int? FromId, Peer ToId, Peer FwdFromId, int? FwdDate, int? ViaBotId, int? ReplyToMsgId, int Date, string Message, MessageMedia Media, ReplyMarkup ReplyMarkup, List<MessageEntity> Entities, int? Views)
            {
                this.Unread = Unread;
                this.Out = Out;
                this.Mentioned = Mentioned;
                this.MediaUnread = MediaUnread;
                this.Id = Id;
                this.FromId = FromId;
                this.ToId = ToId;
                this.FwdFromId = FwdFromId;
                this.FwdDate = FwdDate;
                this.ViaBotId = ViaBotId;
                this.ReplyToMsgId = ReplyToMsgId;
                this.Date = Date;
                this.Message = Message;
                this.Media = Media;
                this.ReplyMarkup = ReplyMarkup;
                this.Entities = Entities;
                this.Views = Views;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Unread != null ? 1 << 0 : 0) |
                    (Out != null ? 1 << 1 : 0) |
                    (Mentioned != null ? 1 << 4 : 0) |
                    (MediaUnread != null ? 1 << 5 : 0) |
                    (FromId != null ? 1 << 8 : 0) |
                    (FwdFromId != null ? 1 << 2 : 0) |
                    (FwdDate != null ? 1 << 2 : 0) |
                    (ViaBotId != null ? 1 << 11 : 0) |
                    (ReplyToMsgId != null ? 1 << 3 : 0) |
                    (Media != null ? 1 << 9 : 0) |
                    (ReplyMarkup != null ? 1 << 6 : 0) |
                    (Entities != null ? 1 << 7 : 0) |
                    (Views != null ? 1 << 10 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Unread != null) {

                }

                if (Out != null) {

                }

                if (Mentioned != null) {

                }

                if (MediaUnread != null) {

                }

                writer.Write(Id);
                if (FromId != null) {
                    writer.Write(FromId.Value);
                }

                ToId.Write(writer);
                if (FwdFromId != null) {
                    FwdFromId.Write(writer);
                }

                if (FwdDate != null) {
                    writer.Write(FwdDate.Value);
                }

                if (ViaBotId != null) {
                    writer.Write(ViaBotId.Value);
                }

                if (ReplyToMsgId != null) {
                    writer.Write(ReplyToMsgId.Value);
                }

                writer.Write(Date);
                writer.Write(Message);
                if (Media != null) {
                    Media.Write(writer);
                }

                if (ReplyMarkup != null) {
                    ReplyMarkup.Write(writer);
                }

                if (Entities != null) {
                    writer.Write(0x1cb5c415); // vector code
                    writer.Write(Entities.Count);
                    foreach (MessageEntity EntitiesElement in Entities)
                        EntitiesElement.Write(writer);
                }

                if (Views != null) {
                    writer.Write(Views.Value);
                }

            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    Unread = reader.ReadTrue();
                }

                if ((flags & (1 << 1)) != 0) {
                    Out = reader.ReadTrue();
                }

                if ((flags & (1 << 4)) != 0) {
                    Mentioned = reader.ReadTrue();
                }

                if ((flags & (1 << 5)) != 0) {
                    MediaUnread = reader.ReadTrue();
                }

                Id = reader.ReadInt32();
                if ((flags & (1 << 8)) != 0) {
                    FromId = reader.ReadInt32();
                }

                ToId = reader.Read<Peer>();
                if ((flags & (1 << 2)) != 0) {
                    FwdFromId = reader.Read<Peer>();
                }

                if ((flags & (1 << 2)) != 0) {
                    FwdDate = reader.ReadInt32();
                }

                if ((flags & (1 << 11)) != 0) {
                    ViaBotId = reader.ReadInt32();
                }

                if ((flags & (1 << 3)) != 0) {
                    ReplyToMsgId = reader.ReadInt32();
                }

                Date = reader.ReadInt32();
                Message = reader.ReadString();
                if ((flags & (1 << 9)) != 0) {
                    Media = reader.Read<MessageMedia>();
                }

                if ((flags & (1 << 6)) != 0) {
                    ReplyMarkup = reader.Read<ReplyMarkup>();
                }

                if ((flags & (1 << 7)) != 0) {
                    reader.ReadInt32(); // vector code
                    int EntitiesLength = reader.ReadInt32();
                    Entities = new List<MessageEntity>(EntitiesLength);
                    for (int EntitiesIndex = 0; EntitiesIndex < EntitiesLength; EntitiesIndex++)
                        Entities.Add(reader.Read<MessageEntity>());
                    }

                if ((flags & (1 << 10)) != 0) {
                    Views = reader.ReadInt32();
                }

            }

            public override string ToString()
            {
                return string.Format("(MessageType Unread:{0} Out:{1} Mentioned:{2} MediaUnread:{3} Id:{4} FromId:{5} ToId:{6} FwdFromId:{7} FwdDate:{8} ViaBotId:{9} ReplyToMsgId:{10} Date:{11} Message:{12} Media:{13} ReplyMarkup:{14} Entities:{15} Views:{16})", Unread, Out, Mentioned, MediaUnread, Id, FromId, ToId, FwdFromId, FwdDate, ViaBotId, ReplyToMsgId, Date, Message, Media, ReplyMarkup, Entities, Views);
            }
        }

        public class MessageServiceType : Message
        {
            public override uint ConstructorCode => 0xc06b9607;

            public True Unread;
            public True Out;
            public True Mentioned;
            public True MediaUnread;
            public int Id;
            public int? FromId;
            public Peer ToId;
            public int Date;
            public MessageAction Action;

            public MessageServiceType() { }

            /// <summary>
            /// The following arguments can be null: Unread, Out, Mentioned, MediaUnread, FromId
            /// </summary>
            /// <param name="Unread">Can be null</param>
            /// <param name="Out">Can be null</param>
            /// <param name="Mentioned">Can be null</param>
            /// <param name="MediaUnread">Can be null</param>
            /// <param name="Id">Can NOT be null</param>
            /// <param name="FromId">Can be null</param>
            /// <param name="ToId">Can NOT be null</param>
            /// <param name="Date">Can NOT be null</param>
            /// <param name="Action">Can NOT be null</param>
            public MessageServiceType(True Unread, True Out, True Mentioned, True MediaUnread, int Id, int? FromId, Peer ToId, int Date, MessageAction Action)
            {
                this.Unread = Unread;
                this.Out = Out;
                this.Mentioned = Mentioned;
                this.MediaUnread = MediaUnread;
                this.Id = Id;
                this.FromId = FromId;
                this.ToId = ToId;
                this.Date = Date;
                this.Action = Action;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Unread != null ? 1 << 0 : 0) |
                    (Out != null ? 1 << 1 : 0) |
                    (Mentioned != null ? 1 << 4 : 0) |
                    (MediaUnread != null ? 1 << 5 : 0) |
                    (FromId != null ? 1 << 8 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Unread != null) {

                }

                if (Out != null) {

                }

                if (Mentioned != null) {

                }

                if (MediaUnread != null) {

                }

                writer.Write(Id);
                if (FromId != null) {
                    writer.Write(FromId.Value);
                }

                ToId.Write(writer);
                writer.Write(Date);
                Action.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    Unread = reader.ReadTrue();
                }

                if ((flags & (1 << 1)) != 0) {
                    Out = reader.ReadTrue();
                }

                if ((flags & (1 << 4)) != 0) {
                    Mentioned = reader.ReadTrue();
                }

                if ((flags & (1 << 5)) != 0) {
                    MediaUnread = reader.ReadTrue();
                }

                Id = reader.ReadInt32();
                if ((flags & (1 << 8)) != 0) {
                    FromId = reader.ReadInt32();
                }

                ToId = reader.Read<Peer>();
                Date = reader.ReadInt32();
                Action = reader.Read<MessageAction>();
            }

            public override string ToString()
            {
                return string.Format("(MessageServiceType Unread:{0} Out:{1} Mentioned:{2} MediaUnread:{3} Id:{4} FromId:{5} ToId:{6} Date:{7} Action:{8})", Unread, Out, Mentioned, MediaUnread, Id, FromId, ToId, Date, Action);
            }
        }

        public class MessageMediaEmptyType : MessageMedia
        {
            public override uint ConstructorCode => 0x3ded6320;

            public MessageMediaEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(MessageMediaEmptyType)";
            }
        }

        public class MessageMediaPhotoType : MessageMedia
        {
            public override uint ConstructorCode => 0x3d8ce53d;

            public Photo Photo;
            public string Caption;

            public MessageMediaPhotoType() { }

            public MessageMediaPhotoType(Photo Photo, string Caption)
            {
                this.Photo = Photo;
                this.Caption = Caption;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Photo.Write(writer);
                writer.Write(Caption);
            }

            public override void Read(TBinaryReader reader)
            {
                Photo = reader.Read<Photo>();
                Caption = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(MessageMediaPhotoType Photo:{0} Caption:{1})", Photo, Caption);
            }
        }

        public class MessageMediaGeoType : MessageMedia
        {
            public override uint ConstructorCode => 0x56e0d474;

            public GeoPoint Geo;

            public MessageMediaGeoType() { }

            public MessageMediaGeoType(GeoPoint Geo)
            {
                this.Geo = Geo;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Geo.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Geo = reader.Read<GeoPoint>();
            }

            public override string ToString()
            {
                return string.Format("(MessageMediaGeoType Geo:{0})", Geo);
            }
        }

        public class MessageMediaContactType : MessageMedia
        {
            public override uint ConstructorCode => 0x5e7d2f39;

            public string PhoneNumber;
            public string FirstName;
            public string LastName;
            public int UserId;

            public MessageMediaContactType() { }

            public MessageMediaContactType(string PhoneNumber, string FirstName, string LastName, int UserId)
            {
                this.PhoneNumber = PhoneNumber;
                this.FirstName = FirstName;
                this.LastName = LastName;
                this.UserId = UserId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PhoneNumber);
                writer.Write(FirstName);
                writer.Write(LastName);
                writer.Write(UserId);
            }

            public override void Read(TBinaryReader reader)
            {
                PhoneNumber = reader.ReadString();
                FirstName = reader.ReadString();
                LastName = reader.ReadString();
                UserId = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessageMediaContactType PhoneNumber:{0} FirstName:{1} LastName:{2} UserId:{3})", PhoneNumber, FirstName, LastName, UserId);
            }
        }

        public class MessageMediaUnsupportedType : MessageMedia
        {
            public override uint ConstructorCode => 0x9f84f49e;

            public MessageMediaUnsupportedType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(MessageMediaUnsupportedType)";
            }
        }

        public class MessageMediaDocumentType : MessageMedia
        {
            public override uint ConstructorCode => 0xf3e02ea8;

            public Document Document;
            public string Caption;

            public MessageMediaDocumentType() { }

            public MessageMediaDocumentType(Document Document, string Caption)
            {
                this.Document = Document;
                this.Caption = Caption;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Document.Write(writer);
                writer.Write(Caption);
            }

            public override void Read(TBinaryReader reader)
            {
                Document = reader.Read<Document>();
                Caption = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(MessageMediaDocumentType Document:{0} Caption:{1})", Document, Caption);
            }
        }

        public class MessageMediaWebPageType : MessageMedia
        {
            public override uint ConstructorCode => 0xa32dd600;

            public WebPage Webpage;

            public MessageMediaWebPageType() { }

            public MessageMediaWebPageType(WebPage Webpage)
            {
                this.Webpage = Webpage;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Webpage.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Webpage = reader.Read<WebPage>();
            }

            public override string ToString()
            {
                return string.Format("(MessageMediaWebPageType Webpage:{0})", Webpage);
            }
        }

        public class MessageMediaVenueType : MessageMedia
        {
            public override uint ConstructorCode => 0x7912b71f;

            public GeoPoint Geo;
            public string Title;
            public string Address;
            public string Provider;
            public string VenueId;

            public MessageMediaVenueType() { }

            public MessageMediaVenueType(GeoPoint Geo, string Title, string Address, string Provider, string VenueId)
            {
                this.Geo = Geo;
                this.Title = Title;
                this.Address = Address;
                this.Provider = Provider;
                this.VenueId = VenueId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Geo.Write(writer);
                writer.Write(Title);
                writer.Write(Address);
                writer.Write(Provider);
                writer.Write(VenueId);
            }

            public override void Read(TBinaryReader reader)
            {
                Geo = reader.Read<GeoPoint>();
                Title = reader.ReadString();
                Address = reader.ReadString();
                Provider = reader.ReadString();
                VenueId = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(MessageMediaVenueType Geo:{0} Title:{1} Address:{2} Provider:{3} VenueId:{4})", Geo, Title, Address, Provider, VenueId);
            }
        }

        public class MessageActionEmptyType : MessageAction
        {
            public override uint ConstructorCode => 0xb6aef7b0;

            public MessageActionEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(MessageActionEmptyType)";
            }
        }

        public class MessageActionChatCreateType : MessageAction
        {
            public override uint ConstructorCode => 0xa6638b9a;

            public string Title;
            public List<int> Users;

            public MessageActionChatCreateType() { }

            public MessageActionChatCreateType(string Title, List<int> Users)
            {
                this.Title = Title;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Title);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (int UsersElement in Users)
                    writer.Write(UsersElement);
            }

            public override void Read(TBinaryReader reader)
            {
                Title = reader.ReadString();
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<int>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.ReadInt32());
            }

            public override string ToString()
            {
                return string.Format("(MessageActionChatCreateType Title:{0} Users:{1})", Title, Users);
            }
        }

        public class MessageActionChatEditTitleType : MessageAction
        {
            public override uint ConstructorCode => 0xb5a1ce5a;

            public string Title;

            public MessageActionChatEditTitleType() { }

            public MessageActionChatEditTitleType(string Title)
            {
                this.Title = Title;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Title);
            }

            public override void Read(TBinaryReader reader)
            {
                Title = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(MessageActionChatEditTitleType Title:{0})", Title);
            }
        }

        public class MessageActionChatEditPhotoType : MessageAction
        {
            public override uint ConstructorCode => 0x7fcb13a8;

            public Photo Photo;

            public MessageActionChatEditPhotoType() { }

            public MessageActionChatEditPhotoType(Photo Photo)
            {
                this.Photo = Photo;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Photo.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Photo = reader.Read<Photo>();
            }

            public override string ToString()
            {
                return string.Format("(MessageActionChatEditPhotoType Photo:{0})", Photo);
            }
        }

        public class MessageActionChatDeletePhotoType : MessageAction
        {
            public override uint ConstructorCode => 0x95e3fbef;

            public MessageActionChatDeletePhotoType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(MessageActionChatDeletePhotoType)";
            }
        }

        public class MessageActionChatAddUserType : MessageAction
        {
            public override uint ConstructorCode => 0x488a7337;

            public List<int> Users;

            public MessageActionChatAddUserType() { }

            public MessageActionChatAddUserType(List<int> Users)
            {
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (int UsersElement in Users)
                    writer.Write(UsersElement);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<int>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.ReadInt32());
            }

            public override string ToString()
            {
                return string.Format("(MessageActionChatAddUserType Users:{0})", Users);
            }
        }

        public class MessageActionChatDeleteUserType : MessageAction
        {
            public override uint ConstructorCode => 0xb2ae9b0c;

            public int UserId;

            public MessageActionChatDeleteUserType() { }

            public MessageActionChatDeleteUserType(int UserId)
            {
                this.UserId = UserId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessageActionChatDeleteUserType UserId:{0})", UserId);
            }
        }

        public class MessageActionChatJoinedByLinkType : MessageAction
        {
            public override uint ConstructorCode => 0xf89cf5e8;

            public int InviterId;

            public MessageActionChatJoinedByLinkType() { }

            public MessageActionChatJoinedByLinkType(int InviterId)
            {
                this.InviterId = InviterId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(InviterId);
            }

            public override void Read(TBinaryReader reader)
            {
                InviterId = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessageActionChatJoinedByLinkType InviterId:{0})", InviterId);
            }
        }

        public class MessageActionChannelCreateType : MessageAction
        {
            public override uint ConstructorCode => 0x95d2ac92;

            public string Title;

            public MessageActionChannelCreateType() { }

            public MessageActionChannelCreateType(string Title)
            {
                this.Title = Title;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Title);
            }

            public override void Read(TBinaryReader reader)
            {
                Title = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(MessageActionChannelCreateType Title:{0})", Title);
            }
        }

        public class MessageActionChatMigrateToType : MessageAction
        {
            public override uint ConstructorCode => 0x51bdb021;

            public int ChannelId;

            public MessageActionChatMigrateToType() { }

            public MessageActionChatMigrateToType(int ChannelId)
            {
                this.ChannelId = ChannelId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChannelId);
            }

            public override void Read(TBinaryReader reader)
            {
                ChannelId = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessageActionChatMigrateToType ChannelId:{0})", ChannelId);
            }
        }

        public class MessageActionChannelMigrateFromType : MessageAction
        {
            public override uint ConstructorCode => 0xb055eaee;

            public string Title;
            public int ChatId;

            public MessageActionChannelMigrateFromType() { }

            public MessageActionChannelMigrateFromType(string Title, int ChatId)
            {
                this.Title = Title;
                this.ChatId = ChatId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Title);
                writer.Write(ChatId);
            }

            public override void Read(TBinaryReader reader)
            {
                Title = reader.ReadString();
                ChatId = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessageActionChannelMigrateFromType Title:{0} ChatId:{1})", Title, ChatId);
            }
        }

        public class DialogType : Dialog
        {
            public override uint ConstructorCode => 0xc1dd804a;

            public Peer Peer;
            public int TopMessage;
            public int ReadInboxMaxId;
            public int UnreadCount;
            public PeerNotifySettings NotifySettings;

            public DialogType() { }

            public DialogType(Peer Peer, int TopMessage, int ReadInboxMaxId, int UnreadCount, PeerNotifySettings NotifySettings)
            {
                this.Peer = Peer;
                this.TopMessage = TopMessage;
                this.ReadInboxMaxId = ReadInboxMaxId;
                this.UnreadCount = UnreadCount;
                this.NotifySettings = NotifySettings;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                writer.Write(TopMessage);
                writer.Write(ReadInboxMaxId);
                writer.Write(UnreadCount);
                NotifySettings.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Peer = reader.Read<Peer>();
                TopMessage = reader.ReadInt32();
                ReadInboxMaxId = reader.ReadInt32();
                UnreadCount = reader.ReadInt32();
                NotifySettings = reader.Read<PeerNotifySettings>();
            }

            public override string ToString()
            {
                return string.Format("(DialogType Peer:{0} TopMessage:{1} ReadInboxMaxId:{2} UnreadCount:{3} NotifySettings:{4})", Peer, TopMessage, ReadInboxMaxId, UnreadCount, NotifySettings);
            }
        }

        public class DialogChannelType : Dialog
        {
            public override uint ConstructorCode => 0x5b8496b2;

            public Peer Peer;
            public int TopMessage;
            public int TopImportantMessage;
            public int ReadInboxMaxId;
            public int UnreadCount;
            public int UnreadImportantCount;
            public PeerNotifySettings NotifySettings;
            public int Pts;

            public DialogChannelType() { }

            public DialogChannelType(Peer Peer, int TopMessage, int TopImportantMessage, int ReadInboxMaxId, int UnreadCount, int UnreadImportantCount, PeerNotifySettings NotifySettings, int Pts)
            {
                this.Peer = Peer;
                this.TopMessage = TopMessage;
                this.TopImportantMessage = TopImportantMessage;
                this.ReadInboxMaxId = ReadInboxMaxId;
                this.UnreadCount = UnreadCount;
                this.UnreadImportantCount = UnreadImportantCount;
                this.NotifySettings = NotifySettings;
                this.Pts = Pts;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                writer.Write(TopMessage);
                writer.Write(TopImportantMessage);
                writer.Write(ReadInboxMaxId);
                writer.Write(UnreadCount);
                writer.Write(UnreadImportantCount);
                NotifySettings.Write(writer);
                writer.Write(Pts);
            }

            public override void Read(TBinaryReader reader)
            {
                Peer = reader.Read<Peer>();
                TopMessage = reader.ReadInt32();
                TopImportantMessage = reader.ReadInt32();
                ReadInboxMaxId = reader.ReadInt32();
                UnreadCount = reader.ReadInt32();
                UnreadImportantCount = reader.ReadInt32();
                NotifySettings = reader.Read<PeerNotifySettings>();
                Pts = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(DialogChannelType Peer:{0} TopMessage:{1} TopImportantMessage:{2} ReadInboxMaxId:{3} UnreadCount:{4} UnreadImportantCount:{5} NotifySettings:{6} Pts:{7})", Peer, TopMessage, TopImportantMessage, ReadInboxMaxId, UnreadCount, UnreadImportantCount, NotifySettings, Pts);
            }
        }

        public class PhotoEmptyType : Photo
        {
            public override uint ConstructorCode => 0x2331b22d;

            public long Id;

            public PhotoEmptyType() { }

            public PhotoEmptyType(long Id)
            {
                this.Id = Id;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(PhotoEmptyType Id:{0})", Id);
            }
        }

        public class PhotoType : Photo
        {
            public override uint ConstructorCode => 0xcded42fe;

            public long Id;
            public long AccessHash;
            public int Date;
            public List<PhotoSize> Sizes;

            public PhotoType() { }

            public PhotoType(long Id, long AccessHash, int Date, List<PhotoSize> Sizes)
            {
                this.Id = Id;
                this.AccessHash = AccessHash;
                this.Date = Date;
                this.Sizes = Sizes;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(AccessHash);
                writer.Write(Date);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Sizes.Count);
                foreach (PhotoSize SizesElement in Sizes)
                    SizesElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt64();
                AccessHash = reader.ReadInt64();
                Date = reader.ReadInt32();
                reader.ReadInt32(); // vector code
                int SizesLength = reader.ReadInt32();
                Sizes = new List<PhotoSize>(SizesLength);
                for (int SizesIndex = 0; SizesIndex < SizesLength; SizesIndex++)
                    Sizes.Add(reader.Read<PhotoSize>());
            }

            public override string ToString()
            {
                return string.Format("(PhotoType Id:{0} AccessHash:{1} Date:{2} Sizes:{3})", Id, AccessHash, Date, Sizes);
            }
        }

        public class PhotoSizeEmptyType : PhotoSize
        {
            public override uint ConstructorCode => 0x0e17e23c;

            public string Type;

            public PhotoSizeEmptyType() { }

            public PhotoSizeEmptyType(string Type)
            {
                this.Type = Type;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Type);
            }

            public override void Read(TBinaryReader reader)
            {
                Type = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(PhotoSizeEmptyType Type:{0})", Type);
            }
        }

        public class PhotoSizeType : PhotoSize
        {
            public override uint ConstructorCode => 0x77bfb61b;

            public string Type;
            public FileLocation Location;
            public int W;
            public int H;
            public int Size;

            public PhotoSizeType() { }

            public PhotoSizeType(string Type, FileLocation Location, int W, int H, int Size)
            {
                this.Type = Type;
                this.Location = Location;
                this.W = W;
                this.H = H;
                this.Size = Size;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Type);
                Location.Write(writer);
                writer.Write(W);
                writer.Write(H);
                writer.Write(Size);
            }

            public override void Read(TBinaryReader reader)
            {
                Type = reader.ReadString();
                Location = reader.Read<FileLocation>();
                W = reader.ReadInt32();
                H = reader.ReadInt32();
                Size = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(PhotoSizeType Type:{0} Location:{1} W:{2} H:{3} Size:{4})", Type, Location, W, H, Size);
            }
        }

        public class PhotoCachedSizeType : PhotoSize
        {
            public override uint ConstructorCode => 0xe9a734fa;

            public string Type;
            public FileLocation Location;
            public int W;
            public int H;
            public byte[] Bytes;

            public PhotoCachedSizeType() { }

            public PhotoCachedSizeType(string Type, FileLocation Location, int W, int H, byte[] Bytes)
            {
                this.Type = Type;
                this.Location = Location;
                this.W = W;
                this.H = H;
                this.Bytes = Bytes;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Type);
                Location.Write(writer);
                writer.Write(W);
                writer.Write(H);
                writer.Write(Bytes);
            }

            public override void Read(TBinaryReader reader)
            {
                Type = reader.ReadString();
                Location = reader.Read<FileLocation>();
                W = reader.ReadInt32();
                H = reader.ReadInt32();
                Bytes = reader.ReadBytes();
            }

            public override string ToString()
            {
                return string.Format("(PhotoCachedSizeType Type:{0} Location:{1} W:{2} H:{3} Bytes:{4})", Type, Location, W, H, Bytes);
            }
        }

        public class GeoPointEmptyType : GeoPoint
        {
            public override uint ConstructorCode => 0x1117dd5f;

            public GeoPointEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(GeoPointEmptyType)";
            }
        }

        public class GeoPointType : GeoPoint
        {
            public override uint ConstructorCode => 0x2049d70c;

            public double Long;
            public double Lat;

            public GeoPointType() { }

            public GeoPointType(double Long, double Lat)
            {
                this.Long = Long;
                this.Lat = Lat;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Long);
                writer.Write(Lat);
            }

            public override void Read(TBinaryReader reader)
            {
                Long = reader.ReadDouble();
                Lat = reader.ReadDouble();
            }

            public override string ToString()
            {
                return string.Format("(GeoPointType Long:{0} Lat:{1})", Long, Lat);
            }
        }

        public class AuthCheckedPhoneType : AuthCheckedPhone
        {
            public override uint ConstructorCode => 0x811ea28e;

            public bool PhoneRegistered;

            public AuthCheckedPhoneType() { }

            public AuthCheckedPhoneType(bool PhoneRegistered)
            {
                this.PhoneRegistered = PhoneRegistered;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PhoneRegistered);
            }

            public override void Read(TBinaryReader reader)
            {
                PhoneRegistered = reader.ReadBoolean();
            }

            public override string ToString()
            {
                return string.Format("(AuthCheckedPhoneType PhoneRegistered:{0})", PhoneRegistered);
            }
        }

        public class AuthSentCodeType : AuthSentCode
        {
            public override uint ConstructorCode => 0xefed51d9;

            public bool PhoneRegistered;
            public string PhoneCodeHash;
            public int SendCallTimeout;
            public bool IsPassword;

            public AuthSentCodeType() { }

            public AuthSentCodeType(bool PhoneRegistered, string PhoneCodeHash, int SendCallTimeout, bool IsPassword)
            {
                this.PhoneRegistered = PhoneRegistered;
                this.PhoneCodeHash = PhoneCodeHash;
                this.SendCallTimeout = SendCallTimeout;
                this.IsPassword = IsPassword;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PhoneRegistered);
                writer.Write(PhoneCodeHash);
                writer.Write(SendCallTimeout);
                writer.Write(IsPassword);
            }

            public override void Read(TBinaryReader reader)
            {
                PhoneRegistered = reader.ReadBoolean();
                PhoneCodeHash = reader.ReadString();
                SendCallTimeout = reader.ReadInt32();
                IsPassword = reader.ReadBoolean();
            }

            public override string ToString()
            {
                return string.Format("(AuthSentCodeType PhoneRegistered:{0} PhoneCodeHash:{1} SendCallTimeout:{2} IsPassword:{3})", PhoneRegistered, PhoneCodeHash, SendCallTimeout, IsPassword);
            }
        }

        public class AuthSentAppCodeType : AuthSentCode
        {
            public override uint ConstructorCode => 0xe325edcf;

            public bool PhoneRegistered;
            public string PhoneCodeHash;
            public int SendCallTimeout;
            public bool IsPassword;

            public AuthSentAppCodeType() { }

            public AuthSentAppCodeType(bool PhoneRegistered, string PhoneCodeHash, int SendCallTimeout, bool IsPassword)
            {
                this.PhoneRegistered = PhoneRegistered;
                this.PhoneCodeHash = PhoneCodeHash;
                this.SendCallTimeout = SendCallTimeout;
                this.IsPassword = IsPassword;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PhoneRegistered);
                writer.Write(PhoneCodeHash);
                writer.Write(SendCallTimeout);
                writer.Write(IsPassword);
            }

            public override void Read(TBinaryReader reader)
            {
                PhoneRegistered = reader.ReadBoolean();
                PhoneCodeHash = reader.ReadString();
                SendCallTimeout = reader.ReadInt32();
                IsPassword = reader.ReadBoolean();
            }

            public override string ToString()
            {
                return string.Format("(AuthSentAppCodeType PhoneRegistered:{0} PhoneCodeHash:{1} SendCallTimeout:{2} IsPassword:{3})", PhoneRegistered, PhoneCodeHash, SendCallTimeout, IsPassword);
            }
        }

        public class AuthAuthorizationType : AuthAuthorization
        {
            public override uint ConstructorCode => 0xff036af1;

            public User User;

            public AuthAuthorizationType() { }

            public AuthAuthorizationType(User User)
            {
                this.User = User;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                User.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                User = reader.Read<User>();
            }

            public override string ToString()
            {
                return string.Format("(AuthAuthorizationType User:{0})", User);
            }
        }

        public class AuthExportedAuthorizationType : AuthExportedAuthorization
        {
            public override uint ConstructorCode => 0xdf969c2d;

            public int Id;
            public byte[] Bytes;

            public AuthExportedAuthorizationType() { }

            public AuthExportedAuthorizationType(int Id, byte[] Bytes)
            {
                this.Id = Id;
                this.Bytes = Bytes;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(Bytes);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt32();
                Bytes = reader.ReadBytes();
            }

            public override string ToString()
            {
                return string.Format("(AuthExportedAuthorizationType Id:{0} Bytes:{1})", Id, Bytes);
            }
        }

        public class InputNotifyPeerType : InputNotifyPeer
        {
            public override uint ConstructorCode => 0xb8bc5b0c;

            public InputPeer Peer;

            public InputNotifyPeerType() { }

            public InputNotifyPeerType(InputPeer Peer)
            {
                this.Peer = Peer;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Peer = reader.Read<InputPeer>();
            }

            public override string ToString()
            {
                return string.Format("(InputNotifyPeerType Peer:{0})", Peer);
            }
        }

        public class InputNotifyUsersType : InputNotifyPeer
        {
            public override uint ConstructorCode => 0x193b4417;

            public InputNotifyUsersType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputNotifyUsersType)";
            }
        }

        public class InputNotifyChatsType : InputNotifyPeer
        {
            public override uint ConstructorCode => 0x4a95e84e;

            public InputNotifyChatsType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputNotifyChatsType)";
            }
        }

        public class InputNotifyAllType : InputNotifyPeer
        {
            public override uint ConstructorCode => 0xa429b886;

            public InputNotifyAllType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputNotifyAllType)";
            }
        }

        public class InputPeerNotifyEventsEmptyType : InputPeerNotifyEvents
        {
            public override uint ConstructorCode => 0xf03064d8;

            public InputPeerNotifyEventsEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputPeerNotifyEventsEmptyType)";
            }
        }

        public class InputPeerNotifyEventsAllType : InputPeerNotifyEvents
        {
            public override uint ConstructorCode => 0xe86a2c74;

            public InputPeerNotifyEventsAllType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputPeerNotifyEventsAllType)";
            }
        }

        public class InputPeerNotifySettingsType : InputPeerNotifySettings
        {
            public override uint ConstructorCode => 0x46a2ce98;

            public int MuteUntil;
            public string Sound;
            public bool ShowPreviews;
            public int EventsMask;

            public InputPeerNotifySettingsType() { }

            public InputPeerNotifySettingsType(int MuteUntil, string Sound, bool ShowPreviews, int EventsMask)
            {
                this.MuteUntil = MuteUntil;
                this.Sound = Sound;
                this.ShowPreviews = ShowPreviews;
                this.EventsMask = EventsMask;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(MuteUntil);
                writer.Write(Sound);
                writer.Write(ShowPreviews);
                writer.Write(EventsMask);
            }

            public override void Read(TBinaryReader reader)
            {
                MuteUntil = reader.ReadInt32();
                Sound = reader.ReadString();
                ShowPreviews = reader.ReadBoolean();
                EventsMask = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(InputPeerNotifySettingsType MuteUntil:{0} Sound:{1} ShowPreviews:{2} EventsMask:{3})", MuteUntil, Sound, ShowPreviews, EventsMask);
            }
        }

        public class PeerNotifyEventsEmptyType : PeerNotifyEvents
        {
            public override uint ConstructorCode => 0xadd53cb3;

            public PeerNotifyEventsEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(PeerNotifyEventsEmptyType)";
            }
        }

        public class PeerNotifyEventsAllType : PeerNotifyEvents
        {
            public override uint ConstructorCode => 0x6d1ded88;

            public PeerNotifyEventsAllType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(PeerNotifyEventsAllType)";
            }
        }

        public class PeerNotifySettingsEmptyType : PeerNotifySettings
        {
            public override uint ConstructorCode => 0x70a68512;

            public PeerNotifySettingsEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(PeerNotifySettingsEmptyType)";
            }
        }

        public class PeerNotifySettingsType : PeerNotifySettings
        {
            public override uint ConstructorCode => 0x8d5e11ee;

            public int MuteUntil;
            public string Sound;
            public bool ShowPreviews;
            public int EventsMask;

            public PeerNotifySettingsType() { }

            public PeerNotifySettingsType(int MuteUntil, string Sound, bool ShowPreviews, int EventsMask)
            {
                this.MuteUntil = MuteUntil;
                this.Sound = Sound;
                this.ShowPreviews = ShowPreviews;
                this.EventsMask = EventsMask;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(MuteUntil);
                writer.Write(Sound);
                writer.Write(ShowPreviews);
                writer.Write(EventsMask);
            }

            public override void Read(TBinaryReader reader)
            {
                MuteUntil = reader.ReadInt32();
                Sound = reader.ReadString();
                ShowPreviews = reader.ReadBoolean();
                EventsMask = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(PeerNotifySettingsType MuteUntil:{0} Sound:{1} ShowPreviews:{2} EventsMask:{3})", MuteUntil, Sound, ShowPreviews, EventsMask);
            }
        }

        public class WallPaperType : WallPaper
        {
            public override uint ConstructorCode => 0xccb03657;

            public int Id;
            public string Title;
            public List<PhotoSize> Sizes;
            public int Color;

            public WallPaperType() { }

            public WallPaperType(int Id, string Title, List<PhotoSize> Sizes, int Color)
            {
                this.Id = Id;
                this.Title = Title;
                this.Sizes = Sizes;
                this.Color = Color;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(Title);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Sizes.Count);
                foreach (PhotoSize SizesElement in Sizes)
                    SizesElement.Write(writer);
                writer.Write(Color);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt32();
                Title = reader.ReadString();
                reader.ReadInt32(); // vector code
                int SizesLength = reader.ReadInt32();
                Sizes = new List<PhotoSize>(SizesLength);
                for (int SizesIndex = 0; SizesIndex < SizesLength; SizesIndex++)
                    Sizes.Add(reader.Read<PhotoSize>());
                Color = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(WallPaperType Id:{0} Title:{1} Sizes:{2} Color:{3})", Id, Title, Sizes, Color);
            }
        }

        public class WallPaperSolidType : WallPaper
        {
            public override uint ConstructorCode => 0x63117f24;

            public int Id;
            public string Title;
            public int BgColor;
            public int Color;

            public WallPaperSolidType() { }

            public WallPaperSolidType(int Id, string Title, int BgColor, int Color)
            {
                this.Id = Id;
                this.Title = Title;
                this.BgColor = BgColor;
                this.Color = Color;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(Title);
                writer.Write(BgColor);
                writer.Write(Color);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt32();
                Title = reader.ReadString();
                BgColor = reader.ReadInt32();
                Color = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(WallPaperSolidType Id:{0} Title:{1} BgColor:{2} Color:{3})", Id, Title, BgColor, Color);
            }
        }

        public class InputReportReasonSpamType : ReportReason
        {
            public override uint ConstructorCode => 0x58dbcab8;

            public InputReportReasonSpamType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputReportReasonSpamType)";
            }
        }

        public class InputReportReasonViolenceType : ReportReason
        {
            public override uint ConstructorCode => 0x1e22c78d;

            public InputReportReasonViolenceType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputReportReasonViolenceType)";
            }
        }

        public class InputReportReasonPornographyType : ReportReason
        {
            public override uint ConstructorCode => 0x2e59d922;

            public InputReportReasonPornographyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputReportReasonPornographyType)";
            }
        }

        public class InputReportReasonOtherType : ReportReason
        {
            public override uint ConstructorCode => 0xe1746d0a;

            public string Text;

            public InputReportReasonOtherType() { }

            public InputReportReasonOtherType(string Text)
            {
                this.Text = Text;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Text);
            }

            public override void Read(TBinaryReader reader)
            {
                Text = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(InputReportReasonOtherType Text:{0})", Text);
            }
        }

        public class UserFullType : UserFull
        {
            public override uint ConstructorCode => 0x5a89ac5b;

            public User User;
            public ContactsLink Link;
            public Photo ProfilePhoto;
            public PeerNotifySettings NotifySettings;
            public bool Blocked;
            public BotInfo BotInfo;

            public UserFullType() { }

            public UserFullType(User User, ContactsLink Link, Photo ProfilePhoto, PeerNotifySettings NotifySettings, bool Blocked, BotInfo BotInfo)
            {
                this.User = User;
                this.Link = Link;
                this.ProfilePhoto = ProfilePhoto;
                this.NotifySettings = NotifySettings;
                this.Blocked = Blocked;
                this.BotInfo = BotInfo;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                User.Write(writer);
                Link.Write(writer);
                ProfilePhoto.Write(writer);
                NotifySettings.Write(writer);
                writer.Write(Blocked);
                BotInfo.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                User = reader.Read<User>();
                Link = reader.Read<ContactsLink>();
                ProfilePhoto = reader.Read<Photo>();
                NotifySettings = reader.Read<PeerNotifySettings>();
                Blocked = reader.ReadBoolean();
                BotInfo = reader.Read<BotInfo>();
            }

            public override string ToString()
            {
                return string.Format("(UserFullType User:{0} Link:{1} ProfilePhoto:{2} NotifySettings:{3} Blocked:{4} BotInfo:{5})", User, Link, ProfilePhoto, NotifySettings, Blocked, BotInfo);
            }
        }

        public class ContactType : Contact
        {
            public override uint ConstructorCode => 0xf911c994;

            public int UserId;
            public bool Mutual;

            public ContactType() { }

            public ContactType(int UserId, bool Mutual)
            {
                this.UserId = UserId;
                this.Mutual = Mutual;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(Mutual);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                Mutual = reader.ReadBoolean();
            }

            public override string ToString()
            {
                return string.Format("(ContactType UserId:{0} Mutual:{1})", UserId, Mutual);
            }
        }

        public class ImportedContactType : ImportedContact
        {
            public override uint ConstructorCode => 0xd0028438;

            public int UserId;
            public long ClientId;

            public ImportedContactType() { }

            public ImportedContactType(int UserId, long ClientId)
            {
                this.UserId = UserId;
                this.ClientId = ClientId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(ClientId);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                ClientId = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(ImportedContactType UserId:{0} ClientId:{1})", UserId, ClientId);
            }
        }

        public class ContactBlockedType : ContactBlocked
        {
            public override uint ConstructorCode => 0x561bc879;

            public int UserId;
            public int Date;

            public ContactBlockedType() { }

            public ContactBlockedType(int UserId, int Date)
            {
                this.UserId = UserId;
                this.Date = Date;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(Date);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                Date = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(ContactBlockedType UserId:{0} Date:{1})", UserId, Date);
            }
        }

        public class ContactStatusType : ContactStatus
        {
            public override uint ConstructorCode => 0xd3680c61;

            public int UserId;
            public UserStatus Status;

            public ContactStatusType() { }

            public ContactStatusType(int UserId, UserStatus Status)
            {
                this.UserId = UserId;
                this.Status = Status;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                Status.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                Status = reader.Read<UserStatus>();
            }

            public override string ToString()
            {
                return string.Format("(ContactStatusType UserId:{0} Status:{1})", UserId, Status);
            }
        }

        public class ContactsLinkType : ContactsLink
        {
            public override uint ConstructorCode => 0x3ace484c;

            public ContactLink MyLink;
            public ContactLink ForeignLink;
            public User User;

            public ContactsLinkType() { }

            public ContactsLinkType(ContactLink MyLink, ContactLink ForeignLink, User User)
            {
                this.MyLink = MyLink;
                this.ForeignLink = ForeignLink;
                this.User = User;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                MyLink.Write(writer);
                ForeignLink.Write(writer);
                User.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                MyLink = reader.Read<ContactLink>();
                ForeignLink = reader.Read<ContactLink>();
                User = reader.Read<User>();
            }

            public override string ToString()
            {
                return string.Format("(ContactsLinkType MyLink:{0} ForeignLink:{1} User:{2})", MyLink, ForeignLink, User);
            }
        }

        public class ContactsContactsNotModifiedType : ContactsContacts
        {
            public override uint ConstructorCode => 0xb74ba9d2;

            public ContactsContactsNotModifiedType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(ContactsContactsNotModifiedType)";
            }
        }

        public class ContactsContactsType : ContactsContacts
        {
            public override uint ConstructorCode => 0x6f8b8cb2;

            public List<Contact> Contacts;
            public List<User> Users;

            public ContactsContactsType() { }

            public ContactsContactsType(List<Contact> Contacts, List<User> Users)
            {
                this.Contacts = Contacts;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Contacts.Count);
                foreach (Contact ContactsElement in Contacts)
                    ContactsElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int ContactsLength = reader.ReadInt32();
                Contacts = new List<Contact>(ContactsLength);
                for (int ContactsIndex = 0; ContactsIndex < ContactsLength; ContactsIndex++)
                    Contacts.Add(reader.Read<Contact>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(ContactsContactsType Contacts:{0} Users:{1})", Contacts, Users);
            }
        }

        public class ContactsImportedContactsType : ContactsImportedContacts
        {
            public override uint ConstructorCode => 0xad524315;

            public List<ImportedContact> Imported;
            public List<long> RetryContacts;
            public List<User> Users;

            public ContactsImportedContactsType() { }

            public ContactsImportedContactsType(List<ImportedContact> Imported, List<long> RetryContacts, List<User> Users)
            {
                this.Imported = Imported;
                this.RetryContacts = RetryContacts;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Imported.Count);
                foreach (ImportedContact ImportedElement in Imported)
                    ImportedElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(RetryContacts.Count);
                foreach (long RetryContactsElement in RetryContacts)
                    writer.Write(RetryContactsElement);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int ImportedLength = reader.ReadInt32();
                Imported = new List<ImportedContact>(ImportedLength);
                for (int ImportedIndex = 0; ImportedIndex < ImportedLength; ImportedIndex++)
                    Imported.Add(reader.Read<ImportedContact>());
                reader.ReadInt32(); // vector code
                int RetryContactsLength = reader.ReadInt32();
                RetryContacts = new List<long>(RetryContactsLength);
                for (int RetryContactsIndex = 0; RetryContactsIndex < RetryContactsLength; RetryContactsIndex++)
                    RetryContacts.Add(reader.ReadInt64());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(ContactsImportedContactsType Imported:{0} RetryContacts:{1} Users:{2})", Imported, RetryContacts, Users);
            }
        }

        public class ContactsBlockedType : ContactsBlocked
        {
            public override uint ConstructorCode => 0x1c138d15;

            public List<ContactBlocked> Blocked;
            public List<User> Users;

            public ContactsBlockedType() { }

            public ContactsBlockedType(List<ContactBlocked> Blocked, List<User> Users)
            {
                this.Blocked = Blocked;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Blocked.Count);
                foreach (ContactBlocked BlockedElement in Blocked)
                    BlockedElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int BlockedLength = reader.ReadInt32();
                Blocked = new List<ContactBlocked>(BlockedLength);
                for (int BlockedIndex = 0; BlockedIndex < BlockedLength; BlockedIndex++)
                    Blocked.Add(reader.Read<ContactBlocked>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(ContactsBlockedType Blocked:{0} Users:{1})", Blocked, Users);
            }
        }

        public class ContactsBlockedSliceType : ContactsBlocked
        {
            public override uint ConstructorCode => 0x900802a1;

            public int Count;
            public List<ContactBlocked> Blocked;
            public List<User> Users;

            public ContactsBlockedSliceType() { }

            public ContactsBlockedSliceType(int Count, List<ContactBlocked> Blocked, List<User> Users)
            {
                this.Count = Count;
                this.Blocked = Blocked;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Count);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Blocked.Count);
                foreach (ContactBlocked BlockedElement in Blocked)
                    BlockedElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Count = reader.ReadInt32();
                reader.ReadInt32(); // vector code
                int BlockedLength = reader.ReadInt32();
                Blocked = new List<ContactBlocked>(BlockedLength);
                for (int BlockedIndex = 0; BlockedIndex < BlockedLength; BlockedIndex++)
                    Blocked.Add(reader.Read<ContactBlocked>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(ContactsBlockedSliceType Count:{0} Blocked:{1} Users:{2})", Count, Blocked, Users);
            }
        }

        public class MessagesDialogsType : MessagesDialogs
        {
            public override uint ConstructorCode => 0x15ba6c40;

            public List<Dialog> Dialogs;
            public List<Message> Messages;
            public List<Chat> Chats;
            public List<User> Users;

            public MessagesDialogsType() { }

            public MessagesDialogsType(List<Dialog> Dialogs, List<Message> Messages, List<Chat> Chats, List<User> Users)
            {
                this.Dialogs = Dialogs;
                this.Messages = Messages;
                this.Chats = Chats;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Dialogs.Count);
                foreach (Dialog DialogsElement in Dialogs)
                    DialogsElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Messages.Count);
                foreach (Message MessagesElement in Messages)
                    MessagesElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Chats.Count);
                foreach (Chat ChatsElement in Chats)
                    ChatsElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int DialogsLength = reader.ReadInt32();
                Dialogs = new List<Dialog>(DialogsLength);
                for (int DialogsIndex = 0; DialogsIndex < DialogsLength; DialogsIndex++)
                    Dialogs.Add(reader.Read<Dialog>());
                reader.ReadInt32(); // vector code
                int MessagesLength = reader.ReadInt32();
                Messages = new List<Message>(MessagesLength);
                for (int MessagesIndex = 0; MessagesIndex < MessagesLength; MessagesIndex++)
                    Messages.Add(reader.Read<Message>());
                reader.ReadInt32(); // vector code
                int ChatsLength = reader.ReadInt32();
                Chats = new List<Chat>(ChatsLength);
                for (int ChatsIndex = 0; ChatsIndex < ChatsLength; ChatsIndex++)
                    Chats.Add(reader.Read<Chat>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(MessagesDialogsType Dialogs:{0} Messages:{1} Chats:{2} Users:{3})", Dialogs, Messages, Chats, Users);
            }
        }

        public class MessagesDialogsSliceType : MessagesDialogs
        {
            public override uint ConstructorCode => 0x71e094f3;

            public int Count;
            public List<Dialog> Dialogs;
            public List<Message> Messages;
            public List<Chat> Chats;
            public List<User> Users;

            public MessagesDialogsSliceType() { }

            public MessagesDialogsSliceType(int Count, List<Dialog> Dialogs, List<Message> Messages, List<Chat> Chats, List<User> Users)
            {
                this.Count = Count;
                this.Dialogs = Dialogs;
                this.Messages = Messages;
                this.Chats = Chats;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Count);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Dialogs.Count);
                foreach (Dialog DialogsElement in Dialogs)
                    DialogsElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Messages.Count);
                foreach (Message MessagesElement in Messages)
                    MessagesElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Chats.Count);
                foreach (Chat ChatsElement in Chats)
                    ChatsElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Count = reader.ReadInt32();
                reader.ReadInt32(); // vector code
                int DialogsLength = reader.ReadInt32();
                Dialogs = new List<Dialog>(DialogsLength);
                for (int DialogsIndex = 0; DialogsIndex < DialogsLength; DialogsIndex++)
                    Dialogs.Add(reader.Read<Dialog>());
                reader.ReadInt32(); // vector code
                int MessagesLength = reader.ReadInt32();
                Messages = new List<Message>(MessagesLength);
                for (int MessagesIndex = 0; MessagesIndex < MessagesLength; MessagesIndex++)
                    Messages.Add(reader.Read<Message>());
                reader.ReadInt32(); // vector code
                int ChatsLength = reader.ReadInt32();
                Chats = new List<Chat>(ChatsLength);
                for (int ChatsIndex = 0; ChatsIndex < ChatsLength; ChatsIndex++)
                    Chats.Add(reader.Read<Chat>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(MessagesDialogsSliceType Count:{0} Dialogs:{1} Messages:{2} Chats:{3} Users:{4})", Count, Dialogs, Messages, Chats, Users);
            }
        }

        public class MessagesMessagesType : MessagesMessages
        {
            public override uint ConstructorCode => 0x8c718e87;

            public List<Message> Messages;
            public List<Chat> Chats;
            public List<User> Users;

            public MessagesMessagesType() { }

            public MessagesMessagesType(List<Message> Messages, List<Chat> Chats, List<User> Users)
            {
                this.Messages = Messages;
                this.Chats = Chats;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Messages.Count);
                foreach (Message MessagesElement in Messages)
                    MessagesElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Chats.Count);
                foreach (Chat ChatsElement in Chats)
                    ChatsElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int MessagesLength = reader.ReadInt32();
                Messages = new List<Message>(MessagesLength);
                for (int MessagesIndex = 0; MessagesIndex < MessagesLength; MessagesIndex++)
                    Messages.Add(reader.Read<Message>());
                reader.ReadInt32(); // vector code
                int ChatsLength = reader.ReadInt32();
                Chats = new List<Chat>(ChatsLength);
                for (int ChatsIndex = 0; ChatsIndex < ChatsLength; ChatsIndex++)
                    Chats.Add(reader.Read<Chat>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(MessagesMessagesType Messages:{0} Chats:{1} Users:{2})", Messages, Chats, Users);
            }
        }

        public class MessagesMessagesSliceType : MessagesMessages
        {
            public override uint ConstructorCode => 0x0b446ae3;

            public int Count;
            public List<Message> Messages;
            public List<Chat> Chats;
            public List<User> Users;

            public MessagesMessagesSliceType() { }

            public MessagesMessagesSliceType(int Count, List<Message> Messages, List<Chat> Chats, List<User> Users)
            {
                this.Count = Count;
                this.Messages = Messages;
                this.Chats = Chats;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Count);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Messages.Count);
                foreach (Message MessagesElement in Messages)
                    MessagesElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Chats.Count);
                foreach (Chat ChatsElement in Chats)
                    ChatsElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Count = reader.ReadInt32();
                reader.ReadInt32(); // vector code
                int MessagesLength = reader.ReadInt32();
                Messages = new List<Message>(MessagesLength);
                for (int MessagesIndex = 0; MessagesIndex < MessagesLength; MessagesIndex++)
                    Messages.Add(reader.Read<Message>());
                reader.ReadInt32(); // vector code
                int ChatsLength = reader.ReadInt32();
                Chats = new List<Chat>(ChatsLength);
                for (int ChatsIndex = 0; ChatsIndex < ChatsLength; ChatsIndex++)
                    Chats.Add(reader.Read<Chat>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(MessagesMessagesSliceType Count:{0} Messages:{1} Chats:{2} Users:{3})", Count, Messages, Chats, Users);
            }
        }

        public class MessagesChannelMessagesType : MessagesMessages
        {
            public override uint ConstructorCode => 0xbc0f17bc;

            public int Pts;
            public int Count;
            public List<Message> Messages;
            public List<MessageGroup> Collapsed;
            public List<Chat> Chats;
            public List<User> Users;

            public MessagesChannelMessagesType() { }

            /// <summary>
            /// The following arguments can be null: Collapsed
            /// </summary>
            /// <param name="Pts">Can NOT be null</param>
            /// <param name="Count">Can NOT be null</param>
            /// <param name="Messages">Can NOT be null</param>
            /// <param name="Collapsed">Can be null</param>
            /// <param name="Chats">Can NOT be null</param>
            /// <param name="Users">Can NOT be null</param>
            public MessagesChannelMessagesType(int Pts, int Count, List<Message> Messages, List<MessageGroup> Collapsed, List<Chat> Chats, List<User> Users)
            {
                this.Pts = Pts;
                this.Count = Count;
                this.Messages = Messages;
                this.Collapsed = Collapsed;
                this.Chats = Chats;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Collapsed != null ? 1 << 0 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                writer.Write(Pts);
                writer.Write(Count);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Messages.Count);
                foreach (Message MessagesElement in Messages)
                    MessagesElement.Write(writer);
                if (Collapsed != null) {
                    writer.Write(0x1cb5c415); // vector code
                    writer.Write(Collapsed.Count);
                    foreach (MessageGroup CollapsedElement in Collapsed)
                        CollapsedElement.Write(writer);
                }

                writer.Write(0x1cb5c415); // vector code
                writer.Write(Chats.Count);
                foreach (Chat ChatsElement in Chats)
                    ChatsElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                Pts = reader.ReadInt32();
                Count = reader.ReadInt32();
                reader.ReadInt32(); // vector code
                int MessagesLength = reader.ReadInt32();
                Messages = new List<Message>(MessagesLength);
                for (int MessagesIndex = 0; MessagesIndex < MessagesLength; MessagesIndex++)
                    Messages.Add(reader.Read<Message>());
                if ((flags & (1 << 0)) != 0) {
                    reader.ReadInt32(); // vector code
                    int CollapsedLength = reader.ReadInt32();
                    Collapsed = new List<MessageGroup>(CollapsedLength);
                    for (int CollapsedIndex = 0; CollapsedIndex < CollapsedLength; CollapsedIndex++)
                        Collapsed.Add(reader.Read<MessageGroup>());
                    }

                reader.ReadInt32(); // vector code
                int ChatsLength = reader.ReadInt32();
                Chats = new List<Chat>(ChatsLength);
                for (int ChatsIndex = 0; ChatsIndex < ChatsLength; ChatsIndex++)
                    Chats.Add(reader.Read<Chat>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(MessagesChannelMessagesType Pts:{0} Count:{1} Messages:{2} Collapsed:{3} Chats:{4} Users:{5})", Pts, Count, Messages, Collapsed, Chats, Users);
            }
        }

        public class MessagesChatsType : MessagesChats
        {
            public override uint ConstructorCode => 0x64ff9fd5;

            public List<Chat> Chats;

            public MessagesChatsType() { }

            public MessagesChatsType(List<Chat> Chats)
            {
                this.Chats = Chats;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Chats.Count);
                foreach (Chat ChatsElement in Chats)
                    ChatsElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int ChatsLength = reader.ReadInt32();
                Chats = new List<Chat>(ChatsLength);
                for (int ChatsIndex = 0; ChatsIndex < ChatsLength; ChatsIndex++)
                    Chats.Add(reader.Read<Chat>());
            }

            public override string ToString()
            {
                return string.Format("(MessagesChatsType Chats:{0})", Chats);
            }
        }

        public class MessagesChatFullType : MessagesChatFull
        {
            public override uint ConstructorCode => 0xe5d7d19c;

            public ChatFull FullChat;
            public List<Chat> Chats;
            public List<User> Users;

            public MessagesChatFullType() { }

            public MessagesChatFullType(ChatFull FullChat, List<Chat> Chats, List<User> Users)
            {
                this.FullChat = FullChat;
                this.Chats = Chats;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                FullChat.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Chats.Count);
                foreach (Chat ChatsElement in Chats)
                    ChatsElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                FullChat = reader.Read<ChatFull>();
                reader.ReadInt32(); // vector code
                int ChatsLength = reader.ReadInt32();
                Chats = new List<Chat>(ChatsLength);
                for (int ChatsIndex = 0; ChatsIndex < ChatsLength; ChatsIndex++)
                    Chats.Add(reader.Read<Chat>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(MessagesChatFullType FullChat:{0} Chats:{1} Users:{2})", FullChat, Chats, Users);
            }
        }

        public class MessagesAffectedHistoryType : MessagesAffectedHistory
        {
            public override uint ConstructorCode => 0xb45c69d1;

            public int Pts;
            public int PtsCount;
            public int Offset;

            public MessagesAffectedHistoryType() { }

            public MessagesAffectedHistoryType(int Pts, int PtsCount, int Offset)
            {
                this.Pts = Pts;
                this.PtsCount = PtsCount;
                this.Offset = Offset;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Pts);
                writer.Write(PtsCount);
                writer.Write(Offset);
            }

            public override void Read(TBinaryReader reader)
            {
                Pts = reader.ReadInt32();
                PtsCount = reader.ReadInt32();
                Offset = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessagesAffectedHistoryType Pts:{0} PtsCount:{1} Offset:{2})", Pts, PtsCount, Offset);
            }
        }

        public class InputMessagesFilterEmptyType : MessagesFilter
        {
            public override uint ConstructorCode => 0x57e2f66c;

            public InputMessagesFilterEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputMessagesFilterEmptyType)";
            }
        }

        public class InputMessagesFilterPhotosType : MessagesFilter
        {
            public override uint ConstructorCode => 0x9609a51c;

            public InputMessagesFilterPhotosType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputMessagesFilterPhotosType)";
            }
        }

        public class InputMessagesFilterVideoType : MessagesFilter
        {
            public override uint ConstructorCode => 0x9fc00e65;

            public InputMessagesFilterVideoType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputMessagesFilterVideoType)";
            }
        }

        public class InputMessagesFilterPhotoVideoType : MessagesFilter
        {
            public override uint ConstructorCode => 0x56e9f0e4;

            public InputMessagesFilterPhotoVideoType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputMessagesFilterPhotoVideoType)";
            }
        }

        public class InputMessagesFilterPhotoVideoDocumentsType : MessagesFilter
        {
            public override uint ConstructorCode => 0xd95e73bb;

            public InputMessagesFilterPhotoVideoDocumentsType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputMessagesFilterPhotoVideoDocumentsType)";
            }
        }

        public class InputMessagesFilterDocumentType : MessagesFilter
        {
            public override uint ConstructorCode => 0x9eddf188;

            public InputMessagesFilterDocumentType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputMessagesFilterDocumentType)";
            }
        }

        public class InputMessagesFilterUrlType : MessagesFilter
        {
            public override uint ConstructorCode => 0x7ef0dd87;

            public InputMessagesFilterUrlType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputMessagesFilterUrlType)";
            }
        }

        public class InputMessagesFilterGifType : MessagesFilter
        {
            public override uint ConstructorCode => 0xffc86587;

            public InputMessagesFilterGifType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputMessagesFilterGifType)";
            }
        }

        public class InputMessagesFilterVoiceType : MessagesFilter
        {
            public override uint ConstructorCode => 0x50f5c392;

            public InputMessagesFilterVoiceType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputMessagesFilterVoiceType)";
            }
        }

        public class InputMessagesFilterMusicType : MessagesFilter
        {
            public override uint ConstructorCode => 0x3751b49e;

            public InputMessagesFilterMusicType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputMessagesFilterMusicType)";
            }
        }

        public class UpdateNewMessageType : Update
        {
            public override uint ConstructorCode => 0x1f2b0afd;

            public Message Message;
            public int Pts;
            public int PtsCount;

            public UpdateNewMessageType() { }

            public UpdateNewMessageType(Message Message, int Pts, int PtsCount)
            {
                this.Message = Message;
                this.Pts = Pts;
                this.PtsCount = PtsCount;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Message.Write(writer);
                writer.Write(Pts);
                writer.Write(PtsCount);
            }

            public override void Read(TBinaryReader reader)
            {
                Message = reader.Read<Message>();
                Pts = reader.ReadInt32();
                PtsCount = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateNewMessageType Message:{0} Pts:{1} PtsCount:{2})", Message, Pts, PtsCount);
            }
        }

        public class UpdateMessageIDType : Update
        {
            public override uint ConstructorCode => 0x4e90bfd6;

            public int Id;
            public long RandomId;

            public UpdateMessageIDType() { }

            public UpdateMessageIDType(int Id, long RandomId)
            {
                this.Id = Id;
                this.RandomId = RandomId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(RandomId);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt32();
                RandomId = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(UpdateMessageIDType Id:{0} RandomId:{1})", Id, RandomId);
            }
        }

        public class UpdateDeleteMessagesType : Update
        {
            public override uint ConstructorCode => 0xa20db0e5;

            public List<int> Messages;
            public int Pts;
            public int PtsCount;

            public UpdateDeleteMessagesType() { }

            public UpdateDeleteMessagesType(List<int> Messages, int Pts, int PtsCount)
            {
                this.Messages = Messages;
                this.Pts = Pts;
                this.PtsCount = PtsCount;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Messages.Count);
                foreach (int MessagesElement in Messages)
                    writer.Write(MessagesElement);
                writer.Write(Pts);
                writer.Write(PtsCount);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int MessagesLength = reader.ReadInt32();
                Messages = new List<int>(MessagesLength);
                for (int MessagesIndex = 0; MessagesIndex < MessagesLength; MessagesIndex++)
                    Messages.Add(reader.ReadInt32());
                Pts = reader.ReadInt32();
                PtsCount = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateDeleteMessagesType Messages:{0} Pts:{1} PtsCount:{2})", Messages, Pts, PtsCount);
            }
        }

        public class UpdateUserTypingType : Update
        {
            public override uint ConstructorCode => 0x5c486927;

            public int UserId;
            public SendMessageAction Action;

            public UpdateUserTypingType() { }

            public UpdateUserTypingType(int UserId, SendMessageAction Action)
            {
                this.UserId = UserId;
                this.Action = Action;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                Action.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                Action = reader.Read<SendMessageAction>();
            }

            public override string ToString()
            {
                return string.Format("(UpdateUserTypingType UserId:{0} Action:{1})", UserId, Action);
            }
        }

        public class UpdateChatUserTypingType : Update
        {
            public override uint ConstructorCode => 0x9a65ea1f;

            public int ChatId;
            public int UserId;
            public SendMessageAction Action;

            public UpdateChatUserTypingType() { }

            public UpdateChatUserTypingType(int ChatId, int UserId, SendMessageAction Action)
            {
                this.ChatId = ChatId;
                this.UserId = UserId;
                this.Action = Action;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
                writer.Write(UserId);
                Action.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                ChatId = reader.ReadInt32();
                UserId = reader.ReadInt32();
                Action = reader.Read<SendMessageAction>();
            }

            public override string ToString()
            {
                return string.Format("(UpdateChatUserTypingType ChatId:{0} UserId:{1} Action:{2})", ChatId, UserId, Action);
            }
        }

        public class UpdateChatParticipantsType : Update
        {
            public override uint ConstructorCode => 0x07761198;

            public ChatParticipants Participants;

            public UpdateChatParticipantsType() { }

            public UpdateChatParticipantsType(ChatParticipants Participants)
            {
                this.Participants = Participants;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Participants.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Participants = reader.Read<ChatParticipants>();
            }

            public override string ToString()
            {
                return string.Format("(UpdateChatParticipantsType Participants:{0})", Participants);
            }
        }

        public class UpdateUserStatusType : Update
        {
            public override uint ConstructorCode => 0x1bfbd823;

            public int UserId;
            public UserStatus Status;

            public UpdateUserStatusType() { }

            public UpdateUserStatusType(int UserId, UserStatus Status)
            {
                this.UserId = UserId;
                this.Status = Status;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                Status.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                Status = reader.Read<UserStatus>();
            }

            public override string ToString()
            {
                return string.Format("(UpdateUserStatusType UserId:{0} Status:{1})", UserId, Status);
            }
        }

        public class UpdateUserNameType : Update
        {
            public override uint ConstructorCode => 0xa7332b73;

            public int UserId;
            public string FirstName;
            public string LastName;
            public string Username;

            public UpdateUserNameType() { }

            public UpdateUserNameType(int UserId, string FirstName, string LastName, string Username)
            {
                this.UserId = UserId;
                this.FirstName = FirstName;
                this.LastName = LastName;
                this.Username = Username;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(FirstName);
                writer.Write(LastName);
                writer.Write(Username);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                FirstName = reader.ReadString();
                LastName = reader.ReadString();
                Username = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(UpdateUserNameType UserId:{0} FirstName:{1} LastName:{2} Username:{3})", UserId, FirstName, LastName, Username);
            }
        }

        public class UpdateUserPhotoType : Update
        {
            public override uint ConstructorCode => 0x95313b0c;

            public int UserId;
            public int Date;
            public UserProfilePhoto Photo;
            public bool Previous;

            public UpdateUserPhotoType() { }

            public UpdateUserPhotoType(int UserId, int Date, UserProfilePhoto Photo, bool Previous)
            {
                this.UserId = UserId;
                this.Date = Date;
                this.Photo = Photo;
                this.Previous = Previous;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(Date);
                Photo.Write(writer);
                writer.Write(Previous);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                Date = reader.ReadInt32();
                Photo = reader.Read<UserProfilePhoto>();
                Previous = reader.ReadBoolean();
            }

            public override string ToString()
            {
                return string.Format("(UpdateUserPhotoType UserId:{0} Date:{1} Photo:{2} Previous:{3})", UserId, Date, Photo, Previous);
            }
        }

        public class UpdateContactRegisteredType : Update
        {
            public override uint ConstructorCode => 0x2575bbb9;

            public int UserId;
            public int Date;

            public UpdateContactRegisteredType() { }

            public UpdateContactRegisteredType(int UserId, int Date)
            {
                this.UserId = UserId;
                this.Date = Date;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(Date);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                Date = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateContactRegisteredType UserId:{0} Date:{1})", UserId, Date);
            }
        }

        public class UpdateContactLinkType : Update
        {
            public override uint ConstructorCode => 0x9d2e67c5;

            public int UserId;
            public ContactLink MyLink;
            public ContactLink ForeignLink;

            public UpdateContactLinkType() { }

            public UpdateContactLinkType(int UserId, ContactLink MyLink, ContactLink ForeignLink)
            {
                this.UserId = UserId;
                this.MyLink = MyLink;
                this.ForeignLink = ForeignLink;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                MyLink.Write(writer);
                ForeignLink.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                MyLink = reader.Read<ContactLink>();
                ForeignLink = reader.Read<ContactLink>();
            }

            public override string ToString()
            {
                return string.Format("(UpdateContactLinkType UserId:{0} MyLink:{1} ForeignLink:{2})", UserId, MyLink, ForeignLink);
            }
        }

        public class UpdateNewAuthorizationType : Update
        {
            public override uint ConstructorCode => 0x8f06529a;

            public long AuthKeyId;
            public int Date;
            public string Device;
            public string Location;

            public UpdateNewAuthorizationType() { }

            public UpdateNewAuthorizationType(long AuthKeyId, int Date, string Device, string Location)
            {
                this.AuthKeyId = AuthKeyId;
                this.Date = Date;
                this.Device = Device;
                this.Location = Location;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(AuthKeyId);
                writer.Write(Date);
                writer.Write(Device);
                writer.Write(Location);
            }

            public override void Read(TBinaryReader reader)
            {
                AuthKeyId = reader.ReadInt64();
                Date = reader.ReadInt32();
                Device = reader.ReadString();
                Location = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(UpdateNewAuthorizationType AuthKeyId:{0} Date:{1} Device:{2} Location:{3})", AuthKeyId, Date, Device, Location);
            }
        }

        public class UpdateNewEncryptedMessageType : Update
        {
            public override uint ConstructorCode => 0x12bcbd9a;

            public EncryptedMessage Message;
            public int Qts;

            public UpdateNewEncryptedMessageType() { }

            public UpdateNewEncryptedMessageType(EncryptedMessage Message, int Qts)
            {
                this.Message = Message;
                this.Qts = Qts;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Message.Write(writer);
                writer.Write(Qts);
            }

            public override void Read(TBinaryReader reader)
            {
                Message = reader.Read<EncryptedMessage>();
                Qts = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateNewEncryptedMessageType Message:{0} Qts:{1})", Message, Qts);
            }
        }

        public class UpdateEncryptedChatTypingType : Update
        {
            public override uint ConstructorCode => 0x1710f156;

            public int ChatId;

            public UpdateEncryptedChatTypingType() { }

            public UpdateEncryptedChatTypingType(int ChatId)
            {
                this.ChatId = ChatId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
            }

            public override void Read(TBinaryReader reader)
            {
                ChatId = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateEncryptedChatTypingType ChatId:{0})", ChatId);
            }
        }

        public class UpdateEncryptionType : Update
        {
            public override uint ConstructorCode => 0xb4a2e88d;

            public EncryptedChat Chat;
            public int Date;

            public UpdateEncryptionType() { }

            public UpdateEncryptionType(EncryptedChat Chat, int Date)
            {
                this.Chat = Chat;
                this.Date = Date;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Chat.Write(writer);
                writer.Write(Date);
            }

            public override void Read(TBinaryReader reader)
            {
                Chat = reader.Read<EncryptedChat>();
                Date = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateEncryptionType Chat:{0} Date:{1})", Chat, Date);
            }
        }

        public class UpdateEncryptedMessagesReadType : Update
        {
            public override uint ConstructorCode => 0x38fe25b7;

            public int ChatId;
            public int MaxDate;
            public int Date;

            public UpdateEncryptedMessagesReadType() { }

            public UpdateEncryptedMessagesReadType(int ChatId, int MaxDate, int Date)
            {
                this.ChatId = ChatId;
                this.MaxDate = MaxDate;
                this.Date = Date;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
                writer.Write(MaxDate);
                writer.Write(Date);
            }

            public override void Read(TBinaryReader reader)
            {
                ChatId = reader.ReadInt32();
                MaxDate = reader.ReadInt32();
                Date = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateEncryptedMessagesReadType ChatId:{0} MaxDate:{1} Date:{2})", ChatId, MaxDate, Date);
            }
        }

        public class UpdateChatParticipantAddType : Update
        {
            public override uint ConstructorCode => 0xea4b0e5c;

            public int ChatId;
            public int UserId;
            public int InviterId;
            public int Date;
            public int Version;

            public UpdateChatParticipantAddType() { }

            public UpdateChatParticipantAddType(int ChatId, int UserId, int InviterId, int Date, int Version)
            {
                this.ChatId = ChatId;
                this.UserId = UserId;
                this.InviterId = InviterId;
                this.Date = Date;
                this.Version = Version;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
                writer.Write(UserId);
                writer.Write(InviterId);
                writer.Write(Date);
                writer.Write(Version);
            }

            public override void Read(TBinaryReader reader)
            {
                ChatId = reader.ReadInt32();
                UserId = reader.ReadInt32();
                InviterId = reader.ReadInt32();
                Date = reader.ReadInt32();
                Version = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateChatParticipantAddType ChatId:{0} UserId:{1} InviterId:{2} Date:{3} Version:{4})", ChatId, UserId, InviterId, Date, Version);
            }
        }

        public class UpdateChatParticipantDeleteType : Update
        {
            public override uint ConstructorCode => 0x6e5f8c22;

            public int ChatId;
            public int UserId;
            public int Version;

            public UpdateChatParticipantDeleteType() { }

            public UpdateChatParticipantDeleteType(int ChatId, int UserId, int Version)
            {
                this.ChatId = ChatId;
                this.UserId = UserId;
                this.Version = Version;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
                writer.Write(UserId);
                writer.Write(Version);
            }

            public override void Read(TBinaryReader reader)
            {
                ChatId = reader.ReadInt32();
                UserId = reader.ReadInt32();
                Version = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateChatParticipantDeleteType ChatId:{0} UserId:{1} Version:{2})", ChatId, UserId, Version);
            }
        }

        public class UpdateDcOptionsType : Update
        {
            public override uint ConstructorCode => 0x8e5e9873;

            public List<DcOption> DcOptions;

            public UpdateDcOptionsType() { }

            public UpdateDcOptionsType(List<DcOption> DcOptions)
            {
                this.DcOptions = DcOptions;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(DcOptions.Count);
                foreach (DcOption DcOptionsElement in DcOptions)
                    DcOptionsElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int DcOptionsLength = reader.ReadInt32();
                DcOptions = new List<DcOption>(DcOptionsLength);
                for (int DcOptionsIndex = 0; DcOptionsIndex < DcOptionsLength; DcOptionsIndex++)
                    DcOptions.Add(reader.Read<DcOption>());
            }

            public override string ToString()
            {
                return string.Format("(UpdateDcOptionsType DcOptions:{0})", DcOptions);
            }
        }

        public class UpdateUserBlockedType : Update
        {
            public override uint ConstructorCode => 0x80ece81a;

            public int UserId;
            public bool Blocked;

            public UpdateUserBlockedType() { }

            public UpdateUserBlockedType(int UserId, bool Blocked)
            {
                this.UserId = UserId;
                this.Blocked = Blocked;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(Blocked);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                Blocked = reader.ReadBoolean();
            }

            public override string ToString()
            {
                return string.Format("(UpdateUserBlockedType UserId:{0} Blocked:{1})", UserId, Blocked);
            }
        }

        public class UpdateNotifySettingsType : Update
        {
            public override uint ConstructorCode => 0xbec268ef;

            public NotifyPeer Peer;
            public PeerNotifySettings NotifySettings;

            public UpdateNotifySettingsType() { }

            public UpdateNotifySettingsType(NotifyPeer Peer, PeerNotifySettings NotifySettings)
            {
                this.Peer = Peer;
                this.NotifySettings = NotifySettings;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                NotifySettings.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Peer = reader.Read<NotifyPeer>();
                NotifySettings = reader.Read<PeerNotifySettings>();
            }

            public override string ToString()
            {
                return string.Format("(UpdateNotifySettingsType Peer:{0} NotifySettings:{1})", Peer, NotifySettings);
            }
        }

        public class UpdateServiceNotificationType : Update
        {
            public override uint ConstructorCode => 0x382dd3e4;

            public string Type;
            public string Message;
            public MessageMedia Media;
            public bool Popup;

            public UpdateServiceNotificationType() { }

            public UpdateServiceNotificationType(string Type, string Message, MessageMedia Media, bool Popup)
            {
                this.Type = Type;
                this.Message = Message;
                this.Media = Media;
                this.Popup = Popup;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Type);
                writer.Write(Message);
                Media.Write(writer);
                writer.Write(Popup);
            }

            public override void Read(TBinaryReader reader)
            {
                Type = reader.ReadString();
                Message = reader.ReadString();
                Media = reader.Read<MessageMedia>();
                Popup = reader.ReadBoolean();
            }

            public override string ToString()
            {
                return string.Format("(UpdateServiceNotificationType Type:{0} Message:{1} Media:{2} Popup:{3})", Type, Message, Media, Popup);
            }
        }

        public class UpdatePrivacyType : Update
        {
            public override uint ConstructorCode => 0xee3b272a;

            public PrivacyKey Key;
            public List<PrivacyRule> Rules;

            public UpdatePrivacyType() { }

            public UpdatePrivacyType(PrivacyKey Key, List<PrivacyRule> Rules)
            {
                this.Key = Key;
                this.Rules = Rules;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Key.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Rules.Count);
                foreach (PrivacyRule RulesElement in Rules)
                    RulesElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Key = reader.Read<PrivacyKey>();
                reader.ReadInt32(); // vector code
                int RulesLength = reader.ReadInt32();
                Rules = new List<PrivacyRule>(RulesLength);
                for (int RulesIndex = 0; RulesIndex < RulesLength; RulesIndex++)
                    Rules.Add(reader.Read<PrivacyRule>());
            }

            public override string ToString()
            {
                return string.Format("(UpdatePrivacyType Key:{0} Rules:{1})", Key, Rules);
            }
        }

        public class UpdateUserPhoneType : Update
        {
            public override uint ConstructorCode => 0x12b9417b;

            public int UserId;
            public string Phone;

            public UpdateUserPhoneType() { }

            public UpdateUserPhoneType(int UserId, string Phone)
            {
                this.UserId = UserId;
                this.Phone = Phone;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(Phone);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                Phone = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(UpdateUserPhoneType UserId:{0} Phone:{1})", UserId, Phone);
            }
        }

        public class UpdateReadHistoryInboxType : Update
        {
            public override uint ConstructorCode => 0x9961fd5c;

            public Peer Peer;
            public int MaxId;
            public int Pts;
            public int PtsCount;

            public UpdateReadHistoryInboxType() { }

            public UpdateReadHistoryInboxType(Peer Peer, int MaxId, int Pts, int PtsCount)
            {
                this.Peer = Peer;
                this.MaxId = MaxId;
                this.Pts = Pts;
                this.PtsCount = PtsCount;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                writer.Write(MaxId);
                writer.Write(Pts);
                writer.Write(PtsCount);
            }

            public override void Read(TBinaryReader reader)
            {
                Peer = reader.Read<Peer>();
                MaxId = reader.ReadInt32();
                Pts = reader.ReadInt32();
                PtsCount = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateReadHistoryInboxType Peer:{0} MaxId:{1} Pts:{2} PtsCount:{3})", Peer, MaxId, Pts, PtsCount);
            }
        }

        public class UpdateReadHistoryOutboxType : Update
        {
            public override uint ConstructorCode => 0x2f2f21bf;

            public Peer Peer;
            public int MaxId;
            public int Pts;
            public int PtsCount;

            public UpdateReadHistoryOutboxType() { }

            public UpdateReadHistoryOutboxType(Peer Peer, int MaxId, int Pts, int PtsCount)
            {
                this.Peer = Peer;
                this.MaxId = MaxId;
                this.Pts = Pts;
                this.PtsCount = PtsCount;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                writer.Write(MaxId);
                writer.Write(Pts);
                writer.Write(PtsCount);
            }

            public override void Read(TBinaryReader reader)
            {
                Peer = reader.Read<Peer>();
                MaxId = reader.ReadInt32();
                Pts = reader.ReadInt32();
                PtsCount = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateReadHistoryOutboxType Peer:{0} MaxId:{1} Pts:{2} PtsCount:{3})", Peer, MaxId, Pts, PtsCount);
            }
        }

        public class UpdateWebPageType : Update
        {
            public override uint ConstructorCode => 0x7f891213;

            public WebPage Webpage;
            public int Pts;
            public int PtsCount;

            public UpdateWebPageType() { }

            public UpdateWebPageType(WebPage Webpage, int Pts, int PtsCount)
            {
                this.Webpage = Webpage;
                this.Pts = Pts;
                this.PtsCount = PtsCount;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Webpage.Write(writer);
                writer.Write(Pts);
                writer.Write(PtsCount);
            }

            public override void Read(TBinaryReader reader)
            {
                Webpage = reader.Read<WebPage>();
                Pts = reader.ReadInt32();
                PtsCount = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateWebPageType Webpage:{0} Pts:{1} PtsCount:{2})", Webpage, Pts, PtsCount);
            }
        }

        public class UpdateReadMessagesContentsType : Update
        {
            public override uint ConstructorCode => 0x68c13933;

            public List<int> Messages;
            public int Pts;
            public int PtsCount;

            public UpdateReadMessagesContentsType() { }

            public UpdateReadMessagesContentsType(List<int> Messages, int Pts, int PtsCount)
            {
                this.Messages = Messages;
                this.Pts = Pts;
                this.PtsCount = PtsCount;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Messages.Count);
                foreach (int MessagesElement in Messages)
                    writer.Write(MessagesElement);
                writer.Write(Pts);
                writer.Write(PtsCount);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int MessagesLength = reader.ReadInt32();
                Messages = new List<int>(MessagesLength);
                for (int MessagesIndex = 0; MessagesIndex < MessagesLength; MessagesIndex++)
                    Messages.Add(reader.ReadInt32());
                Pts = reader.ReadInt32();
                PtsCount = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateReadMessagesContentsType Messages:{0} Pts:{1} PtsCount:{2})", Messages, Pts, PtsCount);
            }
        }

        public class UpdateChannelTooLongType : Update
        {
            public override uint ConstructorCode => 0x60946422;

            public int ChannelId;

            public UpdateChannelTooLongType() { }

            public UpdateChannelTooLongType(int ChannelId)
            {
                this.ChannelId = ChannelId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChannelId);
            }

            public override void Read(TBinaryReader reader)
            {
                ChannelId = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateChannelTooLongType ChannelId:{0})", ChannelId);
            }
        }

        public class UpdateChannelType : Update
        {
            public override uint ConstructorCode => 0xb6d45656;

            public int ChannelId;

            public UpdateChannelType() { }

            public UpdateChannelType(int ChannelId)
            {
                this.ChannelId = ChannelId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChannelId);
            }

            public override void Read(TBinaryReader reader)
            {
                ChannelId = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateChannelType ChannelId:{0})", ChannelId);
            }
        }

        public class UpdateChannelGroupType : Update
        {
            public override uint ConstructorCode => 0xc36c1e3c;

            public int ChannelId;
            public MessageGroup Group;

            public UpdateChannelGroupType() { }

            public UpdateChannelGroupType(int ChannelId, MessageGroup Group)
            {
                this.ChannelId = ChannelId;
                this.Group = Group;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChannelId);
                Group.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                ChannelId = reader.ReadInt32();
                Group = reader.Read<MessageGroup>();
            }

            public override string ToString()
            {
                return string.Format("(UpdateChannelGroupType ChannelId:{0} Group:{1})", ChannelId, Group);
            }
        }

        public class UpdateNewChannelMessageType : Update
        {
            public override uint ConstructorCode => 0x62ba04d9;

            public Message Message;
            public int Pts;
            public int PtsCount;

            public UpdateNewChannelMessageType() { }

            public UpdateNewChannelMessageType(Message Message, int Pts, int PtsCount)
            {
                this.Message = Message;
                this.Pts = Pts;
                this.PtsCount = PtsCount;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Message.Write(writer);
                writer.Write(Pts);
                writer.Write(PtsCount);
            }

            public override void Read(TBinaryReader reader)
            {
                Message = reader.Read<Message>();
                Pts = reader.ReadInt32();
                PtsCount = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateNewChannelMessageType Message:{0} Pts:{1} PtsCount:{2})", Message, Pts, PtsCount);
            }
        }

        public class UpdateReadChannelInboxType : Update
        {
            public override uint ConstructorCode => 0x4214f37f;

            public int ChannelId;
            public int MaxId;

            public UpdateReadChannelInboxType() { }

            public UpdateReadChannelInboxType(int ChannelId, int MaxId)
            {
                this.ChannelId = ChannelId;
                this.MaxId = MaxId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChannelId);
                writer.Write(MaxId);
            }

            public override void Read(TBinaryReader reader)
            {
                ChannelId = reader.ReadInt32();
                MaxId = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateReadChannelInboxType ChannelId:{0} MaxId:{1})", ChannelId, MaxId);
            }
        }

        public class UpdateDeleteChannelMessagesType : Update
        {
            public override uint ConstructorCode => 0xc37521c9;

            public int ChannelId;
            public List<int> Messages;
            public int Pts;
            public int PtsCount;

            public UpdateDeleteChannelMessagesType() { }

            public UpdateDeleteChannelMessagesType(int ChannelId, List<int> Messages, int Pts, int PtsCount)
            {
                this.ChannelId = ChannelId;
                this.Messages = Messages;
                this.Pts = Pts;
                this.PtsCount = PtsCount;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChannelId);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Messages.Count);
                foreach (int MessagesElement in Messages)
                    writer.Write(MessagesElement);
                writer.Write(Pts);
                writer.Write(PtsCount);
            }

            public override void Read(TBinaryReader reader)
            {
                ChannelId = reader.ReadInt32();
                reader.ReadInt32(); // vector code
                int MessagesLength = reader.ReadInt32();
                Messages = new List<int>(MessagesLength);
                for (int MessagesIndex = 0; MessagesIndex < MessagesLength; MessagesIndex++)
                    Messages.Add(reader.ReadInt32());
                Pts = reader.ReadInt32();
                PtsCount = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateDeleteChannelMessagesType ChannelId:{0} Messages:{1} Pts:{2} PtsCount:{3})", ChannelId, Messages, Pts, PtsCount);
            }
        }

        public class UpdateChannelMessageViewsType : Update
        {
            public override uint ConstructorCode => 0x98a12b4b;

            public int ChannelId;
            public int Id;
            public int Views;

            public UpdateChannelMessageViewsType() { }

            public UpdateChannelMessageViewsType(int ChannelId, int Id, int Views)
            {
                this.ChannelId = ChannelId;
                this.Id = Id;
                this.Views = Views;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChannelId);
                writer.Write(Id);
                writer.Write(Views);
            }

            public override void Read(TBinaryReader reader)
            {
                ChannelId = reader.ReadInt32();
                Id = reader.ReadInt32();
                Views = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateChannelMessageViewsType ChannelId:{0} Id:{1} Views:{2})", ChannelId, Id, Views);
            }
        }

        public class UpdateChatAdminsType : Update
        {
            public override uint ConstructorCode => 0x6e947941;

            public int ChatId;
            public bool Enabled;
            public int Version;

            public UpdateChatAdminsType() { }

            public UpdateChatAdminsType(int ChatId, bool Enabled, int Version)
            {
                this.ChatId = ChatId;
                this.Enabled = Enabled;
                this.Version = Version;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
                writer.Write(Enabled);
                writer.Write(Version);
            }

            public override void Read(TBinaryReader reader)
            {
                ChatId = reader.ReadInt32();
                Enabled = reader.ReadBoolean();
                Version = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateChatAdminsType ChatId:{0} Enabled:{1} Version:{2})", ChatId, Enabled, Version);
            }
        }

        public class UpdateChatParticipantAdminType : Update
        {
            public override uint ConstructorCode => 0xb6901959;

            public int ChatId;
            public int UserId;
            public bool IsAdmin;
            public int Version;

            public UpdateChatParticipantAdminType() { }

            public UpdateChatParticipantAdminType(int ChatId, int UserId, bool IsAdmin, int Version)
            {
                this.ChatId = ChatId;
                this.UserId = UserId;
                this.IsAdmin = IsAdmin;
                this.Version = Version;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
                writer.Write(UserId);
                writer.Write(IsAdmin);
                writer.Write(Version);
            }

            public override void Read(TBinaryReader reader)
            {
                ChatId = reader.ReadInt32();
                UserId = reader.ReadInt32();
                IsAdmin = reader.ReadBoolean();
                Version = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateChatParticipantAdminType ChatId:{0} UserId:{1} IsAdmin:{2} Version:{3})", ChatId, UserId, IsAdmin, Version);
            }
        }

        public class UpdateNewStickerSetType : Update
        {
            public override uint ConstructorCode => 0x688a30aa;

            public MessagesStickerSet Stickerset;

            public UpdateNewStickerSetType() { }

            public UpdateNewStickerSetType(MessagesStickerSet Stickerset)
            {
                this.Stickerset = Stickerset;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Stickerset.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Stickerset = reader.Read<MessagesStickerSet>();
            }

            public override string ToString()
            {
                return string.Format("(UpdateNewStickerSetType Stickerset:{0})", Stickerset);
            }
        }

        public class UpdateStickerSetsOrderType : Update
        {
            public override uint ConstructorCode => 0xf0dfb451;

            public List<long> Order;

            public UpdateStickerSetsOrderType() { }

            public UpdateStickerSetsOrderType(List<long> Order)
            {
                this.Order = Order;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Order.Count);
                foreach (long OrderElement in Order)
                    writer.Write(OrderElement);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int OrderLength = reader.ReadInt32();
                Order = new List<long>(OrderLength);
                for (int OrderIndex = 0; OrderIndex < OrderLength; OrderIndex++)
                    Order.Add(reader.ReadInt64());
            }

            public override string ToString()
            {
                return string.Format("(UpdateStickerSetsOrderType Order:{0})", Order);
            }
        }

        public class UpdateStickerSetsType : Update
        {
            public override uint ConstructorCode => 0x43ae3dec;

            public UpdateStickerSetsType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(UpdateStickerSetsType)";
            }
        }

        public class UpdateSavedGifsType : Update
        {
            public override uint ConstructorCode => 0x9375341e;

            public UpdateSavedGifsType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(UpdateSavedGifsType)";
            }
        }

        public class UpdateBotInlineQueryType : Update
        {
            public override uint ConstructorCode => 0xc01eea08;

            public long QueryId;
            public int UserId;
            public string Query;
            public string Offset;

            public UpdateBotInlineQueryType() { }

            public UpdateBotInlineQueryType(long QueryId, int UserId, string Query, string Offset)
            {
                this.QueryId = QueryId;
                this.UserId = UserId;
                this.Query = Query;
                this.Offset = Offset;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(QueryId);
                writer.Write(UserId);
                writer.Write(Query);
                writer.Write(Offset);
            }

            public override void Read(TBinaryReader reader)
            {
                QueryId = reader.ReadInt64();
                UserId = reader.ReadInt32();
                Query = reader.ReadString();
                Offset = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(UpdateBotInlineQueryType QueryId:{0} UserId:{1} Query:{2} Offset:{3})", QueryId, UserId, Query, Offset);
            }
        }

        public class UpdateBotInlineSendType : Update
        {
            public override uint ConstructorCode => 0x0f69e113;

            public int UserId;
            public string Query;
            public string Id;

            public UpdateBotInlineSendType() { }

            public UpdateBotInlineSendType(int UserId, string Query, string Id)
            {
                this.UserId = UserId;
                this.Query = Query;
                this.Id = Id;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(Query);
                writer.Write(Id);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                Query = reader.ReadString();
                Id = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(UpdateBotInlineSendType UserId:{0} Query:{1} Id:{2})", UserId, Query, Id);
            }
        }

        public class UpdatesStateType : UpdatesState
        {
            public override uint ConstructorCode => 0xa56c2a3e;

            public int Pts;
            public int Qts;
            public int Date;
            public int Seq;
            public int UnreadCount;

            public UpdatesStateType() { }

            public UpdatesStateType(int Pts, int Qts, int Date, int Seq, int UnreadCount)
            {
                this.Pts = Pts;
                this.Qts = Qts;
                this.Date = Date;
                this.Seq = Seq;
                this.UnreadCount = UnreadCount;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Pts);
                writer.Write(Qts);
                writer.Write(Date);
                writer.Write(Seq);
                writer.Write(UnreadCount);
            }

            public override void Read(TBinaryReader reader)
            {
                Pts = reader.ReadInt32();
                Qts = reader.ReadInt32();
                Date = reader.ReadInt32();
                Seq = reader.ReadInt32();
                UnreadCount = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdatesStateType Pts:{0} Qts:{1} Date:{2} Seq:{3} UnreadCount:{4})", Pts, Qts, Date, Seq, UnreadCount);
            }
        }

        public class UpdatesDifferenceEmptyType : UpdatesDifference
        {
            public override uint ConstructorCode => 0x5d75a138;

            public int Date;
            public int Seq;

            public UpdatesDifferenceEmptyType() { }

            public UpdatesDifferenceEmptyType(int Date, int Seq)
            {
                this.Date = Date;
                this.Seq = Seq;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Date);
                writer.Write(Seq);
            }

            public override void Read(TBinaryReader reader)
            {
                Date = reader.ReadInt32();
                Seq = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdatesDifferenceEmptyType Date:{0} Seq:{1})", Date, Seq);
            }
        }

        public class UpdatesDifferenceType : UpdatesDifference
        {
            public override uint ConstructorCode => 0x00f49ca0;

            public List<Message> NewMessages;
            public List<EncryptedMessage> NewEncryptedMessages;
            public List<Update> OtherUpdates;
            public List<Chat> Chats;
            public List<User> Users;
            public UpdatesState State;

            public UpdatesDifferenceType() { }

            public UpdatesDifferenceType(List<Message> NewMessages, List<EncryptedMessage> NewEncryptedMessages, List<Update> OtherUpdates, List<Chat> Chats, List<User> Users, UpdatesState State)
            {
                this.NewMessages = NewMessages;
                this.NewEncryptedMessages = NewEncryptedMessages;
                this.OtherUpdates = OtherUpdates;
                this.Chats = Chats;
                this.Users = Users;
                this.State = State;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(NewMessages.Count);
                foreach (Message NewMessagesElement in NewMessages)
                    NewMessagesElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(NewEncryptedMessages.Count);
                foreach (EncryptedMessage NewEncryptedMessagesElement in NewEncryptedMessages)
                    NewEncryptedMessagesElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(OtherUpdates.Count);
                foreach (Update OtherUpdatesElement in OtherUpdates)
                    OtherUpdatesElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Chats.Count);
                foreach (Chat ChatsElement in Chats)
                    ChatsElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
                State.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int NewMessagesLength = reader.ReadInt32();
                NewMessages = new List<Message>(NewMessagesLength);
                for (int NewMessagesIndex = 0; NewMessagesIndex < NewMessagesLength; NewMessagesIndex++)
                    NewMessages.Add(reader.Read<Message>());
                reader.ReadInt32(); // vector code
                int NewEncryptedMessagesLength = reader.ReadInt32();
                NewEncryptedMessages = new List<EncryptedMessage>(NewEncryptedMessagesLength);
                for (int NewEncryptedMessagesIndex = 0; NewEncryptedMessagesIndex < NewEncryptedMessagesLength; NewEncryptedMessagesIndex++)
                    NewEncryptedMessages.Add(reader.Read<EncryptedMessage>());
                reader.ReadInt32(); // vector code
                int OtherUpdatesLength = reader.ReadInt32();
                OtherUpdates = new List<Update>(OtherUpdatesLength);
                for (int OtherUpdatesIndex = 0; OtherUpdatesIndex < OtherUpdatesLength; OtherUpdatesIndex++)
                    OtherUpdates.Add(reader.Read<Update>());
                reader.ReadInt32(); // vector code
                int ChatsLength = reader.ReadInt32();
                Chats = new List<Chat>(ChatsLength);
                for (int ChatsIndex = 0; ChatsIndex < ChatsLength; ChatsIndex++)
                    Chats.Add(reader.Read<Chat>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
                State = reader.Read<UpdatesState>();
            }

            public override string ToString()
            {
                return string.Format("(UpdatesDifferenceType NewMessages:{0} NewEncryptedMessages:{1} OtherUpdates:{2} Chats:{3} Users:{4} State:{5})", NewMessages, NewEncryptedMessages, OtherUpdates, Chats, Users, State);
            }
        }

        public class UpdatesDifferenceSliceType : UpdatesDifference
        {
            public override uint ConstructorCode => 0xa8fb1981;

            public List<Message> NewMessages;
            public List<EncryptedMessage> NewEncryptedMessages;
            public List<Update> OtherUpdates;
            public List<Chat> Chats;
            public List<User> Users;
            public UpdatesState IntermediateState;

            public UpdatesDifferenceSliceType() { }

            public UpdatesDifferenceSliceType(List<Message> NewMessages, List<EncryptedMessage> NewEncryptedMessages, List<Update> OtherUpdates, List<Chat> Chats, List<User> Users, UpdatesState IntermediateState)
            {
                this.NewMessages = NewMessages;
                this.NewEncryptedMessages = NewEncryptedMessages;
                this.OtherUpdates = OtherUpdates;
                this.Chats = Chats;
                this.Users = Users;
                this.IntermediateState = IntermediateState;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(NewMessages.Count);
                foreach (Message NewMessagesElement in NewMessages)
                    NewMessagesElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(NewEncryptedMessages.Count);
                foreach (EncryptedMessage NewEncryptedMessagesElement in NewEncryptedMessages)
                    NewEncryptedMessagesElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(OtherUpdates.Count);
                foreach (Update OtherUpdatesElement in OtherUpdates)
                    OtherUpdatesElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Chats.Count);
                foreach (Chat ChatsElement in Chats)
                    ChatsElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
                IntermediateState.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int NewMessagesLength = reader.ReadInt32();
                NewMessages = new List<Message>(NewMessagesLength);
                for (int NewMessagesIndex = 0; NewMessagesIndex < NewMessagesLength; NewMessagesIndex++)
                    NewMessages.Add(reader.Read<Message>());
                reader.ReadInt32(); // vector code
                int NewEncryptedMessagesLength = reader.ReadInt32();
                NewEncryptedMessages = new List<EncryptedMessage>(NewEncryptedMessagesLength);
                for (int NewEncryptedMessagesIndex = 0; NewEncryptedMessagesIndex < NewEncryptedMessagesLength; NewEncryptedMessagesIndex++)
                    NewEncryptedMessages.Add(reader.Read<EncryptedMessage>());
                reader.ReadInt32(); // vector code
                int OtherUpdatesLength = reader.ReadInt32();
                OtherUpdates = new List<Update>(OtherUpdatesLength);
                for (int OtherUpdatesIndex = 0; OtherUpdatesIndex < OtherUpdatesLength; OtherUpdatesIndex++)
                    OtherUpdates.Add(reader.Read<Update>());
                reader.ReadInt32(); // vector code
                int ChatsLength = reader.ReadInt32();
                Chats = new List<Chat>(ChatsLength);
                for (int ChatsIndex = 0; ChatsIndex < ChatsLength; ChatsIndex++)
                    Chats.Add(reader.Read<Chat>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
                IntermediateState = reader.Read<UpdatesState>();
            }

            public override string ToString()
            {
                return string.Format("(UpdatesDifferenceSliceType NewMessages:{0} NewEncryptedMessages:{1} OtherUpdates:{2} Chats:{3} Users:{4} IntermediateState:{5})", NewMessages, NewEncryptedMessages, OtherUpdates, Chats, Users, IntermediateState);
            }
        }

        public class UpdatesTooLongType : Updates
        {
            public override uint ConstructorCode => 0xe317af7e;

            public UpdatesTooLongType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(UpdatesTooLongType)";
            }
        }

        public class UpdateShortMessageType : Updates
        {
            public override uint ConstructorCode => 0x13e4deaa;

            public True Unread;
            public True Out;
            public True Mentioned;
            public True MediaUnread;
            public int Id;
            public int UserId;
            public string Message;
            public int Pts;
            public int PtsCount;
            public int Date;
            public Peer FwdFromId;
            public int? FwdDate;
            public int? ViaBotId;
            public int? ReplyToMsgId;
            public List<MessageEntity> Entities;

            public UpdateShortMessageType() { }

            /// <summary>
            /// The following arguments can be null: Unread, Out, Mentioned, MediaUnread, FwdFromId, FwdDate, ViaBotId, ReplyToMsgId, Entities
            /// </summary>
            /// <param name="Unread">Can be null</param>
            /// <param name="Out">Can be null</param>
            /// <param name="Mentioned">Can be null</param>
            /// <param name="MediaUnread">Can be null</param>
            /// <param name="Id">Can NOT be null</param>
            /// <param name="UserId">Can NOT be null</param>
            /// <param name="Message">Can NOT be null</param>
            /// <param name="Pts">Can NOT be null</param>
            /// <param name="PtsCount">Can NOT be null</param>
            /// <param name="Date">Can NOT be null</param>
            /// <param name="FwdFromId">Can be null</param>
            /// <param name="FwdDate">Can be null</param>
            /// <param name="ViaBotId">Can be null</param>
            /// <param name="ReplyToMsgId">Can be null</param>
            /// <param name="Entities">Can be null</param>
            public UpdateShortMessageType(True Unread, True Out, True Mentioned, True MediaUnread, int Id, int UserId, string Message, int Pts, int PtsCount, int Date, Peer FwdFromId, int? FwdDate, int? ViaBotId, int? ReplyToMsgId, List<MessageEntity> Entities)
            {
                this.Unread = Unread;
                this.Out = Out;
                this.Mentioned = Mentioned;
                this.MediaUnread = MediaUnread;
                this.Id = Id;
                this.UserId = UserId;
                this.Message = Message;
                this.Pts = Pts;
                this.PtsCount = PtsCount;
                this.Date = Date;
                this.FwdFromId = FwdFromId;
                this.FwdDate = FwdDate;
                this.ViaBotId = ViaBotId;
                this.ReplyToMsgId = ReplyToMsgId;
                this.Entities = Entities;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Unread != null ? 1 << 0 : 0) |
                    (Out != null ? 1 << 1 : 0) |
                    (Mentioned != null ? 1 << 4 : 0) |
                    (MediaUnread != null ? 1 << 5 : 0) |
                    (FwdFromId != null ? 1 << 2 : 0) |
                    (FwdDate != null ? 1 << 2 : 0) |
                    (ViaBotId != null ? 1 << 11 : 0) |
                    (ReplyToMsgId != null ? 1 << 3 : 0) |
                    (Entities != null ? 1 << 7 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Unread != null) {

                }

                if (Out != null) {

                }

                if (Mentioned != null) {

                }

                if (MediaUnread != null) {

                }

                writer.Write(Id);
                writer.Write(UserId);
                writer.Write(Message);
                writer.Write(Pts);
                writer.Write(PtsCount);
                writer.Write(Date);
                if (FwdFromId != null) {
                    FwdFromId.Write(writer);
                }

                if (FwdDate != null) {
                    writer.Write(FwdDate.Value);
                }

                if (ViaBotId != null) {
                    writer.Write(ViaBotId.Value);
                }

                if (ReplyToMsgId != null) {
                    writer.Write(ReplyToMsgId.Value);
                }

                if (Entities != null) {
                    writer.Write(0x1cb5c415); // vector code
                    writer.Write(Entities.Count);
                    foreach (MessageEntity EntitiesElement in Entities)
                        EntitiesElement.Write(writer);
                }

            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    Unread = reader.ReadTrue();
                }

                if ((flags & (1 << 1)) != 0) {
                    Out = reader.ReadTrue();
                }

                if ((flags & (1 << 4)) != 0) {
                    Mentioned = reader.ReadTrue();
                }

                if ((flags & (1 << 5)) != 0) {
                    MediaUnread = reader.ReadTrue();
                }

                Id = reader.ReadInt32();
                UserId = reader.ReadInt32();
                Message = reader.ReadString();
                Pts = reader.ReadInt32();
                PtsCount = reader.ReadInt32();
                Date = reader.ReadInt32();
                if ((flags & (1 << 2)) != 0) {
                    FwdFromId = reader.Read<Peer>();
                }

                if ((flags & (1 << 2)) != 0) {
                    FwdDate = reader.ReadInt32();
                }

                if ((flags & (1 << 11)) != 0) {
                    ViaBotId = reader.ReadInt32();
                }

                if ((flags & (1 << 3)) != 0) {
                    ReplyToMsgId = reader.ReadInt32();
                }

                if ((flags & (1 << 7)) != 0) {
                    reader.ReadInt32(); // vector code
                    int EntitiesLength = reader.ReadInt32();
                    Entities = new List<MessageEntity>(EntitiesLength);
                    for (int EntitiesIndex = 0; EntitiesIndex < EntitiesLength; EntitiesIndex++)
                        Entities.Add(reader.Read<MessageEntity>());
                    }

            }

            public override string ToString()
            {
                return string.Format("(UpdateShortMessageType Unread:{0} Out:{1} Mentioned:{2} MediaUnread:{3} Id:{4} UserId:{5} Message:{6} Pts:{7} PtsCount:{8} Date:{9} FwdFromId:{10} FwdDate:{11} ViaBotId:{12} ReplyToMsgId:{13} Entities:{14})", Unread, Out, Mentioned, MediaUnread, Id, UserId, Message, Pts, PtsCount, Date, FwdFromId, FwdDate, ViaBotId, ReplyToMsgId, Entities);
            }
        }

        public class UpdateShortChatMessageType : Updates
        {
            public override uint ConstructorCode => 0x248afa62;

            public True Unread;
            public True Out;
            public True Mentioned;
            public True MediaUnread;
            public int Id;
            public int FromId;
            public int ChatId;
            public string Message;
            public int Pts;
            public int PtsCount;
            public int Date;
            public Peer FwdFromId;
            public int? FwdDate;
            public int? ViaBotId;
            public int? ReplyToMsgId;
            public List<MessageEntity> Entities;

            public UpdateShortChatMessageType() { }

            /// <summary>
            /// The following arguments can be null: Unread, Out, Mentioned, MediaUnread, FwdFromId, FwdDate, ViaBotId, ReplyToMsgId, Entities
            /// </summary>
            /// <param name="Unread">Can be null</param>
            /// <param name="Out">Can be null</param>
            /// <param name="Mentioned">Can be null</param>
            /// <param name="MediaUnread">Can be null</param>
            /// <param name="Id">Can NOT be null</param>
            /// <param name="FromId">Can NOT be null</param>
            /// <param name="ChatId">Can NOT be null</param>
            /// <param name="Message">Can NOT be null</param>
            /// <param name="Pts">Can NOT be null</param>
            /// <param name="PtsCount">Can NOT be null</param>
            /// <param name="Date">Can NOT be null</param>
            /// <param name="FwdFromId">Can be null</param>
            /// <param name="FwdDate">Can be null</param>
            /// <param name="ViaBotId">Can be null</param>
            /// <param name="ReplyToMsgId">Can be null</param>
            /// <param name="Entities">Can be null</param>
            public UpdateShortChatMessageType(True Unread, True Out, True Mentioned, True MediaUnread, int Id, int FromId, int ChatId, string Message, int Pts, int PtsCount, int Date, Peer FwdFromId, int? FwdDate, int? ViaBotId, int? ReplyToMsgId, List<MessageEntity> Entities)
            {
                this.Unread = Unread;
                this.Out = Out;
                this.Mentioned = Mentioned;
                this.MediaUnread = MediaUnread;
                this.Id = Id;
                this.FromId = FromId;
                this.ChatId = ChatId;
                this.Message = Message;
                this.Pts = Pts;
                this.PtsCount = PtsCount;
                this.Date = Date;
                this.FwdFromId = FwdFromId;
                this.FwdDate = FwdDate;
                this.ViaBotId = ViaBotId;
                this.ReplyToMsgId = ReplyToMsgId;
                this.Entities = Entities;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Unread != null ? 1 << 0 : 0) |
                    (Out != null ? 1 << 1 : 0) |
                    (Mentioned != null ? 1 << 4 : 0) |
                    (MediaUnread != null ? 1 << 5 : 0) |
                    (FwdFromId != null ? 1 << 2 : 0) |
                    (FwdDate != null ? 1 << 2 : 0) |
                    (ViaBotId != null ? 1 << 11 : 0) |
                    (ReplyToMsgId != null ? 1 << 3 : 0) |
                    (Entities != null ? 1 << 7 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Unread != null) {

                }

                if (Out != null) {

                }

                if (Mentioned != null) {

                }

                if (MediaUnread != null) {

                }

                writer.Write(Id);
                writer.Write(FromId);
                writer.Write(ChatId);
                writer.Write(Message);
                writer.Write(Pts);
                writer.Write(PtsCount);
                writer.Write(Date);
                if (FwdFromId != null) {
                    FwdFromId.Write(writer);
                }

                if (FwdDate != null) {
                    writer.Write(FwdDate.Value);
                }

                if (ViaBotId != null) {
                    writer.Write(ViaBotId.Value);
                }

                if (ReplyToMsgId != null) {
                    writer.Write(ReplyToMsgId.Value);
                }

                if (Entities != null) {
                    writer.Write(0x1cb5c415); // vector code
                    writer.Write(Entities.Count);
                    foreach (MessageEntity EntitiesElement in Entities)
                        EntitiesElement.Write(writer);
                }

            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    Unread = reader.ReadTrue();
                }

                if ((flags & (1 << 1)) != 0) {
                    Out = reader.ReadTrue();
                }

                if ((flags & (1 << 4)) != 0) {
                    Mentioned = reader.ReadTrue();
                }

                if ((flags & (1 << 5)) != 0) {
                    MediaUnread = reader.ReadTrue();
                }

                Id = reader.ReadInt32();
                FromId = reader.ReadInt32();
                ChatId = reader.ReadInt32();
                Message = reader.ReadString();
                Pts = reader.ReadInt32();
                PtsCount = reader.ReadInt32();
                Date = reader.ReadInt32();
                if ((flags & (1 << 2)) != 0) {
                    FwdFromId = reader.Read<Peer>();
                }

                if ((flags & (1 << 2)) != 0) {
                    FwdDate = reader.ReadInt32();
                }

                if ((flags & (1 << 11)) != 0) {
                    ViaBotId = reader.ReadInt32();
                }

                if ((flags & (1 << 3)) != 0) {
                    ReplyToMsgId = reader.ReadInt32();
                }

                if ((flags & (1 << 7)) != 0) {
                    reader.ReadInt32(); // vector code
                    int EntitiesLength = reader.ReadInt32();
                    Entities = new List<MessageEntity>(EntitiesLength);
                    for (int EntitiesIndex = 0; EntitiesIndex < EntitiesLength; EntitiesIndex++)
                        Entities.Add(reader.Read<MessageEntity>());
                    }

            }

            public override string ToString()
            {
                return string.Format("(UpdateShortChatMessageType Unread:{0} Out:{1} Mentioned:{2} MediaUnread:{3} Id:{4} FromId:{5} ChatId:{6} Message:{7} Pts:{8} PtsCount:{9} Date:{10} FwdFromId:{11} FwdDate:{12} ViaBotId:{13} ReplyToMsgId:{14} Entities:{15})", Unread, Out, Mentioned, MediaUnread, Id, FromId, ChatId, Message, Pts, PtsCount, Date, FwdFromId, FwdDate, ViaBotId, ReplyToMsgId, Entities);
            }
        }

        public class UpdateShortType : Updates
        {
            public override uint ConstructorCode => 0x78d4dec1;

            public Update Update;
            public int Date;

            public UpdateShortType() { }

            public UpdateShortType(Update Update, int Date)
            {
                this.Update = Update;
                this.Date = Date;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Update.Write(writer);
                writer.Write(Date);
            }

            public override void Read(TBinaryReader reader)
            {
                Update = reader.Read<Update>();
                Date = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdateShortType Update:{0} Date:{1})", Update, Date);
            }
        }

        public class UpdatesCombinedType : Updates
        {
            public override uint ConstructorCode => 0x725b04c3;

            public List<Update> Updates;
            public List<User> Users;
            public List<Chat> Chats;
            public int Date;
            public int SeqStart;
            public int Seq;

            public UpdatesCombinedType() { }

            public UpdatesCombinedType(List<Update> Updates, List<User> Users, List<Chat> Chats, int Date, int SeqStart, int Seq)
            {
                this.Updates = Updates;
                this.Users = Users;
                this.Chats = Chats;
                this.Date = Date;
                this.SeqStart = SeqStart;
                this.Seq = Seq;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Updates.Count);
                foreach (Update UpdatesElement in Updates)
                    UpdatesElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Chats.Count);
                foreach (Chat ChatsElement in Chats)
                    ChatsElement.Write(writer);
                writer.Write(Date);
                writer.Write(SeqStart);
                writer.Write(Seq);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int UpdatesLength = reader.ReadInt32();
                Updates = new List<Update>(UpdatesLength);
                for (int UpdatesIndex = 0; UpdatesIndex < UpdatesLength; UpdatesIndex++)
                    Updates.Add(reader.Read<Update>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
                reader.ReadInt32(); // vector code
                int ChatsLength = reader.ReadInt32();
                Chats = new List<Chat>(ChatsLength);
                for (int ChatsIndex = 0; ChatsIndex < ChatsLength; ChatsIndex++)
                    Chats.Add(reader.Read<Chat>());
                Date = reader.ReadInt32();
                SeqStart = reader.ReadInt32();
                Seq = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdatesCombinedType Updates:{0} Users:{1} Chats:{2} Date:{3} SeqStart:{4} Seq:{5})", Updates, Users, Chats, Date, SeqStart, Seq);
            }
        }

        public class UpdatesType : Updates
        {
            public override uint ConstructorCode => 0x74ae4240;

            public List<Update> Updates;
            public List<User> Users;
            public List<Chat> Chats;
            public int Date;
            public int Seq;

            public UpdatesType() { }

            public UpdatesType(List<Update> Updates, List<User> Users, List<Chat> Chats, int Date, int Seq)
            {
                this.Updates = Updates;
                this.Users = Users;
                this.Chats = Chats;
                this.Date = Date;
                this.Seq = Seq;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Updates.Count);
                foreach (Update UpdatesElement in Updates)
                    UpdatesElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Chats.Count);
                foreach (Chat ChatsElement in Chats)
                    ChatsElement.Write(writer);
                writer.Write(Date);
                writer.Write(Seq);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int UpdatesLength = reader.ReadInt32();
                Updates = new List<Update>(UpdatesLength);
                for (int UpdatesIndex = 0; UpdatesIndex < UpdatesLength; UpdatesIndex++)
                    Updates.Add(reader.Read<Update>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
                reader.ReadInt32(); // vector code
                int ChatsLength = reader.ReadInt32();
                Chats = new List<Chat>(ChatsLength);
                for (int ChatsIndex = 0; ChatsIndex < ChatsLength; ChatsIndex++)
                    Chats.Add(reader.Read<Chat>());
                Date = reader.ReadInt32();
                Seq = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(UpdatesType Updates:{0} Users:{1} Chats:{2} Date:{3} Seq:{4})", Updates, Users, Chats, Date, Seq);
            }
        }

        public class UpdateShortSentMessageType : Updates
        {
            public override uint ConstructorCode => 0x11f1331c;

            public True Unread;
            public True Out;
            public int Id;
            public int Pts;
            public int PtsCount;
            public int Date;
            public MessageMedia Media;
            public List<MessageEntity> Entities;

            public UpdateShortSentMessageType() { }

            /// <summary>
            /// The following arguments can be null: Unread, Out, Media, Entities
            /// </summary>
            /// <param name="Unread">Can be null</param>
            /// <param name="Out">Can be null</param>
            /// <param name="Id">Can NOT be null</param>
            /// <param name="Pts">Can NOT be null</param>
            /// <param name="PtsCount">Can NOT be null</param>
            /// <param name="Date">Can NOT be null</param>
            /// <param name="Media">Can be null</param>
            /// <param name="Entities">Can be null</param>
            public UpdateShortSentMessageType(True Unread, True Out, int Id, int Pts, int PtsCount, int Date, MessageMedia Media, List<MessageEntity> Entities)
            {
                this.Unread = Unread;
                this.Out = Out;
                this.Id = Id;
                this.Pts = Pts;
                this.PtsCount = PtsCount;
                this.Date = Date;
                this.Media = Media;
                this.Entities = Entities;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Unread != null ? 1 << 0 : 0) |
                    (Out != null ? 1 << 1 : 0) |
                    (Media != null ? 1 << 9 : 0) |
                    (Entities != null ? 1 << 7 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Unread != null) {

                }

                if (Out != null) {

                }

                writer.Write(Id);
                writer.Write(Pts);
                writer.Write(PtsCount);
                writer.Write(Date);
                if (Media != null) {
                    Media.Write(writer);
                }

                if (Entities != null) {
                    writer.Write(0x1cb5c415); // vector code
                    writer.Write(Entities.Count);
                    foreach (MessageEntity EntitiesElement in Entities)
                        EntitiesElement.Write(writer);
                }

            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    Unread = reader.ReadTrue();
                }

                if ((flags & (1 << 1)) != 0) {
                    Out = reader.ReadTrue();
                }

                Id = reader.ReadInt32();
                Pts = reader.ReadInt32();
                PtsCount = reader.ReadInt32();
                Date = reader.ReadInt32();
                if ((flags & (1 << 9)) != 0) {
                    Media = reader.Read<MessageMedia>();
                }

                if ((flags & (1 << 7)) != 0) {
                    reader.ReadInt32(); // vector code
                    int EntitiesLength = reader.ReadInt32();
                    Entities = new List<MessageEntity>(EntitiesLength);
                    for (int EntitiesIndex = 0; EntitiesIndex < EntitiesLength; EntitiesIndex++)
                        Entities.Add(reader.Read<MessageEntity>());
                    }

            }

            public override string ToString()
            {
                return string.Format("(UpdateShortSentMessageType Unread:{0} Out:{1} Id:{2} Pts:{3} PtsCount:{4} Date:{5} Media:{6} Entities:{7})", Unread, Out, Id, Pts, PtsCount, Date, Media, Entities);
            }
        }

        public class PhotosPhotosType : PhotosPhotos
        {
            public override uint ConstructorCode => 0x8dca6aa5;

            public List<Photo> Photos;
            public List<User> Users;

            public PhotosPhotosType() { }

            public PhotosPhotosType(List<Photo> Photos, List<User> Users)
            {
                this.Photos = Photos;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Photos.Count);
                foreach (Photo PhotosElement in Photos)
                    PhotosElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int PhotosLength = reader.ReadInt32();
                Photos = new List<Photo>(PhotosLength);
                for (int PhotosIndex = 0; PhotosIndex < PhotosLength; PhotosIndex++)
                    Photos.Add(reader.Read<Photo>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(PhotosPhotosType Photos:{0} Users:{1})", Photos, Users);
            }
        }

        public class PhotosPhotosSliceType : PhotosPhotos
        {
            public override uint ConstructorCode => 0x15051f54;

            public int Count;
            public List<Photo> Photos;
            public List<User> Users;

            public PhotosPhotosSliceType() { }

            public PhotosPhotosSliceType(int Count, List<Photo> Photos, List<User> Users)
            {
                this.Count = Count;
                this.Photos = Photos;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Count);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Photos.Count);
                foreach (Photo PhotosElement in Photos)
                    PhotosElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Count = reader.ReadInt32();
                reader.ReadInt32(); // vector code
                int PhotosLength = reader.ReadInt32();
                Photos = new List<Photo>(PhotosLength);
                for (int PhotosIndex = 0; PhotosIndex < PhotosLength; PhotosIndex++)
                    Photos.Add(reader.Read<Photo>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(PhotosPhotosSliceType Count:{0} Photos:{1} Users:{2})", Count, Photos, Users);
            }
        }

        public class PhotosPhotoType : PhotosPhoto
        {
            public override uint ConstructorCode => 0x20212ca8;

            public Photo Photo;
            public List<User> Users;

            public PhotosPhotoType() { }

            public PhotosPhotoType(Photo Photo, List<User> Users)
            {
                this.Photo = Photo;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Photo.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Photo = reader.Read<Photo>();
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(PhotosPhotoType Photo:{0} Users:{1})", Photo, Users);
            }
        }

        public class UploadFileType : UploadFile
        {
            public override uint ConstructorCode => 0x096a18d5;

            public StorageFileType Type;
            public int Mtime;
            public byte[] Bytes;

            public UploadFileType() { }

            public UploadFileType(StorageFileType Type, int Mtime, byte[] Bytes)
            {
                this.Type = Type;
                this.Mtime = Mtime;
                this.Bytes = Bytes;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Type.Write(writer);
                writer.Write(Mtime);
                writer.Write(Bytes);
            }

            public override void Read(TBinaryReader reader)
            {
                Type = reader.Read<StorageFileType>();
                Mtime = reader.ReadInt32();
                Bytes = reader.ReadBytes();
            }

            public override string ToString()
            {
                return string.Format("(UploadFileType Type:{0} Mtime:{1} Bytes:{2})", Type, Mtime, Bytes);
            }
        }

        public class DcOptionType : DcOption
        {
            public override uint ConstructorCode => 0x05d8c6cc;

            public True Ipv6;
            public True MediaOnly;
            public int Id;
            public string IpAddress;
            public int Port;

            public DcOptionType() { }

            /// <summary>
            /// The following arguments can be null: Ipv6, MediaOnly
            /// </summary>
            /// <param name="Ipv6">Can be null</param>
            /// <param name="MediaOnly">Can be null</param>
            /// <param name="Id">Can NOT be null</param>
            /// <param name="IpAddress">Can NOT be null</param>
            /// <param name="Port">Can NOT be null</param>
            public DcOptionType(True Ipv6, True MediaOnly, int Id, string IpAddress, int Port)
            {
                this.Ipv6 = Ipv6;
                this.MediaOnly = MediaOnly;
                this.Id = Id;
                this.IpAddress = IpAddress;
                this.Port = Port;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Ipv6 != null ? 1 << 0 : 0) |
                    (MediaOnly != null ? 1 << 1 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Ipv6 != null) {

                }

                if (MediaOnly != null) {

                }

                writer.Write(Id);
                writer.Write(IpAddress);
                writer.Write(Port);
            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    Ipv6 = reader.ReadTrue();
                }

                if ((flags & (1 << 1)) != 0) {
                    MediaOnly = reader.ReadTrue();
                }

                Id = reader.ReadInt32();
                IpAddress = reader.ReadString();
                Port = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(DcOptionType Ipv6:{0} MediaOnly:{1} Id:{2} IpAddress:{3} Port:{4})", Ipv6, MediaOnly, Id, IpAddress, Port);
            }
        }

        public class ConfigType : Config
        {
            public override uint ConstructorCode => 0x06bbc5f8;

            public int Date;
            public int Expires;
            public bool TestMode;
            public int ThisDc;
            public List<DcOption> DcOptions;
            public int ChatSizeMax;
            public int MegagroupSizeMax;
            public int ForwardedCountMax;
            public int OnlineUpdatePeriodMs;
            public int OfflineBlurTimeoutMs;
            public int OfflineIdleTimeoutMs;
            public int OnlineCloudTimeoutMs;
            public int NotifyCloudDelayMs;
            public int NotifyDefaultDelayMs;
            public int ChatBigSize;
            public int PushChatPeriodMs;
            public int PushChatLimit;
            public int SavedGifsLimit;
            public List<DisabledFeature> DisabledFeatures;

            public ConfigType() { }

            public ConfigType(int Date, int Expires, bool TestMode, int ThisDc, List<DcOption> DcOptions, int ChatSizeMax, int MegagroupSizeMax, int ForwardedCountMax, int OnlineUpdatePeriodMs, int OfflineBlurTimeoutMs, int OfflineIdleTimeoutMs, int OnlineCloudTimeoutMs, int NotifyCloudDelayMs, int NotifyDefaultDelayMs, int ChatBigSize, int PushChatPeriodMs, int PushChatLimit, int SavedGifsLimit, List<DisabledFeature> DisabledFeatures)
            {
                this.Date = Date;
                this.Expires = Expires;
                this.TestMode = TestMode;
                this.ThisDc = ThisDc;
                this.DcOptions = DcOptions;
                this.ChatSizeMax = ChatSizeMax;
                this.MegagroupSizeMax = MegagroupSizeMax;
                this.ForwardedCountMax = ForwardedCountMax;
                this.OnlineUpdatePeriodMs = OnlineUpdatePeriodMs;
                this.OfflineBlurTimeoutMs = OfflineBlurTimeoutMs;
                this.OfflineIdleTimeoutMs = OfflineIdleTimeoutMs;
                this.OnlineCloudTimeoutMs = OnlineCloudTimeoutMs;
                this.NotifyCloudDelayMs = NotifyCloudDelayMs;
                this.NotifyDefaultDelayMs = NotifyDefaultDelayMs;
                this.ChatBigSize = ChatBigSize;
                this.PushChatPeriodMs = PushChatPeriodMs;
                this.PushChatLimit = PushChatLimit;
                this.SavedGifsLimit = SavedGifsLimit;
                this.DisabledFeatures = DisabledFeatures;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Date);
                writer.Write(Expires);
                writer.Write(TestMode);
                writer.Write(ThisDc);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(DcOptions.Count);
                foreach (DcOption DcOptionsElement in DcOptions)
                    DcOptionsElement.Write(writer);
                writer.Write(ChatSizeMax);
                writer.Write(MegagroupSizeMax);
                writer.Write(ForwardedCountMax);
                writer.Write(OnlineUpdatePeriodMs);
                writer.Write(OfflineBlurTimeoutMs);
                writer.Write(OfflineIdleTimeoutMs);
                writer.Write(OnlineCloudTimeoutMs);
                writer.Write(NotifyCloudDelayMs);
                writer.Write(NotifyDefaultDelayMs);
                writer.Write(ChatBigSize);
                writer.Write(PushChatPeriodMs);
                writer.Write(PushChatLimit);
                writer.Write(SavedGifsLimit);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(DisabledFeatures.Count);
                foreach (DisabledFeature DisabledFeaturesElement in DisabledFeatures)
                    DisabledFeaturesElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Date = reader.ReadInt32();
                Expires = reader.ReadInt32();
                TestMode = reader.ReadBoolean();
                ThisDc = reader.ReadInt32();
                reader.ReadInt32(); // vector code
                int DcOptionsLength = reader.ReadInt32();
                DcOptions = new List<DcOption>(DcOptionsLength);
                for (int DcOptionsIndex = 0; DcOptionsIndex < DcOptionsLength; DcOptionsIndex++)
                    DcOptions.Add(reader.Read<DcOption>());
                ChatSizeMax = reader.ReadInt32();
                MegagroupSizeMax = reader.ReadInt32();
                ForwardedCountMax = reader.ReadInt32();
                OnlineUpdatePeriodMs = reader.ReadInt32();
                OfflineBlurTimeoutMs = reader.ReadInt32();
                OfflineIdleTimeoutMs = reader.ReadInt32();
                OnlineCloudTimeoutMs = reader.ReadInt32();
                NotifyCloudDelayMs = reader.ReadInt32();
                NotifyDefaultDelayMs = reader.ReadInt32();
                ChatBigSize = reader.ReadInt32();
                PushChatPeriodMs = reader.ReadInt32();
                PushChatLimit = reader.ReadInt32();
                SavedGifsLimit = reader.ReadInt32();
                reader.ReadInt32(); // vector code
                int DisabledFeaturesLength = reader.ReadInt32();
                DisabledFeatures = new List<DisabledFeature>(DisabledFeaturesLength);
                for (int DisabledFeaturesIndex = 0; DisabledFeaturesIndex < DisabledFeaturesLength; DisabledFeaturesIndex++)
                    DisabledFeatures.Add(reader.Read<DisabledFeature>());
            }

            public override string ToString()
            {
                return string.Format("(ConfigType Date:{0} Expires:{1} TestMode:{2} ThisDc:{3} DcOptions:{4} ChatSizeMax:{5} MegagroupSizeMax:{6} ForwardedCountMax:{7} OnlineUpdatePeriodMs:{8} OfflineBlurTimeoutMs:{9} OfflineIdleTimeoutMs:{10} OnlineCloudTimeoutMs:{11} NotifyCloudDelayMs:{12} NotifyDefaultDelayMs:{13} ChatBigSize:{14} PushChatPeriodMs:{15} PushChatLimit:{16} SavedGifsLimit:{17} DisabledFeatures:{18})", Date, Expires, TestMode, ThisDc, DcOptions, ChatSizeMax, MegagroupSizeMax, ForwardedCountMax, OnlineUpdatePeriodMs, OfflineBlurTimeoutMs, OfflineIdleTimeoutMs, OnlineCloudTimeoutMs, NotifyCloudDelayMs, NotifyDefaultDelayMs, ChatBigSize, PushChatPeriodMs, PushChatLimit, SavedGifsLimit, DisabledFeatures);
            }
        }

        public class NearestDcType : NearestDc
        {
            public override uint ConstructorCode => 0x8e1a1775;

            public string Country;
            public int ThisDc;
            public int NearestDc;

            public NearestDcType() { }

            public NearestDcType(string Country, int ThisDc, int NearestDc)
            {
                this.Country = Country;
                this.ThisDc = ThisDc;
                this.NearestDc = NearestDc;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Country);
                writer.Write(ThisDc);
                writer.Write(NearestDc);
            }

            public override void Read(TBinaryReader reader)
            {
                Country = reader.ReadString();
                ThisDc = reader.ReadInt32();
                NearestDc = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(NearestDcType Country:{0} ThisDc:{1} NearestDc:{2})", Country, ThisDc, NearestDc);
            }
        }

        public class HelpAppUpdateType : HelpAppUpdate
        {
            public override uint ConstructorCode => 0x8987f311;

            public int Id;
            public bool Critical;
            public string Url;
            public string Text;

            public HelpAppUpdateType() { }

            public HelpAppUpdateType(int Id, bool Critical, string Url, string Text)
            {
                this.Id = Id;
                this.Critical = Critical;
                this.Url = Url;
                this.Text = Text;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(Critical);
                writer.Write(Url);
                writer.Write(Text);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt32();
                Critical = reader.ReadBoolean();
                Url = reader.ReadString();
                Text = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(HelpAppUpdateType Id:{0} Critical:{1} Url:{2} Text:{3})", Id, Critical, Url, Text);
            }
        }

        public class HelpNoAppUpdateType : HelpAppUpdate
        {
            public override uint ConstructorCode => 0xc45a6536;

            public HelpNoAppUpdateType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(HelpNoAppUpdateType)";
            }
        }

        public class HelpInviteTextType : HelpInviteText
        {
            public override uint ConstructorCode => 0x18cb9f78;

            public string Message;

            public HelpInviteTextType() { }

            public HelpInviteTextType(string Message)
            {
                this.Message = Message;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Message);
            }

            public override void Read(TBinaryReader reader)
            {
                Message = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(HelpInviteTextType Message:{0})", Message);
            }
        }

        public class EncryptedChatEmptyType : EncryptedChat
        {
            public override uint ConstructorCode => 0xab7ec0a0;

            public int Id;

            public EncryptedChatEmptyType() { }

            public EncryptedChatEmptyType(int Id)
            {
                this.Id = Id;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(EncryptedChatEmptyType Id:{0})", Id);
            }
        }

        public class EncryptedChatWaitingType : EncryptedChat
        {
            public override uint ConstructorCode => 0x3bf703dc;

            public int Id;
            public long AccessHash;
            public int Date;
            public int AdminId;
            public int ParticipantId;

            public EncryptedChatWaitingType() { }

            public EncryptedChatWaitingType(int Id, long AccessHash, int Date, int AdminId, int ParticipantId)
            {
                this.Id = Id;
                this.AccessHash = AccessHash;
                this.Date = Date;
                this.AdminId = AdminId;
                this.ParticipantId = ParticipantId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(AccessHash);
                writer.Write(Date);
                writer.Write(AdminId);
                writer.Write(ParticipantId);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt32();
                AccessHash = reader.ReadInt64();
                Date = reader.ReadInt32();
                AdminId = reader.ReadInt32();
                ParticipantId = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(EncryptedChatWaitingType Id:{0} AccessHash:{1} Date:{2} AdminId:{3} ParticipantId:{4})", Id, AccessHash, Date, AdminId, ParticipantId);
            }
        }

        public class EncryptedChatRequestedType : EncryptedChat
        {
            public override uint ConstructorCode => 0xc878527e;

            public int Id;
            public long AccessHash;
            public int Date;
            public int AdminId;
            public int ParticipantId;
            public byte[] GA;

            public EncryptedChatRequestedType() { }

            public EncryptedChatRequestedType(int Id, long AccessHash, int Date, int AdminId, int ParticipantId, byte[] GA)
            {
                this.Id = Id;
                this.AccessHash = AccessHash;
                this.Date = Date;
                this.AdminId = AdminId;
                this.ParticipantId = ParticipantId;
                this.GA = GA;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(AccessHash);
                writer.Write(Date);
                writer.Write(AdminId);
                writer.Write(ParticipantId);
                writer.Write(GA);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt32();
                AccessHash = reader.ReadInt64();
                Date = reader.ReadInt32();
                AdminId = reader.ReadInt32();
                ParticipantId = reader.ReadInt32();
                GA = reader.ReadBytes();
            }

            public override string ToString()
            {
                return string.Format("(EncryptedChatRequestedType Id:{0} AccessHash:{1} Date:{2} AdminId:{3} ParticipantId:{4} GA:{5})", Id, AccessHash, Date, AdminId, ParticipantId, GA);
            }
        }

        public class EncryptedChatType : EncryptedChat
        {
            public override uint ConstructorCode => 0xfa56ce36;

            public int Id;
            public long AccessHash;
            public int Date;
            public int AdminId;
            public int ParticipantId;
            public byte[] GAOrB;
            public long KeyFingerprint;

            public EncryptedChatType() { }

            public EncryptedChatType(int Id, long AccessHash, int Date, int AdminId, int ParticipantId, byte[] GAOrB, long KeyFingerprint)
            {
                this.Id = Id;
                this.AccessHash = AccessHash;
                this.Date = Date;
                this.AdminId = AdminId;
                this.ParticipantId = ParticipantId;
                this.GAOrB = GAOrB;
                this.KeyFingerprint = KeyFingerprint;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(AccessHash);
                writer.Write(Date);
                writer.Write(AdminId);
                writer.Write(ParticipantId);
                writer.Write(GAOrB);
                writer.Write(KeyFingerprint);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt32();
                AccessHash = reader.ReadInt64();
                Date = reader.ReadInt32();
                AdminId = reader.ReadInt32();
                ParticipantId = reader.ReadInt32();
                GAOrB = reader.ReadBytes();
                KeyFingerprint = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(EncryptedChatType Id:{0} AccessHash:{1} Date:{2} AdminId:{3} ParticipantId:{4} GAOrB:{5} KeyFingerprint:{6})", Id, AccessHash, Date, AdminId, ParticipantId, GAOrB, KeyFingerprint);
            }
        }

        public class EncryptedChatDiscardedType : EncryptedChat
        {
            public override uint ConstructorCode => 0x13d6dd27;

            public int Id;

            public EncryptedChatDiscardedType() { }

            public EncryptedChatDiscardedType(int Id)
            {
                this.Id = Id;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(EncryptedChatDiscardedType Id:{0})", Id);
            }
        }

        public class InputEncryptedChatType : InputEncryptedChat
        {
            public override uint ConstructorCode => 0xf141b5e1;

            public int ChatId;
            public long AccessHash;

            public InputEncryptedChatType() { }

            public InputEncryptedChatType(int ChatId, long AccessHash)
            {
                this.ChatId = ChatId;
                this.AccessHash = AccessHash;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChatId);
                writer.Write(AccessHash);
            }

            public override void Read(TBinaryReader reader)
            {
                ChatId = reader.ReadInt32();
                AccessHash = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(InputEncryptedChatType ChatId:{0} AccessHash:{1})", ChatId, AccessHash);
            }
        }

        public class EncryptedFileEmptyType : EncryptedFile
        {
            public override uint ConstructorCode => 0xc21f497e;

            public EncryptedFileEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(EncryptedFileEmptyType)";
            }
        }

        public class EncryptedFileType : EncryptedFile
        {
            public override uint ConstructorCode => 0x4a70994c;

            public long Id;
            public long AccessHash;
            public int Size;
            public int DcId;
            public int KeyFingerprint;

            public EncryptedFileType() { }

            public EncryptedFileType(long Id, long AccessHash, int Size, int DcId, int KeyFingerprint)
            {
                this.Id = Id;
                this.AccessHash = AccessHash;
                this.Size = Size;
                this.DcId = DcId;
                this.KeyFingerprint = KeyFingerprint;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(AccessHash);
                writer.Write(Size);
                writer.Write(DcId);
                writer.Write(KeyFingerprint);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt64();
                AccessHash = reader.ReadInt64();
                Size = reader.ReadInt32();
                DcId = reader.ReadInt32();
                KeyFingerprint = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(EncryptedFileType Id:{0} AccessHash:{1} Size:{2} DcId:{3} KeyFingerprint:{4})", Id, AccessHash, Size, DcId, KeyFingerprint);
            }
        }

        public class InputEncryptedFileEmptyType : InputEncryptedFile
        {
            public override uint ConstructorCode => 0x1837c364;

            public InputEncryptedFileEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputEncryptedFileEmptyType)";
            }
        }

        public class InputEncryptedFileUploadedType : InputEncryptedFile
        {
            public override uint ConstructorCode => 0x64bd0306;

            public long Id;
            public int Parts;
            public string Md5Checksum;
            public int KeyFingerprint;

            public InputEncryptedFileUploadedType() { }

            public InputEncryptedFileUploadedType(long Id, int Parts, string Md5Checksum, int KeyFingerprint)
            {
                this.Id = Id;
                this.Parts = Parts;
                this.Md5Checksum = Md5Checksum;
                this.KeyFingerprint = KeyFingerprint;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(Parts);
                writer.Write(Md5Checksum);
                writer.Write(KeyFingerprint);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt64();
                Parts = reader.ReadInt32();
                Md5Checksum = reader.ReadString();
                KeyFingerprint = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(InputEncryptedFileUploadedType Id:{0} Parts:{1} Md5Checksum:{2} KeyFingerprint:{3})", Id, Parts, Md5Checksum, KeyFingerprint);
            }
        }

        public class InputEncryptedFileType : InputEncryptedFile
        {
            public override uint ConstructorCode => 0x5a17b5e5;

            public long Id;
            public long AccessHash;

            public InputEncryptedFileType() { }

            public InputEncryptedFileType(long Id, long AccessHash)
            {
                this.Id = Id;
                this.AccessHash = AccessHash;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(AccessHash);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt64();
                AccessHash = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(InputEncryptedFileType Id:{0} AccessHash:{1})", Id, AccessHash);
            }
        }

        public class InputEncryptedFileBigUploadedType : InputEncryptedFile
        {
            public override uint ConstructorCode => 0x2dc173c8;

            public long Id;
            public int Parts;
            public int KeyFingerprint;

            public InputEncryptedFileBigUploadedType() { }

            public InputEncryptedFileBigUploadedType(long Id, int Parts, int KeyFingerprint)
            {
                this.Id = Id;
                this.Parts = Parts;
                this.KeyFingerprint = KeyFingerprint;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(Parts);
                writer.Write(KeyFingerprint);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt64();
                Parts = reader.ReadInt32();
                KeyFingerprint = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(InputEncryptedFileBigUploadedType Id:{0} Parts:{1} KeyFingerprint:{2})", Id, Parts, KeyFingerprint);
            }
        }

        public class EncryptedMessageType : EncryptedMessage
        {
            public override uint ConstructorCode => 0xed18c118;

            public long RandomId;
            public int ChatId;
            public int Date;
            public byte[] Bytes;
            public EncryptedFile File;

            public EncryptedMessageType() { }

            public EncryptedMessageType(long RandomId, int ChatId, int Date, byte[] Bytes, EncryptedFile File)
            {
                this.RandomId = RandomId;
                this.ChatId = ChatId;
                this.Date = Date;
                this.Bytes = Bytes;
                this.File = File;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(RandomId);
                writer.Write(ChatId);
                writer.Write(Date);
                writer.Write(Bytes);
                File.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                RandomId = reader.ReadInt64();
                ChatId = reader.ReadInt32();
                Date = reader.ReadInt32();
                Bytes = reader.ReadBytes();
                File = reader.Read<EncryptedFile>();
            }

            public override string ToString()
            {
                return string.Format("(EncryptedMessageType RandomId:{0} ChatId:{1} Date:{2} Bytes:{3} File:{4})", RandomId, ChatId, Date, Bytes, File);
            }
        }

        public class EncryptedMessageServiceType : EncryptedMessage
        {
            public override uint ConstructorCode => 0x23734b06;

            public long RandomId;
            public int ChatId;
            public int Date;
            public byte[] Bytes;

            public EncryptedMessageServiceType() { }

            public EncryptedMessageServiceType(long RandomId, int ChatId, int Date, byte[] Bytes)
            {
                this.RandomId = RandomId;
                this.ChatId = ChatId;
                this.Date = Date;
                this.Bytes = Bytes;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(RandomId);
                writer.Write(ChatId);
                writer.Write(Date);
                writer.Write(Bytes);
            }

            public override void Read(TBinaryReader reader)
            {
                RandomId = reader.ReadInt64();
                ChatId = reader.ReadInt32();
                Date = reader.ReadInt32();
                Bytes = reader.ReadBytes();
            }

            public override string ToString()
            {
                return string.Format("(EncryptedMessageServiceType RandomId:{0} ChatId:{1} Date:{2} Bytes:{3})", RandomId, ChatId, Date, Bytes);
            }
        }

        public class MessagesDhConfigNotModifiedType : MessagesDhConfig
        {
            public override uint ConstructorCode => 0xc0e24635;

            public byte[] Random;

            public MessagesDhConfigNotModifiedType() { }

            public MessagesDhConfigNotModifiedType(byte[] Random)
            {
                this.Random = Random;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Random);
            }

            public override void Read(TBinaryReader reader)
            {
                Random = reader.ReadBytes();
            }

            public override string ToString()
            {
                return string.Format("(MessagesDhConfigNotModifiedType Random:{0})", Random);
            }
        }

        public class MessagesDhConfigType : MessagesDhConfig
        {
            public override uint ConstructorCode => 0x2c221edd;

            public int G;
            public byte[] P;
            public int Version;
            public byte[] Random;

            public MessagesDhConfigType() { }

            public MessagesDhConfigType(int G, byte[] P, int Version, byte[] Random)
            {
                this.G = G;
                this.P = P;
                this.Version = Version;
                this.Random = Random;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(G);
                writer.Write(P);
                writer.Write(Version);
                writer.Write(Random);
            }

            public override void Read(TBinaryReader reader)
            {
                G = reader.ReadInt32();
                P = reader.ReadBytes();
                Version = reader.ReadInt32();
                Random = reader.ReadBytes();
            }

            public override string ToString()
            {
                return string.Format("(MessagesDhConfigType G:{0} P:{1} Version:{2} Random:{3})", G, P, Version, Random);
            }
        }

        public class MessagesSentEncryptedMessageType : MessagesSentEncryptedMessage
        {
            public override uint ConstructorCode => 0x560f8935;

            public int Date;

            public MessagesSentEncryptedMessageType() { }

            public MessagesSentEncryptedMessageType(int Date)
            {
                this.Date = Date;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Date);
            }

            public override void Read(TBinaryReader reader)
            {
                Date = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessagesSentEncryptedMessageType Date:{0})", Date);
            }
        }

        public class MessagesSentEncryptedFileType : MessagesSentEncryptedMessage
        {
            public override uint ConstructorCode => 0x9493ff32;

            public int Date;
            public EncryptedFile File;

            public MessagesSentEncryptedFileType() { }

            public MessagesSentEncryptedFileType(int Date, EncryptedFile File)
            {
                this.Date = Date;
                this.File = File;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Date);
                File.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Date = reader.ReadInt32();
                File = reader.Read<EncryptedFile>();
            }

            public override string ToString()
            {
                return string.Format("(MessagesSentEncryptedFileType Date:{0} File:{1})", Date, File);
            }
        }

        public class InputDocumentEmptyType : InputDocument
        {
            public override uint ConstructorCode => 0x72f0eaae;

            public InputDocumentEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputDocumentEmptyType)";
            }
        }

        public class InputDocumentType : InputDocument
        {
            public override uint ConstructorCode => 0x18798952;

            public long Id;
            public long AccessHash;

            public InputDocumentType() { }

            public InputDocumentType(long Id, long AccessHash)
            {
                this.Id = Id;
                this.AccessHash = AccessHash;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(AccessHash);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt64();
                AccessHash = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(InputDocumentType Id:{0} AccessHash:{1})", Id, AccessHash);
            }
        }

        public class DocumentEmptyType : Document
        {
            public override uint ConstructorCode => 0x36f8c871;

            public long Id;

            public DocumentEmptyType() { }

            public DocumentEmptyType(long Id)
            {
                this.Id = Id;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(DocumentEmptyType Id:{0})", Id);
            }
        }

        public class DocumentType : Document
        {
            public override uint ConstructorCode => 0xf9a39f4f;

            public long Id;
            public long AccessHash;
            public int Date;
            public string MimeType;
            public int Size;
            public PhotoSize Thumb;
            public int DcId;
            public List<DocumentAttribute> Attributes;

            public DocumentType() { }

            public DocumentType(long Id, long AccessHash, int Date, string MimeType, int Size, PhotoSize Thumb, int DcId, List<DocumentAttribute> Attributes)
            {
                this.Id = Id;
                this.AccessHash = AccessHash;
                this.Date = Date;
                this.MimeType = MimeType;
                this.Size = Size;
                this.Thumb = Thumb;
                this.DcId = DcId;
                this.Attributes = Attributes;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(AccessHash);
                writer.Write(Date);
                writer.Write(MimeType);
                writer.Write(Size);
                Thumb.Write(writer);
                writer.Write(DcId);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Attributes.Count);
                foreach (DocumentAttribute AttributesElement in Attributes)
                    AttributesElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt64();
                AccessHash = reader.ReadInt64();
                Date = reader.ReadInt32();
                MimeType = reader.ReadString();
                Size = reader.ReadInt32();
                Thumb = reader.Read<PhotoSize>();
                DcId = reader.ReadInt32();
                reader.ReadInt32(); // vector code
                int AttributesLength = reader.ReadInt32();
                Attributes = new List<DocumentAttribute>(AttributesLength);
                for (int AttributesIndex = 0; AttributesIndex < AttributesLength; AttributesIndex++)
                    Attributes.Add(reader.Read<DocumentAttribute>());
            }

            public override string ToString()
            {
                return string.Format("(DocumentType Id:{0} AccessHash:{1} Date:{2} MimeType:{3} Size:{4} Thumb:{5} DcId:{6} Attributes:{7})", Id, AccessHash, Date, MimeType, Size, Thumb, DcId, Attributes);
            }
        }

        public class HelpSupportType : HelpSupport
        {
            public override uint ConstructorCode => 0x17c6b5f6;

            public string PhoneNumber;
            public User User;

            public HelpSupportType() { }

            public HelpSupportType(string PhoneNumber, User User)
            {
                this.PhoneNumber = PhoneNumber;
                this.User = User;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PhoneNumber);
                User.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                PhoneNumber = reader.ReadString();
                User = reader.Read<User>();
            }

            public override string ToString()
            {
                return string.Format("(HelpSupportType PhoneNumber:{0} User:{1})", PhoneNumber, User);
            }
        }

        public class NotifyPeerType : NotifyPeer
        {
            public override uint ConstructorCode => 0x9fd40bd8;

            public Peer Peer;

            public NotifyPeerType() { }

            public NotifyPeerType(Peer Peer)
            {
                this.Peer = Peer;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Peer = reader.Read<Peer>();
            }

            public override string ToString()
            {
                return string.Format("(NotifyPeerType Peer:{0})", Peer);
            }
        }

        public class NotifyUsersType : NotifyPeer
        {
            public override uint ConstructorCode => 0xb4c83b4c;

            public NotifyUsersType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(NotifyUsersType)";
            }
        }

        public class NotifyChatsType : NotifyPeer
        {
            public override uint ConstructorCode => 0xc007cec3;

            public NotifyChatsType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(NotifyChatsType)";
            }
        }

        public class NotifyAllType : NotifyPeer
        {
            public override uint ConstructorCode => 0x74d07c60;

            public NotifyAllType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(NotifyAllType)";
            }
        }

        public class SendMessageTypingActionType : SendMessageAction
        {
            public override uint ConstructorCode => 0x16bf744e;

            public SendMessageTypingActionType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(SendMessageTypingActionType)";
            }
        }

        public class SendMessageCancelActionType : SendMessageAction
        {
            public override uint ConstructorCode => 0xfd5ec8f5;

            public SendMessageCancelActionType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(SendMessageCancelActionType)";
            }
        }

        public class SendMessageRecordVideoActionType : SendMessageAction
        {
            public override uint ConstructorCode => 0xa187d66f;

            public SendMessageRecordVideoActionType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(SendMessageRecordVideoActionType)";
            }
        }

        public class SendMessageUploadVideoActionType : SendMessageAction
        {
            public override uint ConstructorCode => 0xe9763aec;

            public int Progress;

            public SendMessageUploadVideoActionType() { }

            public SendMessageUploadVideoActionType(int Progress)
            {
                this.Progress = Progress;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Progress);
            }

            public override void Read(TBinaryReader reader)
            {
                Progress = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(SendMessageUploadVideoActionType Progress:{0})", Progress);
            }
        }

        public class SendMessageRecordAudioActionType : SendMessageAction
        {
            public override uint ConstructorCode => 0xd52f73f7;

            public SendMessageRecordAudioActionType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(SendMessageRecordAudioActionType)";
            }
        }

        public class SendMessageUploadAudioActionType : SendMessageAction
        {
            public override uint ConstructorCode => 0xf351d7ab;

            public int Progress;

            public SendMessageUploadAudioActionType() { }

            public SendMessageUploadAudioActionType(int Progress)
            {
                this.Progress = Progress;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Progress);
            }

            public override void Read(TBinaryReader reader)
            {
                Progress = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(SendMessageUploadAudioActionType Progress:{0})", Progress);
            }
        }

        public class SendMessageUploadPhotoActionType : SendMessageAction
        {
            public override uint ConstructorCode => 0xd1d34a26;

            public int Progress;

            public SendMessageUploadPhotoActionType() { }

            public SendMessageUploadPhotoActionType(int Progress)
            {
                this.Progress = Progress;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Progress);
            }

            public override void Read(TBinaryReader reader)
            {
                Progress = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(SendMessageUploadPhotoActionType Progress:{0})", Progress);
            }
        }

        public class SendMessageUploadDocumentActionType : SendMessageAction
        {
            public override uint ConstructorCode => 0xaa0cd9e4;

            public int Progress;

            public SendMessageUploadDocumentActionType() { }

            public SendMessageUploadDocumentActionType(int Progress)
            {
                this.Progress = Progress;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Progress);
            }

            public override void Read(TBinaryReader reader)
            {
                Progress = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(SendMessageUploadDocumentActionType Progress:{0})", Progress);
            }
        }

        public class SendMessageGeoLocationActionType : SendMessageAction
        {
            public override uint ConstructorCode => 0x176f8ba1;

            public SendMessageGeoLocationActionType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(SendMessageGeoLocationActionType)";
            }
        }

        public class SendMessageChooseContactActionType : SendMessageAction
        {
            public override uint ConstructorCode => 0x628cbc6f;

            public SendMessageChooseContactActionType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(SendMessageChooseContactActionType)";
            }
        }

        public class ContactsFoundType : ContactsFound
        {
            public override uint ConstructorCode => 0x1aa1f784;

            public List<Peer> Results;
            public List<Chat> Chats;
            public List<User> Users;

            public ContactsFoundType() { }

            public ContactsFoundType(List<Peer> Results, List<Chat> Chats, List<User> Users)
            {
                this.Results = Results;
                this.Chats = Chats;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Results.Count);
                foreach (Peer ResultsElement in Results)
                    ResultsElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Chats.Count);
                foreach (Chat ChatsElement in Chats)
                    ChatsElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int ResultsLength = reader.ReadInt32();
                Results = new List<Peer>(ResultsLength);
                for (int ResultsIndex = 0; ResultsIndex < ResultsLength; ResultsIndex++)
                    Results.Add(reader.Read<Peer>());
                reader.ReadInt32(); // vector code
                int ChatsLength = reader.ReadInt32();
                Chats = new List<Chat>(ChatsLength);
                for (int ChatsIndex = 0; ChatsIndex < ChatsLength; ChatsIndex++)
                    Chats.Add(reader.Read<Chat>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(ContactsFoundType Results:{0} Chats:{1} Users:{2})", Results, Chats, Users);
            }
        }

        public class InputPrivacyKeyStatusTimestampType : InputPrivacyKey
        {
            public override uint ConstructorCode => 0x4f96cb18;

            public InputPrivacyKeyStatusTimestampType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputPrivacyKeyStatusTimestampType)";
            }
        }

        public class InputPrivacyKeyChatInviteType : InputPrivacyKey
        {
            public override uint ConstructorCode => 0xbdfb0426;

            public InputPrivacyKeyChatInviteType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputPrivacyKeyChatInviteType)";
            }
        }

        public class PrivacyKeyStatusTimestampType : PrivacyKey
        {
            public override uint ConstructorCode => 0xbc2eab30;

            public PrivacyKeyStatusTimestampType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(PrivacyKeyStatusTimestampType)";
            }
        }

        public class PrivacyKeyChatInviteType : PrivacyKey
        {
            public override uint ConstructorCode => 0x500e6dfa;

            public PrivacyKeyChatInviteType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(PrivacyKeyChatInviteType)";
            }
        }

        public class InputPrivacyValueAllowContactsType : InputPrivacyRule
        {
            public override uint ConstructorCode => 0x0d09e07b;

            public InputPrivacyValueAllowContactsType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputPrivacyValueAllowContactsType)";
            }
        }

        public class InputPrivacyValueAllowAllType : InputPrivacyRule
        {
            public override uint ConstructorCode => 0x184b35ce;

            public InputPrivacyValueAllowAllType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputPrivacyValueAllowAllType)";
            }
        }

        public class InputPrivacyValueAllowUsersType : InputPrivacyRule
        {
            public override uint ConstructorCode => 0x131cc67f;

            public List<InputUser> Users;

            public InputPrivacyValueAllowUsersType() { }

            public InputPrivacyValueAllowUsersType(List<InputUser> Users)
            {
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (InputUser UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<InputUser>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<InputUser>());
            }

            public override string ToString()
            {
                return string.Format("(InputPrivacyValueAllowUsersType Users:{0})", Users);
            }
        }

        public class InputPrivacyValueDisallowContactsType : InputPrivacyRule
        {
            public override uint ConstructorCode => 0x0ba52007;

            public InputPrivacyValueDisallowContactsType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputPrivacyValueDisallowContactsType)";
            }
        }

        public class InputPrivacyValueDisallowAllType : InputPrivacyRule
        {
            public override uint ConstructorCode => 0xd66b66c9;

            public InputPrivacyValueDisallowAllType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputPrivacyValueDisallowAllType)";
            }
        }

        public class InputPrivacyValueDisallowUsersType : InputPrivacyRule
        {
            public override uint ConstructorCode => 0x90110467;

            public List<InputUser> Users;

            public InputPrivacyValueDisallowUsersType() { }

            public InputPrivacyValueDisallowUsersType(List<InputUser> Users)
            {
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (InputUser UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<InputUser>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<InputUser>());
            }

            public override string ToString()
            {
                return string.Format("(InputPrivacyValueDisallowUsersType Users:{0})", Users);
            }
        }

        public class PrivacyValueAllowContactsType : PrivacyRule
        {
            public override uint ConstructorCode => 0xfffe1bac;

            public PrivacyValueAllowContactsType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(PrivacyValueAllowContactsType)";
            }
        }

        public class PrivacyValueAllowAllType : PrivacyRule
        {
            public override uint ConstructorCode => 0x65427b82;

            public PrivacyValueAllowAllType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(PrivacyValueAllowAllType)";
            }
        }

        public class PrivacyValueAllowUsersType : PrivacyRule
        {
            public override uint ConstructorCode => 0x4d5bbe0c;

            public List<int> Users;

            public PrivacyValueAllowUsersType() { }

            public PrivacyValueAllowUsersType(List<int> Users)
            {
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (int UsersElement in Users)
                    writer.Write(UsersElement);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<int>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.ReadInt32());
            }

            public override string ToString()
            {
                return string.Format("(PrivacyValueAllowUsersType Users:{0})", Users);
            }
        }

        public class PrivacyValueDisallowContactsType : PrivacyRule
        {
            public override uint ConstructorCode => 0xf888fa1a;

            public PrivacyValueDisallowContactsType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(PrivacyValueDisallowContactsType)";
            }
        }

        public class PrivacyValueDisallowAllType : PrivacyRule
        {
            public override uint ConstructorCode => 0x8b73e763;

            public PrivacyValueDisallowAllType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(PrivacyValueDisallowAllType)";
            }
        }

        public class PrivacyValueDisallowUsersType : PrivacyRule
        {
            public override uint ConstructorCode => 0x0c7f49b7;

            public List<int> Users;

            public PrivacyValueDisallowUsersType() { }

            public PrivacyValueDisallowUsersType(List<int> Users)
            {
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (int UsersElement in Users)
                    writer.Write(UsersElement);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<int>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.ReadInt32());
            }

            public override string ToString()
            {
                return string.Format("(PrivacyValueDisallowUsersType Users:{0})", Users);
            }
        }

        public class AccountPrivacyRulesType : AccountPrivacyRules
        {
            public override uint ConstructorCode => 0x554abb6f;

            public List<PrivacyRule> Rules;
            public List<User> Users;

            public AccountPrivacyRulesType() { }

            public AccountPrivacyRulesType(List<PrivacyRule> Rules, List<User> Users)
            {
                this.Rules = Rules;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Rules.Count);
                foreach (PrivacyRule RulesElement in Rules)
                    RulesElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int RulesLength = reader.ReadInt32();
                Rules = new List<PrivacyRule>(RulesLength);
                for (int RulesIndex = 0; RulesIndex < RulesLength; RulesIndex++)
                    Rules.Add(reader.Read<PrivacyRule>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(AccountPrivacyRulesType Rules:{0} Users:{1})", Rules, Users);
            }
        }

        public class AccountDaysTTLType : AccountDaysTTL
        {
            public override uint ConstructorCode => 0xb8d0afdf;

            public int Days;

            public AccountDaysTTLType() { }

            public AccountDaysTTLType(int Days)
            {
                this.Days = Days;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Days);
            }

            public override void Read(TBinaryReader reader)
            {
                Days = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(AccountDaysTTLType Days:{0})", Days);
            }
        }

        public class AccountSentChangePhoneCodeType : AccountSentChangePhoneCode
        {
            public override uint ConstructorCode => 0xa4f58c4c;

            public string PhoneCodeHash;
            public int SendCallTimeout;

            public AccountSentChangePhoneCodeType() { }

            public AccountSentChangePhoneCodeType(string PhoneCodeHash, int SendCallTimeout)
            {
                this.PhoneCodeHash = PhoneCodeHash;
                this.SendCallTimeout = SendCallTimeout;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(PhoneCodeHash);
                writer.Write(SendCallTimeout);
            }

            public override void Read(TBinaryReader reader)
            {
                PhoneCodeHash = reader.ReadString();
                SendCallTimeout = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(AccountSentChangePhoneCodeType PhoneCodeHash:{0} SendCallTimeout:{1})", PhoneCodeHash, SendCallTimeout);
            }
        }

        public class DocumentAttributeImageSizeType : DocumentAttribute
        {
            public override uint ConstructorCode => 0x6c37c15c;

            public int W;
            public int H;

            public DocumentAttributeImageSizeType() { }

            public DocumentAttributeImageSizeType(int W, int H)
            {
                this.W = W;
                this.H = H;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(W);
                writer.Write(H);
            }

            public override void Read(TBinaryReader reader)
            {
                W = reader.ReadInt32();
                H = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(DocumentAttributeImageSizeType W:{0} H:{1})", W, H);
            }
        }

        public class DocumentAttributeAnimatedType : DocumentAttribute
        {
            public override uint ConstructorCode => 0x11b58939;

            public DocumentAttributeAnimatedType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(DocumentAttributeAnimatedType)";
            }
        }

        public class DocumentAttributeStickerType : DocumentAttribute
        {
            public override uint ConstructorCode => 0x3a556302;

            public string Alt;
            public InputStickerSet Stickerset;

            public DocumentAttributeStickerType() { }

            public DocumentAttributeStickerType(string Alt, InputStickerSet Stickerset)
            {
                this.Alt = Alt;
                this.Stickerset = Stickerset;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Alt);
                Stickerset.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Alt = reader.ReadString();
                Stickerset = reader.Read<InputStickerSet>();
            }

            public override string ToString()
            {
                return string.Format("(DocumentAttributeStickerType Alt:{0} Stickerset:{1})", Alt, Stickerset);
            }
        }

        public class DocumentAttributeVideoType : DocumentAttribute
        {
            public override uint ConstructorCode => 0x5910cccb;

            public int Duration;
            public int W;
            public int H;

            public DocumentAttributeVideoType() { }

            public DocumentAttributeVideoType(int Duration, int W, int H)
            {
                this.Duration = Duration;
                this.W = W;
                this.H = H;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Duration);
                writer.Write(W);
                writer.Write(H);
            }

            public override void Read(TBinaryReader reader)
            {
                Duration = reader.ReadInt32();
                W = reader.ReadInt32();
                H = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(DocumentAttributeVideoType Duration:{0} W:{1} H:{2})", Duration, W, H);
            }
        }

        public class DocumentAttributeAudioType : DocumentAttribute
        {
            public override uint ConstructorCode => 0x9852f9c6;

            public True Voice;
            public int Duration;
            public string Title;
            public string Performer;
            public byte[] Waveform;

            public DocumentAttributeAudioType() { }

            /// <summary>
            /// The following arguments can be null: Voice, Title, Performer, Waveform
            /// </summary>
            /// <param name="Voice">Can be null</param>
            /// <param name="Duration">Can NOT be null</param>
            /// <param name="Title">Can be null</param>
            /// <param name="Performer">Can be null</param>
            /// <param name="Waveform">Can be null</param>
            public DocumentAttributeAudioType(True Voice, int Duration, string Title, string Performer, byte[] Waveform)
            {
                this.Voice = Voice;
                this.Duration = Duration;
                this.Title = Title;
                this.Performer = Performer;
                this.Waveform = Waveform;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Voice != null ? 1 << 10 : 0) |
                    (Title != null ? 1 << 0 : 0) |
                    (Performer != null ? 1 << 1 : 0) |
                    (Waveform != null ? 1 << 2 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Voice != null) {

                }

                writer.Write(Duration);
                if (Title != null) {
                    writer.Write(Title);
                }

                if (Performer != null) {
                    writer.Write(Performer);
                }

                if (Waveform != null) {
                    writer.Write(Waveform);
                }

            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 10)) != 0) {
                    Voice = reader.ReadTrue();
                }

                Duration = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    Title = reader.ReadString();
                }

                if ((flags & (1 << 1)) != 0) {
                    Performer = reader.ReadString();
                }

                if ((flags & (1 << 2)) != 0) {
                    Waveform = reader.ReadBytes();
                }

            }

            public override string ToString()
            {
                return string.Format("(DocumentAttributeAudioType Voice:{0} Duration:{1} Title:{2} Performer:{3} Waveform:{4})", Voice, Duration, Title, Performer, Waveform);
            }
        }

        public class DocumentAttributeFilenameType : DocumentAttribute
        {
            public override uint ConstructorCode => 0x15590068;

            public string FileName;

            public DocumentAttributeFilenameType() { }

            public DocumentAttributeFilenameType(string FileName)
            {
                this.FileName = FileName;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(FileName);
            }

            public override void Read(TBinaryReader reader)
            {
                FileName = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(DocumentAttributeFilenameType FileName:{0})", FileName);
            }
        }

        public class MessagesStickersNotModifiedType : MessagesStickers
        {
            public override uint ConstructorCode => 0xf1749a22;

            public MessagesStickersNotModifiedType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(MessagesStickersNotModifiedType)";
            }
        }

        public class MessagesStickersType : MessagesStickers
        {
            public override uint ConstructorCode => 0x8a8ecd32;

            public string Hash;
            public List<Document> Stickers;

            public MessagesStickersType() { }

            public MessagesStickersType(string Hash, List<Document> Stickers)
            {
                this.Hash = Hash;
                this.Stickers = Stickers;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Hash);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Stickers.Count);
                foreach (Document StickersElement in Stickers)
                    StickersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Hash = reader.ReadString();
                reader.ReadInt32(); // vector code
                int StickersLength = reader.ReadInt32();
                Stickers = new List<Document>(StickersLength);
                for (int StickersIndex = 0; StickersIndex < StickersLength; StickersIndex++)
                    Stickers.Add(reader.Read<Document>());
            }

            public override string ToString()
            {
                return string.Format("(MessagesStickersType Hash:{0} Stickers:{1})", Hash, Stickers);
            }
        }

        public class StickerPackType : StickerPack
        {
            public override uint ConstructorCode => 0x12b299d4;

            public string Emoticon;
            public List<long> Documents;

            public StickerPackType() { }

            public StickerPackType(string Emoticon, List<long> Documents)
            {
                this.Emoticon = Emoticon;
                this.Documents = Documents;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Emoticon);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Documents.Count);
                foreach (long DocumentsElement in Documents)
                    writer.Write(DocumentsElement);
            }

            public override void Read(TBinaryReader reader)
            {
                Emoticon = reader.ReadString();
                reader.ReadInt32(); // vector code
                int DocumentsLength = reader.ReadInt32();
                Documents = new List<long>(DocumentsLength);
                for (int DocumentsIndex = 0; DocumentsIndex < DocumentsLength; DocumentsIndex++)
                    Documents.Add(reader.ReadInt64());
            }

            public override string ToString()
            {
                return string.Format("(StickerPackType Emoticon:{0} Documents:{1})", Emoticon, Documents);
            }
        }

        public class MessagesAllStickersNotModifiedType : MessagesAllStickers
        {
            public override uint ConstructorCode => 0xe86602c3;

            public MessagesAllStickersNotModifiedType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(MessagesAllStickersNotModifiedType)";
            }
        }

        public class MessagesAllStickersType : MessagesAllStickers
        {
            public override uint ConstructorCode => 0xedfd405f;

            public int Hash;
            public List<StickerSet> Sets;

            public MessagesAllStickersType() { }

            public MessagesAllStickersType(int Hash, List<StickerSet> Sets)
            {
                this.Hash = Hash;
                this.Sets = Sets;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Hash);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Sets.Count);
                foreach (StickerSet SetsElement in Sets)
                    SetsElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Hash = reader.ReadInt32();
                reader.ReadInt32(); // vector code
                int SetsLength = reader.ReadInt32();
                Sets = new List<StickerSet>(SetsLength);
                for (int SetsIndex = 0; SetsIndex < SetsLength; SetsIndex++)
                    Sets.Add(reader.Read<StickerSet>());
            }

            public override string ToString()
            {
                return string.Format("(MessagesAllStickersType Hash:{0} Sets:{1})", Hash, Sets);
            }
        }

        public class DisabledFeatureType : DisabledFeature
        {
            public override uint ConstructorCode => 0xae636f24;

            public string Feature;
            public string Description;

            public DisabledFeatureType() { }

            public DisabledFeatureType(string Feature, string Description)
            {
                this.Feature = Feature;
                this.Description = Description;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Feature);
                writer.Write(Description);
            }

            public override void Read(TBinaryReader reader)
            {
                Feature = reader.ReadString();
                Description = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(DisabledFeatureType Feature:{0} Description:{1})", Feature, Description);
            }
        }

        public class MessagesAffectedMessagesType : MessagesAffectedMessages
        {
            public override uint ConstructorCode => 0x84d19185;

            public int Pts;
            public int PtsCount;

            public MessagesAffectedMessagesType() { }

            public MessagesAffectedMessagesType(int Pts, int PtsCount)
            {
                this.Pts = Pts;
                this.PtsCount = PtsCount;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Pts);
                writer.Write(PtsCount);
            }

            public override void Read(TBinaryReader reader)
            {
                Pts = reader.ReadInt32();
                PtsCount = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessagesAffectedMessagesType Pts:{0} PtsCount:{1})", Pts, PtsCount);
            }
        }

        public class ContactLinkUnknownType : ContactLink
        {
            public override uint ConstructorCode => 0x5f4f9247;

            public ContactLinkUnknownType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(ContactLinkUnknownType)";
            }
        }

        public class ContactLinkNoneType : ContactLink
        {
            public override uint ConstructorCode => 0xfeedd3ad;

            public ContactLinkNoneType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(ContactLinkNoneType)";
            }
        }

        public class ContactLinkHasPhoneType : ContactLink
        {
            public override uint ConstructorCode => 0x268f3f59;

            public ContactLinkHasPhoneType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(ContactLinkHasPhoneType)";
            }
        }

        public class ContactLinkContactType : ContactLink
        {
            public override uint ConstructorCode => 0xd502c2d0;

            public ContactLinkContactType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(ContactLinkContactType)";
            }
        }

        public class WebPageEmptyType : WebPage
        {
            public override uint ConstructorCode => 0xeb1477e8;

            public long Id;

            public WebPageEmptyType() { }

            public WebPageEmptyType(long Id)
            {
                this.Id = Id;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(WebPageEmptyType Id:{0})", Id);
            }
        }

        public class WebPagePendingType : WebPage
        {
            public override uint ConstructorCode => 0xc586da1c;

            public long Id;
            public int Date;

            public WebPagePendingType() { }

            public WebPagePendingType(long Id, int Date)
            {
                this.Id = Id;
                this.Date = Date;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(Date);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt64();
                Date = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(WebPagePendingType Id:{0} Date:{1})", Id, Date);
            }
        }

        public class WebPageType : WebPage
        {
            public override uint ConstructorCode => 0xca820ed7;

            public long Id;
            public string Url;
            public string DisplayUrl;
            public string Type;
            public string SiteName;
            public string Title;
            public string Description;
            public Photo Photo;
            public string EmbedUrl;
            public string EmbedType;
            public int? EmbedWidth;
            public int? EmbedHeight;
            public int? Duration;
            public string Author;
            public Document Document;

            public WebPageType() { }

            /// <summary>
            /// The following arguments can be null: Type, SiteName, Title, Description, Photo, EmbedUrl, EmbedType, EmbedWidth, EmbedHeight, Duration, Author, Document
            /// </summary>
            /// <param name="Id">Can NOT be null</param>
            /// <param name="Url">Can NOT be null</param>
            /// <param name="DisplayUrl">Can NOT be null</param>
            /// <param name="Type">Can be null</param>
            /// <param name="SiteName">Can be null</param>
            /// <param name="Title">Can be null</param>
            /// <param name="Description">Can be null</param>
            /// <param name="Photo">Can be null</param>
            /// <param name="EmbedUrl">Can be null</param>
            /// <param name="EmbedType">Can be null</param>
            /// <param name="EmbedWidth">Can be null</param>
            /// <param name="EmbedHeight">Can be null</param>
            /// <param name="Duration">Can be null</param>
            /// <param name="Author">Can be null</param>
            /// <param name="Document">Can be null</param>
            public WebPageType(long Id, string Url, string DisplayUrl, string Type, string SiteName, string Title, string Description, Photo Photo, string EmbedUrl, string EmbedType, int? EmbedWidth, int? EmbedHeight, int? Duration, string Author, Document Document)
            {
                this.Id = Id;
                this.Url = Url;
                this.DisplayUrl = DisplayUrl;
                this.Type = Type;
                this.SiteName = SiteName;
                this.Title = Title;
                this.Description = Description;
                this.Photo = Photo;
                this.EmbedUrl = EmbedUrl;
                this.EmbedType = EmbedType;
                this.EmbedWidth = EmbedWidth;
                this.EmbedHeight = EmbedHeight;
                this.Duration = Duration;
                this.Author = Author;
                this.Document = Document;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Type != null ? 1 << 0 : 0) |
                    (SiteName != null ? 1 << 1 : 0) |
                    (Title != null ? 1 << 2 : 0) |
                    (Description != null ? 1 << 3 : 0) |
                    (Photo != null ? 1 << 4 : 0) |
                    (EmbedUrl != null ? 1 << 5 : 0) |
                    (EmbedType != null ? 1 << 5 : 0) |
                    (EmbedWidth != null ? 1 << 6 : 0) |
                    (EmbedHeight != null ? 1 << 6 : 0) |
                    (Duration != null ? 1 << 7 : 0) |
                    (Author != null ? 1 << 8 : 0) |
                    (Document != null ? 1 << 9 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                writer.Write(Id);
                writer.Write(Url);
                writer.Write(DisplayUrl);
                if (Type != null) {
                    writer.Write(Type);
                }

                if (SiteName != null) {
                    writer.Write(SiteName);
                }

                if (Title != null) {
                    writer.Write(Title);
                }

                if (Description != null) {
                    writer.Write(Description);
                }

                if (Photo != null) {
                    Photo.Write(writer);
                }

                if (EmbedUrl != null) {
                    writer.Write(EmbedUrl);
                }

                if (EmbedType != null) {
                    writer.Write(EmbedType);
                }

                if (EmbedWidth != null) {
                    writer.Write(EmbedWidth.Value);
                }

                if (EmbedHeight != null) {
                    writer.Write(EmbedHeight.Value);
                }

                if (Duration != null) {
                    writer.Write(Duration.Value);
                }

                if (Author != null) {
                    writer.Write(Author);
                }

                if (Document != null) {
                    Document.Write(writer);
                }

            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                Id = reader.ReadInt64();
                Url = reader.ReadString();
                DisplayUrl = reader.ReadString();
                if ((flags & (1 << 0)) != 0) {
                    Type = reader.ReadString();
                }

                if ((flags & (1 << 1)) != 0) {
                    SiteName = reader.ReadString();
                }

                if ((flags & (1 << 2)) != 0) {
                    Title = reader.ReadString();
                }

                if ((flags & (1 << 3)) != 0) {
                    Description = reader.ReadString();
                }

                if ((flags & (1 << 4)) != 0) {
                    Photo = reader.Read<Photo>();
                }

                if ((flags & (1 << 5)) != 0) {
                    EmbedUrl = reader.ReadString();
                }

                if ((flags & (1 << 5)) != 0) {
                    EmbedType = reader.ReadString();
                }

                if ((flags & (1 << 6)) != 0) {
                    EmbedWidth = reader.ReadInt32();
                }

                if ((flags & (1 << 6)) != 0) {
                    EmbedHeight = reader.ReadInt32();
                }

                if ((flags & (1 << 7)) != 0) {
                    Duration = reader.ReadInt32();
                }

                if ((flags & (1 << 8)) != 0) {
                    Author = reader.ReadString();
                }

                if ((flags & (1 << 9)) != 0) {
                    Document = reader.Read<Document>();
                }

            }

            public override string ToString()
            {
                return string.Format("(WebPageType Id:{0} Url:{1} DisplayUrl:{2} Type:{3} SiteName:{4} Title:{5} Description:{6} Photo:{7} EmbedUrl:{8} EmbedType:{9} EmbedWidth:{10} EmbedHeight:{11} Duration:{12} Author:{13} Document:{14})", Id, Url, DisplayUrl, Type, SiteName, Title, Description, Photo, EmbedUrl, EmbedType, EmbedWidth, EmbedHeight, Duration, Author, Document);
            }
        }

        public class AuthorizationType : Authorization
        {
            public override uint ConstructorCode => 0x7bf2e6f6;

            public long Hash;
            public int Flags;
            public string DeviceModel;
            public string Platform;
            public string SystemVersion;
            public int ApiId;
            public string AppName;
            public string AppVersion;
            public int DateCreated;
            public int DateActive;
            public string Ip;
            public string Country;
            public string Region;

            public AuthorizationType() { }

            public AuthorizationType(long Hash, int Flags, string DeviceModel, string Platform, string SystemVersion, int ApiId, string AppName, string AppVersion, int DateCreated, int DateActive, string Ip, string Country, string Region)
            {
                this.Hash = Hash;
                this.Flags = Flags;
                this.DeviceModel = DeviceModel;
                this.Platform = Platform;
                this.SystemVersion = SystemVersion;
                this.ApiId = ApiId;
                this.AppName = AppName;
                this.AppVersion = AppVersion;
                this.DateCreated = DateCreated;
                this.DateActive = DateActive;
                this.Ip = Ip;
                this.Country = Country;
                this.Region = Region;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Hash);
                writer.Write(Flags);
                writer.Write(DeviceModel);
                writer.Write(Platform);
                writer.Write(SystemVersion);
                writer.Write(ApiId);
                writer.Write(AppName);
                writer.Write(AppVersion);
                writer.Write(DateCreated);
                writer.Write(DateActive);
                writer.Write(Ip);
                writer.Write(Country);
                writer.Write(Region);
            }

            public override void Read(TBinaryReader reader)
            {
                Hash = reader.ReadInt64();
                Flags = reader.ReadInt32();
                DeviceModel = reader.ReadString();
                Platform = reader.ReadString();
                SystemVersion = reader.ReadString();
                ApiId = reader.ReadInt32();
                AppName = reader.ReadString();
                AppVersion = reader.ReadString();
                DateCreated = reader.ReadInt32();
                DateActive = reader.ReadInt32();
                Ip = reader.ReadString();
                Country = reader.ReadString();
                Region = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(AuthorizationType Hash:{0} Flags:{1} DeviceModel:{2} Platform:{3} SystemVersion:{4} ApiId:{5} AppName:{6} AppVersion:{7} DateCreated:{8} DateActive:{9} Ip:{10} Country:{11} Region:{12})", Hash, Flags, DeviceModel, Platform, SystemVersion, ApiId, AppName, AppVersion, DateCreated, DateActive, Ip, Country, Region);
            }
        }

        public class AccountAuthorizationsType : AccountAuthorizations
        {
            public override uint ConstructorCode => 0x1250abde;

            public List<Authorization> Authorizations;

            public AccountAuthorizationsType() { }

            public AccountAuthorizationsType(List<Authorization> Authorizations)
            {
                this.Authorizations = Authorizations;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Authorizations.Count);
                foreach (Authorization AuthorizationsElement in Authorizations)
                    AuthorizationsElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int AuthorizationsLength = reader.ReadInt32();
                Authorizations = new List<Authorization>(AuthorizationsLength);
                for (int AuthorizationsIndex = 0; AuthorizationsIndex < AuthorizationsLength; AuthorizationsIndex++)
                    Authorizations.Add(reader.Read<Authorization>());
            }

            public override string ToString()
            {
                return string.Format("(AccountAuthorizationsType Authorizations:{0})", Authorizations);
            }
        }

        public class AccountNoPasswordType : AccountPassword
        {
            public override uint ConstructorCode => 0x96dabc18;

            public byte[] NewSalt;
            public string EmailUnconfirmedPattern;

            public AccountNoPasswordType() { }

            public AccountNoPasswordType(byte[] NewSalt, string EmailUnconfirmedPattern)
            {
                this.NewSalt = NewSalt;
                this.EmailUnconfirmedPattern = EmailUnconfirmedPattern;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(NewSalt);
                writer.Write(EmailUnconfirmedPattern);
            }

            public override void Read(TBinaryReader reader)
            {
                NewSalt = reader.ReadBytes();
                EmailUnconfirmedPattern = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(AccountNoPasswordType NewSalt:{0} EmailUnconfirmedPattern:{1})", NewSalt, EmailUnconfirmedPattern);
            }
        }

        public class AccountPasswordType : AccountPassword
        {
            public override uint ConstructorCode => 0x7c18141c;

            public byte[] CurrentSalt;
            public byte[] NewSalt;
            public string Hint;
            public bool HasRecovery;
            public string EmailUnconfirmedPattern;

            public AccountPasswordType() { }

            public AccountPasswordType(byte[] CurrentSalt, byte[] NewSalt, string Hint, bool HasRecovery, string EmailUnconfirmedPattern)
            {
                this.CurrentSalt = CurrentSalt;
                this.NewSalt = NewSalt;
                this.Hint = Hint;
                this.HasRecovery = HasRecovery;
                this.EmailUnconfirmedPattern = EmailUnconfirmedPattern;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(CurrentSalt);
                writer.Write(NewSalt);
                writer.Write(Hint);
                writer.Write(HasRecovery);
                writer.Write(EmailUnconfirmedPattern);
            }

            public override void Read(TBinaryReader reader)
            {
                CurrentSalt = reader.ReadBytes();
                NewSalt = reader.ReadBytes();
                Hint = reader.ReadString();
                HasRecovery = reader.ReadBoolean();
                EmailUnconfirmedPattern = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(AccountPasswordType CurrentSalt:{0} NewSalt:{1} Hint:{2} HasRecovery:{3} EmailUnconfirmedPattern:{4})", CurrentSalt, NewSalt, Hint, HasRecovery, EmailUnconfirmedPattern);
            }
        }

        public class AccountPasswordSettingsType : AccountPasswordSettings
        {
            public override uint ConstructorCode => 0xb7b72ab3;

            public string Email;

            public AccountPasswordSettingsType() { }

            public AccountPasswordSettingsType(string Email)
            {
                this.Email = Email;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Email);
            }

            public override void Read(TBinaryReader reader)
            {
                Email = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(AccountPasswordSettingsType Email:{0})", Email);
            }
        }

        public class AccountPasswordInputSettingsType : AccountPasswordInputSettings
        {
            public override uint ConstructorCode => 0x86916deb;

            public byte[] NewSalt;
            public byte[] NewPasswordHash;
            public string Hint;
            public string Email;

            public AccountPasswordInputSettingsType() { }

            /// <summary>
            /// The following arguments can be null: NewSalt, NewPasswordHash, Hint, Email
            /// </summary>
            /// <param name="NewSalt">Can be null</param>
            /// <param name="NewPasswordHash">Can be null</param>
            /// <param name="Hint">Can be null</param>
            /// <param name="Email">Can be null</param>
            public AccountPasswordInputSettingsType(byte[] NewSalt, byte[] NewPasswordHash, string Hint, string Email)
            {
                this.NewSalt = NewSalt;
                this.NewPasswordHash = NewPasswordHash;
                this.Hint = Hint;
                this.Email = Email;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (NewSalt != null ? 1 << 0 : 0) |
                    (NewPasswordHash != null ? 1 << 0 : 0) |
                    (Hint != null ? 1 << 0 : 0) |
                    (Email != null ? 1 << 1 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (NewSalt != null) {
                    writer.Write(NewSalt);
                }

                if (NewPasswordHash != null) {
                    writer.Write(NewPasswordHash);
                }

                if (Hint != null) {
                    writer.Write(Hint);
                }

                if (Email != null) {
                    writer.Write(Email);
                }

            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    NewSalt = reader.ReadBytes();
                }

                if ((flags & (1 << 0)) != 0) {
                    NewPasswordHash = reader.ReadBytes();
                }

                if ((flags & (1 << 0)) != 0) {
                    Hint = reader.ReadString();
                }

                if ((flags & (1 << 1)) != 0) {
                    Email = reader.ReadString();
                }

            }

            public override string ToString()
            {
                return string.Format("(AccountPasswordInputSettingsType NewSalt:{0} NewPasswordHash:{1} Hint:{2} Email:{3})", NewSalt, NewPasswordHash, Hint, Email);
            }
        }

        public class AuthPasswordRecoveryType : AuthPasswordRecovery
        {
            public override uint ConstructorCode => 0x137948a5;

            public string EmailPattern;

            public AuthPasswordRecoveryType() { }

            public AuthPasswordRecoveryType(string EmailPattern)
            {
                this.EmailPattern = EmailPattern;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(EmailPattern);
            }

            public override void Read(TBinaryReader reader)
            {
                EmailPattern = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(AuthPasswordRecoveryType EmailPattern:{0})", EmailPattern);
            }
        }

        public class ReceivedNotifyMessageType : ReceivedNotifyMessage
        {
            public override uint ConstructorCode => 0xa384b779;

            public int Id;
            public int Flags;

            public ReceivedNotifyMessageType() { }

            public ReceivedNotifyMessageType(int Id, int Flags)
            {
                this.Id = Id;
                this.Flags = Flags;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(Flags);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt32();
                Flags = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(ReceivedNotifyMessageType Id:{0} Flags:{1})", Id, Flags);
            }
        }

        public class ChatInviteEmptyType : ExportedChatInvite
        {
            public override uint ConstructorCode => 0x69df3769;

            public ChatInviteEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(ChatInviteEmptyType)";
            }
        }

        public class ChatInviteExportedType : ExportedChatInvite
        {
            public override uint ConstructorCode => 0xfc2e05bc;

            public string Link;

            public ChatInviteExportedType() { }

            public ChatInviteExportedType(string Link)
            {
                this.Link = Link;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Link);
            }

            public override void Read(TBinaryReader reader)
            {
                Link = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(ChatInviteExportedType Link:{0})", Link);
            }
        }

        public class ChatInviteAlreadyType : ChatInvite
        {
            public override uint ConstructorCode => 0x5a686d7c;

            public Chat Chat;

            public ChatInviteAlreadyType() { }

            public ChatInviteAlreadyType(Chat Chat)
            {
                this.Chat = Chat;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Chat.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Chat = reader.Read<Chat>();
            }

            public override string ToString()
            {
                return string.Format("(ChatInviteAlreadyType Chat:{0})", Chat);
            }
        }

        public class ChatInviteType : ChatInvite
        {
            public override uint ConstructorCode => 0x93e99b60;

            public True Channel;
            public True Broadcast;
            public True Public;
            public True Megagroup;
            public string Title;

            public ChatInviteType() { }

            /// <summary>
            /// The following arguments can be null: Channel, Broadcast, Public, Megagroup
            /// </summary>
            /// <param name="Channel">Can be null</param>
            /// <param name="Broadcast">Can be null</param>
            /// <param name="Public">Can be null</param>
            /// <param name="Megagroup">Can be null</param>
            /// <param name="Title">Can NOT be null</param>
            public ChatInviteType(True Channel, True Broadcast, True Public, True Megagroup, string Title)
            {
                this.Channel = Channel;
                this.Broadcast = Broadcast;
                this.Public = Public;
                this.Megagroup = Megagroup;
                this.Title = Title;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Channel != null ? 1 << 0 : 0) |
                    (Broadcast != null ? 1 << 1 : 0) |
                    (Public != null ? 1 << 2 : 0) |
                    (Megagroup != null ? 1 << 3 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Channel != null) {

                }

                if (Broadcast != null) {

                }

                if (Public != null) {

                }

                if (Megagroup != null) {

                }

                writer.Write(Title);
            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    Channel = reader.ReadTrue();
                }

                if ((flags & (1 << 1)) != 0) {
                    Broadcast = reader.ReadTrue();
                }

                if ((flags & (1 << 2)) != 0) {
                    Public = reader.ReadTrue();
                }

                if ((flags & (1 << 3)) != 0) {
                    Megagroup = reader.ReadTrue();
                }

                Title = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(ChatInviteType Channel:{0} Broadcast:{1} Public:{2} Megagroup:{3} Title:{4})", Channel, Broadcast, Public, Megagroup, Title);
            }
        }

        public class InputStickerSetEmptyType : InputStickerSet
        {
            public override uint ConstructorCode => 0xffb62b95;

            public InputStickerSetEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputStickerSetEmptyType)";
            }
        }

        public class InputStickerSetIDType : InputStickerSet
        {
            public override uint ConstructorCode => 0x9de7a269;

            public long Id;
            public long AccessHash;

            public InputStickerSetIDType() { }

            public InputStickerSetIDType(long Id, long AccessHash)
            {
                this.Id = Id;
                this.AccessHash = AccessHash;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(AccessHash);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadInt64();
                AccessHash = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(InputStickerSetIDType Id:{0} AccessHash:{1})", Id, AccessHash);
            }
        }

        public class InputStickerSetShortNameType : InputStickerSet
        {
            public override uint ConstructorCode => 0x861cc8a0;

            public string ShortName;

            public InputStickerSetShortNameType() { }

            public InputStickerSetShortNameType(string ShortName)
            {
                this.ShortName = ShortName;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ShortName);
            }

            public override void Read(TBinaryReader reader)
            {
                ShortName = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(InputStickerSetShortNameType ShortName:{0})", ShortName);
            }
        }

        public class StickerSetType : StickerSet
        {
            public override uint ConstructorCode => 0xcd303b41;

            public True Installed;
            public True Disabled;
            public True Official;
            public long Id;
            public long AccessHash;
            public string Title;
            public string ShortName;
            public int Count;
            public int Hash;

            public StickerSetType() { }

            /// <summary>
            /// The following arguments can be null: Installed, Disabled, Official
            /// </summary>
            /// <param name="Installed">Can be null</param>
            /// <param name="Disabled">Can be null</param>
            /// <param name="Official">Can be null</param>
            /// <param name="Id">Can NOT be null</param>
            /// <param name="AccessHash">Can NOT be null</param>
            /// <param name="Title">Can NOT be null</param>
            /// <param name="ShortName">Can NOT be null</param>
            /// <param name="Count">Can NOT be null</param>
            /// <param name="Hash">Can NOT be null</param>
            public StickerSetType(True Installed, True Disabled, True Official, long Id, long AccessHash, string Title, string ShortName, int Count, int Hash)
            {
                this.Installed = Installed;
                this.Disabled = Disabled;
                this.Official = Official;
                this.Id = Id;
                this.AccessHash = AccessHash;
                this.Title = Title;
                this.ShortName = ShortName;
                this.Count = Count;
                this.Hash = Hash;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Installed != null ? 1 << 0 : 0) |
                    (Disabled != null ? 1 << 1 : 0) |
                    (Official != null ? 1 << 2 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Installed != null) {

                }

                if (Disabled != null) {

                }

                if (Official != null) {

                }

                writer.Write(Id);
                writer.Write(AccessHash);
                writer.Write(Title);
                writer.Write(ShortName);
                writer.Write(Count);
                writer.Write(Hash);
            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    Installed = reader.ReadTrue();
                }

                if ((flags & (1 << 1)) != 0) {
                    Disabled = reader.ReadTrue();
                }

                if ((flags & (1 << 2)) != 0) {
                    Official = reader.ReadTrue();
                }

                Id = reader.ReadInt64();
                AccessHash = reader.ReadInt64();
                Title = reader.ReadString();
                ShortName = reader.ReadString();
                Count = reader.ReadInt32();
                Hash = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(StickerSetType Installed:{0} Disabled:{1} Official:{2} Id:{3} AccessHash:{4} Title:{5} ShortName:{6} Count:{7} Hash:{8})", Installed, Disabled, Official, Id, AccessHash, Title, ShortName, Count, Hash);
            }
        }

        public class MessagesStickerSetType : MessagesStickerSet
        {
            public override uint ConstructorCode => 0xb60a24a6;

            public StickerSet Set;
            public List<StickerPack> Packs;
            public List<Document> Documents;

            public MessagesStickerSetType() { }

            public MessagesStickerSetType(StickerSet Set, List<StickerPack> Packs, List<Document> Documents)
            {
                this.Set = Set;
                this.Packs = Packs;
                this.Documents = Documents;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Set.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Packs.Count);
                foreach (StickerPack PacksElement in Packs)
                    PacksElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Documents.Count);
                foreach (Document DocumentsElement in Documents)
                    DocumentsElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Set = reader.Read<StickerSet>();
                reader.ReadInt32(); // vector code
                int PacksLength = reader.ReadInt32();
                Packs = new List<StickerPack>(PacksLength);
                for (int PacksIndex = 0; PacksIndex < PacksLength; PacksIndex++)
                    Packs.Add(reader.Read<StickerPack>());
                reader.ReadInt32(); // vector code
                int DocumentsLength = reader.ReadInt32();
                Documents = new List<Document>(DocumentsLength);
                for (int DocumentsIndex = 0; DocumentsIndex < DocumentsLength; DocumentsIndex++)
                    Documents.Add(reader.Read<Document>());
            }

            public override string ToString()
            {
                return string.Format("(MessagesStickerSetType Set:{0} Packs:{1} Documents:{2})", Set, Packs, Documents);
            }
        }

        public class BotCommandType : BotCommand
        {
            public override uint ConstructorCode => 0xc27ac8c7;

            public string Command;
            public string Description;

            public BotCommandType() { }

            public BotCommandType(string Command, string Description)
            {
                this.Command = Command;
                this.Description = Description;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Command);
                writer.Write(Description);
            }

            public override void Read(TBinaryReader reader)
            {
                Command = reader.ReadString();
                Description = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(BotCommandType Command:{0} Description:{1})", Command, Description);
            }
        }

        public class BotInfoEmptyType : BotInfo
        {
            public override uint ConstructorCode => 0xbb2e37ce;

            public BotInfoEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(BotInfoEmptyType)";
            }
        }

        public class BotInfoType : BotInfo
        {
            public override uint ConstructorCode => 0x09cf585d;

            public int UserId;
            public int Version;
            public string ShareText;
            public string Description;
            public List<BotCommand> Commands;

            public BotInfoType() { }

            public BotInfoType(int UserId, int Version, string ShareText, string Description, List<BotCommand> Commands)
            {
                this.UserId = UserId;
                this.Version = Version;
                this.ShareText = ShareText;
                this.Description = Description;
                this.Commands = Commands;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(Version);
                writer.Write(ShareText);
                writer.Write(Description);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Commands.Count);
                foreach (BotCommand CommandsElement in Commands)
                    CommandsElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                Version = reader.ReadInt32();
                ShareText = reader.ReadString();
                Description = reader.ReadString();
                reader.ReadInt32(); // vector code
                int CommandsLength = reader.ReadInt32();
                Commands = new List<BotCommand>(CommandsLength);
                for (int CommandsIndex = 0; CommandsIndex < CommandsLength; CommandsIndex++)
                    Commands.Add(reader.Read<BotCommand>());
            }

            public override string ToString()
            {
                return string.Format("(BotInfoType UserId:{0} Version:{1} ShareText:{2} Description:{3} Commands:{4})", UserId, Version, ShareText, Description, Commands);
            }
        }

        public class KeyboardButtonType : KeyboardButton
        {
            public override uint ConstructorCode => 0xa2fa4880;

            public string Text;

            public KeyboardButtonType() { }

            public KeyboardButtonType(string Text)
            {
                this.Text = Text;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Text);
            }

            public override void Read(TBinaryReader reader)
            {
                Text = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(KeyboardButtonType Text:{0})", Text);
            }
        }

        public class KeyboardButtonRowType : KeyboardButtonRow
        {
            public override uint ConstructorCode => 0x77608b83;

            public List<KeyboardButton> Buttons;

            public KeyboardButtonRowType() { }

            public KeyboardButtonRowType(List<KeyboardButton> Buttons)
            {
                this.Buttons = Buttons;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Buttons.Count);
                foreach (KeyboardButton ButtonsElement in Buttons)
                    ButtonsElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                reader.ReadInt32(); // vector code
                int ButtonsLength = reader.ReadInt32();
                Buttons = new List<KeyboardButton>(ButtonsLength);
                for (int ButtonsIndex = 0; ButtonsIndex < ButtonsLength; ButtonsIndex++)
                    Buttons.Add(reader.Read<KeyboardButton>());
            }

            public override string ToString()
            {
                return string.Format("(KeyboardButtonRowType Buttons:{0})", Buttons);
            }
        }

        public class ReplyKeyboardHideType : ReplyMarkup
        {
            public override uint ConstructorCode => 0xa03e5b85;

            public True Selective;

            public ReplyKeyboardHideType() { }

            /// <summary>
            /// The following arguments can be null: Selective
            /// </summary>
            /// <param name="Selective">Can be null</param>
            public ReplyKeyboardHideType(True Selective)
            {
                this.Selective = Selective;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Selective != null ? 1 << 2 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Selective != null) {

                }

            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 2)) != 0) {
                    Selective = reader.ReadTrue();
                }

            }

            public override string ToString()
            {
                return string.Format("(ReplyKeyboardHideType Selective:{0})", Selective);
            }
        }

        public class ReplyKeyboardForceReplyType : ReplyMarkup
        {
            public override uint ConstructorCode => 0xf4108aa0;

            public True SingleUse;
            public True Selective;

            public ReplyKeyboardForceReplyType() { }

            /// <summary>
            /// The following arguments can be null: SingleUse, Selective
            /// </summary>
            /// <param name="SingleUse">Can be null</param>
            /// <param name="Selective">Can be null</param>
            public ReplyKeyboardForceReplyType(True SingleUse, True Selective)
            {
                this.SingleUse = SingleUse;
                this.Selective = Selective;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (SingleUse != null ? 1 << 1 : 0) |
                    (Selective != null ? 1 << 2 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (SingleUse != null) {

                }

                if (Selective != null) {

                }

            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 1)) != 0) {
                    SingleUse = reader.ReadTrue();
                }

                if ((flags & (1 << 2)) != 0) {
                    Selective = reader.ReadTrue();
                }

            }

            public override string ToString()
            {
                return string.Format("(ReplyKeyboardForceReplyType SingleUse:{0} Selective:{1})", SingleUse, Selective);
            }
        }

        public class ReplyKeyboardMarkupType : ReplyMarkup
        {
            public override uint ConstructorCode => 0x3502758c;

            public True Resize;
            public True SingleUse;
            public True Selective;
            public List<KeyboardButtonRow> Rows;

            public ReplyKeyboardMarkupType() { }

            /// <summary>
            /// The following arguments can be null: Resize, SingleUse, Selective
            /// </summary>
            /// <param name="Resize">Can be null</param>
            /// <param name="SingleUse">Can be null</param>
            /// <param name="Selective">Can be null</param>
            /// <param name="Rows">Can NOT be null</param>
            public ReplyKeyboardMarkupType(True Resize, True SingleUse, True Selective, List<KeyboardButtonRow> Rows)
            {
                this.Resize = Resize;
                this.SingleUse = SingleUse;
                this.Selective = Selective;
                this.Rows = Rows;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Resize != null ? 1 << 0 : 0) |
                    (SingleUse != null ? 1 << 1 : 0) |
                    (Selective != null ? 1 << 2 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Resize != null) {

                }

                if (SingleUse != null) {

                }

                if (Selective != null) {

                }

                writer.Write(0x1cb5c415); // vector code
                writer.Write(Rows.Count);
                foreach (KeyboardButtonRow RowsElement in Rows)
                    RowsElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    Resize = reader.ReadTrue();
                }

                if ((flags & (1 << 1)) != 0) {
                    SingleUse = reader.ReadTrue();
                }

                if ((flags & (1 << 2)) != 0) {
                    Selective = reader.ReadTrue();
                }

                reader.ReadInt32(); // vector code
                int RowsLength = reader.ReadInt32();
                Rows = new List<KeyboardButtonRow>(RowsLength);
                for (int RowsIndex = 0; RowsIndex < RowsLength; RowsIndex++)
                    Rows.Add(reader.Read<KeyboardButtonRow>());
            }

            public override string ToString()
            {
                return string.Format("(ReplyKeyboardMarkupType Resize:{0} SingleUse:{1} Selective:{2} Rows:{3})", Resize, SingleUse, Selective, Rows);
            }
        }

        public class HelpAppChangelogEmptyType : HelpAppChangelog
        {
            public override uint ConstructorCode => 0xaf7e0394;

            public HelpAppChangelogEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(HelpAppChangelogEmptyType)";
            }
        }

        public class HelpAppChangelogType : HelpAppChangelog
        {
            public override uint ConstructorCode => 0x4668e6bd;

            public string Text;

            public HelpAppChangelogType() { }

            public HelpAppChangelogType(string Text)
            {
                this.Text = Text;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Text);
            }

            public override void Read(TBinaryReader reader)
            {
                Text = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(HelpAppChangelogType Text:{0})", Text);
            }
        }

        public class MessageEntityUnknownType : MessageEntity
        {
            public override uint ConstructorCode => 0xbb92ba95;

            public int Offset;
            public int Length;

            public MessageEntityUnknownType() { }

            public MessageEntityUnknownType(int Offset, int Length)
            {
                this.Offset = Offset;
                this.Length = Length;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Offset);
                writer.Write(Length);
            }

            public override void Read(TBinaryReader reader)
            {
                Offset = reader.ReadInt32();
                Length = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessageEntityUnknownType Offset:{0} Length:{1})", Offset, Length);
            }
        }

        public class MessageEntityMentionType : MessageEntity
        {
            public override uint ConstructorCode => 0xfa04579d;

            public int Offset;
            public int Length;

            public MessageEntityMentionType() { }

            public MessageEntityMentionType(int Offset, int Length)
            {
                this.Offset = Offset;
                this.Length = Length;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Offset);
                writer.Write(Length);
            }

            public override void Read(TBinaryReader reader)
            {
                Offset = reader.ReadInt32();
                Length = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessageEntityMentionType Offset:{0} Length:{1})", Offset, Length);
            }
        }

        public class MessageEntityHashtagType : MessageEntity
        {
            public override uint ConstructorCode => 0x6f635b0d;

            public int Offset;
            public int Length;

            public MessageEntityHashtagType() { }

            public MessageEntityHashtagType(int Offset, int Length)
            {
                this.Offset = Offset;
                this.Length = Length;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Offset);
                writer.Write(Length);
            }

            public override void Read(TBinaryReader reader)
            {
                Offset = reader.ReadInt32();
                Length = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessageEntityHashtagType Offset:{0} Length:{1})", Offset, Length);
            }
        }

        public class MessageEntityBotCommandType : MessageEntity
        {
            public override uint ConstructorCode => 0x6cef8ac7;

            public int Offset;
            public int Length;

            public MessageEntityBotCommandType() { }

            public MessageEntityBotCommandType(int Offset, int Length)
            {
                this.Offset = Offset;
                this.Length = Length;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Offset);
                writer.Write(Length);
            }

            public override void Read(TBinaryReader reader)
            {
                Offset = reader.ReadInt32();
                Length = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessageEntityBotCommandType Offset:{0} Length:{1})", Offset, Length);
            }
        }

        public class MessageEntityUrlType : MessageEntity
        {
            public override uint ConstructorCode => 0x6ed02538;

            public int Offset;
            public int Length;

            public MessageEntityUrlType() { }

            public MessageEntityUrlType(int Offset, int Length)
            {
                this.Offset = Offset;
                this.Length = Length;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Offset);
                writer.Write(Length);
            }

            public override void Read(TBinaryReader reader)
            {
                Offset = reader.ReadInt32();
                Length = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessageEntityUrlType Offset:{0} Length:{1})", Offset, Length);
            }
        }

        public class MessageEntityEmailType : MessageEntity
        {
            public override uint ConstructorCode => 0x64e475c2;

            public int Offset;
            public int Length;

            public MessageEntityEmailType() { }

            public MessageEntityEmailType(int Offset, int Length)
            {
                this.Offset = Offset;
                this.Length = Length;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Offset);
                writer.Write(Length);
            }

            public override void Read(TBinaryReader reader)
            {
                Offset = reader.ReadInt32();
                Length = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessageEntityEmailType Offset:{0} Length:{1})", Offset, Length);
            }
        }

        public class MessageEntityBoldType : MessageEntity
        {
            public override uint ConstructorCode => 0xbd610bc9;

            public int Offset;
            public int Length;

            public MessageEntityBoldType() { }

            public MessageEntityBoldType(int Offset, int Length)
            {
                this.Offset = Offset;
                this.Length = Length;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Offset);
                writer.Write(Length);
            }

            public override void Read(TBinaryReader reader)
            {
                Offset = reader.ReadInt32();
                Length = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessageEntityBoldType Offset:{0} Length:{1})", Offset, Length);
            }
        }

        public class MessageEntityItalicType : MessageEntity
        {
            public override uint ConstructorCode => 0x826f8b60;

            public int Offset;
            public int Length;

            public MessageEntityItalicType() { }

            public MessageEntityItalicType(int Offset, int Length)
            {
                this.Offset = Offset;
                this.Length = Length;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Offset);
                writer.Write(Length);
            }

            public override void Read(TBinaryReader reader)
            {
                Offset = reader.ReadInt32();
                Length = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessageEntityItalicType Offset:{0} Length:{1})", Offset, Length);
            }
        }

        public class MessageEntityCodeType : MessageEntity
        {
            public override uint ConstructorCode => 0x28a20571;

            public int Offset;
            public int Length;

            public MessageEntityCodeType() { }

            public MessageEntityCodeType(int Offset, int Length)
            {
                this.Offset = Offset;
                this.Length = Length;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Offset);
                writer.Write(Length);
            }

            public override void Read(TBinaryReader reader)
            {
                Offset = reader.ReadInt32();
                Length = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessageEntityCodeType Offset:{0} Length:{1})", Offset, Length);
            }
        }

        public class MessageEntityPreType : MessageEntity
        {
            public override uint ConstructorCode => 0x73924be0;

            public int Offset;
            public int Length;
            public string Language;

            public MessageEntityPreType() { }

            public MessageEntityPreType(int Offset, int Length, string Language)
            {
                this.Offset = Offset;
                this.Length = Length;
                this.Language = Language;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Offset);
                writer.Write(Length);
                writer.Write(Language);
            }

            public override void Read(TBinaryReader reader)
            {
                Offset = reader.ReadInt32();
                Length = reader.ReadInt32();
                Language = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(MessageEntityPreType Offset:{0} Length:{1} Language:{2})", Offset, Length, Language);
            }
        }

        public class MessageEntityTextUrlType : MessageEntity
        {
            public override uint ConstructorCode => 0x76a6d327;

            public int Offset;
            public int Length;
            public string Url;

            public MessageEntityTextUrlType() { }

            public MessageEntityTextUrlType(int Offset, int Length, string Url)
            {
                this.Offset = Offset;
                this.Length = Length;
                this.Url = Url;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Offset);
                writer.Write(Length);
                writer.Write(Url);
            }

            public override void Read(TBinaryReader reader)
            {
                Offset = reader.ReadInt32();
                Length = reader.ReadInt32();
                Url = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(MessageEntityTextUrlType Offset:{0} Length:{1} Url:{2})", Offset, Length, Url);
            }
        }

        public class InputChannelEmptyType : InputChannel
        {
            public override uint ConstructorCode => 0xee8c1e86;

            public InputChannelEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(InputChannelEmptyType)";
            }
        }

        public class InputChannelType : InputChannel
        {
            public override uint ConstructorCode => 0xafeb712e;

            public int ChannelId;
            public long AccessHash;

            public InputChannelType() { }

            public InputChannelType(int ChannelId, long AccessHash)
            {
                this.ChannelId = ChannelId;
                this.AccessHash = AccessHash;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(ChannelId);
                writer.Write(AccessHash);
            }

            public override void Read(TBinaryReader reader)
            {
                ChannelId = reader.ReadInt32();
                AccessHash = reader.ReadInt64();
            }

            public override string ToString()
            {
                return string.Format("(InputChannelType ChannelId:{0} AccessHash:{1})", ChannelId, AccessHash);
            }
        }

        public class ContactsResolvedPeerType : ContactsResolvedPeer
        {
            public override uint ConstructorCode => 0x7f077ad9;

            public Peer Peer;
            public List<Chat> Chats;
            public List<User> Users;

            public ContactsResolvedPeerType() { }

            public ContactsResolvedPeerType(Peer Peer, List<Chat> Chats, List<User> Users)
            {
                this.Peer = Peer;
                this.Chats = Chats;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Peer.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Chats.Count);
                foreach (Chat ChatsElement in Chats)
                    ChatsElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Peer = reader.Read<Peer>();
                reader.ReadInt32(); // vector code
                int ChatsLength = reader.ReadInt32();
                Chats = new List<Chat>(ChatsLength);
                for (int ChatsIndex = 0; ChatsIndex < ChatsLength; ChatsIndex++)
                    Chats.Add(reader.Read<Chat>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(ContactsResolvedPeerType Peer:{0} Chats:{1} Users:{2})", Peer, Chats, Users);
            }
        }

        public class MessageRangeType : MessageRange
        {
            public override uint ConstructorCode => 0x0ae30253;

            public int MinId;
            public int MaxId;

            public MessageRangeType() { }

            public MessageRangeType(int MinId, int MaxId)
            {
                this.MinId = MinId;
                this.MaxId = MaxId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(MinId);
                writer.Write(MaxId);
            }

            public override void Read(TBinaryReader reader)
            {
                MinId = reader.ReadInt32();
                MaxId = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessageRangeType MinId:{0} MaxId:{1})", MinId, MaxId);
            }
        }

        public class MessageGroupType : MessageGroup
        {
            public override uint ConstructorCode => 0xe8346f53;

            public int MinId;
            public int MaxId;
            public int Count;
            public int Date;

            public MessageGroupType() { }

            public MessageGroupType(int MinId, int MaxId, int Count, int Date)
            {
                this.MinId = MinId;
                this.MaxId = MaxId;
                this.Count = Count;
                this.Date = Date;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(MinId);
                writer.Write(MaxId);
                writer.Write(Count);
                writer.Write(Date);
            }

            public override void Read(TBinaryReader reader)
            {
                MinId = reader.ReadInt32();
                MaxId = reader.ReadInt32();
                Count = reader.ReadInt32();
                Date = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(MessageGroupType MinId:{0} MaxId:{1} Count:{2} Date:{3})", MinId, MaxId, Count, Date);
            }
        }

        public class UpdatesChannelDifferenceEmptyType : UpdatesChannelDifference
        {
            public override uint ConstructorCode => 0x3e11affb;

            public True Final;
            public int Pts;
            public int? Timeout;

            public UpdatesChannelDifferenceEmptyType() { }

            /// <summary>
            /// The following arguments can be null: Final, Timeout
            /// </summary>
            /// <param name="Final">Can be null</param>
            /// <param name="Pts">Can NOT be null</param>
            /// <param name="Timeout">Can be null</param>
            public UpdatesChannelDifferenceEmptyType(True Final, int Pts, int? Timeout)
            {
                this.Final = Final;
                this.Pts = Pts;
                this.Timeout = Timeout;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Final != null ? 1 << 0 : 0) |
                    (Timeout != null ? 1 << 1 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Final != null) {

                }

                writer.Write(Pts);
                if (Timeout != null) {
                    writer.Write(Timeout.Value);
                }

            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    Final = reader.ReadTrue();
                }

                Pts = reader.ReadInt32();
                if ((flags & (1 << 1)) != 0) {
                    Timeout = reader.ReadInt32();
                }

            }

            public override string ToString()
            {
                return string.Format("(UpdatesChannelDifferenceEmptyType Final:{0} Pts:{1} Timeout:{2})", Final, Pts, Timeout);
            }
        }

        public class UpdatesChannelDifferenceTooLongType : UpdatesChannelDifference
        {
            public override uint ConstructorCode => 0x5e167646;

            public True Final;
            public int Pts;
            public int? Timeout;
            public int TopMessage;
            public int TopImportantMessage;
            public int ReadInboxMaxId;
            public int UnreadCount;
            public int UnreadImportantCount;
            public List<Message> Messages;
            public List<Chat> Chats;
            public List<User> Users;

            public UpdatesChannelDifferenceTooLongType() { }

            /// <summary>
            /// The following arguments can be null: Final, Timeout
            /// </summary>
            /// <param name="Final">Can be null</param>
            /// <param name="Pts">Can NOT be null</param>
            /// <param name="Timeout">Can be null</param>
            /// <param name="TopMessage">Can NOT be null</param>
            /// <param name="TopImportantMessage">Can NOT be null</param>
            /// <param name="ReadInboxMaxId">Can NOT be null</param>
            /// <param name="UnreadCount">Can NOT be null</param>
            /// <param name="UnreadImportantCount">Can NOT be null</param>
            /// <param name="Messages">Can NOT be null</param>
            /// <param name="Chats">Can NOT be null</param>
            /// <param name="Users">Can NOT be null</param>
            public UpdatesChannelDifferenceTooLongType(True Final, int Pts, int? Timeout, int TopMessage, int TopImportantMessage, int ReadInboxMaxId, int UnreadCount, int UnreadImportantCount, List<Message> Messages, List<Chat> Chats, List<User> Users)
            {
                this.Final = Final;
                this.Pts = Pts;
                this.Timeout = Timeout;
                this.TopMessage = TopMessage;
                this.TopImportantMessage = TopImportantMessage;
                this.ReadInboxMaxId = ReadInboxMaxId;
                this.UnreadCount = UnreadCount;
                this.UnreadImportantCount = UnreadImportantCount;
                this.Messages = Messages;
                this.Chats = Chats;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Final != null ? 1 << 0 : 0) |
                    (Timeout != null ? 1 << 1 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Final != null) {

                }

                writer.Write(Pts);
                if (Timeout != null) {
                    writer.Write(Timeout.Value);
                }

                writer.Write(TopMessage);
                writer.Write(TopImportantMessage);
                writer.Write(ReadInboxMaxId);
                writer.Write(UnreadCount);
                writer.Write(UnreadImportantCount);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Messages.Count);
                foreach (Message MessagesElement in Messages)
                    MessagesElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Chats.Count);
                foreach (Chat ChatsElement in Chats)
                    ChatsElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    Final = reader.ReadTrue();
                }

                Pts = reader.ReadInt32();
                if ((flags & (1 << 1)) != 0) {
                    Timeout = reader.ReadInt32();
                }

                TopMessage = reader.ReadInt32();
                TopImportantMessage = reader.ReadInt32();
                ReadInboxMaxId = reader.ReadInt32();
                UnreadCount = reader.ReadInt32();
                UnreadImportantCount = reader.ReadInt32();
                reader.ReadInt32(); // vector code
                int MessagesLength = reader.ReadInt32();
                Messages = new List<Message>(MessagesLength);
                for (int MessagesIndex = 0; MessagesIndex < MessagesLength; MessagesIndex++)
                    Messages.Add(reader.Read<Message>());
                reader.ReadInt32(); // vector code
                int ChatsLength = reader.ReadInt32();
                Chats = new List<Chat>(ChatsLength);
                for (int ChatsIndex = 0; ChatsIndex < ChatsLength; ChatsIndex++)
                    Chats.Add(reader.Read<Chat>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(UpdatesChannelDifferenceTooLongType Final:{0} Pts:{1} Timeout:{2} TopMessage:{3} TopImportantMessage:{4} ReadInboxMaxId:{5} UnreadCount:{6} UnreadImportantCount:{7} Messages:{8} Chats:{9} Users:{10})", Final, Pts, Timeout, TopMessage, TopImportantMessage, ReadInboxMaxId, UnreadCount, UnreadImportantCount, Messages, Chats, Users);
            }
        }

        public class UpdatesChannelDifferenceType : UpdatesChannelDifference
        {
            public override uint ConstructorCode => 0x2064674e;

            public True Final;
            public int Pts;
            public int? Timeout;
            public List<Message> NewMessages;
            public List<Update> OtherUpdates;
            public List<Chat> Chats;
            public List<User> Users;

            public UpdatesChannelDifferenceType() { }

            /// <summary>
            /// The following arguments can be null: Final, Timeout
            /// </summary>
            /// <param name="Final">Can be null</param>
            /// <param name="Pts">Can NOT be null</param>
            /// <param name="Timeout">Can be null</param>
            /// <param name="NewMessages">Can NOT be null</param>
            /// <param name="OtherUpdates">Can NOT be null</param>
            /// <param name="Chats">Can NOT be null</param>
            /// <param name="Users">Can NOT be null</param>
            public UpdatesChannelDifferenceType(True Final, int Pts, int? Timeout, List<Message> NewMessages, List<Update> OtherUpdates, List<Chat> Chats, List<User> Users)
            {
                this.Final = Final;
                this.Pts = Pts;
                this.Timeout = Timeout;
                this.NewMessages = NewMessages;
                this.OtherUpdates = OtherUpdates;
                this.Chats = Chats;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Final != null ? 1 << 0 : 0) |
                    (Timeout != null ? 1 << 1 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Final != null) {

                }

                writer.Write(Pts);
                if (Timeout != null) {
                    writer.Write(Timeout.Value);
                }

                writer.Write(0x1cb5c415); // vector code
                writer.Write(NewMessages.Count);
                foreach (Message NewMessagesElement in NewMessages)
                    NewMessagesElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(OtherUpdates.Count);
                foreach (Update OtherUpdatesElement in OtherUpdates)
                    OtherUpdatesElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Chats.Count);
                foreach (Chat ChatsElement in Chats)
                    ChatsElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    Final = reader.ReadTrue();
                }

                Pts = reader.ReadInt32();
                if ((flags & (1 << 1)) != 0) {
                    Timeout = reader.ReadInt32();
                }

                reader.ReadInt32(); // vector code
                int NewMessagesLength = reader.ReadInt32();
                NewMessages = new List<Message>(NewMessagesLength);
                for (int NewMessagesIndex = 0; NewMessagesIndex < NewMessagesLength; NewMessagesIndex++)
                    NewMessages.Add(reader.Read<Message>());
                reader.ReadInt32(); // vector code
                int OtherUpdatesLength = reader.ReadInt32();
                OtherUpdates = new List<Update>(OtherUpdatesLength);
                for (int OtherUpdatesIndex = 0; OtherUpdatesIndex < OtherUpdatesLength; OtherUpdatesIndex++)
                    OtherUpdates.Add(reader.Read<Update>());
                reader.ReadInt32(); // vector code
                int ChatsLength = reader.ReadInt32();
                Chats = new List<Chat>(ChatsLength);
                for (int ChatsIndex = 0; ChatsIndex < ChatsLength; ChatsIndex++)
                    Chats.Add(reader.Read<Chat>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(UpdatesChannelDifferenceType Final:{0} Pts:{1} Timeout:{2} NewMessages:{3} OtherUpdates:{4} Chats:{5} Users:{6})", Final, Pts, Timeout, NewMessages, OtherUpdates, Chats, Users);
            }
        }

        public class ChannelMessagesFilterEmptyType : ChannelMessagesFilter
        {
            public override uint ConstructorCode => 0x94d42ee7;

            public ChannelMessagesFilterEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(ChannelMessagesFilterEmptyType)";
            }
        }

        public class ChannelMessagesFilterType : ChannelMessagesFilter
        {
            public override uint ConstructorCode => 0xcd77d957;

            public True ImportantOnly;
            public True ExcludeNewMessages;
            public List<MessageRange> Ranges;

            public ChannelMessagesFilterType() { }

            /// <summary>
            /// The following arguments can be null: ImportantOnly, ExcludeNewMessages
            /// </summary>
            /// <param name="ImportantOnly">Can be null</param>
            /// <param name="ExcludeNewMessages">Can be null</param>
            /// <param name="Ranges">Can NOT be null</param>
            public ChannelMessagesFilterType(True ImportantOnly, True ExcludeNewMessages, List<MessageRange> Ranges)
            {
                this.ImportantOnly = ImportantOnly;
                this.ExcludeNewMessages = ExcludeNewMessages;
                this.Ranges = Ranges;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (ImportantOnly != null ? 1 << 0 : 0) |
                    (ExcludeNewMessages != null ? 1 << 1 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (ImportantOnly != null) {

                }

                if (ExcludeNewMessages != null) {

                }

                writer.Write(0x1cb5c415); // vector code
                writer.Write(Ranges.Count);
                foreach (MessageRange RangesElement in Ranges)
                    RangesElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    ImportantOnly = reader.ReadTrue();
                }

                if ((flags & (1 << 1)) != 0) {
                    ExcludeNewMessages = reader.ReadTrue();
                }

                reader.ReadInt32(); // vector code
                int RangesLength = reader.ReadInt32();
                Ranges = new List<MessageRange>(RangesLength);
                for (int RangesIndex = 0; RangesIndex < RangesLength; RangesIndex++)
                    Ranges.Add(reader.Read<MessageRange>());
            }

            public override string ToString()
            {
                return string.Format("(ChannelMessagesFilterType ImportantOnly:{0} ExcludeNewMessages:{1} Ranges:{2})", ImportantOnly, ExcludeNewMessages, Ranges);
            }
        }

        public class ChannelMessagesFilterCollapsedType : ChannelMessagesFilter
        {
            public override uint ConstructorCode => 0xfa01232e;

            public ChannelMessagesFilterCollapsedType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(ChannelMessagesFilterCollapsedType)";
            }
        }

        public class ChannelParticipantType : ChannelParticipant
        {
            public override uint ConstructorCode => 0x15ebac1d;

            public int UserId;
            public int Date;

            public ChannelParticipantType() { }

            public ChannelParticipantType(int UserId, int Date)
            {
                this.UserId = UserId;
                this.Date = Date;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(Date);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                Date = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(ChannelParticipantType UserId:{0} Date:{1})", UserId, Date);
            }
        }

        public class ChannelParticipantSelfType : ChannelParticipant
        {
            public override uint ConstructorCode => 0xa3289a6d;

            public int UserId;
            public int InviterId;
            public int Date;

            public ChannelParticipantSelfType() { }

            public ChannelParticipantSelfType(int UserId, int InviterId, int Date)
            {
                this.UserId = UserId;
                this.InviterId = InviterId;
                this.Date = Date;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(InviterId);
                writer.Write(Date);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                InviterId = reader.ReadInt32();
                Date = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(ChannelParticipantSelfType UserId:{0} InviterId:{1} Date:{2})", UserId, InviterId, Date);
            }
        }

        public class ChannelParticipantModeratorType : ChannelParticipant
        {
            public override uint ConstructorCode => 0x91057fef;

            public int UserId;
            public int InviterId;
            public int Date;

            public ChannelParticipantModeratorType() { }

            public ChannelParticipantModeratorType(int UserId, int InviterId, int Date)
            {
                this.UserId = UserId;
                this.InviterId = InviterId;
                this.Date = Date;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(InviterId);
                writer.Write(Date);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                InviterId = reader.ReadInt32();
                Date = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(ChannelParticipantModeratorType UserId:{0} InviterId:{1} Date:{2})", UserId, InviterId, Date);
            }
        }

        public class ChannelParticipantEditorType : ChannelParticipant
        {
            public override uint ConstructorCode => 0x98192d61;

            public int UserId;
            public int InviterId;
            public int Date;

            public ChannelParticipantEditorType() { }

            public ChannelParticipantEditorType(int UserId, int InviterId, int Date)
            {
                this.UserId = UserId;
                this.InviterId = InviterId;
                this.Date = Date;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(InviterId);
                writer.Write(Date);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                InviterId = reader.ReadInt32();
                Date = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(ChannelParticipantEditorType UserId:{0} InviterId:{1} Date:{2})", UserId, InviterId, Date);
            }
        }

        public class ChannelParticipantKickedType : ChannelParticipant
        {
            public override uint ConstructorCode => 0x8cc5e69a;

            public int UserId;
            public int KickedBy;
            public int Date;

            public ChannelParticipantKickedType() { }

            public ChannelParticipantKickedType(int UserId, int KickedBy, int Date)
            {
                this.UserId = UserId;
                this.KickedBy = KickedBy;
                this.Date = Date;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
                writer.Write(KickedBy);
                writer.Write(Date);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
                KickedBy = reader.ReadInt32();
                Date = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(ChannelParticipantKickedType UserId:{0} KickedBy:{1} Date:{2})", UserId, KickedBy, Date);
            }
        }

        public class ChannelParticipantCreatorType : ChannelParticipant
        {
            public override uint ConstructorCode => 0xe3e2e1f9;

            public int UserId;

            public ChannelParticipantCreatorType() { }

            public ChannelParticipantCreatorType(int UserId)
            {
                this.UserId = UserId;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(UserId);
            }

            public override void Read(TBinaryReader reader)
            {
                UserId = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(ChannelParticipantCreatorType UserId:{0})", UserId);
            }
        }

        public class ChannelParticipantsRecentType : ChannelParticipantsFilter
        {
            public override uint ConstructorCode => 0xde3f3c79;

            public ChannelParticipantsRecentType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(ChannelParticipantsRecentType)";
            }
        }

        public class ChannelParticipantsAdminsType : ChannelParticipantsFilter
        {
            public override uint ConstructorCode => 0xb4608969;

            public ChannelParticipantsAdminsType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(ChannelParticipantsAdminsType)";
            }
        }

        public class ChannelParticipantsKickedType : ChannelParticipantsFilter
        {
            public override uint ConstructorCode => 0x3c37bb7a;

            public ChannelParticipantsKickedType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(ChannelParticipantsKickedType)";
            }
        }

        public class ChannelParticipantsBotsType : ChannelParticipantsFilter
        {
            public override uint ConstructorCode => 0xb0d1865b;

            public ChannelParticipantsBotsType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(ChannelParticipantsBotsType)";
            }
        }

        public class ChannelRoleEmptyType : ChannelParticipantRole
        {
            public override uint ConstructorCode => 0xb285a0c6;

            public ChannelRoleEmptyType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(ChannelRoleEmptyType)";
            }
        }

        public class ChannelRoleModeratorType : ChannelParticipantRole
        {
            public override uint ConstructorCode => 0x9618d975;

            public ChannelRoleModeratorType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(ChannelRoleModeratorType)";
            }
        }

        public class ChannelRoleEditorType : ChannelParticipantRole
        {
            public override uint ConstructorCode => 0x820bfe8c;

            public ChannelRoleEditorType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(ChannelRoleEditorType)";
            }
        }

        public class ChannelsChannelParticipantsType : ChannelsChannelParticipants
        {
            public override uint ConstructorCode => 0xf56ee2a8;

            public int Count;
            public List<ChannelParticipant> Participants;
            public List<User> Users;

            public ChannelsChannelParticipantsType() { }

            public ChannelsChannelParticipantsType(int Count, List<ChannelParticipant> Participants, List<User> Users)
            {
                this.Count = Count;
                this.Participants = Participants;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Count);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Participants.Count);
                foreach (ChannelParticipant ParticipantsElement in Participants)
                    ParticipantsElement.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Count = reader.ReadInt32();
                reader.ReadInt32(); // vector code
                int ParticipantsLength = reader.ReadInt32();
                Participants = new List<ChannelParticipant>(ParticipantsLength);
                for (int ParticipantsIndex = 0; ParticipantsIndex < ParticipantsLength; ParticipantsIndex++)
                    Participants.Add(reader.Read<ChannelParticipant>());
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(ChannelsChannelParticipantsType Count:{0} Participants:{1} Users:{2})", Count, Participants, Users);
            }
        }

        public class ChannelsChannelParticipantType : ChannelsChannelParticipant
        {
            public override uint ConstructorCode => 0xd0d9b163;

            public ChannelParticipant Participant;
            public List<User> Users;

            public ChannelsChannelParticipantType() { }

            public ChannelsChannelParticipantType(ChannelParticipant Participant, List<User> Users)
            {
                this.Participant = Participant;
                this.Users = Users;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                Participant.Write(writer);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Users.Count);
                foreach (User UsersElement in Users)
                    UsersElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Participant = reader.Read<ChannelParticipant>();
                reader.ReadInt32(); // vector code
                int UsersLength = reader.ReadInt32();
                Users = new List<User>(UsersLength);
                for (int UsersIndex = 0; UsersIndex < UsersLength; UsersIndex++)
                    Users.Add(reader.Read<User>());
            }

            public override string ToString()
            {
                return string.Format("(ChannelsChannelParticipantType Participant:{0} Users:{1})", Participant, Users);
            }
        }

        public class HelpTermsOfServiceType : HelpTermsOfService
        {
            public override uint ConstructorCode => 0xf1ee3e90;

            public string Text;

            public HelpTermsOfServiceType() { }

            public HelpTermsOfServiceType(string Text)
            {
                this.Text = Text;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Text);
            }

            public override void Read(TBinaryReader reader)
            {
                Text = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(HelpTermsOfServiceType Text:{0})", Text);
            }
        }

        public class FoundGifType : FoundGif
        {
            public override uint ConstructorCode => 0x162ecc1f;

            public string Url;
            public string ThumbUrl;
            public string ContentUrl;
            public string ContentType;
            public int W;
            public int H;

            public FoundGifType() { }

            public FoundGifType(string Url, string ThumbUrl, string ContentUrl, string ContentType, int W, int H)
            {
                this.Url = Url;
                this.ThumbUrl = ThumbUrl;
                this.ContentUrl = ContentUrl;
                this.ContentType = ContentType;
                this.W = W;
                this.H = H;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Url);
                writer.Write(ThumbUrl);
                writer.Write(ContentUrl);
                writer.Write(ContentType);
                writer.Write(W);
                writer.Write(H);
            }

            public override void Read(TBinaryReader reader)
            {
                Url = reader.ReadString();
                ThumbUrl = reader.ReadString();
                ContentUrl = reader.ReadString();
                ContentType = reader.ReadString();
                W = reader.ReadInt32();
                H = reader.ReadInt32();
            }

            public override string ToString()
            {
                return string.Format("(FoundGifType Url:{0} ThumbUrl:{1} ContentUrl:{2} ContentType:{3} W:{4} H:{5})", Url, ThumbUrl, ContentUrl, ContentType, W, H);
            }
        }

        public class FoundGifCachedType : FoundGif
        {
            public override uint ConstructorCode => 0x9c750409;

            public string Url;
            public Photo Photo;
            public Document Document;

            public FoundGifCachedType() { }

            public FoundGifCachedType(string Url, Photo Photo, Document Document)
            {
                this.Url = Url;
                this.Photo = Photo;
                this.Document = Document;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Url);
                Photo.Write(writer);
                Document.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Url = reader.ReadString();
                Photo = reader.Read<Photo>();
                Document = reader.Read<Document>();
            }

            public override string ToString()
            {
                return string.Format("(FoundGifCachedType Url:{0} Photo:{1} Document:{2})", Url, Photo, Document);
            }
        }

        public class MessagesFoundGifsType : MessagesFoundGifs
        {
            public override uint ConstructorCode => 0x450a1c0a;

            public int NextOffset;
            public List<FoundGif> Results;

            public MessagesFoundGifsType() { }

            public MessagesFoundGifsType(int NextOffset, List<FoundGif> Results)
            {
                this.NextOffset = NextOffset;
                this.Results = Results;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(NextOffset);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Results.Count);
                foreach (FoundGif ResultsElement in Results)
                    ResultsElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                NextOffset = reader.ReadInt32();
                reader.ReadInt32(); // vector code
                int ResultsLength = reader.ReadInt32();
                Results = new List<FoundGif>(ResultsLength);
                for (int ResultsIndex = 0; ResultsIndex < ResultsLength; ResultsIndex++)
                    Results.Add(reader.Read<FoundGif>());
            }

            public override string ToString()
            {
                return string.Format("(MessagesFoundGifsType NextOffset:{0} Results:{1})", NextOffset, Results);
            }
        }

        public class MessagesSavedGifsNotModifiedType : MessagesSavedGifs
        {
            public override uint ConstructorCode => 0xe8025ca2;

            public MessagesSavedGifsNotModifiedType() { }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
            }

            public override void Read(TBinaryReader reader)
            {
            }

            public override string ToString()
            {
                return "(MessagesSavedGifsNotModifiedType)";
            }
        }

        public class MessagesSavedGifsType : MessagesSavedGifs
        {
            public override uint ConstructorCode => 0x2e0709a5;

            public int Hash;
            public List<Document> Gifs;

            public MessagesSavedGifsType() { }

            public MessagesSavedGifsType(int Hash, List<Document> Gifs)
            {
                this.Hash = Hash;
                this.Gifs = Gifs;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Hash);
                writer.Write(0x1cb5c415); // vector code
                writer.Write(Gifs.Count);
                foreach (Document GifsElement in Gifs)
                    GifsElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Hash = reader.ReadInt32();
                reader.ReadInt32(); // vector code
                int GifsLength = reader.ReadInt32();
                Gifs = new List<Document>(GifsLength);
                for (int GifsIndex = 0; GifsIndex < GifsLength; GifsIndex++)
                    Gifs.Add(reader.Read<Document>());
            }

            public override string ToString()
            {
                return string.Format("(MessagesSavedGifsType Hash:{0} Gifs:{1})", Hash, Gifs);
            }
        }

        public class InputBotInlineMessageMediaAutoType : InputBotInlineMessage
        {
            public override uint ConstructorCode => 0x2e43e587;

            public string Caption;

            public InputBotInlineMessageMediaAutoType() { }

            public InputBotInlineMessageMediaAutoType(string Caption)
            {
                this.Caption = Caption;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Caption);
            }

            public override void Read(TBinaryReader reader)
            {
                Caption = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(InputBotInlineMessageMediaAutoType Caption:{0})", Caption);
            }
        }

        public class InputBotInlineMessageTextType : InputBotInlineMessage
        {
            public override uint ConstructorCode => 0xadf0df71;

            public True NoWebpage;
            public string Message;
            public List<MessageEntity> Entities;

            public InputBotInlineMessageTextType() { }

            /// <summary>
            /// The following arguments can be null: NoWebpage, Entities
            /// </summary>
            /// <param name="NoWebpage">Can be null</param>
            /// <param name="Message">Can NOT be null</param>
            /// <param name="Entities">Can be null</param>
            public InputBotInlineMessageTextType(True NoWebpage, string Message, List<MessageEntity> Entities)
            {
                this.NoWebpage = NoWebpage;
                this.Message = Message;
                this.Entities = Entities;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (NoWebpage != null ? 1 << 0 : 0) |
                    (Entities != null ? 1 << 1 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (NoWebpage != null) {

                }

                writer.Write(Message);
                if (Entities != null) {
                    writer.Write(0x1cb5c415); // vector code
                    writer.Write(Entities.Count);
                    foreach (MessageEntity EntitiesElement in Entities)
                        EntitiesElement.Write(writer);
                }

            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    NoWebpage = reader.ReadTrue();
                }

                Message = reader.ReadString();
                if ((flags & (1 << 1)) != 0) {
                    reader.ReadInt32(); // vector code
                    int EntitiesLength = reader.ReadInt32();
                    Entities = new List<MessageEntity>(EntitiesLength);
                    for (int EntitiesIndex = 0; EntitiesIndex < EntitiesLength; EntitiesIndex++)
                        Entities.Add(reader.Read<MessageEntity>());
                    }

            }

            public override string ToString()
            {
                return string.Format("(InputBotInlineMessageTextType NoWebpage:{0} Message:{1} Entities:{2})", NoWebpage, Message, Entities);
            }
        }

        public class InputBotInlineResultType : InputBotInlineResult
        {
            public override uint ConstructorCode => 0x2cbbe15a;

            public string Id;
            public string Type;
            public string Title;
            public string Description;
            public string Url;
            public string ThumbUrl;
            public string ContentUrl;
            public string ContentType;
            public int? W;
            public int? H;
            public int? Duration;
            public InputBotInlineMessage SendMessage;

            public InputBotInlineResultType() { }

            /// <summary>
            /// The following arguments can be null: Title, Description, Url, ThumbUrl, ContentUrl, ContentType, W, H, Duration
            /// </summary>
            /// <param name="Id">Can NOT be null</param>
            /// <param name="Type">Can NOT be null</param>
            /// <param name="Title">Can be null</param>
            /// <param name="Description">Can be null</param>
            /// <param name="Url">Can be null</param>
            /// <param name="ThumbUrl">Can be null</param>
            /// <param name="ContentUrl">Can be null</param>
            /// <param name="ContentType">Can be null</param>
            /// <param name="W">Can be null</param>
            /// <param name="H">Can be null</param>
            /// <param name="Duration">Can be null</param>
            /// <param name="SendMessage">Can NOT be null</param>
            public InputBotInlineResultType(string Id, string Type, string Title, string Description, string Url, string ThumbUrl, string ContentUrl, string ContentType, int? W, int? H, int? Duration, InputBotInlineMessage SendMessage)
            {
                this.Id = Id;
                this.Type = Type;
                this.Title = Title;
                this.Description = Description;
                this.Url = Url;
                this.ThumbUrl = ThumbUrl;
                this.ContentUrl = ContentUrl;
                this.ContentType = ContentType;
                this.W = W;
                this.H = H;
                this.Duration = Duration;
                this.SendMessage = SendMessage;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Title != null ? 1 << 1 : 0) |
                    (Description != null ? 1 << 2 : 0) |
                    (Url != null ? 1 << 3 : 0) |
                    (ThumbUrl != null ? 1 << 4 : 0) |
                    (ContentUrl != null ? 1 << 5 : 0) |
                    (ContentType != null ? 1 << 5 : 0) |
                    (W != null ? 1 << 6 : 0) |
                    (H != null ? 1 << 6 : 0) |
                    (Duration != null ? 1 << 7 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                writer.Write(Id);
                writer.Write(Type);
                if (Title != null) {
                    writer.Write(Title);
                }

                if (Description != null) {
                    writer.Write(Description);
                }

                if (Url != null) {
                    writer.Write(Url);
                }

                if (ThumbUrl != null) {
                    writer.Write(ThumbUrl);
                }

                if (ContentUrl != null) {
                    writer.Write(ContentUrl);
                }

                if (ContentType != null) {
                    writer.Write(ContentType);
                }

                if (W != null) {
                    writer.Write(W.Value);
                }

                if (H != null) {
                    writer.Write(H.Value);
                }

                if (Duration != null) {
                    writer.Write(Duration.Value);
                }

                SendMessage.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                Id = reader.ReadString();
                Type = reader.ReadString();
                if ((flags & (1 << 1)) != 0) {
                    Title = reader.ReadString();
                }

                if ((flags & (1 << 2)) != 0) {
                    Description = reader.ReadString();
                }

                if ((flags & (1 << 3)) != 0) {
                    Url = reader.ReadString();
                }

                if ((flags & (1 << 4)) != 0) {
                    ThumbUrl = reader.ReadString();
                }

                if ((flags & (1 << 5)) != 0) {
                    ContentUrl = reader.ReadString();
                }

                if ((flags & (1 << 5)) != 0) {
                    ContentType = reader.ReadString();
                }

                if ((flags & (1 << 6)) != 0) {
                    W = reader.ReadInt32();
                }

                if ((flags & (1 << 6)) != 0) {
                    H = reader.ReadInt32();
                }

                if ((flags & (1 << 7)) != 0) {
                    Duration = reader.ReadInt32();
                }

                SendMessage = reader.Read<InputBotInlineMessage>();
            }

            public override string ToString()
            {
                return string.Format("(InputBotInlineResultType Id:{0} Type:{1} Title:{2} Description:{3} Url:{4} ThumbUrl:{5} ContentUrl:{6} ContentType:{7} W:{8} H:{9} Duration:{10} SendMessage:{11})", Id, Type, Title, Description, Url, ThumbUrl, ContentUrl, ContentType, W, H, Duration, SendMessage);
            }
        }

        public class BotInlineMessageMediaAutoType : BotInlineMessage
        {
            public override uint ConstructorCode => 0xfc56e87d;

            public string Caption;

            public BotInlineMessageMediaAutoType() { }

            public BotInlineMessageMediaAutoType(string Caption)
            {
                this.Caption = Caption;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Caption);
            }

            public override void Read(TBinaryReader reader)
            {
                Caption = reader.ReadString();
            }

            public override string ToString()
            {
                return string.Format("(BotInlineMessageMediaAutoType Caption:{0})", Caption);
            }
        }

        public class BotInlineMessageTextType : BotInlineMessage
        {
            public override uint ConstructorCode => 0xa56197a9;

            public True NoWebpage;
            public string Message;
            public List<MessageEntity> Entities;

            public BotInlineMessageTextType() { }

            /// <summary>
            /// The following arguments can be null: NoWebpage, Entities
            /// </summary>
            /// <param name="NoWebpage">Can be null</param>
            /// <param name="Message">Can NOT be null</param>
            /// <param name="Entities">Can be null</param>
            public BotInlineMessageTextType(True NoWebpage, string Message, List<MessageEntity> Entities)
            {
                this.NoWebpage = NoWebpage;
                this.Message = Message;
                this.Entities = Entities;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (NoWebpage != null ? 1 << 0 : 0) |
                    (Entities != null ? 1 << 1 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (NoWebpage != null) {

                }

                writer.Write(Message);
                if (Entities != null) {
                    writer.Write(0x1cb5c415); // vector code
                    writer.Write(Entities.Count);
                    foreach (MessageEntity EntitiesElement in Entities)
                        EntitiesElement.Write(writer);
                }

            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    NoWebpage = reader.ReadTrue();
                }

                Message = reader.ReadString();
                if ((flags & (1 << 1)) != 0) {
                    reader.ReadInt32(); // vector code
                    int EntitiesLength = reader.ReadInt32();
                    Entities = new List<MessageEntity>(EntitiesLength);
                    for (int EntitiesIndex = 0; EntitiesIndex < EntitiesLength; EntitiesIndex++)
                        Entities.Add(reader.Read<MessageEntity>());
                    }

            }

            public override string ToString()
            {
                return string.Format("(BotInlineMessageTextType NoWebpage:{0} Message:{1} Entities:{2})", NoWebpage, Message, Entities);
            }
        }

        public class BotInlineMediaResultDocumentType : BotInlineResult
        {
            public override uint ConstructorCode => 0xf897d33e;

            public string Id;
            public string Type;
            public Document Document;
            public BotInlineMessage SendMessage;

            public BotInlineMediaResultDocumentType() { }

            public BotInlineMediaResultDocumentType(string Id, string Type, Document Document, BotInlineMessage SendMessage)
            {
                this.Id = Id;
                this.Type = Type;
                this.Document = Document;
                this.SendMessage = SendMessage;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(Type);
                Document.Write(writer);
                SendMessage.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadString();
                Type = reader.ReadString();
                Document = reader.Read<Document>();
                SendMessage = reader.Read<BotInlineMessage>();
            }

            public override string ToString()
            {
                return string.Format("(BotInlineMediaResultDocumentType Id:{0} Type:{1} Document:{2} SendMessage:{3})", Id, Type, Document, SendMessage);
            }
        }

        public class BotInlineMediaResultPhotoType : BotInlineResult
        {
            public override uint ConstructorCode => 0xc5528587;

            public string Id;
            public string Type;
            public Photo Photo;
            public BotInlineMessage SendMessage;

            public BotInlineMediaResultPhotoType() { }

            public BotInlineMediaResultPhotoType(string Id, string Type, Photo Photo, BotInlineMessage SendMessage)
            {
                this.Id = Id;
                this.Type = Type;
                this.Photo = Photo;
                this.SendMessage = SendMessage;
            }

            public override void Write(TBinaryWriter writer)
            {
                writer.Write(ConstructorCode);
                writer.Write(Id);
                writer.Write(Type);
                Photo.Write(writer);
                SendMessage.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                Id = reader.ReadString();
                Type = reader.ReadString();
                Photo = reader.Read<Photo>();
                SendMessage = reader.Read<BotInlineMessage>();
            }

            public override string ToString()
            {
                return string.Format("(BotInlineMediaResultPhotoType Id:{0} Type:{1} Photo:{2} SendMessage:{3})", Id, Type, Photo, SendMessage);
            }
        }

        public class BotInlineResultType : BotInlineResult
        {
            public override uint ConstructorCode => 0x9bebaeb9;

            public string Id;
            public string Type;
            public string Title;
            public string Description;
            public string Url;
            public string ThumbUrl;
            public string ContentUrl;
            public string ContentType;
            public int? W;
            public int? H;
            public int? Duration;
            public BotInlineMessage SendMessage;

            public BotInlineResultType() { }

            /// <summary>
            /// The following arguments can be null: Title, Description, Url, ThumbUrl, ContentUrl, ContentType, W, H, Duration
            /// </summary>
            /// <param name="Id">Can NOT be null</param>
            /// <param name="Type">Can NOT be null</param>
            /// <param name="Title">Can be null</param>
            /// <param name="Description">Can be null</param>
            /// <param name="Url">Can be null</param>
            /// <param name="ThumbUrl">Can be null</param>
            /// <param name="ContentUrl">Can be null</param>
            /// <param name="ContentType">Can be null</param>
            /// <param name="W">Can be null</param>
            /// <param name="H">Can be null</param>
            /// <param name="Duration">Can be null</param>
            /// <param name="SendMessage">Can NOT be null</param>
            public BotInlineResultType(string Id, string Type, string Title, string Description, string Url, string ThumbUrl, string ContentUrl, string ContentType, int? W, int? H, int? Duration, BotInlineMessage SendMessage)
            {
                this.Id = Id;
                this.Type = Type;
                this.Title = Title;
                this.Description = Description;
                this.Url = Url;
                this.ThumbUrl = ThumbUrl;
                this.ContentUrl = ContentUrl;
                this.ContentType = ContentType;
                this.W = W;
                this.H = H;
                this.Duration = Duration;
                this.SendMessage = SendMessage;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Title != null ? 1 << 1 : 0) |
                    (Description != null ? 1 << 2 : 0) |
                    (Url != null ? 1 << 3 : 0) |
                    (ThumbUrl != null ? 1 << 4 : 0) |
                    (ContentUrl != null ? 1 << 5 : 0) |
                    (ContentType != null ? 1 << 5 : 0) |
                    (W != null ? 1 << 6 : 0) |
                    (H != null ? 1 << 6 : 0) |
                    (Duration != null ? 1 << 7 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                writer.Write(Id);
                writer.Write(Type);
                if (Title != null) {
                    writer.Write(Title);
                }

                if (Description != null) {
                    writer.Write(Description);
                }

                if (Url != null) {
                    writer.Write(Url);
                }

                if (ThumbUrl != null) {
                    writer.Write(ThumbUrl);
                }

                if (ContentUrl != null) {
                    writer.Write(ContentUrl);
                }

                if (ContentType != null) {
                    writer.Write(ContentType);
                }

                if (W != null) {
                    writer.Write(W.Value);
                }

                if (H != null) {
                    writer.Write(H.Value);
                }

                if (Duration != null) {
                    writer.Write(Duration.Value);
                }

                SendMessage.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                Id = reader.ReadString();
                Type = reader.ReadString();
                if ((flags & (1 << 1)) != 0) {
                    Title = reader.ReadString();
                }

                if ((flags & (1 << 2)) != 0) {
                    Description = reader.ReadString();
                }

                if ((flags & (1 << 3)) != 0) {
                    Url = reader.ReadString();
                }

                if ((flags & (1 << 4)) != 0) {
                    ThumbUrl = reader.ReadString();
                }

                if ((flags & (1 << 5)) != 0) {
                    ContentUrl = reader.ReadString();
                }

                if ((flags & (1 << 5)) != 0) {
                    ContentType = reader.ReadString();
                }

                if ((flags & (1 << 6)) != 0) {
                    W = reader.ReadInt32();
                }

                if ((flags & (1 << 6)) != 0) {
                    H = reader.ReadInt32();
                }

                if ((flags & (1 << 7)) != 0) {
                    Duration = reader.ReadInt32();
                }

                SendMessage = reader.Read<BotInlineMessage>();
            }

            public override string ToString()
            {
                return string.Format("(BotInlineResultType Id:{0} Type:{1} Title:{2} Description:{3} Url:{4} ThumbUrl:{5} ContentUrl:{6} ContentType:{7} W:{8} H:{9} Duration:{10} SendMessage:{11})", Id, Type, Title, Description, Url, ThumbUrl, ContentUrl, ContentType, W, H, Duration, SendMessage);
            }
        }

        public class MessagesBotResultsType : MessagesBotResults
        {
            public override uint ConstructorCode => 0x1170b0a3;

            public True Gallery;
            public long QueryId;
            public string NextOffset;
            public List<BotInlineResult> Results;

            public MessagesBotResultsType() { }

            /// <summary>
            /// The following arguments can be null: Gallery, NextOffset
            /// </summary>
            /// <param name="Gallery">Can be null</param>
            /// <param name="QueryId">Can NOT be null</param>
            /// <param name="NextOffset">Can be null</param>
            /// <param name="Results">Can NOT be null</param>
            public MessagesBotResultsType(True Gallery, long QueryId, string NextOffset, List<BotInlineResult> Results)
            {
                this.Gallery = Gallery;
                this.QueryId = QueryId;
                this.NextOffset = NextOffset;
                this.Results = Results;
            }

            public override void Write(TBinaryWriter writer)
            {
                int flags =
                    (Gallery != null ? 1 << 0 : 0) |
                    (NextOffset != null ? 1 << 1 : 0);

                writer.Write(ConstructorCode);
                writer.Write(flags);

                if (Gallery != null) {

                }

                writer.Write(QueryId);
                if (NextOffset != null) {
                    writer.Write(NextOffset);
                }

                writer.Write(0x1cb5c415); // vector code
                writer.Write(Results.Count);
                foreach (BotInlineResult ResultsElement in Results)
                    ResultsElement.Write(writer);
            }

            public override void Read(TBinaryReader reader)
            {
                int flags = reader.ReadInt32();
                if ((flags & (1 << 0)) != 0) {
                    Gallery = reader.ReadTrue();
                }

                QueryId = reader.ReadInt64();
                if ((flags & (1 << 1)) != 0) {
                    NextOffset = reader.ReadString();
                }

                reader.ReadInt32(); // vector code
                int ResultsLength = reader.ReadInt32();
                Results = new List<BotInlineResult>(ResultsLength);
                for (int ResultsIndex = 0; ResultsIndex < ResultsLength; ResultsIndex++)
                    Results.Add(reader.Read<BotInlineResult>());
            }

            public override string ToString()
            {
                return string.Format("(MessagesBotResultsType Gallery:{0} QueryId:{1} NextOffset:{2} Results:{3})", Gallery, QueryId, NextOffset, Results);
            }
        }

        #endregion
    }
}
