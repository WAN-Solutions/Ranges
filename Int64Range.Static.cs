using System;
using System.Collections.Generic;
using System.IO;

namespace wan24.Ranges
{
    public partial class Int64Range
    {
        /// <summary>
        /// Zero range
        /// </summary>
        public static readonly Int64Range Zero = new Int64Range(isReadonly: true);

        /// <summary>
        /// Full range
        /// </summary>
        public static readonly Int64Range Full = new Int64Range(long.MinValue, ulong.MaxValue, isReadonly: true);

        /// <summary>
        /// Negative range
        /// </summary>
        public static readonly Int64Range Negative = new Int64Range(long.MinValue, (ulong)-(decimal)long.MinValue - 1, isReadonly: true);

        /// <summary>
        /// Positive range
        /// </summary>
        public static readonly Int64Range Positive = new Int64Range(1, long.MaxValue - 1, isReadonly: true);

        /// <summary>
        /// Create a range from/to including
        /// </summary>
        /// <param name="fromIncluding">From including</param>
        /// <param name="toIncluding">To including</param>
        /// <param name="isReadOnly">Is readonly?</param>
        /// <returns>Range</returns>
        public static Int64Range FromTo(long fromIncluding, long toIncluding, bool isReadOnly = false)
        {
            if (toIncluding < fromIncluding) throw new ArgumentOutOfRangeException(nameof(toIncluding));
            return new Int64Range(fromIncluding, (uint)(toIncluding - fromIncluding), isReadOnly);
        }

        /// <summary>
        /// Get ranges from an array of numbers
        /// </summary>
        /// <param name="arr">Array (having distinct values!)</param>
        /// <param name="sort">Sort first?</param>
        /// <returns>Ranges</returns>
        public static IEnumerable<Int64Range> FromArray(long[] arr, bool sort = true)
        {
            if (arr == null) throw new ArgumentNullException(nameof(arr));
            if (arr.Length < 1) yield break;
            if (sort) Array.Sort(arr);
            long start = 0, end = 0, index = 0;
            for (bool first = true; index < arr.LongLength; end++, index++, first = false)
            {
                if (first)
                {
                    end = start = arr[index];
                    continue;
                }
                if (end > arr[index]) throw new InvalidDataException("Sorting required");
                if (arr[index] == end) continue;
                yield return new Int64Range(start, (ulong)(end - 1 - start));
                end = start = arr[index];
            }
            yield return new Int64Range(start, (ulong)(end - 1 - start));
        }

        /// <summary>
        /// Get the number of ranges in an array of numbers
        /// </summary>
        /// <param name="arr">Array (having distinct values!)</param>
        /// <param name="sort">Sort first?</param>
        /// <returns>Number of contained ranges</returns>
        public static ulong CountRanges(long[] arr, bool sort = true)
        {
            if (arr == null) throw new ArgumentNullException(nameof(arr));
            if (arr.Length < 1) return 0;
            if (sort) Array.Sort(arr);
            bool first = true;
            long end = 0;
            ulong res = 0;
            for (int index = 0; index < arr.Length; end++, index++, first = false)
            {
                if (first)
                {
                    end = arr[index];
                    continue;
                }
                if (end > arr[index]) throw new InvalidDataException("Sorting required");
                if (arr[index] == end) continue;
                res++;
                end = arr[index];
            }
            return ++res;
        }

        /// <summary>
        /// Determine if an array contains multiple ranges
        /// </summary>
        /// <param name="arr">Array (having distinct values!)</param>
        /// <param name="sort">Sort first?</param>
        /// <returns>Contains more then one range?</returns>
        public static bool ContainsMultipleRanges(long[] arr, bool sort = true)
        {
            if (arr == null) throw new ArgumentNullException(nameof(arr));
            if (arr.Length < 1) return false;
            if (sort) Array.Sort(arr);
            long end = long.MinValue, index = 0;
            ulong found = 0;
            for (bool first = true; index < arr.Length; end++, index++, first = false)
            {
                if (first)
                {
                    end = arr[index];
                    continue;
                }
                if (end > arr[index]) throw new InvalidDataException("Sorting required");
                if (arr[index] == end) continue;
                end++;
                index++;
                if (end > arr[index]) throw new InvalidDataException("Sorting required");
                return true;
            }
            return ++found > 1;
        }

        /// <summary>
        /// Enumerate from start until <c>long.MaxValue</c>
        /// </summary>
        /// <param name="start">Start</param>
        /// <returns>Enumerable</returns>
        public static IEnumerable<long> EnumerateFrom(long start)
        {
            for (long len = long.MaxValue - 1; start < len; start++) yield return start;
            yield return long.MaxValue;
        }
    }
}
