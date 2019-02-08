// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModbusConversions.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbus.Extensions
{
    #region Using Directives

    using System;
    using System.Collections;
    using System.Linq;
    using System.Text;

    #endregion

    /// <summary>
    /// Class holding extension for Modbus specific data conversions.
    /// </summary>
    public static class ModbusConversions
    {
        #region Convert From Registers

        /// <summary>
        /// Converts an array of one UInt16 value to a 16 bit BitArray.
        /// </summary>
        /// <param name="registers">The ushort array.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <returns>The 16 bit BitArray.</returns>
        public static BitArray ToBitArray(this ushort[] registers, bool swapBytes = false)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("ToBitArray", "registers");
            }

            if (registers.Length < 1)
            {
                throw new ArgumentOutOfRangeException("ToBitArray", "registers");
            }

            BitArray value = new BitArray(ToBytes(new ushort[] { registers[0] }, swapBytes).Reverse().ToArray());
#if MODBUS_DEBUG
            Debug.WriteLine($"{registers.ToHexString()} => {value}");
#endif
            return value;
        }

        /// <summary>
        /// Converts an array of UInt16 values to a 16 bit integer.
        /// </summary>
        /// <param name="registers">The ushort array.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <returns>The 16 bit integer.</returns>
        public static short ToShort(this ushort[] registers, bool swapBytes = false)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("ToShort", "registers");
            }

            if (registers.Length != 1)
            {
                throw new ArgumentOutOfRangeException("ToShort", "registers");
            }

            byte[] bytes = ToBytes(registers, swapBytes).Reverse().ToArray();
            short value = BitConverter.ToInt16(bytes, 0);
#if MODBUS_DEBUG
            Debug.WriteLine($"{registers.ToHexString()} => {value}");
#endif
            return value;
        }

        /// <summary>
        /// Converts an array of UInt16 values to an unsigned 16 bit integer.
        /// </summary>
        /// <param name="registers">The ushort array.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <returns>The 16 bit unsigned integer.</returns>
        public static ushort ToUShort(this ushort[] registers, bool swapBytes = false)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("ToUShort", "registers");
            }

            if (registers.Length != 1)
            {
                throw new ArgumentOutOfRangeException("ToUShort", "registers");
            }

            byte[] bytes = ToBytes(registers, swapBytes).Reverse().ToArray();
            ushort value = BitConverter.ToUInt16(bytes, 0);
#if MODBUS_DEBUG
            Debug.WriteLine($"{registers.ToHexString()} => {value}");
#endif
            return value;
        }

        /// <summary>
        /// Converts an array of UInt16 values to a 32 bit integer.
        /// </summary>
        /// <param name="registers">The ushort array.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <param name="swapWords">Flag indicating that the words have to be swapped.</param>
        /// <returns>The 32 bit integer.</returns>
        public static int ToInt32(this ushort[] registers, bool swapBytes = false, bool swapWords = false)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("ToInt32", "registers");
            }

            if (registers.Length != 2)
            {
                throw new ArgumentOutOfRangeException("ToInt32", "registers");
            }

            byte[] bytes = ToBytes(registers.SwapWords(swapWords), swapBytes).Reverse().ToArray();
            int value = BitConverter.ToInt32(bytes, 0);
#if MODBUS_DEBUG
            Debug.WriteLine($"{registers.ToHexString()} => {value}");
#endif
            return value;
        }

        /// <summary>
        /// Converts an array of UInt16 values to an unsigned 32 bit integer.
        /// </summary>
        /// <param name="registers">The ushort array.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <param name="swapWords">Flag indicating that the words have to be swapped.</param>
        /// <returns>The unsigned 32 bit integer.</returns>
        public static uint ToUInt32(this ushort[] registers, bool swapBytes = false, bool swapWords = false)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("ToUInt32", "registers");
            }

            if (registers.Length != 2)
            {
                throw new ArgumentOutOfRangeException("ToUInt32", "registers");
            }

            byte[] bytes = ToBytes(registers.SwapWords(swapWords), swapBytes).Reverse().ToArray();
            uint value = BitConverter.ToUInt32(bytes, 0);
#if MODBUS_DEBUG
            Debug.WriteLine($"{registers.ToHexString()} => {value}");
