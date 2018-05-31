using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using OrderCakes;

namespace OrderCakes.License
{
    class Program
    {
        private static void GenerateNewKeyPair()
        {

            string withSecret;
            string woSecret;

            using (var rsaCsp = new RSACryptoServiceProvider())
            {
                withSecret = rsaCsp.ToXmlString(true);
                woSecret = rsaCsp.ToXmlString(false);
            }

            File.WriteAllText("private.xml", withSecret);
            File.WriteAllText("public.xml", woSecret);
        }
        static void Main(string[] args)
        {
            if (args.Any(a => a == "--generate"))
            {
                GenerateNewKeyPair();
            }

            var dto = new LicenceDto()
            {
                ValidUntil = DateTime.Now.AddDays(30)
            };

            var fileName = string.Join("", DateTime.Now.ToString().Where(c => char.IsDigit(c)));
            new LicenceGenerator().CreateLicenseFile(dto, fileName + ".ocake_licence");
        }
    }

    class LicenceGenerator
    {
        private static string PrivateKey = @"<RSAKeyValue><Modulus>wRTyAHqbc3oIHBYtsO3qgVCeFWIl/mHAlCEKjI9trV4WcgU1sOFJ9QzvpJ51gKJM7NIRE/kKuEBZXTs5st6ecNUlA983i7CyLqWlNxwx3IlTP76+szBfHLnyV2ZAC/+siBNh5Xmrtt16DMzuAdbeyfya758ifI5fSSd2XmPOXg0=</Modulus><Exponent>AQAB</Exponent><P>8IN7uTb/qh7PflK8ezsP/SLtF/5Op9DN+VufBsNSb3jzJ3Z5oPd3heqeoB5kwPbSQXzw88MrUNFWOR2FtAqliw==</P><Q>zYOeNwekAjN1BiV566xJQ3DakWUuLFv3NpsfnhxJM1aXlgnKkQu6EKOGqMiIhN7gW6N6JNu9wvzTDzyQ4ZLtxw==</Q><DP>eCFURNihrnkhLnloxyxi+g7d2aQd6Vgz6R7IOXqJzD/fQ5C7g5jXTD456MQFkxQ1RJyBRV/wXLeSl4iVZa4DrQ==</DP><DQ>NGDKLx5MZ58zwShGBZG4bm7R1eKivP2HaSxqB4MQCPyVz9CZBxSlDF6REG1jlfGz3scwdzpmB88l/6khB8zJuw==</DQ><InverseQ>1QtrrxcKhciprCIbZ/IHmrhYwF0snnj6WAD4R4A8YzTdmQfwJ2tZpOrJEfy3DSUERt9wDXhS28URscpr3JMzyA==</InverseQ><D>OIiedx9vJrejaj3gMP7oe+aT8uTjBQGwNm8aPJ++WFXFMqtt1hWQ5wxbmpK/CuP6rBix4Ww5BEvrHo45zbK7JwHYw2cyQBUP1GjUWXG7mIcTcOCn7Vm/b7w6C7cLOrsmBZNA1k05/ZQPfKE+xXnV9J8DK4yLOAJaPb/A8xFfXe0=</D></RSAKeyValue>";
        public void CreateLicenseFile(LicenceDto dto, string fileName)
        {
            var ms = new MemoryStream();
            new XmlSerializer(typeof(LicenceDto)).Serialize(ms, dto);
            RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider();

            rsaKey.FromXmlString(PrivateKey);

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.PreserveWhitespace = true;
            ms.Seek(0, SeekOrigin.Begin);
            xmlDoc.Load(ms);
            SignXml(xmlDoc, rsaKey);
            xmlDoc.Save(fileName);
        }

        public static void SignXml(XmlDocument xmlDoc, RSA Key)
        {
            if (xmlDoc == null)
                throw new ArgumentException("xmlDoc");
            if (Key == null)
                throw new ArgumentException("Key");

            SignedXml signedXml = new SignedXml(xmlDoc);
            signedXml.SigningKey = Key;

            Reference reference = new Reference();
            reference.Uri = "";

            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            signedXml.AddReference(reference);
            signedXml.ComputeSignature();

            XmlElement xmlDigitalSignature = signedXml.GetXml();
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
        }
    }
}

