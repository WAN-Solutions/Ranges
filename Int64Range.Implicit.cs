using System;

namespace wan24.Ranges
{
    public partial class Int64Range
    {
        /// <summary>
        /// Cast as boolean
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator bool(Int64Range range) => range._Count > 0;

        /// <summary>
        /// Cast as string
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator string(Int64Range range) => range.ToString();

        /// <summary>
        /// Cast as sbyte
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator sbyte(Int64Range range) => ((IConvertible)range).ToSByte(provider: null);

        /// <summary>
        /// Cast as byte
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator byte(Int64Range range) => ((IConvertible)range).ToByte(provider: null);

        /// <summary>
        /// Cast as short
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator short(Int64Range range) => ((IConvertible)range).ToInt16(provider: null);

        /// <summary>
        /// Cast as ushort
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator ushort(Int64Range range) => ((IConvertible)range).ToUInt16(provider: null);

        /// <summary>
        /// Cast as int
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator int(Int64Range range) => ((IConvertible)range).ToInt32(provider: null);

        /// <summary>
        /// Cast as uint
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator uint(Int64Range range) => ((IConvertible)range).ToUInt32(provider: null);

        /// <summary>
        /// Cast as long
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator long(Int64Range range) => ((IConvertible)range).ToInt64(provider: null);

        /// <summary>
        /// Cast as ulong
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator ulong(Int64Range range) => ((IConvertible)range).ToUInt64(provider: null);

        /// <summary>
        /// Cast as 64 bit integer range
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator Int32Range(Int64Range range) => new Int32Range((int)range.FromIncluding, (uint)range.Count, range.IsReadonly);
    }
}