#endif
            return value;
        }

        /// <summary>
        /// Converts an array of UInt16 values to an IEEE 32 bit float number.
        /// </summary>
        /// <param name="registers">The ushort array.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <param name="swapWords">Flag indicating that the words have to be swapped.</param>
        /// <returns>The IEEE 32 bit float number.</returns>
        public static float ToFloat(this ushort[] registers, bool swapBytes = false, bool swapWords = false)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("ToFloat", "registers");
            }

            if (registers.Length != 2)
            {
                throw new ArgumentOutOfRangeException("ToFloat", "registers");
            }

            byte[] bytes = ToBytes(registers.SwapWords(swapWords), swapBytes).Reverse().ToArray();
            float value = BitConverter.ToSingle(bytes, 0);
#if MODBUS_DEBUG
            Debug.WriteLine($"{registers.ToHexString()} => {value}");
#endif
            return value;
        }

        /// <summary>
        /// Converts an array of UInt16 values to an IEEE 64 bit float number.
        /// </summary>
        /// <param name="registers">The ushort array.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <param name="swapWords">Flag indicating that the words have to be swapped.</param>
        /// <returns>The IEEE 64 bit float number.</returns>
        public static double ToDouble(this ushort[] registers, bool swapBytes = false, bool swapWords = false)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("ToDouble", "registers");
            }

            if (registers.Length != 4)
            {
                throw new ArgumentOutOfRangeException("ToDouble", "registers");
            }

            byte[] bytes = ToBytes(registers.SwapWords(swapWords), swapBytes).Reverse().ToArray();
            double value = BitConverter.ToDouble(bytes, 0);
#if MODBUS_DEBUG
            Debug.WriteLine($"{registers.ToHexString()} => {value}");
#endif
            return value;
        }

        /// <summary>
        /// Converts an array of UInt16 values to a long integer.
        /// </summary>
        /// <param name="registers">The ushort array.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <param name="swapWords">Flag indicating that the words have to be swapped.</param>
        /// <returns>The long integer.</returns>
        public static long ToLong(this ushort[] registers, bool swapBytes = false, bool swapWords = false)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("ToLong", "registers");
            }

            if (registers.Length != 4)
            {
                throw new ArgumentOutOfRangeException("ToLong", "registers");
            }

            byte[] bytes = ToBytes(registers.SwapWords(swapWords), swapBytes).Reverse().ToArray();
            long value = BitConverter.ToInt64(bytes, 0);
#if MODBUS_DEBUG
            Debug.WriteLine($"{registers.ToHexString()} => {value}");
#endif
            return value;
        }

        /// <summary>
        /// Converts an array of UInt16 values to an unsigned long integer.
        /// </summary>
        /// <param name="registers">The ushort array.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <param name="swapWords">Flag indicating that the words have to be swapped.</param>
        /// <returns>Theunsigned long integer.</returns>
        public static ulong ToULong(this ushort[] registers, bool swapBytes = false, bool swapWords = false)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("ToULong", "registers");
            }

            if (registers.Length != 4)
            {
                throw new ArgumentOutOfRangeException("ToULong", "registers");
            }

            byte[] bytes = ToBytes(registers.SwapWords(swapWords), swapBytes).Reverse().ToArray();
            ulong value = BitConverter.ToUInt64(bytes, 0);
#if MODBUS_DEBUG
            Debug.WriteLine($"{registers.ToHexString()} => {value}");
#endif
            return value;
        }

        /// <summary>
        /// Converts an array of UInt16 values to an ASCII string.
        /// Note that string will contain all ASCII characters until
        /// the first zero byte value will be encountered.
        /// </summary>
        /// <param name="registers">The ushort array.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <returns>The ASCII string.</returns>
        public static string ToASCII(this ushort[] registers, bool swapBytes = false)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("ToString", "registers");
            }

            if (registers.Length == 0)
            {
                throw new ArgumentOutOfRangeException("ToString", "registers");
            }

            byte[] bytes = registers.ToBytes(swapBytes);
            byte[] chars = new byte[bytes.Length];
            int j = 0;

            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0)
                {
                    break;
                }

                chars[j++] = bytes[i];
            }

            Array.Resize(ref chars, j);

            string value = Encoding.ASCII.GetString(chars);
#if MODBUS_DEBUG
            Debug.WriteLine($"{registers.ToHexString()} => {value}");
