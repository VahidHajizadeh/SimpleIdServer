﻿using SQLite;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SimpleIdServer.Mobile.Models
{
    public class CredentialRecord
    {
        public CredentialRecord()
        {

        }

        public CredentialRecord(byte[] credentialId, X509Certificate2 certificate, ECDsa privateKey, uint sigCount)
        {
            Id = Convert.ToBase64String(credentialId);
            PublicKey = Convert.ToBase64String(certificate.RawData);
            ECDsaPublic = privateKey.ExportSubjectPublicKeyInfo();
            ECDsaPrivate = privateKey.ExportECPrivateKey();
            SigCount = sigCount;
        }

        [PrimaryKey]
        public string Id { get; set; }
        public string PublicKey { get; set; }
        public byte[] ECDsaPublic { get; set; }
        public byte[] ECDsaPrivate { get; set; }
        public uint SigCount { get; set; }
        public X509Certificate2 Certificate
        {
            get
            {
                return new X509Certificate2(Convert.FromBase64String(PublicKey));
            }
        }
        public ECDsa PrivateKey
        {
            get
            {
                var ecdsa = ECDsa.Create();
                ecdsa.ImportSubjectPublicKeyInfo(ECDsaPublic, out var _);
                ecdsa.ImportECPrivateKey(ECDsaPrivate, out var _);
                return ecdsa;
            }
        }
        public byte[] IdPayload
        {
            get
            {
                return Convert.FromBase64String(Id);
            }
        }
    }
}
