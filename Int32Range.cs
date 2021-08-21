using System;

namespace wan24.Ranges
{
    /// <summary>
    /// 32 bit signed integer range
    /// </summary>
    public partial class Int32Range : ICloneable
    {
        /// <summary>
        /// From including
        /// </summary>
        protected int _FromIncluding = 0;
        /// <summary>
        /// Count of numbers in the range excluding the first number (0..n)
        /// </summary>
        protected uint _Count = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fromIncluding">From including</param>
        /// <param name="count">Count of numbers in the range excluding the first number (0..n)</param>
        /// <param name="isReadonly">Is readonly?</param>
        public Int32Range(int fromIncluding = 0, uint count = 0, bool isReadonly = false)
        {
            if (fromIncluding + count > uint.MaxValue) throw new ArgumentOutOfRangeException(nameof(count));
            _FromIncluding = fromIncluding;
            _Count = count;
            ToIncluding = (int)(fromIncluding + count);
            IsReadonly = isReadonly;
        }

        /// <summary>
        /// Get the number at an index
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Number</returns>
        public int this[int index] => index >= 0 && index < _Count ? _FromIncluding + index : throw new ArgumentOutOfRangeException(nameof(index));

        /// <summary>
        /// From including
        /// </summary>
        public int FromIncluding
        {
            get => _FromIncluding;
            set
            {
                if (IsReadonly) throw new InvalidOperationException("Readonly");
                if (value + _Count > uint.MaxValue) throw new ArgumentOutOfRangeException(nameof(value));
                _FromIncluding = value;
                ToIncluding = (int)(value + _Count);
            }
        }

        /// <summary>
        /// Count of numbers in the range
        /// </summary>
        public uint Count
        {
            get => _Count + 1;
            set
            {
                if (IsReadonly) throw new InvalidOperationException("Readonly");
                value--;
                if (_FromIncluding + value > uint.MaxValue) throw new ArgumentOutOfRangeException(nameof(value));
                _Count = value;
                ToIncluding = (int)(_FromIncluding + value);
            }
        }

        /// <summary>
        /// To including
        /// </summary>
        public int ToIncluding { get; protected set; }

        /// <summary>
        /// From and to including
        /// </summary>
        public (int, int) FromToIncluding => (FromIncluding, ToIncluding);

        /// <summary>
        /// Is readonly?
        /// </summary>
        public bool IsReadonly { get; protected set; }

        /// <summary>
        /// Determine if a number matches within this range
        /// </summary>
        /// <param name="number">Number</param>
        /// <returns>Does match?</returns>
        public bool IsMatch(int number) => number >= _FromIncluding && number <= ToIncluding;

        /// <summary>
        /// Determine if a range matches within this range
        /// </summary>
        /// <param name="range">Range</param>
        /// <returns>Does match?</returns>
        public bool IsMatch(Int32Range range) => range.FromIncluding >= _FromIncluding && range.ToIncluding <= ToIncluding;

        /// <summary>
        /// Determine if a range matches within this range
        /// </summary>
        /// <param name="range">Range</param>
        /// <returns>Does match?</returns>
        public bool IsMatch(Int64Range range) => range.FromIncluding >= _FromIncluding && range.ToIncluding <= ToIncluding;

        /// <summary>
        /// Make this instance readonly
        /// </summary>
        /// <returns>This</returns>
        public Int32Range MakeReadonly()
        {
            IsReadonly = true;
            return this;
        }

        /// <summary>
        /// Create a writable instance clone
        /// </summary>
        /// <returns>Writable instance clone</returns>
        public Int32Range CreateWritable() => new Int32Range(_FromIncluding, _Count);

        /// <inheritdoc/>
        public object Clone() => new Int32Range(_FromIncluding, _Count, IsReadonly);

        /// <inheritdoc/>
        public override int GetHashCode() => (391 + _FromIncluding.GetHashCode()) * 23 + _Count.GetHashCode() * 23 + IsReadonly.GetHashCode();

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Int32Range range && range._FromIncluding == _FromIncluding && range._Count == _Count;

        /// <inheritdoc/>
        public override string ToString() => $"{_FromIncluding}..{ToIncluding} ({_Count + 1})";
    }
}
