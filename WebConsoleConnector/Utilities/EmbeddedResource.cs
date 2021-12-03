using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace WebConsoleConnector.Utilities
{
    public static class EmbeddedResource<T>
    {
        public static Stream StreamOf(string name)
        {
            var assembly = typeof(T).GetTypeInfo().Assembly;

            string assemblyName = assembly.FullName.Substring(0, assembly.FullName.IndexOf(','));
            string path = $"{assemblyName}.{name.Replace("/", ".")}";
            return assembly.GetManifestResourceStream(path);
        }

        public static byte[] ReadAllBytes(string name)
        {
            int BUFFER_SIZE = 4_096;
            List<byte[]> buffers = new();
            byte[] buffer = new byte[BUFFER_SIZE];
            var stream = StreamOf(name);
            while (true)
            {
                int count = stream.Read(buffer, 0, BUFFER_SIZE);
                if (count == BUFFER_SIZE) buffers.Add(buffer);
                else
                {
                    byte[] result = new byte[BUFFER_SIZE * buffers.Count + count];
                    for (int i = 0; i < buffers.Count; i++) Array.Copy(buffers[i], 0, result, i * BUFFER_SIZE, BUFFER_SIZE);
                    Array.Copy(buffer, 0, result, buffers.Count * BUFFER_SIZE, count);
                    return result;
                }
            }

        }
    }
}
