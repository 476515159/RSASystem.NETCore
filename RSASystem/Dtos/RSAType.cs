using System;
using System.Collections.Generic;
using System.Text;

namespace RSASystem
{
    /// <summary>
	/// RSA算法类型
	/// </summary>
    public enum RSAType
    {
        /// <summary>
		/// SHA1
		/// </summary>
		SHA1 = 0,
        /// <summary>
        /// SHA256 密钥长度至少为2048
        /// </summary>
        SHA256 = 1,

        SHA384 = 2,

        SHA512 = 3,

        MD5 = 4

    }
}
