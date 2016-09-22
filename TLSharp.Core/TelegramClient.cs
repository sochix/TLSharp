using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TLSharp.Core.Auth;
using TLSharp.Core.MTProto;
using TLSharp.Core.MTProto.Crypto;
using TLSharp.Core.Network;
using TLSharp.Core.Requests;
using MD5 = System.Security.Cryptography.MD5;

namespace TLSharp.Core
{
    public class TelegramClient
    {
        private MtProtoSender _sender;
        private AuthKey _key;
        private TcpTransport _transport;
        private string _apiHash = "";
        private int _apiId = 0;
        private Session _session;
        private List<DcOption> dcOptions;

        public enum sms_type { numeric_code_via_sms = 0, numeric_code_via_telegram = 5 }

        public TelegramClient(ISessionStore store, string sessionUserId, int apiId, string apiHash)
        {
            _apiHash = apiHash;
            _apiId = apiId;
            if (_apiId == 0)
                throw new InvalidOperationException("Your API_ID is invalid. Do a configuration first https://github.com/sochix/TLSharp#quick-configuration");

            if (string.IsNullOrEmpty(_apiHash))
                throw new InvalidOperationException("Your API_ID is invalid. Do a configuration first https://github.com/sochix/TLSharp#quick-configuration");

            _session = Session.TryLoadOrCreateNew(store, sessionUserId);
            _transport = new TcpTransport(_session.ServerAddress, _session.Port);
        }

        public async Task<bool> Connect(bool reconnect = false)
        {
            if (_session.AuthKey == null || reconnect)
            {
                var result = await Authenticator.DoAuthentication(_transport);
                _session.AuthKey = result.AuthKey;
                _session.TimeOffset = result.TimeOffset;
            }

            _sender = new MtProtoSender(_transport, _session);

            if (!reconnect)
            {
                var request = new InitConnectionRequest(_apiId);

                await _sender.Send(request);
                await _sender.Receive(request);

                dcOptions = request.ConfigConstructor.dc_options;
            }

            return true;
        }

        private async Task ReconnectToDc(int dcId)
        {
            if (dcOptions == null || !dcOptions.Any())
                throw new InvalidOperationException($"Can't reconnect. Establish initial connection first.");

            var dc = dcOptions.Cast<DcOptionConstructor>().First(d => d.id == dcId);

            _transport = new TcpTransport(dc.ip_address, dc.port);
            _session.ServerAddress = dc.ip_address;
            _session.Port = dc.port;

            await Connect(true);
        }

