using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MoraES.Model
{
    public struct MoraEvent
    {
        public ulong timestamp;
        public ContentTypeEnum contentType;
        public byte[] payload;

        public MoraEvent(ulong timestamp, ContentTypeEnum contentType, byte[] payload)
        {
            this.timestamp = timestamp;
            this.contentType = contentType;
            this.payload = payload;
        }
        public byte[] ToByteArray()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(this.timestamp);
                    bw.Write((ushort)this.contentType);
                    bw.Write(this.payload);
                }
                return ms.ToArray();
            }
        }
    }

    public enum ContentTypeEnum : ushort
    {
        BINARY,
        JSON,
        STRING
    }
}