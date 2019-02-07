using System;
using System.Collections.Generic;
using System.Text;
using HashLib;

namespace CodingBlocks.SimpleSerialize
{
    public static class HashHelper
    {
        public static byte[] Keccak256(byte[] input)
        {
            return HashFactory.Crypto.SHA3.CreateKeccak256().ComputeBytes(input).GetBytes();
        }
    }
}
