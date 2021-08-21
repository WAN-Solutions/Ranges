using System;

namespace wan24.Ranges
{
    public partial class Int32Range : IConvertible
    {
        /// <inheritdoc/>
        TypeCode IConvertible.GetTypeCode() => TypeCode.Int32;

        /// <inheritdoc/>
        bool IConvertible.ToBoolean(IFormatProvider provider) => _Count > 0;

        /// <inheritdoc/>
        byte IConvertible.ToByte(IFormatProvider provider) => (byte)(_Count + 1);

        /// <inheritdoc/>
        char IConvertible.ToChar(IFormatProvider provider) => throw new InvalidCastException();

        /// <inheritdoc/>
        DateTime IConvertible.ToDateTime(IFormatProvider provider) => throw new InvalidCastException();

        /// <inheritdoc/>
        decimal IConvertible.ToDecimal(IFormatProvider provider) => _Count + 1;

        /// <inheritdoc/>
        double IConvertible.ToDouble(IFormatProvider provider) => _Count + 1;

        /// <inheritdoc/>
        short IConvertible.ToInt16(IFormatProvider provider) => (short)(_Count + 1);

        /// <inheritdoc/>
        int IConvertible.ToInt32(IFormatProvider provider) => (int)(_Count + 1);

        /// <inheritdoc/>
        long IConvertible.ToInt64(IFormatProvider provider) => _Count + 1;

        /// <inheritdoc/>
        sbyte IConvertible.ToSByte(IFormatProvider provider) => (sbyte)(_Count + 1);

        /// <inheritdoc/>
        float IConvertible.ToSingle(IFormatProvider provider) => _Count + 1;

        /// <inheritdoc/>
        string IConvertible.ToString(IFormatProvider provider) => ToString();

        /// <inheritdoc/>
        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            if (conversionType == typeof(bool)) return ((IConvertible)this).ToBoolean(provider);
            if (conversionType == typeof(byte)) return ((IConvertible)this).ToByte(provider);
            if (conversionType == typeof(char)) return ((IConvertible)this).ToChar(provider);
            if (conversionType == typeof(DateTime)) return ((IConvertible)this).ToDateTime(provider);
            if (conversionType == typeof(decimal)) return ((IConvertible)this).ToDecimal(provider);
            if (conversionType == typeof(double)) return ((IConvertible)this).ToDouble(provider);
            if (conversionType == typeof(short)) return ((IConvertible)this).ToInt16(provider);
            if (conversionType == typeof(int)) return ((IConvertible)this).ToInt32(provider);
            if (conversionType == typeof(long)) return ((IConvertible)this).ToInt64(provider);
            if (conversionType == typeof(sbyte)) return ((IConvertible)this).ToSByte(provider);
            if (conversionType == typeof(float)) return ((IConvertible)this).ToSingle(provider);
            if (conversionType == typeof(string)) return ((IConvertible)this).ToString(provider);
            if (conversionType == typeof(ushort)) return ((IConvertible)this).ToUInt16(provider);
            if (conversionType == typeof(uint)) return ((IConvertible)this).ToUInt32(provider);
            if (conversionType == typeof(ulong)) return ((IConvertible)this).ToUInt64(provider);
            if (conversionType == typeof(Int64Range)) return (Int64Range)this;
            throw new ArgumentException("Unsupported conversion type", nameof(conversionType));
        }

        /// <inheritdoc/>
        ushort IConvertible.ToUInt16(IFormatProvider provider) => (ushort)(_Count + 1);

        /// <inheritdoc/>
        uint IConvertible.ToUInt32(IFormatProvider provider) => _Count + 1;

        /// <inheritdoc/>
        ulong IConvertible.ToUInt64(IFormatProvider provider) => _Count + 1;
    }
}
