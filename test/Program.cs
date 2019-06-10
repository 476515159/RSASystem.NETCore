using RSASystem;
using System;
using System.Text;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            string privateKey = @"MIICWwIBAAKBgQCYMTaH4NMw5fQsgx3pv8xaAxhOdtUz/m5nfl9XHtGboXNzUzx/
            ehh8c3RgpGcig83JXOYOXafmx3OS28Ed3e2fmC5Yo5RD6DRHnBCid28EjQ5PgCTd
            dNqDvcXU3YMH4sO1qNBx5MQpk92Kt/NdquIJmzhgpjP15MO5CFHcU90ZvQIDAQAB
            AoGANlvdjkrPI/f+bqemV4caBkx0shHftOJ7rJuGkid/1oakJdzlDuMdO9ZBCwOt
            krZhGjsEML1i6xryPNIg9/n8lSdQqIUW61HXYwKUK5xQWz/MstWbbIx3t5driQFR
            Fv53NLdemeF/0AJiD5COO1fkoM+1By2LlI0ths8cQLcOpgECQQDIc46yV1N5IuS0
            MYD0LgggaJ08WF0PrwjXgs+DRp3+ZE5WTs1JDkBQM9E598xbmy7AAFtdtR3L5CH8
            5Qh+KfwJAkEAwl4MuRMdIjiHiw1YoIUliy6t3XPvxeOEiG/P15adKrxI5A5QylM0
            TtbZT3YZurdy3nrJ75LxuU9cSYKzxtVFFQJAMboJElD7kjeHyPPm66xns7KAHzJE
            k9l2NhBrbkOcejlj/aE65/6zEbJpGxpQBgGvTU5JXCvMIoKLs/MVckb0EQJASze+
            ULkW4zFhMuy9SZF9T/mGi1bciYZcubgbhODifbFTu/3WQhYk/gWjH18i4eEwcOyv
            zSjepsoRetk73UyXaQJAOfr3Gg1dGvoLiwZ3fXoDVupahnKg73SAd72+24qQs2AT
            16T8FKop259xisLu+WSUTfSUhao5qOpZJ/PTwFRlzw==";
            string publicKey = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCYMTaH4NMw5fQsgx3pv8xaAxhO
            dtUz/m5nfl9XHtGboXNzUzx/ehh8c3RgpGcig83JXOYOXafmx3OS28Ed3e2fmC5Y
            o5RD6DRHnBCid28EjQ5PgCTddNqDvcXU3YMH4sO1qNBx5MQpk92Kt/NdquIJmzhg
            pjP15MO5CFHcU90ZvQIDAQAB";
            using (var rsa = new RSASystemHelper(RSAType.MD5, Encoding.UTF8, privateKey, publicKey))
            {
                string data = "123";
                Console.WriteLine("(base64测试)原字符串:" + data);
                string enText = rsa.Encrypt(data);
                Console.WriteLine("加密:" + enText);
                Console.WriteLine("解密:" + rsa.Decrypt(enText));
                string sign = rsa.Sign(data);
                Console.WriteLine("签名:" + sign);
                Console.WriteLine("验签:" + rsa.Verify(data, sign));
            }

            string priveateKey2 = HexByte.ByteToHexStr(Convert.FromBase64String(privateKey));
            string publicKey2 = HexByte.ByteToHexStr(Convert.FromBase64String(publicKey));
            using (var rsa2 = new RSASystemHelper(RSAType.MD5, Encoding.UTF8, priveateKey2, publicKey2))
            {
                Console.WriteLine();
                string data2 = "456";
                Console.WriteLine("(16进制测试)原字符串:" + data2);
                string enText2 = rsa2.Encrypt(data2);
                Console.WriteLine("加密:" + enText2);
                Console.WriteLine("解密:" + rsa2.Decrypt(enText2));
                string sign2 = rsa2.Sign(data2);
                Console.WriteLine("签名:" + sign2);
                Console.WriteLine("验签:" + rsa2.Verify(data2, sign2));
            }

            Console.ReadKey();
        }
    }
}
