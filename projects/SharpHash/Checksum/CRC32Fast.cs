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
using System;

namespace SharpHash.Checksum
{
    internal abstract class CRC32Fast : Hash, IChecksum, IBlockHash, IHash16, ITransformBlock
    {
        protected UInt32 CurrentCRC = 0;

        public CRC32Fast()
            : base(4, 1)
        { } // end constructor

        public override void Initialize()
        {
            CurrentCRC = 0;
        } // end function Initialize

        public override IHashResult TransformFinal()
        {
            IHashResult res = new HashResult(CurrentCRC);
            Initialize();

            return res;
        } // end function TransformFinal

        protected void LocalCRCCompute(UInt32[] a_CRCTable, byte[] a_data, Int32 a_index,
            Int32 a_length)
        {
            UInt32 LCRC, LA, LB, LC, LD;
            UInt32[] LCRCTable;

            LCRC = ~CurrentCRC; // LCRC = UInt32.MaxValue ^ FCurrentCRC;
            LCRCTable = a_CRCTable;
            while (a_length >= 16)
            {
                LA = LCRCTable[(3 * 256) + a_data[a_index + 12]] ^ LCRCTable
                    [(2 * 256) + a_data[a_index + 13]] ^ LCRCTable
                    [(1 * 256) + a_data[a_index + 14]] ^ LCRCTable
                    [(0 * 256) + a_data[a_index + 15]];

                LB = LCRCTable[(7 * 256) + a_data[a_index + 8]] ^ LCRCTable
                    [(6 * 256) + a_data[a_index + 9]] ^ LCRCTable
                    [(5 * 256) + a_data[a_index + 10]] ^ LCRCTable
                    [(4 * 256) + a_data[a_index + 11]];

                LC = LCRCTable[(11 * 256) + a_data[a_index + 4]] ^ LCRCTable
                    [(10 * 256) + a_data[a_index + 5]] ^ LCRCTable
                    [(9 * 256) + a_data[a_index + 6]] ^ LCRCTable
                    [(8 * 256) + a_data[a_index + 7]];

                LD = LCRCTable[(15 * 256) + ((LCRC & 0xFF) ^ a_data[a_index])] ^ LCRCTable
                    [(14 * 256) + (((LCRC >> 8) & 0xFF) ^ a_data[a_index + 1])] ^ LCRCTable
                    [(13 * 256) + (((LCRC >> 16) & 0xFF) ^ a_data[a_index + 2])] ^ LCRCTable
                    [(12 * 256) + ((LCRC >> 24) ^ a_data[a_index + 3])];

                LCRC = LD ^ LC ^ LB ^ LA;

                a_index += 16;
                a_length -= 16;
            } // end while

            a_length--;
            while (a_length >= 0)
            {
                LCRC = LCRCTable[(byte)(LCRC ^ a_data[a_index])] ^ (LCRC >> 8);
                a_index++;
                a_length--;
            } // end while

            CurrentCRC = ~LCRC; // CurrentCRC = LCRC ^ UInt32.MaxValue;
        } // end function LocalCRCCompute

        public static UInt32[] Init_CRC_Table(UInt32 a_polynomial)
        {
            Int32 LIdx, LJIdx, LKIdx;
            UInt32 LRes;

            UInt32[] res = new UInt32[16 * 256];

            for (LIdx = 0; LIdx < 256; LIdx++)
            {
                LRes = (UInt32)LIdx;
                for (LJIdx = 0; LJIdx < 16; LJIdx++)
                {
                    LKIdx = 0;
                    while (LKIdx < 8)
                    {
                        // faster branchless variant
                        LRes = (UInt32)((LRes >> 1) ^ (-(Int32)(LRes & 1) & a_polynomial));
                        res[(LJIdx * 256) + LIdx] = LRes;
                        LKIdx++;
                    } // end while
                } // end for
            } // end for

            return res;
        } // end function Init_CRC_Table

    } // end class CRC32Fast

    internal sealed class CRC32_PKZIP_Fast : CRC32Fast
    {
        public CRC32_PKZIP_Fast()
        {
            CRC32_PKZIP_Table = Init_CRC_Table(CRC32_PKZIP_Polynomial);
        } // end constructor

        // Polynomial Reversed
        private static readonly UInt32 CRC32_PKZIP_Polynomial = 0xEDB88320;

        private UInt32[] CRC32_PKZIP_Table = null;

        public override IHash Clone()
        {
            CRC32_PKZIP_Fast HashInstance = new CRC32_PKZIP_Fast();
            HashInstance.CurrentCRC = CurrentCRC;

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone

        public override void TransformBytes(byte[] a_data, Int32 a_index, Int32 a_length)
        {
            LocalCRCCompute(CRC32_PKZIP_Table, a_data, a_index, a_length);
        } // end function TransformBytes

    } // end class CRC32_PKZIP

    internal sealed class CRC32_CASTAGNOLI_Fast : CRC32Fast
    {
        // Polynomial Reversed
        private static readonly UInt32 CRC32_CASTAGNOLI_Polynomial = 0x82F63B78;

        private UInt32[] CRC32_CASTAGNOLI_Table = null;

        public CRC32_CASTAGNOLI_Fast()
        {
            CRC32_CASTAGNOLI_Table = Init_CRC_Table(CRC32_CASTAGNOLI_Polynomial);
        } // end constructor

        public override IHash Clone()
        {
            CRC32_CASTAGNOLI_Fast HashInstance = new CRC32_CASTAGNOLI_Fast();
            HashInstance.CurrentCRC = CurrentCRC;

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone

        public override void TransformBytes(byte[] a_data, Int32 a_index, Int32 a_length)
        {
            LocalCRCCompute(CRC32_CASTAGNOLI_Table, a_data, a_index, a_length);
        } // end function TransformBytes

    } // end class CRC32_CASTAGNOLI
}