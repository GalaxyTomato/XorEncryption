using System.Text;
using System.IO;

namespace XorEncryption
{
    /// <summary>
    /// Xor encryption and decryption algorithm
    /// </summary>
    public static class XorEncryptDecrypt
    {
        /// <summary>
        /// XOR加密/解密文件，可处理大文件
        /// </summary>
        /// <param name="inFile">输入文件</param>
        /// <param name="outFile">输出文件</param>
        /// <param name="key">密钥</param>
        /// <param name="bigFileSize">自定义大文件[字节]数，大于此数的文件将按字节读取后处理，否则一次性读取后处理。（建议设置较大的数，推荐60mb） </param>
        /// <returns>成功/失败</returns>
        public static bool XorFile(string inFile, string outFile, int key, int bigFileSize)
        {
            try
            {
                if (bigFileSize < 0 || bigFileSize >= int.MaxValue) { bigFileSize = 1; }
                FileInfo fi = new FileInfo(inFile);
                if (fi.Length > bigFileSize) //大文件按字节读取后处理，cpu占用和磁盘占用较高
                {
                    using (FileStream fsIn = new FileStream(inFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (FileStream fsOut = new FileStream(outFile, FileMode.Create))
                        {
                            int data;
                            while ((data = fsIn.ReadByte()) != -1)
                            {
                                int temp = data ^ key;
                                fsOut.WriteByte((byte)temp);
                            }
                        }
                    }
                    return true;
                }
                else //小文件一次性读取到内存处理,速度更快,显著降低磁盘占用率和cpu使用率
                {
                    byte[] bytes = File.ReadAllBytes(inFile);
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        bytes[i] = (byte)(bytes[i] ^ key);
                    }
                    using (FileStream fsOut = new FileStream(outFile, FileMode.Create))
                    {
                        fsOut.Write(bytes, 0, bytes.Length);
                    }
                    return true;
                }

            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// XOR加密解密字符串，结果可能乱码但不影响加解密
        /// </summary>
        /// <param name="text">明文或密文</param>
        /// <param name="key">密码</param>
        /// <returns>密文或明文</returns>
        public static string XorString(string text, string key)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                result.Append((char)((uint)text[i] ^ (uint)key[i % key.Length]));
            }
            return result.ToString();
        }
    }
}
