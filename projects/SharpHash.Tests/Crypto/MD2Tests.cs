///////////////////////////////////////////////////////////////////////
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

namespace SharpHash.Crypto.Tests
{
    [TestClass]
    public class MD2Tests : CryptoHashBaseTests
    {
        public MD2Tests()
        {
            hash = HashFactory.Crypto.CreateMD2();

            ExpectedHashOfEmptyData = "8350E5A3E24C153DF2275C9F80692773";
            ExpectedHashOfDefaultData = "DFBE28FF5A3C23CAA85BE5848F16524E";
            ExpectedHashOfOnetoNine = "12BD4EFDD922B5C8C7B773F26EF4E35F";
            ExpectedHashOfabcde = "DFF9959487649F5C7AF5D0680A9A5D22";
            ExpectedHashOfDefaultDataWithHMACWithLongKey = "03D7546FEADF29A91CEB40290A27E081";
            ExpectedHashOfDefaultDataWithHMACWithShortKey = "C5F4625462CD5CF7723C19E8566F6790";
        }
    }
}