        public bool IsUserAuthorized()
        {
            return _session.User != null;
        }

        
        public async Task<string> SendCodeRequest(string phoneNumber, sms_type tokenDestination = sms_type.numeric_code_via_telegram)
        {
            var completed = false;

            AuthSendCodeRequest request = null;

            while (!completed)
            {
                request = new AuthSendCodeRequest(phoneNumber, (int)tokenDestination, _apiId, _apiHash, "en");
                try
                {
                    await _sender.Send(request);
                    await _sender.Receive(request);

                    completed = true;
                }
                catch (InvalidOperationException ex)
                {
                    if (ex.Message.StartsWith("Your phone number registered to") && ex.Data["dcId"] != null)
                    {
                        await ReconnectToDc((int)ex.Data["dcId"]);
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return request._phoneCodeHash;
        }

        public async Task<User> MakeAuth(string phoneNumber, string phoneCodeHash, string code)
        {
            var request = new AuthSignInRequest(phoneNumber, phoneCodeHash, code);
            await _sender.Send(request);
            await _sender.Receive(request);

            OnUserAuthenticated(request.user, request.SessionExpires);

            return request.user;
        }

        //public async Task<User> SignUp(string phoneNumber, string phoneCodeHash, string code, string firstName, string lastName)
        //{
        //    var request = new AuthSignUpRequest(phoneNumber, phoneCodeHash, code, firstName, lastName);
        //    await _sender.Send(request);
        //    await _sender.Receive(request);

        //    OnUserAuthenticated(request.user, request.SessionExpires);

        //    return request.user;
        //}

        private void OnUserAuthenticated(User user, int sessionExpiration)
        {
            _session.User = user;
            _session.SessionExpires = sessionExpiration;

            _session.Save();
        }

        //public async Task<InputFile> UploadFile(string name, byte[] data)
        //{
        //    var partSize = 65536;

        //    var file_id = DateTime.Now.Ticks;

        //    var partedData = new Dictionary<int, byte[]>();
        //    var parts = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(data.Length) / Convert.ToDouble(partSize)));
        //    var remainBytes = data.Length;
        //    for (int i = 0; i < parts; i++)
        //    {
        //        partedData.Add(i, data
        //            .Skip(i * partSize)
        //            .Take(remainBytes < partSize ? remainBytes : partSize)
        //            .ToArray());

        //        remainBytes -= partSize;
        //    }

        //    for (int i = 0; i < parts; i++)
        //    {
        //        var saveFilePartRequest = new Upload_SaveFilePartRequest(file_id, i, partedData[i]);
        //        await _sender.Send(saveFilePartRequest);
        //        await _sender.Receive(saveFilePartRequest);

        //        if (saveFilePartRequest.Done == false)
        //            throw new InvalidOperationException($"File part {i} does not uploaded");
        //    }

        //    string md5_checksum;
        //    using (var md5 = MD5.Create())
        //    {
        //        var hash = md5.ComputeHash(data);
        //        var hashResult = new StringBuilder(hash.Length * 2);

        //        for (int i = 0; i < hash.Length; i++)
        //            hashResult.Append(hash[i].ToString("x2"));

        //        md5_checksum = hashResult.ToString();
        //    }

        //    var inputFile = new InputFileConstructor(file_id, parts, name, md5_checksum);

        //    return inputFile;
        //}

        //public async Task<bool> SendMediaMessage(int contactId, InputFile file)
        //{
        //    var request = new Message_SendMediaRequest(
        //        new InputPeerContactConstructor(contactId),
        //        new InputMediaUploadedPhotoConstructor(file));

        //    await _sender.Send(request);
        //    await _sender.Receive(request);

        //    return true;
        //}

        //public async Task<int?> ImportContactByPhoneNumber(string phoneNumber)
        //{
        //    if (!validateNumber(phoneNumber))
        //        throw new InvalidOperationException("Invalid phone number. It should be only digit string, from 5 to 20 digits.");

        //    var request = new ImportContactRequest(new InputPhoneContactConstructor(0, phoneNumber, "My Test Name", String.Empty));
        //    await _sender.Send(request);
        //    await _sender.Receive(request);

        //    var importedUser = (ImportedContactConstructor)request.imported.FirstOrDefault();

        //    return importedUser?.user_id;
        //}

        //public async Task<int?> ImportByUserName(string username)
        //{
        //    if (string.IsNullOrEmpty(username))
        //        throw new InvalidOperationException("Username can't be null");

        //    var request = new ImportByUserName(username);
        //    await _sender.Send(request);
        //    await _sender.Receive(request);

        //    return request.id;
        //}

        //public async Task SendMessage(int id, string message)
        //{
        //    var request = new SendMessageRequest(new InputPeerContactConstructor(id), message);

        //    await _sender.Send(request);
        //    await _sender.Receive(request);
        //}

        //public async Task<List<Message>> GetMessagesHistoryForContact(int user_id, int offset, int limit, int max_id = -1)
        //{
        //    var request = new GetHistoryRequest(new InputPeerContactConstructor(user_id), offset, max_id, limit);
        //    await _sender.Send(request);
        //    await _sender.Receive(request);

        //    return request.messages;
        //}

        //public async Task<Tuple<storage_FileType, byte[]>> GetFile(long volume_id, int local_id, long secret, int offset, int limit)
        //{
        //    var request = new GetFileRequest(new InputFileLocationConstructor(volume_id, local_id, secret), offset, limit);
        //    await _sender.Send(request);
        //    await _sender.Receive(request);

        //    return Tuple.Create(request.type, request.bytes);
        //}

        //public async Task<MessageDialogs> GetDialogs(int offset, int limit, int max_id = 0)
        //{
        //    var request = new GetDialogsRequest(offset, max_id, limit);
        //    await _sender.Send(request);
        //    await _sender.Receive(request);

        //    return new MessageDialogs
        //    {
        //        Dialogs = request.dialogs,
        //        Messages = request.messages,
        //        Chats = request.chats,
        //        Users = request.users,
        //    };
        //}

        //public async Task<UserFull> GetUserFull(int user_id)
        //{
        //    var request = new GetUserFullRequest(user_id);
        //    await _sender.Send(request);
        //    await _sender.Receive(request);

        //    return request._userFull;
        //}

        //private bool validateNumber(string number)
        //{
        //    var regex = new Regex("^\\d{7,20}$");

        //    return regex.IsMatch(number);
        //}

        //public async Task<ContactsContacts> GetContacts(IList<int> contactIds = null)
        //{
        //    var request = new GetContactsRequest(contactIds);
        //    await _sender.Send(request);
        //    await _sender.Receive(request);

        //    return new ContactsContacts
        //    {
        //        Contacts = request.Contacts,
        //        Users = request.Users,
        //    };
        //}

        //public async Task<Messages_statedMessageConstructor> CreateChat(string title, List<string> userPhonesToInvite)
        //{
        //    var userIdsToInvite = new List<int>();
        //    foreach (var userPhone in userPhonesToInvite)
        //    {
        //        var uid = await ImportContactByPhoneNumber(userPhone);
        //        if (!uid.HasValue)
        //            throw new InvalidOperationException($"Failed to retrieve contact {userPhone}");

        //        userIdsToInvite.Add(uid.Value);
        //    }

        //    return await CreateChat(title, userIdsToInvite);
        //}

        //public async Task<Messages_statedMessageConstructor> CreateChat(string title, List<int> userIdsToInvite)
        //{
        //    var request = new CreateChatRequest(userIdsToInvite.Select(uid => new InputUserContactConstructor(uid)).ToList(), title);

        //    await _sender.Send(request);
        //    await _sender.Receive(request);

        //    return request.message;
        //}

        //public async Task<Messages_statedMessageConstructor> AddChatUser(int chatId, int userId)
        //{
        //    var request = new AddChatUserRequest(chatId, new InputUserContactConstructor(userId));

        //    await _sender.Send(request);
        //    await _sender.Receive(request);

        //    return request.message;
        //}

        //public async Task<Messages_statedMessageConstructor> DeleteChatUser(int chatId, int userId)
        //{
        //    var request = new DeleteChatUserRequest(chatId, new InputUserContactConstructor(userId));

        //    await _sender.Send(request);
        //    await _sender.Receive(request);

        //    return request.message;
        //}

        //public async Task<Messages_statedMessageConstructor> LeaveChat(int chatId)
        //{
        //    return await DeleteChatUser(chatId, ((UserSelfConstructor) _session.User).id);
        //}

        //public async Task<updates_State> GetUpdatesState()
        //{
        //    var request = new GetUpdatesStateRequest();

        //    await _sender.Send(request);
        //    await _sender.Receive(request);

        //    return request.updates;
        //}

        //public async Task<updates_Difference> GetUpdatesDifference(int lastPts, int lastDate, int lastQts)
        //{
        //    var request = new GetUpdatesDifferenceRequest(lastPts, lastDate, lastQts);

        //    await _sender.Send(request);
        //    await _sender.Receive(request);

        //    return request.updatesDifference;
        //}
    }
}
