using System;

namespace wan24.Ranges
{
    public partial class Int64Range
    {
        /// <summary>
        /// Create a new range that matches two ranges
        /// </summary>
        /// <param name="a">Range 1</param>
        /// <param name="b">Range 2</param>
        /// <returns>Range 1+2 matching new range</returns>
        public static Int64Range operator +(Int64Range a, Int64Range b) => FromTo(Math.Min(a._FromIncluding, b._FromIncluding), Math.Max(a.ToIncluding, b.ToIncluding));

        /// <summary>
        /// Create a new range that matches two ranges
        /// </summary>
        /// <param name="a">Range 1</param>
        /// <param name="b">Range 2</param>
        /// <returns>Range 1+2 matching new range</returns>
        public static Int64Range operator +(Int64Range a, Int32Range b) => FromTo(Math.Min(a._FromIncluding, b.FromIncluding), Math.Max(a.ToIncluding, b.ToIncluding));

        /// <summary>
        /// Extend a range
        /// </summary>
        /// <param name="range">Range</param>
        /// <param name="count">Extend by count</param>
        /// <returns>Updated range or new range (if readonly)</returns>
        public static Int64Range operator +(Int64Range range, uint count)
        {
            if (range.IsReadonly) return new Int64Range(range._FromIncluding, range._Count + count);
            range.Count += count;
            return range;
        }

        /// <summary>
        /// Extend a range
        /// </summary>
        /// <param name="range">Range</param>
        /// <returns>Updated range or new range (if readonly)</returns>
        public static Int64Range operator ++(Int64Range range) => range + 1;

        /// <summary>
        /// Shrink a range
        /// </summary>
        /// <param name="range">Range</param>
        /// <param name="count">Shrink by count</param>
        /// <returns>Updated range or new range (if readonly)</returns>
        public static Int64Range operator -(Int64Range range, ulong count)
        {
            if (range.IsReadonly) return new Int64Range(range._FromIncluding, range._Count - count);
            range.Count -= count;
            return range;
        }

        /// <summary>
        /// Shrink a range
        /// </summary>
        /// <param name="range">Range</param>
        /// <returns>Updated range or new range (if readonly)</returns>
        public static Int64Range operator --(Int64Range range) => range - 1;

        /// <summary>
        /// Shift a range (decrease the from including)
        /// </summary>
        /// <param name="range">Range</param>
        /// <param name="count">Shift by count</param>
        /// <returns>Updated range or new range (if readonly)</returns>
        public static Int64Range operator <<(Int64Range range, int count)
        {
            if (range.IsReadonly) return new Int64Range(range._FromIncluding - count, range._Count);
            range.FromIncluding -= count;
            return range;
        }

        /// <summary>
        /// Shift a range (increase the from including)
        /// </summary>
        /// <param name="range">Range</param>
        /// <param name="count">Shift by count</param>
        /// <returns>Updated range or new range (if readonly)</returns>
        public static Int64Range operator >>(Int64Range range, int count)
        {
            if (range.IsReadonly) return new Int64Range(range._FromIncluding + count, range._Count);
            range.FromIncluding += count;
            return range;
        }

        /// <summary>
        /// Divide a range into multiple ranges
        /// </summary>
        /// <param name="range">Range</param>
        /// <param name="divisor">Divisor</param>
        /// <returns>Divided ranges</returns>
        public static Int64Range[] operator /(Int64Range range, long divisor)
        {
            if (divisor < 1) throw new ArgumentOutOfRangeException(nameof(divisor));
            uint count = (uint)Math.Ceiling((double)(range._Count + 1) / divisor);
            Int64Range[] res = new Int64Range[count];
            long current = range._FromIncluding;
            for (
                ulong i = 0, len, total = 0;
                i < count;
                res[i] = new Int64Range(current, len = (ulong)Math.Min(divisor - 1, (decimal)range._Count - (ulong)divisor * i)), current += (long)len + 1, total -= len + 1, i++
                ) ;
            return res;
        }

        /// <summary>
        /// Determine if range 2 matches into range 1
        /// </summary>
        /// <param name="a">Range 1</param>
        /// <param name="b">Range 2</param>
        /// <returns>Is match?</returns>
        public static bool operator ==(Int64Range a, Int64Range b) => a.IsMatch(b);

        /// <summary>
        /// Determine if range 2 does not match into range 1
        /// </summary>
        /// <param name="a">Range 1</param>
        /// <param name="b">Range 2</param>
        /// <returns>Is not match?</returns>
        public static bool operator !=(Int64Range a, Int64Range b) => !(a == b);

        /// <summary>
        /// Determine if range 2 matches into range 1
        /// </summary>
        /// <param name="a">Range 1</param>
        /// <param name="b">Range 2</param>
        /// <returns>Is match?</returns>
        public static bool operator ==(Int64Range a, Int32Range b) => a.IsMatch(b);

