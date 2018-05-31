using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Xml.Serialization;

namespace OrderCakes
{
    public class LicenceDto
    {
        public DateTime ValidUntil { get; set; }

        public static string PublicKey =
            @"<RSAKeyValue><Modulus>wRTyAHqbc3oIHBYtsO3qgVCeFWIl/mHAlCEKjI9trV4WcgU1sOFJ9QzvpJ51gKJM7NIRE/kKuEBZXTs5st6ecNUlA983i7CyLqWlNxwx3IlTP76+szBfHLnyV2ZAC/+siBNh5Xmrtt16DMzuAdbeyfya758ifI5fSSd2XmPOXg0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
    }

    public class LicenceValidator
    {
        public LicenceValidator()
        {
            var cd = Directory.GetCurrentDirectory();
            foreach (var file in Directory.EnumerateFiles(cd, "*.ocake_licence"))
            {
                if (TryLoadLicense(file))
                {
                    if (IsValid)
                    {
                        return;
                    }
                }
            }
        }

        public bool IsValid
        {
            get { return ValidUntil > DateTime.Now; }
        }

        private bool TryLoadLicense(string fileName)
        {

            RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider();
            rsaKey.FromXmlString(LicenceDto.PublicKey);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load(fileName);

            bool result = VerifyXml(xmlDoc, rsaKey);
            if (!result)
                return false;
            HasLicense = true;

            LicenceDto dto;
            using (var fileStream = File.OpenRead(fileName))
            {
                dto = (LicenceDto)new XmlSerializer(typeof(LicenceDto)).Deserialize(fileStream);
            }

            ValidUntil = dto.ValidUntil;
            return true;

        }

        public DateTime ValidUntil { get; set; }

        public bool HasLicense { get; set; }

        public static Boolean VerifyXml(XmlDocument Doc, RSA Key)
        {

            if (Doc == null)

                throw new ArgumentException("Doc");

            if (Key == null)

                throw new ArgumentException("Key");

            SignedXml signedXml = new SignedXml(Doc);

            XmlNodeList nodeList = Doc.GetElementsByTagName("Signature");

            if (nodeList.Count <= 0)
            {
                throw new CryptographicException("Verification failed: No Signature was found in the document.");
            }

            if (nodeList.Count >= 2)
            {
                throw new CryptographicException("Verification failed: More that one signature was found for the document.");
            }

            signedXml.LoadXml((XmlElement)nodeList[0]);

            return signedXml.CheckSignature(Key);
        }
    }
}