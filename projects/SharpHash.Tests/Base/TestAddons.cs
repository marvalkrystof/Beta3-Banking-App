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
using SharpHash.Interfaces;
using SharpHash.Utils;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharpHash.Tests
{
    public static class TestConstants
    {
        public static readonly Int32[] chunkSize = new Int32[] {
            1, // Test many chunk of < sizeof(int)
			2, // Test many chunk of < sizeof(int)
			3, // Test many chunk of < sizeof(int)
			4, // Test many chunk of = sizeof(int)
			5, // Test many chunk of > sizeof(int)
			6, // Test many chunk of > sizeof(int)
			7, // Test many chunk of > sizeof(int)
			8, // Test many chunk of > 2*sizeof(int)
			9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27,
            28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45,
            46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63,
            64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81,
            82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99,
            100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114,
            115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129,
            130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144,
            145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159,
            160, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174,
            175, 176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189,
            190, 191, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204,
            205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219,
            220, 221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234,
            235, 236, 237, 238, 239, 240, 241, 242, 243, 244, 245, 246, 247, 248, 249,
            250, 251, 252, 253, 254, 255, 256, 257, 258, 259, 260 };
        //, 261, 262, 263, 264,
        // 265, 266, 267, 268, 269, 270, 271, 272, 273, 274, 275, 276, 277, 278, 279,
        // 280, 281, 282, 283, 284, 285, 286, 287, 288, 289, 290, 291, 292, 293, 294,
        // 295, 296, 297, 298, 299, 300, 301, 302, 303, 304, 305, 306, 307, 308, 309,
        // 310, 311, 312, 313, 314, 315, 316, 317, 318, 319, 320, 321, 322, 323, 324,
        // 325, 326, 327, 328, 329, 330, 331, 332, 333, 334, 335, 336, 337, 338, 339,
        // 340, 341, 342, 343, 344, 345, 346, 347, 348, 349, 350, 351, 352, 353, 354,
        // 355, 356, 357, 358, 359, 360, 361, 362, 363, 364, 365, 366, 367, 368, 369,
        // 370, 371, 372, 373, 374, 375, 376, 377, 378, 379, 380, 381, 382, 383, 384,
        // 385, 386, 387, 388, 389, 390, 391, 392, 393, 394, 395, 396, 397, 398, 399,
        // 400, 401, 402, 403, 404, 405, 406, 407, 408, 409, 410, 411, 412, 413, 414,
        // 415, 416, 417, 418, 419, 420, 421, 422, 423, 424, 425, 426, 427, 428, 429,
        // 430, 431, 432, 433, 434, 435, 436, 437, 438, 439, 440, 441, 442, 443, 444,
        // 445, 446, 447, 448, 449, 450, 451, 452, 453, 454, 455, 456, 457, 458, 459,
        // 460, 461, 462, 463, 464, 465, 466, 467, 468, 469, 470, 471, 472, 473, 474,
        // 475, 476, 477, 478, 479, 480, 481, 482, 483, 484, 485, 486, 487, 488, 489,
        // 490, 491, 492, 493, 494, 495, 496, 497, 498, 499, 500, 501, 502, 503, 504,
        // 505, 506, 507, 508, 509, 510, 511, 512, 513, 514, 515, 516, 517, 518, 519,
        // 520, 521, 522, 523, 524, 525, 526, 527, 528, 529, 530, 531, 532, 533, 534,
        // 535, 536, 537, 538, 539, 540, 541, 542, 543, 544, 545, 546, 547, 548, 549,
        // 550, 551, 552, 553, 554, 555, 556, 557, 558, 559, 560, 561, 562, 563, 564,
        // 565, 566, 567, 568, 569, 570, 571, 572, 573, 574, 575, 576, 577, 578, 579,
        // 580, 581, 582, 583, 584, 585, 586, 587, 588, 589, 590, 591, 592, 593, 594,
        // 595, 596, 597, 598, 599, 600, 601, 602, 603, 604, 605, 606, 607, 608, 609,
        // 610, 611, 612, 613, 614, 615, 616, 617, 618, 619, 620, 621, 622, 623, 624,
        // 625, 626, 627, 628, 629, 630, 631, 632, 633, 634, 635, 636, 637, 638, 639,
        // 640, 641, 642, 643, 644, 645, 646, 647, 648, 649, 650, 651, 652, 653, 654,
        // 655, 656, 657, 658, 659, 660, 661, 662, 663, 664, 665, 666, 667, 668, 669,
        // 670, 671, 672, 673, 674, 675, 676, 677, 678, 679, 680, 681, 682, 683, 684,
        // 685, 686, 687, 688, 689, 690, 691, 692, 693, 694, 695, 696, 697, 698, 699,
        // 700, 701, 702, 703, 704, 705, 706, 707, 708, 709, 710, 711, 712, 713, 714,
        // 715, 716, 717, 718, 719, 720, 721, 722, 723, 724, 725, 726, 727, 728, 729,
        // 730, 731, 732, 733, 734, 735, 736, 737, 738, 739, 740, 741, 742, 743, 744,
        // 745, 746, 747, 748, 749, 750, 751, 752, 753, 754, 755, 756, 757, 758, 759,
        // 760, 761, 762, 763, 764, 765, 766, 767, 768, 769, 770, 771, 772, 773, 774,
        // 775, 776, 777, 778, 779, 780, 781, 782, 783, 784, 785, 786, 787, 788, 789,
        // 790, 791, 792, 793, 794, 795, 796, 797, 798, 799, 800, 801, 802, 803, 804,
        // 805, 806, 807, 808, 809, 810, 811, 812, 813, 814, 815, 816, 817, 818, 819,
        // 820, 821, 822, 823, 824, 825, 826, 827, 828, 829, 830, 831, 832, 833, 834,
        // 835, 836, 837, 838, 839, 840, 841, 842, 843, 844, 845, 846, 847, 848, 849,
        // 850, 851, 852, 853, 854, 855, 856, 857, 858, 859, 860, 861, 862, 863, 864,
        // 865, 866, 867, 868, 869, 870, 871, 872, 873, 874, 875, 876, 877, 878, 879,
        // 880, 881, 882, 883, 884, 885, 886, 887, 888, 889, 890, 891, 892, 893, 894,
        // 895, 896, 897, 898, 899, 900, 901, 902, 903, 904, 905, 906, 907, 908, 909,
        // 910, 911, 912, 913, 914, 915, 916, 917, 918, 919, 920, 921, 922, 923, 924,
        // 925, 926, 927, 928, 929, 930, 931, 932, 933, 934, 935, 936, 937, 938, 939,
        // 940, 941, 942, 943, 944, 945, 946, 947, 948, 949, 950, 951, 952, 953, 954,
        // 955, 956, 957, 958, 959, 960, 961, 962, 963, 964, 965, 966, 967, 968, 969,
        // 970, 971, 972, 973, 974, 975, 976, 977, 978, 979, 980, 981, 982, 983, 984,
        // 985, 986, 987, 988, 989, 990, 991, 992, 993, 994, 995, 996, 997, 998, 999,
        // 1000, 1001, 1002, 1003, 1004, 1005, 1006, 1007, 1008, 1009, 1010, 1011,
        // 1012, 1013, 1014, 1015, 1016, 1017, 1018, 1019, 1020, 1021, 1022,
        // 1023, 1024 };

        public static readonly string ChunkedData = "HashLib4Pascal012345678HashLib4Pascal012345678HashLib4Pascal012345678HashLib4Pascal012345678";
        public static readonly string EmptyData = "";
        public static readonly byte[] EmptyBytes = new byte[] { };
        public static readonly string DefaultData = "HashLib4Pascal";
        public static readonly string ShortMessage = "A short message";
        public static readonly string ZerotoFour = "01234";
        public static readonly string OnetoNine = "123456789";
        public static readonly string FEEAABEEF = "EEAABEEF";
        public static readonly string ZeroToThreeInHex = "00010203";
        public static readonly string ZeroToFifteenInHex = "000102030405060708090A0B0C0D0E0F";

        public static readonly string ZeroToOneHundredAndNinetyNineInHex =
            "000102030405060708090A0B0C0D0E0F" + "101112131415161718191A1B1C1D1E1F" +
            "202122232425262728292A2B2C2D2E2F" + "303132333435363738393A3B3C3D3E3F" +
            "404142434445464748494A4B4C4D4E4F" + "505152535455565758595A5B5C5D5E5F" +
            "606162636465666768696A6B6C6D6E6F" + "707172737475767778797A7B7C7D7E7F" +
            "808182838485868788898A8B8C8D8E8F" + "909192939495969798999A9B9C9D9E9F" +
            "A0A1A2A3A4A5A6A7A8A9AAABACADAEAF" + "B0B1B2B3B4B5B6B7B8B9BABBBCBDBEBF" +
            "C0C1C2C3C4C5C6C7";

        public static readonly string RandomStringRecord = "I will not buy this record, it is scratched.";
        public static readonly string RandomStringTobacco = "I will not buy this tobacconist's, it is scratched.";
        public static readonly string QuickBrownDog = "The quick brown fox jumps over the lazy dog";
        public static readonly byte[] Bytesabcde = new byte[] { 0x61, 0x62, 0x63, 0x64, 0x65 };
        public static readonly string HexStringAsKey = "000102030405060708090A0B0C0D0E0F";
        public static readonly string HMACLongStringKey = "I need an Angel";
        public static readonly string HMACShortStringKey = "Hash";
    } // end class TestConstants

    public static class TestHelper
    {
        private static byte[] ChunkOne, ChunkTwo;

        static TestHelper()
        {
            byte[] MainData = Converters.ConvertStringToBytes(TestConstants.DefaultData, Encoding.UTF8);
            Int32 Count = MainData.Length - 3;

            ChunkOne = new byte[Count];
            ChunkTwo = new byte[MainData.Length - Count];

            Utils.Utils.Memcopy(ref ChunkOne, MainData, Count);
            Utils.Utils.Memcopy(ref ChunkTwo, MainData, MainData.Length - Count, Count);
        }

        public static void MultithreadComputeHash(ref string result, IHash hash, Int32 iterations)
        {
            for (Int32 i = 0; i < iterations; i++)
            {
                Thread.Sleep(250);
                result = hash.ComputeString(result, Encoding.UTF8).ToString();
            } // end for
        } // end function MultithreadComputeHash

        public static string LeftStrip(string str, char value)
        {
            Int32 s_pos = 0;
            for (Int32 i = 0; i < str.Length; i++, s_pos++)
            {
                if (str[i] != value) break;
            }// end if

            str = str.Substring(s_pos);

            if (str.Length == 0) return "0";

            return str;
        } // end LeftStrip

        /// <summary>
        /// Compares two byte[] and returns true if equal contents
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Boolean</returns>
        public static bool Compare(byte[] x, byte[] y)
        {
            if (x == null)
            {
                if (y == null) return true;
                return false;
            } // end if

            if (y == null) return false;

            if (x.Length != y.Length) return false;

            // At this point their length are equal
            for (Int32 i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i]) return false;
            } // end for

            return true;
        } // edn function Compare

        public static string StringOfChar(char value, Int32 count)
        {
            string temp = "";
            for (Int32 i = 0; i < count; i++)
                temp += value;
            return temp;
        } // end function StringOfChar

        public static void TestMultithreadingAndCloneCorrect(IHash hash)
        {
            IHash clone_1 = hash.Clone(), clone_2 = hash.Clone(), clone_3 = hash.Clone(),
                clone_4 = hash.Clone(), clone_5 = hash.Clone();

            Int32 iterations = 20; // 20 is idle
            string initial = "start";
            string a = initial, b = initial, c = initial, d = initial, e = initial;

            Task t1 = Task.Factory.StartNew(() => MultithreadComputeHash(ref a, clone_1, iterations));
            Task t2 = Task.Factory.StartNew(() => MultithreadComputeHash(ref b, clone_2, iterations));
            Task t3 = Task.Factory.StartNew(() => MultithreadComputeHash(ref c, clone_3, iterations));
            Task t4 = Task.Factory.StartNew(() => MultithreadComputeHash(ref d, clone_4, iterations));
            Task t5 = Task.Factory.StartNew(() => MultithreadComputeHash(ref e, clone_5, iterations));

            Task.WaitAll(t1, t2, t3, t4, t5);

            bool allEqual = a == b ? b == c ? c == d ? d == e ? true : false : false : false : false;

            Assert.IsTrue(allEqual,
                $"Multithreading test failed for [{hash.Name}]");
        } // end function

        public static void TestForNullBytes(IHash hash)
        {
            string ActualString, ExpectedString;

            //
            ActualString = hash.ComputeBytes(null).ToString();

            //
            hash.Initialize();
            hash.TransformBytes(null, 0, 0);
            hash.TransformBytes(new byte[0], 0, 0);
            ExpectedString = hash.TransformFinal().ToString();

            //
            Assert.AreEqual(ExpectedString, ActualString,
                String.Format("Expected {0} but got {1}.",
                ExpectedString, ActualString));
        } // end function

        public static void TestEmptyStream(string expected, IHash hash)
        {
            Stream stream;
            string ActualString;

            stream = new MemoryStream();

            ActualString = hash.ComputeStream(stream).ToString();

            Assert.AreEqual(expected, ActualString,
                String.Format("Expected {0} but got {1}.",
                expected, ActualString));

            stream.Close(); // close stream
        } // end function

        public static void TestHashCloneIsCorrect(IHash hash)
        {
            IHash Original = hash, Copy;
            string ExpectedString, ActualString;

            // Initialize Original Hash
            Original.Initialize();
            Original.TransformBytes(ChunkOne);

            // Make Copy Of Current State
            Copy = Original.Clone();

            Original.TransformBytes(ChunkTwo);
            ExpectedString = Original.TransformFinal().ToString();

            Copy.TransformBytes(ChunkTwo);
            ActualString = Copy.TransformFinal().ToString();

            Assert.AreEqual(ExpectedString, ActualString,
                String.Format("Expected {0} but got {1}.",
                ExpectedString, ActualString));
        }

        public static void TestHashCloneIsUnique(IHash hash)
        {
            IHash Original = hash, Copy;

            Original.Initialize();
            Original.BufferSize = (64 * 1024); // 64Kb

            // Make Copy Of Current State
            Copy = Original.Clone();
            Copy.BufferSize = (128 * 1024); // 128Kb

            if (Original is IXOF)
            {
                (Original as IXOF).XOFSizeInBits = 128;
                (Copy as IXOF).XOFSizeInBits = 256;
            } // end if

            Assert.AreNotEqual(Original.BufferSize, Copy.BufferSize,
                String.Format("Expected {0} but got {1}.",
                Original.BufferSize, Copy.BufferSize));

            if (Original is IXOF)
                Assert.AreNotEqual((Original as IXOF).XOFSizeInBits,
                    (Copy as IXOF).XOFSizeInBits,
                    String.Format("Expected {0} but got {1}.",
                (Original as IXOF).XOFSizeInBits, (Copy as IXOF).XOFSizeInBits));
        }

        public static void TestHMACCloneIsCorrect(IHash hash)
        {
            IHMAC Original, Copy;

            Original = HashFactory.HMAC.CreateHMAC(hash,
                Converters.ConvertStringToBytes(TestConstants.HMACLongStringKey,
                Encoding.UTF8));
            Original.Initialize();
            Original.TransformBytes(ChunkOne);

            // Make Copy Of Current State
            Copy = (IHMAC)Original.Clone();

            Original.TransformBytes(ChunkTwo);
            string ExpectedString = Original.TransformFinal().ToString();

            Copy.TransformBytes(ChunkTwo);
            string ActualString = Copy.TransformFinal().ToString();

            Assert.AreEqual(ExpectedString, ActualString,
                String.Format("Expected {0} but got {1}.",
                ExpectedString, ActualString));
        } // end function TestHMACCloneIsCorrect

        public static void TestActualAndExpectedData(object actual, object expected, IHash i_hash)
        {
            string ActualString, name = actual.GetType().Name;

            if (name == "String")
            {
                ActualString = i_hash?.ComputeString((string)actual, Encoding.UTF8).ToString();
            }
            else if (actual.GetType().Name == "Byte[]")
            {
                ActualString = i_hash?.ComputeBytes((byte[])actual).ToString();
            }
            else
                throw new NotImplementedException("Kindly implement new type.");

            Assert.AreEqual(expected, ActualString,
                String.Format("Expected {0} but got {1}.",
                expected, ActualString));
        }

        public static void TestIncrementalHash(string actual, string expected, IHash hash)
        {
            hash.Initialize();

            Int32 from = 0, part = 3;
            while (from + part < actual.Length)
            {
                hash.TransformString(actual.Substring(from, part), Encoding.UTF8);
                from += part;
            } // end while

            hash.TransformString(actual.Substring(from), Encoding.UTF8); // hash the remaining part

            string ActualString = LeftStrip(hash.TransformFinal().ToString(), '0');
            expected = LeftStrip(expected, '0');

            Assert.AreEqual(expected, ActualString,
                String.Format("Expected {0} but got {1}.",
                expected, ActualString));
        } // end function TestIncrementalHash

    } // end class TestHelper
}