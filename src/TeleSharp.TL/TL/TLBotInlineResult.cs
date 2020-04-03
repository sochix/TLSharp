using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(295067450)]
    public class TLBotInlineResult : TLAbsBotInlineResult
    {
        public override int Constructor
        {
            get
            {
                return 295067450;
            }
        }

        public int Flags { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public TLAbsWebDocument Thumb { get; set; }
        public TLAbsWebDocument Content { get; set; }
        public TLAbsBotInlineMessage SendMessage { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Id = StringUtil.Deserialize(br);
            Type = StringUtil.Deserialize(br);
            if ((Flags & 2) != 0)
                Title = StringUtil.Deserialize(br);
            else
                Title = null;

            if ((Flags & 4) != 0)
                Description = StringUtil.Deserialize(br);
            else
                Description = null;

            if ((Flags & 8) != 0)
                Url = StringUtil.Deserialize(br);
            else
                Url = null;

            if ((Flags & 16) != 0)
                Thumb = (TLAbsWebDocument)ObjectUtils.DeserializeObject(br);
            else
                Thumb = null;

            if ((Flags & 32) != 0)
                Content = (TLAbsWebDocument)ObjectUtils.DeserializeObject(br);
            else
                Content = null;

            SendMessage = (TLAbsBotInlineMessage)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            StringUtil.Serialize(Id, bw);
            StringUtil.Serialize(Type, bw);
            if ((Flags & 2) != 0)
                StringUtil.Serialize(Title, bw);
            if ((Flags & 4) != 0)
                StringUtil.Serialize(Description, bw);
            if ((Flags & 8) != 0)
                StringUtil.Serialize(Url, bw);
            if ((Flags & 16) != 0)
                ObjectUtils.SerializeObject(Thumb, bw);
            if ((Flags & 32) != 0)
                ObjectUtils.SerializeObject(Content, bw);
            ObjectUtils.SerializeObject(SendMessage, bw);

        }
    }
}
