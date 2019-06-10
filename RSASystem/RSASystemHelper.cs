using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RSASystem
{
    /// <summary>
	/// RSA加解密 使用OpenSSL的公钥加密/私钥解密
	/// 
	/// 公私钥请使用openssl生成  ssh-keygen -t rsa 命令生成的公钥私钥是不行的
	/// 
	/// 时间：2019年6月6日
    /// 作者:lijinlong
	/// </summary>
    public class RSASystemHelper
    {
        private readonly RSA _privateKeyRsaProvider;
        private readonly RSA _publicKeyRsaProvider;
        private readonly HashAlgorithmName _hashAlgorithmName;
        private readonly Encoding _encoding;
        private readonly bool IsBase64;

        /// <summary>
		/// 实例化RSASystemHelper
		/// </summary>
		/// <param name="rsaType">加密算法类型</param>
		/// <param name="encoding">编码类型</param>
		/// <param name="privateKey">私钥</param>
		/// <param name="publicKey">公钥</param>
		public RSASystemHelper(RSAType rsaType, Encoding encoding, string privateKey = null, string publicKey = null)
        {
            _encoding = encoding ?? Encoding.UTF8;
            if (!string.IsNullOrEmpty(privateKey))
            {
                IsBase64 = !JudgeBinary.IsHexadecimal(privateKey);
                _privateKeyRsaProvider = CreateRsaProviderFromPrivateKey(privateKey);
            }

            if (!string.IsNullOrEmpty(publicKey))
            {
                IsBase64 = !JudgeBinary.IsHexadecimal(publicKey);
                _publicKeyRsaProvider = CreateRsaProviderFromPublicKey(publicKey);
            }

            switch (rsaType)
            {
                case RSAType.MD5:
                    _hashAlgorithmName = HashAlgorithmName.MD5;
                    break;
                case RSAType.SHA1:
                    _hashAlgorithmName = HashAlgorithmName.SHA1;
                    break;
                case RSAType.SHA256:
                    _hashAlgorithmName = HashAlgorithmName.SHA256;
                    break;
                case RSAType.SHA384:
                    _hashAlgorithmName = HashAlgorithmName.SHA384;
                    break;
                case RSAType.SHA512:
                    _hashAlgorithmName = HashAlgorithmName.SHA512;
                    break;
                default:
                    _hashAlgorithmName = HashAlgorithmName.MD5;
                    break;
            }
        }

        #region 使用私钥签名

        /// <summary>
        /// 使用私钥签名
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <returns></returns>
        public string Sign(string data)
        {
            byte[] dataBytes = _encoding.GetBytes(data);

            var signatureBytes = _privateKeyRsaProvider.SignData(dataBytes, _hashAlgorithmName, RSASignaturePadding.Pkcs1);
            if (IsBase64)
                return Convert.ToBase64String(signatureBytes);
            else
                return HexByte.ByteToHexStr(signatureBytes);
        }

        #endregion

        #region 使用公钥验证签名

        /// <summary>
        /// 使用公钥验证签名
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public bool Verify(string data, string sign)
        {
            byte[] dataBytes = _encoding.GetBytes(data);
            byte[] signBytes;
            if (IsBase64)
                signBytes = Convert.FromBase64String(sign);
            else
                signBytes = HexByte.StrToToHexByte(sign);

            var verify = _publicKeyRsaProvider.VerifyData(dataBytes, signBytes, _hashAlgorithmName, RSASignaturePadding.Pkcs1);

            return verify;
        }

        #endregion

        #region 解密

        public string Decrypt(string cipherText)
        {
            if (_privateKeyRsaProvider == null)
            {
                throw new ArgumentNullException("_privateKeyRsaProvider is null");
            }
            byte[] textBits;
            if (IsBase64)
                textBits = Convert.FromBase64String(cipherText);
            else
                textBits = HexByte.StrToToHexByte(cipherText);
            return _encoding.GetString(_privateKeyRsaProvider.Decrypt(textBits, RSAEncryptionPadding.Pkcs1));
        }

        #endregion

        #region 加密

        public string Encrypt(string text)
        {
            if (_publicKeyRsaProvider == null)
            {
                throw new ArgumentNullException("_publicKeyRsaProvider is null");
            }
            byte[] textBits= _publicKeyRsaProvider.Encrypt(_encoding.GetBytes(text), RSAEncryptionPadding.Pkcs1);
            if (IsBase64)
                return Convert.ToBase64String(textBits);
            else
                return HexByte.ByteToHexStr(textBits);
        }

        #endregion

        #region 使用私钥创建RSA实例

        public RSA CreateRsaProviderFromPrivateKey(string privateKey)
        {
            byte[] privateKeyBits;
            if (!IsBase64)
            {
                //如果是十六进制,则将十六进制转为byte
                privateKeyBits = HexByte.StrToToHexByte(privateKey);
            }
            else
            {
                //否则默认为base64
                privateKeyBits = Convert.FromBase64String(privateKey);
            }
            return CreateRsaProvider.CreateRsaProviderFromPrivateKey(privateKeyBits);
        }

        #endregion

        #region 使用公钥创建RSA实例
        public RSA CreateRsaProviderFromPublicKey(string publicKeyString)
        {
            byte[] x509Key;
            if (!IsBase64)
            {
                //如果是十六进制,则将十六进制转为byte
                x509Key = HexByte.StrToToHexByte(publicKeyString);
            }
            else
            {
                //否则默认为base64
                x509Key = Convert.FromBase64String(publicKeyString);
            }
            return CreateRsaProvider.CreateRsaProviderFromPublicKey(x509Key);
        }

        #endregion

    }
}
