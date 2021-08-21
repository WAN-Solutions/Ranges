using System;

namespace wan24.Ranges
{
    /// <summary>
    /// 64 bit signed integer range
    /// </summary>
    public partial class Int64Range : ICloneable
    {
        /// <summary>
        /// From including
        /// </summary>
        protected long _FromIncluding = 0;
        /// <summary>
        /// Count of numbers in the range excluding the first number (0..n)
        /// </summary>
        protected ulong _Count = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fromIncluding">From including</param>
        /// <param name="count">Count of numbers in the range excluding the first number (0..n)</param>
        /// <param name="isReadonly">Is readonly?</param>
        public Int64Range(long fromIncluding = 0, ulong count = 0, bool isReadonly = false)
        {
            decimal c = count;
            if (fromIncluding + c > ulong.MaxValue) throw new ArgumentOutOfRangeException(nameof(count));
            _FromIncluding = fromIncluding;
            _Count = count;
            ToIncluding = (long)(fromIncluding + c);
            IsReadonly = isReadonly;
        }

        /// <summary>
        /// Get the number at an index
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Number</returns>
        public long this[long index] => index >= 0 && index < (decimal)_Count ? _FromIncluding + index : throw new ArgumentOutOfRangeException(nameof(index));

        /// <summary>
        /// From including
        /// </summary>
        public long FromIncluding
        {
            get => _FromIncluding;
            set
            {
                if (IsReadonly) throw new InvalidOperationException("Readonly");
                decimal c = _Count;
                if (value + c > ulong.MaxValue) throw new ArgumentOutOfRangeException(nameof(value));
                _FromIncluding = value;
                ToIncluding = (long)(value + c);
            }
        }

        /// <summary>
        /// Count of numbers in the range
        /// </summary>
        public ulong Count
        {
            get => _Count;
            set
            {
                if (IsReadonly) throw new InvalidOperationException("Readonly");
                decimal v = value - 1;
                if (_FromIncluding + v > ulong.MaxValue) throw new ArgumentOutOfRangeException(nameof(value));
                _Count = value;
                ToIncluding = (long)(_FromIncluding + v);
            }
        }

        /// <summary>
        /// To including
        /// </summary>
        public long ToIncluding { get; protected set; }

        /// <summary>
        /// From and to including
        /// </summary>
        public (long, long) FromToIncluding => (FromIncluding, ToIncluding);

        /// <summary>
        /// Is readonly?
        /// </summary>
        public bool IsReadonly { get; protected set; }

        /// <summary>
        /// Determine if a number matches within this range
        /// </summary>
        /// <param name="number">Number</param>
        /// <returns>Does match?</returns>
        public bool IsMatch(long number) => number >= _FromIncluding && number <= ToIncluding;

        /// <summary>
        /// Determine if a range matches within this range
        /// </summary>
        /// <param name="range">Range</param>
        /// <returns>Does match?</returns>
        public bool IsMatch(Int64Range range) => range.FromIncluding >= _FromIncluding && range.ToIncluding <= ToIncluding;

        /// <summary>
        /// Determine if a range matches within this range
        /// </summary>
        /// <param name="range">Range</param>
        /// <returns>Does match?</returns>
        public bool IsMatch(Int32Range range) => range.FromIncluding >= _FromIncluding && range.ToIncluding <= ToIncluding;

        /// <summary>
        /// Make this instance readonly
        /// </summary>
        /// <returns>This</returns>
        public Int64Range MakeReadonly()
        {
            IsReadonly = true;
            return this;
        }

        /// <summary>
        /// Create a writable instance clone
        /// </summary>
        /// <returns>Writable instance clone</returns>
        public Int64Range CreateWritable() => new Int64Range(_FromIncluding, _Count);

        /// <inheritdoc/>
        public object Clone() => new Int64Range(_FromIncluding, _Count, IsReadonly);

        /// <inheritdoc/>
        public override int GetHashCode() => (391 + _FromIncluding.GetHashCode()) * 23 + _Count.GetHashCode() * 23 + IsReadonly.GetHashCode();

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Int64Range range && range._FromIncluding == _FromIncluding && range._Count == _Count;

        /// <inheritdoc/>
        public override string ToString() => $"{_FromIncluding}..{ToIncluding} ({_Count + 1})";
    }
}
