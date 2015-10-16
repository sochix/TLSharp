using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    public class GetNearestDcRequest : MTProtoRequest
    {
        public string country;
        public int this_dc;
        public int nearest_dc;

        public GetNearestDcRequest() { }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0x1fb33026);
        }

        public override void OnResponse(BinaryReader reader)
        {
            var code = reader.ReadUInt32();

            country = Serializers.String.read(reader);
            this_dc = reader.ReadInt32();
            nearest_dc = reader.ReadInt32();

            System.Diagnostics.Debug.WriteLine("country: " + country);
            System.Diagnostics.Debug.WriteLine("this_dc: " + this_dc);
            System.Diagnostics.Debug.WriteLine("nearest: " + nearest_dc);
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Confirmed => true;
        public override bool Responded { get; }
    }
}
