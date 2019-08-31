using System;

namespace XorEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Xor encrypt and decrypt test";
            string password = "mypassword";
            string plain = "This is plain string.";
            string encrypted = XorEncryptDecrypt.XorString(plain, password);
            string decrypted = XorEncryptDecrypt.XorString(encrypted, password);
            Console.WriteLine("Plain:" + plain);
            Console.WriteLine("Encrypted:" + encrypted);
            Console.WriteLine("Decrypted:" + decrypted);
            Console.ReadKey();
        }

    }
}
