namespace iTrip.Controllers
{
    using System;
    using System.Text;

    public sealed class TextEncoder
    {
        public static int BytesToInt(byte[] bytes)
        {
            int i = 0;
            try
            {
                i = BitConverter.ToInt32(bytes, 0);
            }
            catch
            {
            }
            return i;
        }

        public static string BytesToText(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        public static uint BytesToUInt32(byte[] bytes)
        {
            uint i = 0;
            try
            {
                i = BitConverter.ToUInt32(bytes, 0);
            }
            catch
            {
            }
            return i;
        }

        public static byte[] IntToBytes(int i)
        {
            return BitConverter.GetBytes(i);
        }

        public static byte[] TextToBytes(string text)
        {
            byte[] buf = new byte[0];
            if (!string.IsNullOrEmpty(text))
            {
                buf = Encoding.Default.GetBytes(text);
            }
            return buf;
        }

        public static byte[] UIntToBytes(uint i)
        {
            return BitConverter.GetBytes(i);
        }

        /// <summary> Convert a string of hex digits (ex: E4 CA B2) to a byte array. </summary>
        /// <param name="s"> The string containing the hex digits (with or without spaces). </param>
        /// <returns> Returns an array of bytes. </returns>
        public static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        /// <summary> Converts an array of bytes into a formatted string of hex digits (ex: E4 CA B2)</summary>
        /// <param name="data"> The array of bytes to be translated into a string of hex digits. </param>
        /// <returns> Returns a well formatted string of hex digits with spacing. </returns>
        public static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }

        /// <summary> Converts an array of bytes into a formatted string of hex digits (ex: E4 CA B2)</summary>
        /// <param name="data"> The array of bytes to be translated into a string of hex digits. </param>
        /// <returns> Returns a well formatted string of hex digits with spacing. </returns>
        public static string ByteToHexString(byte data)
        {
            return Convert.ToString(data, 16).PadLeft(2, '0').ToUpper();
        }
        public static string ByteToString(byte ba)
        {
            return Convert.ToString(ba, 2).PadLeft(8, '0');
        }
    }
}