        /// <summary>
        /// Determine if range 2 does not match into range 1
        /// </summary>
        /// <param name="a">Range 1</param>
        /// <param name="b">Range 2</param>
        /// <returns>Is not match?</returns>
        public static bool operator !=(Int64Range a, Int32Range b) => !(a == b);

        /// <summary>
        /// Determine if a range matches a number
        /// </summary>
        /// <param name="range">Range</param>
        /// <param name="number">Number</param>
        /// <returns>Number is in range?</returns>
        public static bool operator ==(Int64Range range, long number) => range.IsMatch(number);

        /// <summary>
        /// Determine if a range does not match a number
        /// </summary>
        /// <param name="range">Range</param>
        /// <param name="number">Number</param>
        /// <returns>Number is not in range?</returns>
        public static bool operator !=(Int64Range range, long number) => !(range == number);

        /// <summary>
        /// Determine if range 1 is smaller than range 2
        /// </summary>
        /// <param name="a">Range 1</param>
        /// <param name="b">Range 2</param>
        /// <returns>Is smaller?</returns>
        public static bool operator <(Int64Range a, Int64Range b) => a._Count < b._Count;

        /// <summary>
        /// Determine if range 1 is larger than range 2
        /// </summary>
        /// <param name="a">Range 1</param>
        /// <param name="b">Range 2</param>
        /// <returns>Is larger?</returns>
        public static bool operator >(Int64Range a, Int64Range b) => a._Count > b._Count;

        /// <summary>
        /// Determine if range 1 is smaller than range 2
        /// </summary>
        /// <param name="a">Range 1</param>
        /// <param name="b">Range 2</param>
        /// <returns>Is smaller?</returns>
        public static bool operator <(Int64Range a, Int32Range b) =>  a._Count < b.Count;

        /// <summary>
        /// Determine if range 1 is larger than range 2
        /// </summary>
        /// <param name="a">Range 1</param>
        /// <param name="b">Range 2</param>
        /// <returns>Is larger?</returns>
        public static bool operator >(Int64Range a, Int32Range b) => a._Count > b.Count;

        /// <summary>
        /// Determine if range 1 is smaller or equal than/to range 2
        /// </summary>
        /// <param name="a">Range 1</param>
        /// <param name="b">Range 2</param>
        /// <returns>Is smaller or equal?</returns>
        public static bool operator <=(Int64Range a, Int64Range b) => a._Count <= b._Count;

        /// <summary>
        /// Determine if range 1 is larger or equal than/to range 2
        /// </summary>
        /// <param name="a">Range 1</param>
        /// <param name="b">Range 2</param>
        /// <returns>Is larger or equal?</returns>
        public static bool operator >=(Int64Range a, Int64Range b) => a._Count >= b._Count;

        /// <summary>
        /// Determine if range 1 is smaller or equal than/to range 2
        /// </summary>
        /// <param name="a">Range 1</param>
        /// <param name="b">Range 2</param>
        /// <returns>Is smaller or equal?</returns>
        public static bool operator <=(Int64Range a, Int32Range b) => a._Count <= b.Count;

        /// <summary>
        /// Determine if range 1 is larger or equal than/to range 2
        /// </summary>
        /// <param name="a">Range 1</param>
        /// <param name="b">Range 2</param>
        /// <returns>Is larger or equal?</returns>
        public static bool operator >=(Int64Range a, Int32Range b) => a._Count >= b.Count;

        /// <summary>
        /// Determine if a number exceeds a ranges highest number
        /// </summary>
        /// <param name="range">Range</param>
        /// <param name="number">Number</param>
        /// <returns>Does exceed?</returns>
        public static bool operator <(Int64Range range, long number) => range.ToIncluding < number;

        /// <summary>
        /// Determine if a number is lower than a ranges lowest number
        /// </summary>
        /// <param name="range">Range</param>
        /// <param name="number">Number</param>
        /// <returns>Is lower?</returns>
        public static bool operator >(Int64Range range, long number) => range._FromIncluding > number;

        /// <summary>
        /// Determine if a ranges lowest number is smaller than a number or the range matches the number
        /// </summary>
        /// <param name="range">Range</param>
        /// <param name="number">Number</param>
        /// <returns>Is smaller or equal?</returns>
        public static bool operator <=(Int64Range range, long number) => range.ToIncluding <= number;

        /// <summary>
        /// Determine if a ranges highest number is larger than a number or the range matches the number
        /// </summary>
        /// <param name="range">Range</param>
        /// <param name="number">Number</param>
        /// <returns>Is larger or equal?</returns>
        public static bool operator >=(Int64Range range, long number) => range._FromIncluding >= number;
    }
}
