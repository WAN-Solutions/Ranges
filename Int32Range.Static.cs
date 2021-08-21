using System;
using System.Collections.Generic;
using System.IO;

namespace wan24.Ranges
{
    public partial class Int32Range
    {
        /// <summary>
        /// Zero range
        /// </summary>
        public static readonly Int32Range Zero = new Int32Range(isReadonly: true);

        /// <summary>
        /// Full range
        /// </summary>
        public static readonly Int32Range Full = new Int32Range(int.MinValue, uint.MaxValue, isReadonly: true);

        /// <summary>
        /// Negative range
        /// </summary>
        public static readonly Int32Range Negative = new Int32Range(int.MinValue, (uint)-(long)int.MinValue - 1, isReadonly: true);

        /// <summary>
        /// Positive range
        /// </summary>
        public static readonly Int32Range Positive = new Int32Range(1, int.MaxValue - 1, isReadonly: true);

        /// <summary>
        /// Create a range from/to including
        /// </summary>
        /// <param name="fromIncluding">From including</param>
        /// <param name="toIncluding">To including</param>
        /// <param name="isReadOnly">Is readonly?</param>
        /// <returns>Range</returns>
        public static Int32Range FromTo(int fromIncluding, int toIncluding, bool isReadOnly = false)
        {
            if (toIncluding < fromIncluding) throw new ArgumentOutOfRangeException(nameof(toIncluding));
            return new Int32Range(fromIncluding, (uint)(toIncluding - fromIncluding), isReadOnly);
        }

        /// <summary>
        /// Get ranges from an array of numbers
        /// </summary>
        /// <param name="arr">Array (having distinct values!)</param>
        /// <param name="sort">Sort first?</param>
        /// <returns>Ranges</returns>
        public static IEnumerable<Int32Range> FromArray(int[] arr, bool sort = true)
        {
            if (arr == null) throw new ArgumentNullException(nameof(arr));
            if (arr.Length < 1) yield break;
            if (sort) Array.Sort(arr);
            int start = 0, end = int.MinValue;
            long index = 0;
            for (bool first = true; index < arr.LongLength; end++, index++, first = false)
            {
                if (first)
                {
                    end = start = arr[index];
                    continue;
                }
                if (end > arr[index]) throw new InvalidDataException("Sorting required");
                if (arr[index] == end) continue;
                yield return new Int32Range(start, (uint)(end - 1 - start));
                end = start = arr[index];
            }
            yield return new Int32Range(start, (uint)(end - 1 - start));
        }

        /// <summary>
        /// Get the number of ranges in an array of numbers
        /// </summary>
        /// <param name="arr">Array (having distinct values!)</param>
        /// <param name="sort">Sort first?</param>
        /// <returns>Number of contained ranges</returns>
        public static uint CountRanges(int[] arr, bool sort = true)
        {
            if (arr == null) throw new ArgumentNullException(nameof(arr));
            if (arr.Length < 1) return 0;
            if (sort) Array.Sort(arr);
            int end = int.MinValue, index = 0;
            uint res = 0;
            for (bool first = true; index < arr.Length; end++, index++, first = false)
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
        public static bool ContainsMultipleRanges(int[] arr, bool sort = true)
        {
            if (arr == null) throw new ArgumentNullException(nameof(arr));
            if (arr.Length < 1) return false;
            if (sort) Array.Sort(arr);
            int end = int.MinValue, index = 0;
            uint found = 0;
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
        /// Enumerate from start until <c>int.MaxValue</c>
        /// </summary>
        /// <param name="start">Start</param>
        /// <returns>Enumerable</returns>
        public static IEnumerable<int> EnumerateFrom(int start)
        {
            for (int len = int.MaxValue - 1; start < len; start++) yield return start;
            yield return int.MaxValue;
        }
    }
}
