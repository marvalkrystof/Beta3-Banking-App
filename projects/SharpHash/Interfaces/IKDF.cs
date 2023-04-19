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

using System;

namespace SharpHash.Interfaces
{
    public interface IKDF
    {
        string Name { get; }

        string ToString();

        void Clear();

        /// <summary>
        /// Returns the pseudo-random bytes for this object.
        /// </summary>
        /// <param name="bc">The number of pseudo-random key bytes to generate.</param>
        /// <returns>A byte array filled with pseudo-random key bytes.</returns>
        /// <exception cref="ArgumentOutOfRangeHashLibException">bc must be greater than zero.</exception>
        /// <exception cref="ArgumentHashLibException">invalid start index or end index of internal buffer.</exception>
        byte[] GetBytes(Int32 bc);
    } // end interface IKDF

    public interface IKDFNotBuiltIn : IKDF
    {
        IKDFNotBuiltIn Clone();
    } // end interface IKDFNotBuiltIn

}