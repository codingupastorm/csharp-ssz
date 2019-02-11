using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingBlocks.SimpleSerialize
{
    public static class SSZ
    {
        #region Encoding
        /// <summary>
        /// Encode the 'uint' primitive type.
        /// </summary>
        /// <param name="value">The value to encode.</param>
        /// <param name="bits">The number of bits that make up the integer. Must be a byte number (multiple of 8).</param>
        /// <returns>Encoded uint.</returns>
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

        public static byte[] EncodeBool(bool value)
        {
            return value
                ? new byte[] {0x01}
                : new byte[] {0x00};
        }

        public static byte[] EncodeBytes(byte[] bytes)
        {
            // TODO: Check length of value
            // TODO: Performance? 
            byte[] lengthBytes = EncodeUint((ulong) bytes.Length, 32);
            return lengthBytes.Concat(bytes).ToArray();
        }

        public static byte[] EncodeBytesList()
        {
            throw new NotImplementedException();
        }

        public static byte[] EncodeBoolList()
        {
            throw new NotImplementedException();
        }

        public static byte[] EncodeUintList()
        {
            throw new NotImplementedException();
        }

        public static byte[] EncodeContainer(IList<byte[]> fieldValues)
        {
            // TODO: Check on total length of values.
            // TODO: Optimize performance.
            int totalLength = fieldValues.Sum(x => x.Length);
            byte[] serializedContainer = new byte[totalLength];
            int cursor = 0;

            foreach (var field in fieldValues)
            {
                Array.Copy(field, 0, serializedContainer, cursor, field.Length);
                cursor += field.Length; 
            }

            byte[] lengthBytes = EncodeUint((ulong) totalLength, 32);

            return lengthBytes.Concat(serializedContainer).ToArray();
        }

        #endregion

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
