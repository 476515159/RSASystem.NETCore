using System;
using System.Collections.Generic;
using System.Text;

namespace RSASystem
{
    public interface IRsaHelperService
    {
        string Sign(string content);

        byte[] SignBits(string content);

        bool VerifySign(string content, string sign);

        bool VerifySign(string content, byte[] sign);

        string Decrypt(string cipherText);

        string Decrypt(byte[] cipherBits);

        string Encrypt(string text);

        string Encrypt(byte[] textBits);
    }
}
