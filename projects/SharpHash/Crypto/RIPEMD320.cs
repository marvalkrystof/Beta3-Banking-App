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

using SharpHash.Interfaces;
using SharpHash.Utils;
using System;

namespace SharpHash.Crypto
{
    internal sealed class RIPEMD320 : MDBase, ITransformBlock
    {
        private UInt32[] data = null;

        public RIPEMD320()
            : base(10, 40)
        {
            data = new UInt32[16];
        } // end constructor

        public override IHash Clone()
        {
            RIPEMD320 HashInstance = new RIPEMD320();
            HashInstance.buffer = buffer.Clone();
            HashInstance.processed_bytes = processed_bytes;

            HashInstance.state = state.DeepCopy();

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone

        public override void Initialize()
        {
            state[4] = 0xC3D2E1F0;
            state[5] = 0x76543210;
            state[6] = 0xFEDCBA98;
            state[7] = 0x89ABCDEF;
            state[8] = 0x01234567;
            state[9] = 0x3C2D1E0F;

            base.Initialize();
        } // end function Initialize

        protected override unsafe void TransformBlock(IntPtr a_data,
                Int32 a_data_length, Int32 a_index)
        {
            UInt32 a, b, c, d, e, aa, bb, cc, dd, ee;

            fixed (UInt32* dPtr = data)
            {
                Converters.le32_copy(a_data, a_index, (IntPtr)dPtr, 0, 64);
            }

            a = state[0];
            b = state[1];
            c = state[2];
            d = state[3];
            e = state[4];
            aa = state[5];
            bb = state[6];
            cc = state[7];
            dd = state[8];
            ee = state[9];

            a = a + (data[0] + (b ^ c ^ d));
            a = Bits.RotateLeft32(a, 11) + e;
            c = Bits.RotateLeft32(c, 10);
            e = e + (data[1] + (a ^ b ^ c));
            e = Bits.RotateLeft32(e, 14) + d;
            b = Bits.RotateLeft32(b, 10);
            d = d + (data[2] + (e ^ a ^ b));
            d = Bits.RotateLeft32(d, 15) + c;
            a = Bits.RotateLeft32(a, 10);
            c = c + (data[3] + (d ^ e ^ a));
            c = Bits.RotateLeft32(c, 12) + b;
            e = Bits.RotateLeft32(e, 10);
            b = b + (data[4] + (c ^ d ^ e));
            b = Bits.RotateLeft32(b, 5) + a;
            d = Bits.RotateLeft32(d, 10);
            a = a + (data[5] + (b ^ c ^ d));
            a = Bits.RotateLeft32(a, 8) + e;
            c = Bits.RotateLeft32(c, 10);
            e = e + (data[6] + (a ^ b ^ c));
            e = Bits.RotateLeft32(e, 7) + d;
            b = Bits.RotateLeft32(b, 10);
            d = d + (data[7] + (e ^ a ^ b));
            d = Bits.RotateLeft32(d, 9) + c;
            a = Bits.RotateLeft32(a, 10);
            c = c + (data[8] + (d ^ e ^ a));
            c = Bits.RotateLeft32(c, 11) + b;
            e = Bits.RotateLeft32(e, 10);
            b = b + (data[9] + (c ^ d ^ e));
            b = Bits.RotateLeft32(b, 13) + a;
            d = Bits.RotateLeft32(d, 10);
            a = a + (data[10] + (b ^ c ^ d));
            a = Bits.RotateLeft32(a, 14) + e;
            c = Bits.RotateLeft32(c, 10);
            e = e + (data[11] + (a ^ b ^ c));
            e = Bits.RotateLeft32(e, 15) + d;
            b = Bits.RotateLeft32(b, 10);
            d = d + (data[12] + (e ^ a ^ b));
            d = Bits.RotateLeft32(d, 6) + c;
            a = Bits.RotateLeft32(a, 10);
            c = c + (data[13] + (d ^ e ^ a));
            c = Bits.RotateLeft32(c, 7) + b;
            e = Bits.RotateLeft32(e, 10);
            b = b + (data[14] + (c ^ d ^ e));
            b = Bits.RotateLeft32(b, 9) + a;
            d = Bits.RotateLeft32(d, 10);
            a = a + (data[15] + (b ^ c ^ d));
            a = Bits.RotateLeft32(a, 8) + e;
            c = Bits.RotateLeft32(c, 10);

            aa = aa + (data[5] + C1 + (bb ^ (cc | ~dd)));
            aa = Bits.RotateLeft32(aa, 8) + ee;
            cc = Bits.RotateLeft32(cc, 10);
            ee = ee + (data[14] + C1 + (aa ^ (bb | ~cc)));
            ee = Bits.RotateLeft32(ee, 9) + dd;
            bb = Bits.RotateLeft32(bb, 10);
            dd = dd + (data[7] + C1 + (ee ^ (aa | ~bb)));
            dd = Bits.RotateLeft32(dd, 9) + cc;
            aa = Bits.RotateLeft32(aa, 10);
            cc = cc + (data[0] + C1 + (dd ^ (ee | ~aa)));
            cc = Bits.RotateLeft32(cc, 11) + bb;
            ee = Bits.RotateLeft32(ee, 10);
            bb = bb + (data[9] + C1 + (cc ^ (dd | ~ee)));
            bb = Bits.RotateLeft32(bb, 13) + aa;
            dd = Bits.RotateLeft32(dd, 10);
            aa = aa + (data[2] + C1 + (bb ^ (cc | ~dd)));
            aa = Bits.RotateLeft32(aa, 15) + ee;
            cc = Bits.RotateLeft32(cc, 10);
            ee = ee + (data[11] + C1 + (aa ^ (bb | ~cc)));
            ee = Bits.RotateLeft32(ee, 15) + dd;
            bb = Bits.RotateLeft32(bb, 10);
            dd = dd + (data[4] + C1 + (ee ^ (aa | ~bb)));
            dd = Bits.RotateLeft32(dd, 5) + cc;
            aa = Bits.RotateLeft32(aa, 10);
            cc = cc + (data[13] + C1 + (dd ^ (ee | ~aa)));
            cc = Bits.RotateLeft32(cc, 7) + bb;
            ee = Bits.RotateLeft32(ee, 10);
            bb = bb + (data[6] + C1 + (cc ^ (dd | ~ee)));
            bb = Bits.RotateLeft32(bb, 7) + aa;
            dd = Bits.RotateLeft32(dd, 10);
            aa = aa + (data[15] + C1 + (bb ^ (cc | ~dd)));
            aa = Bits.RotateLeft32(aa, 8) + ee;
            cc = Bits.RotateLeft32(cc, 10);
            ee = ee + (data[8] + C1 + (aa ^ (bb | ~cc)));
            ee = Bits.RotateLeft32(ee, 11) + dd;
            bb = Bits.RotateLeft32(bb, 10);
            dd = dd + (data[1] + C1 + (ee ^ (aa | ~bb)));
            dd = Bits.RotateLeft32(dd, 14) + cc;
            aa = Bits.RotateLeft32(aa, 10);
            cc = cc + (data[10] + C1 + (dd ^ (ee | ~aa)));
            cc = Bits.RotateLeft32(cc, 14) + bb;
            ee = Bits.RotateLeft32(ee, 10);
            bb = bb + (data[3] + C1 + (cc ^ (dd | ~ee)));
            bb = Bits.RotateLeft32(bb, 12) + aa;
            dd = Bits.RotateLeft32(dd, 10);
            aa = aa + (data[12] + C1 + (bb ^ (cc | ~dd)));
            aa = Bits.RotateLeft32(aa, 6) + ee;
            cc = Bits.RotateLeft32(cc, 10);

            e = e + (data[7] + C2 + ((aa & b) | (~aa & c)));
            e = Bits.RotateLeft32(e, 7) + d;
            b = Bits.RotateLeft32(b, 10);
            d = d + (data[4] + C2 + ((e & aa) | (~e & b)));
            d = Bits.RotateLeft32(d, 6) + c;
            aa = Bits.RotateLeft32(aa, 10);
            c = c + (data[13] + C2 + ((d & e) | (~d & aa)));
            c = Bits.RotateLeft32(c, 8) + b;
            e = Bits.RotateLeft32(e, 10);
            b = b + (data[1] + C2 + ((c & d) | (~c & e)));
            b = Bits.RotateLeft32(b, 13) + aa;
            d = Bits.RotateLeft32(d, 10);
            aa = aa + (data[10] + C2 + ((b & c) | (~b & d)));
            aa = Bits.RotateLeft32(aa, 11) + e;
            c = Bits.RotateLeft32(c, 10);
            e = e + (data[6] + C2 + ((aa & b) | (~aa & c)));
            e = Bits.RotateLeft32(e, 9) + d;
            b = Bits.RotateLeft32(b, 10);
            d = d + (data[15] + C2 + ((e & aa) | (~e & b)));
            d = Bits.RotateLeft32(d, 7) + c;
            aa = Bits.RotateLeft32(aa, 10);
            c = c + (data[3] + C2 + ((d & e) | (~d & aa)));
            c = Bits.RotateLeft32(c, 15) + b;
            e = Bits.RotateLeft32(e, 10);
            b = b + (data[12] + C2 + ((c & d) | (~c & e)));
            b = Bits.RotateLeft32(b, 7) + aa;
            d = Bits.RotateLeft32(d, 10);
            aa = aa + (data[0] + C2 + ((b & c) | (~b & d)));
            aa = Bits.RotateLeft32(aa, 12) + e;
            c = Bits.RotateLeft32(c, 10);
            e = e + (data[9] + C2 + ((aa & b) | (~aa & c)));
            e = Bits.RotateLeft32(e, 15) + d;
            b = Bits.RotateLeft32(b, 10);
            d = d + (data[5] + C2 + ((e & aa) | (~e & b)));
            d = Bits.RotateLeft32(d, 9) + c;
            aa = Bits.RotateLeft32(aa, 10);
            c = c + (data[2] + C2 + ((d & e) | (~d & aa)));
            c = Bits.RotateLeft32(c, 11) + b;
            e = Bits.RotateLeft32(e, 10);
            b = b + (data[14] + C2 + ((c & d) | (~c & e)));
            b = Bits.RotateLeft32(b, 7) + aa;
            d = Bits.RotateLeft32(d, 10);
            aa = aa + (data[11] + C2 + ((b & c) | (~b & d)));
            aa = Bits.RotateLeft32(aa, 13) + e;
            c = Bits.RotateLeft32(c, 10);
            e = e + (data[8] + C2 + ((aa & b) | (~aa & c)));
            e = Bits.RotateLeft32(e, 12) + d;
            b = Bits.RotateLeft32(b, 10);

            ee = ee + (data[6] + C3 + ((a & cc) | (bb & ~cc)));
            ee = Bits.RotateLeft32(ee, 9) + dd;
            bb = Bits.RotateLeft32(bb, 10);
            dd = dd + (data[11] + C3 + ((ee & bb) | (a & ~bb)));
            dd = Bits.RotateLeft32(dd, 13) + cc;
            a = Bits.RotateLeft32(a, 10);
            cc = cc + (data[3] + C3 + ((dd & a) | (ee & ~a)));
            cc = Bits.RotateLeft32(cc, 15) + bb;
            ee = Bits.RotateLeft32(ee, 10);
            bb = bb + (data[7] + C3 + ((cc & ee) | (dd & ~ee)));
            bb = Bits.RotateLeft32(bb, 7) + a;
            dd = Bits.RotateLeft32(dd, 10);
            a = a + (data[0] + C3 + ((bb & dd) | (cc & ~dd)));
            a = Bits.RotateLeft32(a, 12) + ee;
            cc = Bits.RotateLeft32(cc, 10);
            ee = ee + (data[13] + C3 + ((a & cc) | (bb & ~cc)));
            ee = Bits.RotateLeft32(ee, 8) + dd;
            bb = Bits.RotateLeft32(bb, 10);
            dd = dd + (data[5] + C3 + ((ee & bb) | (a & ~bb)));
            dd = Bits.RotateLeft32(dd, 9) + cc;
            a = Bits.RotateLeft32(a, 10);
            cc = cc + (data[10] + C3 + ((dd & a) | (ee & ~a)));
            cc = Bits.RotateLeft32(cc, 11) + bb;
            ee = Bits.RotateLeft32(ee, 10);
            bb = bb + (data[14] + C3 + ((cc & ee) | (dd & ~ee)));
            bb = Bits.RotateLeft32(bb, 7) + a;
            dd = Bits.RotateLeft32(dd, 10);
            a = a + (data[15] + C3 + ((bb & dd) | (cc & ~dd)));
            a = Bits.RotateLeft32(a, 7) + ee;
            cc = Bits.RotateLeft32(cc, 10);
            ee = ee + (data[8] + C3 + ((a & cc) | (bb & ~cc)));
            ee = Bits.RotateLeft32(ee, 12) + dd;
            bb = Bits.RotateLeft32(bb, 10);
            dd = dd + (data[12] + C3 + ((ee & bb) | (a & ~bb)));
            dd = Bits.RotateLeft32(dd, 7) + cc;
            a = Bits.RotateLeft32(a, 10);
            cc = cc + (data[4] + C3 + ((dd & a) | (ee & ~a)));
            cc = Bits.RotateLeft32(cc, 6) + bb;
            ee = Bits.RotateLeft32(ee, 10);
            bb = bb + (data[9] + C3 + ((cc & ee) | (dd & ~ee)));
            bb = Bits.RotateLeft32(bb, 15) + a;
            dd = Bits.RotateLeft32(dd, 10);
            a = a + (data[1] + C3 + ((bb & dd) | (cc & ~dd)));
            a = Bits.RotateLeft32(a, 13) + ee;
            cc = Bits.RotateLeft32(cc, 10);
            ee = ee + (data[2] + C3 + ((a & cc) | (bb & ~cc)));
            ee = Bits.RotateLeft32(ee, 11) + dd;
            bb = Bits.RotateLeft32(bb, 10);

            d = d + (data[3] + C4 + ((e | ~aa) ^ bb));
            d = Bits.RotateLeft32(d, 11) + c;
            aa = Bits.RotateLeft32(aa, 10);
            c = c + (data[10] + C4 + ((d | ~e) ^ aa));
            c = Bits.RotateLeft32(c, 13) + bb;
            e = Bits.RotateLeft32(e, 10);
            bb = bb + (data[14] + C4 + ((c | ~d) ^ e));
            bb = Bits.RotateLeft32(bb, 6) + aa;
            d = Bits.RotateLeft32(d, 10);
            aa = aa + (data[4] + C4 + ((bb | ~c) ^ d));
            aa = Bits.RotateLeft32(aa, 7) + e;
            c = Bits.RotateLeft32(c, 10);
            e = e + (data[9] + C4 + ((aa | ~bb) ^ c));
            e = Bits.RotateLeft32(e, 14) + d;
            bb = Bits.RotateLeft32(bb, 10);
            d = d + (data[15] + C4 + ((e | ~aa) ^ bb));
            d = Bits.RotateLeft32(d, 9) + c;
            aa = Bits.RotateLeft32(aa, 10);
            c = c + (data[8] + C4 + ((d | ~e) ^ aa));
            c = Bits.RotateLeft32(c, 13) + bb;
            e = Bits.RotateLeft32(e, 10);
            bb = bb + (data[1] + C4 + ((c | ~d) ^ e));
            bb = Bits.RotateLeft32(bb, 15) + aa;
            d = Bits.RotateLeft32(d, 10);
            aa = aa + (data[2] + C4 + ((bb | ~c) ^ d));
            aa = Bits.RotateLeft32(aa, 14) + e;
            c = Bits.RotateLeft32(c, 10);
            e = e + (data[7] + C4 + ((aa | ~bb) ^ c));
            e = Bits.RotateLeft32(e, 8) + d;
            bb = Bits.RotateLeft32(bb, 10);
            d = d + (data[0] + C4 + ((e | ~aa) ^ bb));
            d = Bits.RotateLeft32(d, 13) + c;
            aa = Bits.RotateLeft32(aa, 10);
            c = c + (data[6] + C4 + ((d | ~e) ^ aa));
            c = Bits.RotateLeft32(c, 6) + bb;
            e = Bits.RotateLeft32(e, 10);
            bb = bb + (data[13] + C4 + ((c | ~d) ^ e));
            bb = Bits.RotateLeft32(bb, 5) + aa;
            d = Bits.RotateLeft32(d, 10);
            aa = aa + (data[11] + C4 + ((bb | ~c) ^ d));
            aa = Bits.RotateLeft32(aa, 12) + e;
            c = Bits.RotateLeft32(c, 10);
            e = e + (data[5] + C4 + ((aa | ~bb) ^ c));
            e = Bits.RotateLeft32(e, 7) + d;
            bb = Bits.RotateLeft32(bb, 10);
            d = d + (data[12] + C4 + ((e | ~aa) ^ bb));
            d = Bits.RotateLeft32(d, 5) + c;
            aa = Bits.RotateLeft32(aa, 10);

            dd = dd + (data[15] + C5 + ((ee | ~a) ^ b));
            dd = Bits.RotateLeft32(dd, 9) + cc;
            a = Bits.RotateLeft32(a, 10);
            cc = cc + (data[5] + C5 + ((dd | ~ee) ^ a));
            cc = Bits.RotateLeft32(cc, 7) + b;
            ee = Bits.RotateLeft32(ee, 10);
            b = b + (data[1] + C5 + ((cc | ~dd) ^ ee));
            b = Bits.RotateLeft32(b, 15) + a;
            dd = Bits.RotateLeft32(dd, 10);
            a = a + (data[3] + C5 + ((b | ~cc) ^ dd));
            a = Bits.RotateLeft32(a, 11) + ee;
            cc = Bits.RotateLeft32(cc, 10);
            ee = ee + (data[7] + C5 + ((a | ~b) ^ cc));
            ee = Bits.RotateLeft32(ee, 8) + dd;
            b = Bits.RotateLeft32(b, 10);
            dd = dd + (data[14] + C5 + ((ee | ~a) ^ b));
            dd = Bits.RotateLeft32(dd, 6) + cc;
            a = Bits.RotateLeft32(a, 10);
            cc = cc + (data[6] + C5 + ((dd | ~ee) ^ a));
            cc = Bits.RotateLeft32(cc, 6) + b;
            ee = Bits.RotateLeft32(ee, 10);
            b = b + (data[9] + C5 + ((cc | ~dd) ^ ee));
            b = Bits.RotateLeft32(b, 14) + a;
            dd = Bits.RotateLeft32(dd, 10);
            a = a + (data[11] + C5 + ((b | ~cc) ^ dd));
            a = Bits.RotateLeft32(a, 12) + ee;
            cc = Bits.RotateLeft32(cc, 10);
            ee = ee + (data[8] + C5 + ((a | ~b) ^ cc));
            ee = Bits.RotateLeft32(ee, 13) + dd;
            b = Bits.RotateLeft32(b, 10);
            dd = dd + (data[12] + C5 + ((ee | ~a) ^ b));
            dd = Bits.RotateLeft32(dd, 5) + cc;
            a = Bits.RotateLeft32(a, 10);
            cc = cc + (data[2] + C5 + ((dd | ~ee) ^ a));
            cc = Bits.RotateLeft32(cc, 14) + b;
            ee = Bits.RotateLeft32(ee, 10);
            b = b + (data[10] + C5 + ((cc | ~dd) ^ ee));
            b = Bits.RotateLeft32(b, 13) + a;
            dd = Bits.RotateLeft32(dd, 10);
            a = a + (data[0] + C5 + ((b | ~cc) ^ dd));
            a = Bits.RotateLeft32(a, 13) + ee;
            cc = Bits.RotateLeft32(cc, 10);
            ee = ee + (data[4] + C5 + ((a | ~b) ^ cc));
            ee = Bits.RotateLeft32(ee, 7) + dd;
            b = Bits.RotateLeft32(b, 10);
            dd = dd + (data[13] + C5 + ((ee | ~a) ^ b));
            dd = Bits.RotateLeft32(dd, 5) + cc;
            a = Bits.RotateLeft32(a, 10);

            cc = cc + (data[1] + C6 + ((d & aa) | (e & ~aa)));
            cc = Bits.RotateLeft32(cc, 11) + bb;
            e = Bits.RotateLeft32(e, 10);
            bb = bb + (data[9] + C6 + ((cc & e) | (d & ~e)));
            bb = Bits.RotateLeft32(bb, 12) + aa;
            d = Bits.RotateLeft32(d, 10);
            aa = aa + (data[11] + C6 + ((bb & d) | (cc & ~d)));
            aa = Bits.RotateLeft32(aa, 14) + e;
            cc = Bits.RotateLeft32(cc, 10);
            e = e + (data[10] + C6 + ((aa & cc) | (bb & ~cc)));
            e = Bits.RotateLeft32(e, 15) + d;
            bb = Bits.RotateLeft32(bb, 10);
            d = d + (data[0] + C6 + ((e & bb) | (aa & ~bb)));
            d = Bits.RotateLeft32(d, 14) + cc;
            aa = Bits.RotateLeft32(aa, 10);
            cc = cc + (data[8] + C6 + ((d & aa) | (e & ~aa)));
            cc = Bits.RotateLeft32(cc, 15) + bb;
            e = Bits.RotateLeft32(e, 10);
            bb = bb + (data[12] + C6 + ((cc & e) | (d & ~e)));
            bb = Bits.RotateLeft32(bb, 9) + aa;
            d = Bits.RotateLeft32(d, 10);
            aa = aa + (data[4] + C6 + ((bb & d) | (cc & ~d)));
            aa = Bits.RotateLeft32(aa, 8) + e;
            cc = Bits.RotateLeft32(cc, 10);
            e = e + (data[13] + C6 + ((aa & cc) | (bb & ~cc)));
            e = Bits.RotateLeft32(e, 9) + d;
            bb = Bits.RotateLeft32(bb, 10);
            d = d + (data[3] + C6 + ((e & bb) | (aa & ~bb)));
            d = Bits.RotateLeft32(d, 14) + cc;
            aa = Bits.RotateLeft32(aa, 10);
            cc = cc + (data[7] + C6 + ((d & aa) | (e & ~aa)));
            cc = Bits.RotateLeft32(cc, 5) + bb;
            e = Bits.RotateLeft32(e, 10);
            bb = bb + (data[15] + C6 + ((cc & e) | (d & ~e)));
            bb = Bits.RotateLeft32(bb, 6) + aa;
            d = Bits.RotateLeft32(d, 10);
            aa = aa + (data[14] + C6 + ((bb & d) | (cc & ~d)));
            aa = Bits.RotateLeft32(aa, 8) + e;
            cc = Bits.RotateLeft32(cc, 10);
            e = e + (data[5] + C6 + ((aa & cc) | (bb & ~cc)));
            e = Bits.RotateLeft32(e, 6) + d;
            bb = Bits.RotateLeft32(bb, 10);
            d = d + (data[6] + C6 + ((e & bb) | (aa & ~bb)));
            d = Bits.RotateLeft32(d, 5) + cc;
            aa = Bits.RotateLeft32(aa, 10);
            cc = cc + (data[2] + C6 + ((d & aa) | (e & ~aa)));
            cc = Bits.RotateLeft32(cc, 12) + bb;
            e = Bits.RotateLeft32(e, 10);

            c = c + (data[8] + C7 + ((dd & ee) | (~dd & a)));
            c = Bits.RotateLeft32(c, 15) + b;
            ee = Bits.RotateLeft32(ee, 10);
            b = b + (data[6] + C7 + ((c & dd) | (~c & ee)));
            b = Bits.RotateLeft32(b, 5) + a;
            dd = Bits.RotateLeft32(dd, 10);
            a = a + (data[4] + C7 + ((b & c) | (~b & dd)));
            a = Bits.RotateLeft32(a, 8) + ee;
            c = Bits.RotateLeft32(c, 10);
            ee = ee + (data[1] + C7 + ((a & b) | (~a & c)));
            ee = Bits.RotateLeft32(ee, 11) + dd;
            b = Bits.RotateLeft32(b, 10);
            dd = dd + (data[3] + C7 + ((ee & a) | (~ee & b)));
            dd = Bits.RotateLeft32(dd, 14) + c;
            a = Bits.RotateLeft32(a, 10);
            c = c + (data[11] + C7 + ((dd & ee) | (~dd & a)));
            c = Bits.RotateLeft32(c, 14) + b;
            ee = Bits.RotateLeft32(ee, 10);
            b = b + (data[15] + C7 + ((c & dd) | (~c & ee)));
            b = Bits.RotateLeft32(b, 6) + a;
            dd = Bits.RotateLeft32(dd, 10);
            a = a + (data[0] + C7 + ((b & c) | (~b & dd)));
            a = Bits.RotateLeft32(a, 14) + ee;
            c = Bits.RotateLeft32(c, 10);
            ee = ee + (data[5] + C7 + ((a & b) | (~a & c)));
            ee = Bits.RotateLeft32(ee, 6) + dd;
            b = Bits.RotateLeft32(b, 10);
            dd = dd + (data[12] + C7 + ((ee & a) | (~ee & b)));
            dd = Bits.RotateLeft32(dd, 9) + c;
            a = Bits.RotateLeft32(a, 10);
            c = c + (data[2] + C7 + ((dd & ee) | (~dd & a)));
            c = Bits.RotateLeft32(c, 12) + b;
            ee = Bits.RotateLeft32(ee, 10);
            b = b + (data[13] + C7 + ((c & dd) | (~c & ee)));
            b = Bits.RotateLeft32(b, 9) + a;
            dd = Bits.RotateLeft32(dd, 10);
            a = a + (data[9] + C7 + ((b & c) | (~b & dd)));
            a = Bits.RotateLeft32(a, 12) + ee;
            c = Bits.RotateLeft32(c, 10);
            ee = ee + (data[7] + C7 + ((a & b) | (~a & c)));
            ee = Bits.RotateLeft32(ee, 5) + dd;
            b = Bits.RotateLeft32(b, 10);
            dd = dd + (data[10] + C7 + ((ee & a) | (~ee & b)));
            dd = Bits.RotateLeft32(dd, 15) + c;
            a = Bits.RotateLeft32(a, 10);
            c = c + (data[14] + C7 + ((dd & ee) | (~dd & a)));
            c = Bits.RotateLeft32(c, 8) + b;
            ee = Bits.RotateLeft32(ee, 10);

            bb = bb + (data[4] + C8 + (cc ^ (dd | ~e)));
            bb = Bits.RotateLeft32(bb, 9) + aa;
            dd = Bits.RotateLeft32(dd, 10);
            aa = aa + (data[0] + C8 + (bb ^ (cc | ~dd)));
            aa = Bits.RotateLeft32(aa, 15) + e;
            cc = Bits.RotateLeft32(cc, 10);
            e = e + (data[5] + C8 + (aa ^ (bb | ~cc)));
            e = Bits.RotateLeft32(e, 5) + dd;
            bb = Bits.RotateLeft32(bb, 10);
            dd = dd + (data[9] + C8 + (e ^ (aa | ~bb)));
            dd = Bits.RotateLeft32(dd, 11) + cc;
            aa = Bits.RotateLeft32(aa, 10);
            cc = cc + (data[7] + C8 + (dd ^ (e | ~aa)));
            cc = Bits.RotateLeft32(cc, 6) + bb;
            e = Bits.RotateLeft32(e, 10);
            bb = bb + (data[12] + C8 + (cc ^ (dd | ~e)));
            bb = Bits.RotateLeft32(bb, 8) + aa;
            dd = Bits.RotateLeft32(dd, 10);
            aa = aa + (data[2] + C8 + (bb ^ (cc | ~dd)));
            aa = Bits.RotateLeft32(aa, 13) + e;
            cc = Bits.RotateLeft32(cc, 10);
            e = e + (data[10] + C8 + (aa ^ (bb | ~cc)));
            e = Bits.RotateLeft32(e, 12) + dd;
            bb = Bits.RotateLeft32(bb, 10);
            dd = dd + (data[14] + C8 + (e ^ (aa | ~bb)));
            dd = Bits.RotateLeft32(dd, 5) + cc;
            aa = Bits.RotateLeft32(aa, 10);
            cc = cc + (data[1] + C8 + (dd ^ (e | ~aa)));
            cc = Bits.RotateLeft32(cc, 12) + bb;
            e = Bits.RotateLeft32(e, 10);
            bb = bb + (data[3] + C8 + (cc ^ (dd | ~e)));
            bb = Bits.RotateLeft32(bb, 13) + aa;
            dd = Bits.RotateLeft32(dd, 10);
            aa = aa + (data[8] + C8 + (bb ^ (cc | ~dd)));
            aa = Bits.RotateLeft32(aa, 14) + e;
            cc = Bits.RotateLeft32(cc, 10);
            e = e + (data[11] + C8 + (aa ^ (bb | ~cc)));
            e = Bits.RotateLeft32(e, 11) + dd;
            bb = Bits.RotateLeft32(bb, 10);
            dd = dd + (data[6] + C8 + (e ^ (aa | ~bb)));
            dd = Bits.RotateLeft32(dd, 8) + cc;
            aa = Bits.RotateLeft32(aa, 10);
            cc = cc + (data[15] + C8 + (dd ^ (e | ~aa)));
            cc = Bits.RotateLeft32(cc, 5) + bb;
            e = Bits.RotateLeft32(e, 10);
            bb = bb + (data[13] + C8 + (cc ^ (dd | ~e)));
            bb = Bits.RotateLeft32(bb, 6) + aa;
            dd = Bits.RotateLeft32(dd, 10);

            b = b + (data[12] + (c ^ d ^ ee));
            b = Bits.RotateLeft32(b, 8) + a;
            d = Bits.RotateLeft32(d, 10);
            a = a + (data[15] + (b ^ c ^ d));
            a = Bits.RotateLeft32(a, 5) + ee;
            c = Bits.RotateLeft32(c, 10);
            ee = ee + (data[10] + (a ^ b ^ c));
            ee = Bits.RotateLeft32(ee, 12) + d;
            b = Bits.RotateLeft32(b, 10);
            d = d + (data[4] + (ee ^ a ^ b));
            d = Bits.RotateLeft32(d, 9) + c;
            a = Bits.RotateLeft32(a, 10);
            c = c + (data[1] + (d ^ ee ^ a));
            c = Bits.RotateLeft32(c, 12) + b;
            ee = Bits.RotateLeft32(ee, 10);
            b = b + (data[5] + (c ^ d ^ ee));
            b = Bits.RotateLeft32(b, 5) + a;
            d = Bits.RotateLeft32(d, 10);
            a = a + (data[8] + (b ^ c ^ d));
            a = Bits.RotateLeft32(a, 14) + ee;
            c = Bits.RotateLeft32(c, 10);
            ee = ee + (data[7] + (a ^ b ^ c));
            ee = Bits.RotateLeft32(ee, 6) + d;
            b = Bits.RotateLeft32(b, 10);
            d = d + (data[6] + (ee ^ a ^ b));
            d = Bits.RotateLeft32(d, 8) + c;
            a = Bits.RotateLeft32(a, 10);
            c = c + (data[2] + (d ^ ee ^ a));
            c = Bits.RotateLeft32(c, 13) + b;
            ee = Bits.RotateLeft32(ee, 10);
            b = b + (data[13] + (c ^ d ^ ee));
            b = Bits.RotateLeft32(b, 6) + a;
            d = Bits.RotateLeft32(d, 10);
            a = a + (data[14] + (b ^ c ^ d));
            a = Bits.RotateLeft32(a, 5) + ee;
            c = Bits.RotateLeft32(c, 10);
            ee = ee + (data[0] + (a ^ b ^ c));
            ee = Bits.RotateLeft32(ee, 15) + d;
            b = Bits.RotateLeft32(b, 10);
            d = d + (data[3] + (ee ^ a ^ b));
            d = Bits.RotateLeft32(d, 13) + c;
            a = Bits.RotateLeft32(a, 10);
            c = c + (data[9] + (d ^ ee ^ a));
            c = Bits.RotateLeft32(c, 11) + b;
            ee = Bits.RotateLeft32(ee, 10);
            b = b + (data[11] + (c ^ d ^ ee));
            b = Bits.RotateLeft32(b, 11) + a;
            d = Bits.RotateLeft32(d, 10);

            state[0] = state[0] + aa;
            state[1] = state[1] + bb;
            state[2] = state[2] + cc;
            state[3] = state[3] + dd;
            state[4] = state[4] + ee;
            state[5] = state[5] + a;
            state[6] = state[6] + b;
            state[7] = state[7] + c;
            state[8] = state[8] + d;
            state[9] = state[9] + e;

            Utils.Utils.Memset(ref data, 0);
        } // end function TransformBlock
    } // end class RIPEMD320
}