#endif
            return value;
        }

        /// <summary>
        /// Converts an array of UInt16 values to a HEX string.
        /// </summary>
        /// <param name="registers">The ushort array.</param>
        /// <param name="length">The number of bytes to be converted.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <returns>The HEX string.</returns>
        public static string ToHex(this ushort[] registers, ushort length, bool swapBytes = false)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("ToHex", "registers");
            }

            if (registers.Length == 0)
            {
                throw new ArgumentOutOfRangeException("ToHex", "registers");
            }

            byte[] bytes = registers.ToBytes(swapBytes);
            string hex = BitConverter.ToString(bytes);
            string value = hex.Replace("-", string.Empty).Substring(0, length * 2);
#if MODBUS_DEBUG
            Debug.WriteLine($"{registers.ToHexString()} => {value}");
#endif
            return value;
        }

        #endregion

        #region Convert To Registers

        /// <summary>
        /// Converts a bit array value to an array of registers (ushort).
        /// </summary>
        /// <param name="value">The bit array value.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <returns>The array of registers.</returns>
        public static ushort[] ToRegisters(this BitArray value, bool swapBytes = false)
        {
            if (value == null)
            {
                throw new ArgumentNullException("ToRegisters", "value");
            }

            if (value.Length != 16)
            {
                throw new ArgumentOutOfRangeException("ToRegisters", "value");
            }

            ushort[] registers = new ushort[] { 0 };

            ushort bitvalue = 1;

            for (int i = 0; i < 16; i++)
            {
                if (value[i])
                {
                    registers[0] += bitvalue;
                }

                bitvalue *= 2;
            }

            if (swapBytes)
            {
                byte[] bytes = BitConverter.GetBytes(registers[0]);
                Array.Reverse(bytes);
                registers[0] = BitConverter.ToUInt16(bytes, 0);
            }

#if MODBUS_DEBUG
            Debug.WriteLine($"{value} => [{registers.ToHexString()}]");
#endif
            return registers;
        }

        /// <summary>
        /// Converts a short value to an array of registers (ushort).
        /// </summary>
        /// <param name="value">The short value.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <param name="swapWords">Flag indicating that the words have to be swapped.</param>
        /// <returns>The array of registers.</returns>
        public static ushort[] ToRegisters(this short value, bool swapBytes = false, bool swapWords = false)
        {
            byte[] bytes = BitConverter.GetBytes(value).Reverse().ToArray();
            ushort[] registers = bytes.ToRegisters(swapBytes);
#if MODBUS_DEBUG
            Debug.WriteLine($"{value} => [{registers.ToHexString()}]");
#endif
            return registers;
        }

        /// <summary>
        /// Converts an ushort value to an array of registers (ushort).
        /// </summary>
        /// <param name="value">The ushort value.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <param name="swapWords">Flag indicating that the words have to be swapped.</param>
        /// <returns>The array of registers.</returns>
        public static ushort[] ToRegisters(this ushort value, bool swapBytes = false, bool swapWords = false)
        {
            byte[] bytes = BitConverter.GetBytes(value).Reverse().ToArray();
            ushort[] registers = bytes.ToRegisters(swapBytes).SwapWords(swapWords);
#if MODBUS_DEBUG
            Debug.WriteLine($"{value} => [{registers.ToHexString()}]");
#endif
            return registers;
        }

        /// <summary>
        /// Converts an int value to an array of registers (ushort).
        /// </summary>
        /// <param name="value">The int value.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <param name="swapWords">Flag indicating that the words have to be swapped.</param>
        /// <returns>The array of registers.</returns>
        public static ushort[] ToRegisters(this int value, bool swapBytes = false, bool swapWords = false)
        {
            byte[] bytes = BitConverter.GetBytes(value).Reverse().ToArray();
            ushort[] registers = bytes.ToRegisters(swapBytes).SwapWords(swapWords);
#if MODBUS_DEBUG
            Debug.WriteLine($"{value} => [{registers.ToHexString()}]");
#endif
            return registers;
        }

        /// <summary>
        /// Converts an unint value to an array of registers (ushort).
        /// </summary>
        /// <param name="value">The uint value.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <param name="swapWords">Flag indicating that the words have to be swapped.</param>
        /// <returns>The array of registers.</returns>
        public static ushort[] ToRegisters(this uint value, bool swapBytes = false, bool swapWords = false)
        {
            byte[] bytes = BitConverter.GetBytes(value).Reverse().ToArray();
            ushort[] registers = bytes.ToRegisters(swapBytes).SwapWords(swapWords);
#if MODBUS_DEBUG
            Debug.WriteLine($"{value} => [{registers.ToHexString()}]");
#endif
            return registers;
        }

        /// <summary>
        /// Converts a IEEE 32 bit float number to an array of UInt16 values.
        /// </summary>
        /// <param name="value">The IEEE 32 bit float number.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <param name="swapWords">Flag indicating that the words have to be swapped.</param>
        /// <returns>The array of registers.</returns>
        public static ushort[] ToRegisters(this float value, bool swapBytes = false, bool swapWords = false)
        {
            byte[] bytes = BitConverter.GetBytes(value).Reverse().ToArray();
            ushort[] registers = bytes.ToRegisters(swapBytes).SwapWords(swapWords);
#if MODBUS_DEBUG
            Debug.WriteLine($"{value} => [{registers.ToHexString()}]");
#endif
            return registers;
        }

        /// <summary>
        /// Converts a IEEE 64 bit float number to an array of UInt16 values.
        /// </summary>
        /// <param name="value">The IEEE 64 bit float number.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <param name="swapWords">Flag indicating that the words have to be swapped.</param>
        /// <returns>The array of registers.</returns>
        public static ushort[] ToRegisters(this double value, bool swapBytes = false, bool swapWords = false)
        {
            byte[] bytes = BitConverter.GetBytes(value).Reverse().ToArray();
            ushort[] registers = bytes.ToRegisters(swapBytes).SwapWords(swapWords);
#if MODBUS_DEBUG
            Debug.WriteLine($"{value} => [{registers.ToHexString()}]");
#endif
            return registers;
        }

        /// <summary>
        /// Converts a long value to an array of registers (ushort).
        /// </summary>
        /// <param name="value">The long value.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <param name="swapWords">Flag indicating that the words have to be swapped.</param>
        /// <returns>The array of registers.</returns>
        public static ushort[] ToRegisters(this long value, bool swapBytes = false, bool swapWords = false)
        {
            byte[] bytes = BitConverter.GetBytes(value).Reverse().ToArray();
            ushort[] registers = bytes.ToRegisters(swapBytes).SwapWords(swapWords);
#if MODBUS_DEBUG
            Debug.WriteLine($"{value} => [{registers.ToHexString()}]");
#endif
            return registers;
        }

        /// <summary>
        /// Converts an ulong value to an array of registers (ushort).
        /// </summary>
        /// <param name="value">The ulong value.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <param name="swapWords">Flag indicating that the words have to be swapped.</param>
        /// <returns>The array of registers.</returns>
        public static ushort[] ToRegisters(this ulong value, bool swapBytes = false, bool swapWords = false)
        {
            byte[] bytes = BitConverter.GetBytes(value).Reverse().ToArray();
            ushort[] registers = bytes.ToRegisters(swapBytes).SwapWords(swapWords);
#if MODBUS_DEBUG
            Debug.WriteLine($"{value} => [{registers.ToHexString()}]");
#endif
            return registers;
        }

        /// <summary>
        /// Converts a string value to an array of registers (ushort).
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <returns>The array of registers.</returns>
        public static ushort[] ToRegisters(this string value, bool swapBytes = false)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            ushort[] registers = bytes.ToRegisters(swapBytes);
