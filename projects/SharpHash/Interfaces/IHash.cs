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
using System.IO;
using System.Text;

namespace SharpHash.Interfaces
{
    public interface IHash
    {
        string Name { get; }
        Int32 BlockSize { get; }
        Int32 HashSize { get; }
        Int32 BufferSize { get; set; }

        IHash Clone();

        IHashResult ComputeString(string a_data, Encoding encoding);

        IHashResult ComputeBytes(byte[] a_data);

        IHashResult ComputeUntyped(IntPtr a_data, Int64 a_length);

        IHashResult ComputeStream(Stream a_stream, Int64 a_length = -1);

        IHashResult ComputeFile(string a_file_name, Int64 a_from = 0, Int64 a_length = -1);

        void Initialize();

        void TransformBytes(byte[] a_data, Int32 a_index, Int32 a_length);

        void TransformBytes(byte[] a_data, Int32 a_index);

        void TransformBytes(byte[] a_data);

        void TransformUntyped(IntPtr a_data, Int64 a_length);

        IHashResult TransformFinal();

        void TransformString(string a_data, Encoding encoding);

        void TransformStream(Stream a_stream, Int64 a_length = -1);

        void TransformFile(string a_file_name, Int64 a_from = 0, Int64 a_length = -1);
    }
}