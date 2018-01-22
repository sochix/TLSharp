using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TeleSharp.TL;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    public class InitConnectionRequest : MTProtoRequest
    {
        public InvokeWithLayerArgs invokeWithLayer { get; set; }
        public InitConnectionArgs initConn { get; set; }
        public GetConfigArgs getConfig { get; set; }
        public TeleSharp.TL.Config Configs { get; set; }

        public InitConnectionRequest(int apiId)
        {
            invokeWithLayer = new InvokeWithLayerArgs();
            initConn = new InitConnectionArgs();
            invokeWithLayer.layer = 53;
            initConn.api_id = apiId;
            initConn.app_version = "1.0-SNAPSHOT";
            initConn.lang_code = "en";
            initConn.system_version = "WinPhone 8.0";
            initConn.device_model = "WinPhone Emulator";
            initConn.query = new GetConfigArgs();
            invokeWithLayer.query = initConn;
        }

        public override void OnSend(BinaryWriter writer)
        {
            Serializer.Serialize(invokeWithLayer, invokeWithLayer.GetType(), writer);
        }


        public override void OnResponse(BinaryReader reader)
        {
            Configs = (TeleSharp.TL.Config)Deserializer.Deserialize(typeof(TeleSharp.TL.Config), reader);
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Responded
        {
            get { return true; }
        }

        public override void OnSendSuccess()
        {

        }
        public override bool Confirmed => true;
    }
}
