using System;
using System.Collections.Generic;
using System.Text;

namespace CodingBlocks.SimpleSerialize
{
    public static class SSZ
    {
        /*
         * There are three 'primitive' data types:
         * - uintN
         * - bytes
         * - bool
         */

        public static byte[] EncodeUint(ulong value, int bits)
        {
            CheckIntBitSize(bits);

            // TODO: Check that the entire value fits into this many bits.

            int byteLength = bits / 8;
            byte[] encoded = new byte[byteLength];
            int shift = 0;

            for (int i = 0; i < byteLength; i++)
            {
                encoded[i] = (byte)((value >> shift) & 0xFF);
                shift += 8;
            }

            return encoded;
        }

        /// <summary>
        /// Check number of bits always a whole byte number.
        /// </summary>
        private static void CheckIntBitSize(int bits)
        {
            if (bits % 8 != 0)
                throw new Exception("Needs to come in multiple of 8 bits.");
        }
    }
}
