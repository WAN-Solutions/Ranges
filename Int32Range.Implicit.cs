using System;

namespace wan24.Ranges
{
    public partial class Int32Range
    {
        /// <summary>
        /// Cast as boolean
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator bool(Int32Range range) => range._Count > 0;

        /// <summary>
        /// Cast as string
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator string(Int32Range range) => range.ToString();

        /// <summary>
        /// Cast as sbyte
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator sbyte(Int32Range range) => ((IConvertible)range).ToSByte(provider: null);

        /// <summary>
        /// Cast as byte
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator byte(Int32Range range) => ((IConvertible)range).ToByte(provider: null);

        /// <summary>
        /// Cast as short
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator short(Int32Range range) => ((IConvertible)range).ToInt16(provider: null);

        /// <summary>
        /// Cast as ushort
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator ushort(Int32Range range) => ((IConvertible)range).ToUInt16(provider: null);

        /// <summary>
        /// Cast as int
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator int(Int32Range range) => ((IConvertible)range).ToInt32(provider: null);

        /// <summary>
        /// Cast as uint
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator uint(Int32Range range) => ((IConvertible)range).ToUInt32(provider: null);

        /// <summary>
        /// Cast as long
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator long(Int32Range range) => ((IConvertible)range).ToInt64(provider: null);

        /// <summary>
        /// Cast as ulong
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator ulong(Int32Range range) => ((IConvertible)range).ToUInt64(provider: null);

        /// <summary>
        /// Cast as 64 bit integer range
        /// </summary>
        /// <param name="range">Range</param>
        public static implicit operator Int64Range(Int32Range range) => new Int64Range(range.FromIncluding,range.Count,range.IsReadonly);
    }
}
