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

using SharpHash.Base;
using SharpHash.Interfaces;
using SharpHash.Utils;
using System;

namespace SharpHash.KDF
{
    internal class PBKDF2_HMACNotBuildInAdapter : KDFNotBuiltIn, IPBKDF2_HMACNotBuiltIn
    {
        private IHMACNotBuiltIn hmacNotBuiltIn;
        private byte[] Password = null, Salt = null, buffer = null;
        private UInt32 IterationCount, Block;
        private Int32 BlockSize, startIndex, endIndex;

        public static readonly string InvalidArgument = "\"bc (ByteCount)\" Argument must be a value greater than zero.";
        public static readonly string InvalidIndex = "Invalid start or end index in the internal buffer.";
        public static readonly string UninitializedInstance = "\"IHash\" instance is uninitialized.";
        public static readonly string EmptyPassword = "Password can't be empty.";
        public static readonly string EmptySalt = "Salt can't be empty.";
        public static readonly string IterationTooSmall = "Iteration must be greater than zero.";

        private PBKDF2_HMACNotBuildInAdapter()
        { } // end cctr

        internal PBKDF2_HMACNotBuildInAdapter(IHash a_underlyingHash, byte[] a_password,
            byte[] a_salt, UInt32 a_iterations)
        {
            if (a_password == null) throw new ArgumentNullHashLibException(EmptyPassword);
            if (a_salt == null) throw new ArgumentNullHashLibException(EmptySalt);
            if (a_iterations <= 0) throw new ArgumentOutOfRangeHashLibException(IterationTooSmall);

            var hash = a_underlyingHash?.Clone() ?? throw new ArgumentNullHashLibException(UninitializedInstance);
            hmacNotBuiltIn = HMACNotBuildInAdapter.CreateHMAC(hash, a_password);

            BlockSize = hmacNotBuiltIn.HashSize;
            buffer = new byte[BlockSize];

            // Copy Password
            Password = a_password.DeepCopy();

            // Copy Salt
            Salt = a_salt.DeepCopy();

            IterationCount = a_iterations;

            Initialize();
        } // end constructor

        public override void Clear()
        {
            ArrayUtils.ZeroFill(ref Password);
            ArrayUtils.ZeroFill(ref Salt);
        } // end function Clear

        public override byte[] GetBytes(Int32 bc)
        {
            Int32 LOffset, LSize, LRemainder, LRemCount;
            byte[] LKey, LT_Block = null;

            if (bc <= 0)
                throw new ArgumentOutOfRangeHashLibException(InvalidArgument);

            LKey = new byte[bc];

            LOffset = 0;
            LSize = endIndex - startIndex;
            if (LSize > 0)
            {
                if (bc >= LSize)
                {
                    Utils.Utils.Memmove(ref LKey, buffer, LSize, startIndex);
                    startIndex = 0;
                    endIndex = 0;
                    LOffset = LOffset + LSize;
                } // end if
                else
                {
                    Utils.Utils.Memmove(ref LKey, buffer, bc, startIndex);
                    startIndex = startIndex + bc;
                    return LKey;
                } // end else
            } // end if

            if (startIndex != 0 && endIndex != 0)
                throw new ArgumentHashLibException(InvalidIndex);

            while (LOffset < bc)
            {
                LT_Block = Func();
                LRemainder = bc - LOffset;
                if (LRemainder > BlockSize)
                {
                    Utils.Utils.Memmove(ref LKey, LT_Block, BlockSize, 0, LOffset);
                    LOffset = LOffset + BlockSize;
                } // end if
                else
                {
                    if (LRemainder > 0)
                        Utils.Utils.Memmove(ref LKey, LT_Block, LRemainder, 0, LOffset);

                    LRemCount = BlockSize - LRemainder;
                    if (LRemCount > 0)
                        Utils.Utils.Memmove(ref buffer, LT_Block, LRemCount,
                            LRemainder, startIndex);

                    endIndex = endIndex + LRemCount;

                    Initialize();

                    return LKey;
                } // end else
            } // end while

            Initialize();

            return LKey;
        } // end function GetBytes

        public override string Name => $"{GetType().Name}({hmacNotBuiltIn.Name})";

        public override string ToString() => Name;

        public override IKDFNotBuiltIn Clone()
        {
            return new PBKDF2_HMACNotBuildInAdapter
            {
                hmacNotBuiltIn = (IHMACNotBuiltIn)hmacNotBuiltIn.Clone(),
                Password = Password.DeepCopy(),
                Salt = Salt.DeepCopy(),
                buffer = buffer.DeepCopy(),
                IterationCount = IterationCount,
                Block = Block,
                BlockSize = BlockSize,
                startIndex = startIndex,
                endIndex = endIndex
            };
        } // end function Clone

        // initializes the state of the operation.
        private void Initialize()
        {
            ArrayUtils.ZeroFill(ref buffer);

            Block = 1;
            startIndex = 0;
            endIndex = 0;
        } // end function Initialize

        // iterative hash function
        private byte[] Func()
        {
            byte[] INT_block = GetBigEndianBytes(Block);
            hmacNotBuiltIn.Initialize();

            hmacNotBuiltIn.TransformBytes(Salt, 0, Salt.Length);
            hmacNotBuiltIn.TransformBytes(INT_block, 0, INT_block.Length);

            byte[] temp = hmacNotBuiltIn.TransformFinal().GetBytes();
            byte[] result = temp.DeepCopy();

            UInt32 i = 2;
            Int32 j = 0;
            while (i <= IterationCount)
            {
                temp = hmacNotBuiltIn.ComputeBytes(temp).GetBytes();
                j = 0;
                while (j < BlockSize)
                {
                    result[j] = (byte)(result[j] ^ temp[j]);
                    j++;
                } // end while

                i++;
            } // end while

            Block++;

            return result;
        } // end function Func

        /// <summary>
        /// Encodes an integer into a 4-byte array, in big endian.
        /// </summary>
        /// <param name="i">The integer to encode.</param>
        /// <returns>array of bytes, in big endian.</returns>
        private static byte[] GetBigEndianBytes(UInt32 i)
        {
            byte[] result = new byte[sizeof(UInt32)];
            Converters.ReadUInt32AsBytesBE(i, ref result, 0);
            return result;
        } // end function GetBigEndianBytes

    }
}