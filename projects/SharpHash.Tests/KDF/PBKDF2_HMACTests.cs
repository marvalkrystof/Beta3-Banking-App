﻿///////////////////////////////////////////////////////////////////////
/// SharpHash Library
/// Copyright(c) 2019 - 2020  Mbadiwe Nnaemeka Ronald
/// Github Repository <https://github.com/ron4fun/SharpHash>
///
/// The contents of this file are subject to the
/// Mozilla Public License Version 2.0 (the "License");
/// you may not use this file except in
/// compliance with the License. You may obtain a copy of the License
/// at https://www.mozilla.org/en-US/MPL/2.0/
///
/// Software distributed under the License is distributed on an "AS IS"
/// basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See
/// the License for the specific language governing rights and
/// limitations under the License.
///
/// Acknowledgements:
///
/// Thanks to Ugochukwu Mmaduekwe (https://github.com/Xor-el) for his creative
/// development of this library in Pascal/Delphi (https://github.com/Xor-el/HashLib4Pascal).
///
/// Also, I will like to thank Udezue Chukwunwike (https://github.com/IzarchTech) for
/// his contributions to the growth and development of this library.
///
////////////////////////////////////////////////////////////////////////

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpHash.Base;
using SharpHash.Interfaces;
using SharpHash.Utils;
using System.Text;

namespace SharpHash.KDF.Tests
{
    // ====================== PBKDF2_HMACSHA1TestCase ======================
    ////////////////////
    // PBKDF2_HMACSHA1
    ///////////////////
    [TestClass]
    public class PBKDF2_HMACSHA1TestCase
    {
        private readonly string ExpectedString = "BFDE6BE94DF7E11DD409BCE20A0255EC327CB936FFE93643";
        private readonly byte[] Password = new byte[] { 0x70, 0x61, 0x73, 0x73, 0x77, 0x6F, 0x72, 0x64 };
        private readonly byte[] Salt = new byte[] { 0x78, 0x57, 0x8E, 0x5A, 0x5D, 0x63, 0xCB, 0x06 };
        private readonly IHash hash = HashFactory.Crypto.CreateSHA1();

        [TestMethod]
        public void TestOne()
        {
            IPBKDF2_HMAC PBKDF2 = HashFactory.KDF.PBKDF2_HMAC.CreatePBKDF2_HMAC(hash, Password, Salt, 2048);
            byte[] Key = PBKDF2.GetBytes(24);
            PBKDF2.Clear();

            string ActualString = Converters.ConvertBytesToHexString(Key, false);

            Assert.AreEqual(ExpectedString, ActualString);
        }
    }

    // ====================== PBKDF2_HMACSHA2_256TestCase ======================
    ////////////////////
    // PBKDF2_HMACSHA2_256
    ///////////////////
    [TestClass]
    public class PBKDF2_HMACSHA2_256TestCase
    {
        private readonly string ExpectedString = "0394A2EDE332C9A13EB82E9B24631604C31DF978B4E2F0FBD2C549944F9D79A5";
        private readonly byte[] Password = Converters.ConvertStringToBytes("password", Encoding.UTF8);
        private readonly byte[] Salt = Converters.ConvertStringToBytes("salt", Encoding.UTF8);
        private readonly IHash hash = HashFactory.Crypto.CreateSHA2_256();

        [TestMethod]
        public void TestOne()
        {
            IPBKDF2_HMAC PBKDF2 = HashFactory.KDF.PBKDF2_HMAC.CreatePBKDF2_HMAC(hash, Password, Salt, 100000);
            byte[] Key = PBKDF2.GetBytes(32);
            PBKDF2.Clear();

            string ActualString = Converters.ConvertBytesToHexString(Key, false);

            Assert.AreEqual(ExpectedString, ActualString);
        }
    }
}