#if MODBUS_DEBUG
            Debug.WriteLine($"{value} => [{registers.ToHexString()}]");
#endif
            return registers;
        }

        /// <summary>
        /// Converts an array of bytes to an array of registers (ushort).
        /// </summary>
        /// <param name="bytes">The byte array.</param>
        /// <param name="swapBytes">Flag indicating that the bytes have to be swapped.</param>
        /// <returns>The array of registers.</returns>
        public static ushort[] ToRegisters(this byte[] bytes, bool swapBytes = false)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("ToRegisters");
            }

            ushort[] registers = new ushort[(bytes.Length + 1) / 2];

            if ((registers.Length * 2) > bytes.Length)
            {
                Array.Resize(ref bytes, registers.Length * 2);
            }

            for (int i = 0; i < registers.Length; i++)
            {
                if (swapBytes)
                {
                    registers[i] = BitConverter.ToUInt16(new byte[] { bytes[(i * 2)], bytes[(i * 2) + 1] }, 0);
                }
                else
                {
                    registers[i] = BitConverter.ToUInt16(new byte[] { bytes[(i * 2) + 1], bytes[(i * 2)] }, 0);
                }
            }

#if MODBUS_DEBUG
            Debug.WriteLine($"{bytes.ToHexString()} => [{registers.ToHexString()}]");
