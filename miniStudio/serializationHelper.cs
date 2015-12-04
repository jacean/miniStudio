using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace miniStudio
{
    class serializationHelper
    {
        public static MemoryStream SerializedToStream(Object graph)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, graph);
            stream.Position = 0;//必须得有这么一个初始化到开头的命令，否则就会有“在分析完成之前就遇到流结尾。”的问题
            return stream;

        }
        public static object DeserializedFromMemory(MemoryStream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
        }
    }
}