#endif
            return registers;
        }

        /// <summary>
        /// Converts an array of registers to an array of bytes optionally swapping bytes.
        /// </summary>
        /// <param name="registers">The ushort array.</param>
        /// <param name="swap">A boolean value indicating that the bytes have to be swapped.</param>
        /// <returns>The array of bytes.</returns>
        public static byte[] ToBytes(this ushort[] registers, bool swap = false)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("ToBytes", "registers");
            }

            byte[] bytes = new byte[registers.Length * 2];

            for (int i = 0; i < registers.Length; i++)
            {
                byte[] array = BitConverter.GetBytes(registers[i]);

                if (swap)
                {
                    bytes[(2 * i)] = array[0];
                    bytes[(2 * i) + 1] = array[1];
                }
                else
                {
                    bytes[(2 * i)] = array[1];
                    bytes[(2 * i) + 1] = array[0];
                }
            }

#if MODBUS_DEBUG
            Debug.WriteLine($"{registers.ToHexString()} => [{bytes.ToHexString()}]");
#endif
            return bytes;
        }

        /// <summary>
        /// Returns the array of registers with optionally reverse word order.
        /// Note that the word order is always swapped by default.
        /// </summary>
        /// <param name="registers">The ushort array.</param>
        /// <param name="swap">A boolean value indicating that the words have to be swapped.</param>
        /// <returns>The array of ushort values.</returns>
        public static ushort[] SwapWords(this ushort[] registers, bool swap = false)
        {
            if (swap)
            {
                switch (registers.Length)
                {
                    case 2:
                        return new ushort[] { registers[1], registers[0] };
                    case 4:
                        return new ushort[] { registers[3], registers[2], registers[1], registers[0] };
                }
            }
            else
            {
                switch (registers.Length)
                {
                    case 2:
                        return new ushort[] { registers[0], registers[1] };
                    case 4:
                        return new ushort[] { registers[0], registers[1], registers[2], registers[3] };
                }
            }

            return registers;
        }

        #endregion

        #region Helper Functions

        /// <summary>
        /// Converts an array of registers to a HEX string.
        /// </summary>
        /// <param name="registers">The array of registers.</param>
        /// <returns>The HEX string representation.</returns>
        public static string ToHexString(this ushort[] registers)
        {
            string text = "[";

            for (int i = 0; i < registers.Length; i++)
            {
                text += registers[i].ToString("X4");

                if (i < registers.Length - 1)
                {
                    text += ", ";
                }
            }

            text += "]";

            return text;
        }

        /// <summary>
        /// Converts an array of bytes to a HEX string.
        /// </summary>
        /// <param name="registers">The byte array.</param>
        /// <returns>The HEX string representation.</returns>
        public static string ToHexString(this byte[] registers)
        {
            string text = "[";

            for (int i = 0; i < registers.Length; i++)
            {
                text += registers[i].ToString("X2");

                if (i < registers.Length - 1)
                {
                    text += ", ";
                }
            }

            text += "]";

            return text;
        }


        /// <summary>
        /// Converts a 16-bit BitArray to a string of 0 and 1s.
        /// </summary>
        /// <param name="array">The bit array.</param>
        /// <returns>The string of 0 and 1s.</returns>
        public static string ToDigitString(this BitArray array)
        {
            var builder = new StringBuilder();

            foreach (var bit in array.Cast<bool>())
            {
                builder.Append(bit ? "1" : "0");
            }

            return builder.ToString();
        }

        /// <summary>
        /// Converts a string of 0 and 1s to a 16-bit BitArray.
        /// </summary>
        /// <param name="bits">The string of 0 and 1s.</param>
        /// <returns>The 16-bit BitArray.</returns>
        public static BitArray ToBitArray(this string bits)
        {
            if (bits == null)
            {
                bits = new string('0', 16);
            }

            if (bits.Length > 16)
            {
                bits = bits.Substring(0, 16);
            }

            bool[] data = new bool[16];
            int index = 0;

            foreach (char c in bits)
            {
                switch (c)
                {
                    case '0':
                        data[index] = false;
                        break;
                    case '1':
                        data[index] = true;
                        break;
                    default:
                        break;
                }

                ++index;
            }

            return new BitArray(data);
        }

        #endregion
    }
}