using System;
using System.Collections;
using System.Collections.Generic;

namespace wan24.Ranges
{
    public partial class Int64Range : IEnumerable<long>, IEnumerable
    {
        /// <summary>
        /// Enumerable range
        /// </summary>
        public IEnumerable<long> Range
        {
            get
            {
                for (long current = _FromIncluding, len = ToIncluding; current <= len; current++) yield return current;
            }
        }

        /// <summary>
        /// Get an enumerable range with stepping support
        /// </summary>
        /// <param name="stepping">Stepping</param>
        /// <returns>Enumerable range</returns>
        public IEnumerable<long> GetRange(long stepping)
        {
            if (stepping < 1 || stepping > (decimal)_Count) throw new ArgumentOutOfRangeException(nameof(stepping));
            for (long current = _FromIncluding, len = ToIncluding; current <= len; current += stepping) yield return current;
        }

#if UNSAFE
        /// <summary>
        /// Create an array including all numbers from within the range
        /// </summary>
        /// <param name="stepping">Stepping</param>
        /// <returns>All numbers from within the range</returns>
        public unsafe long[] CreateRangeArray(long stepping = 1)
        {
            if (stepping < 1 || stepping > (decimal)_Count) throw new ArgumentOutOfRangeException(nameof(stepping));
            long[] res = new long[(long)Math.Ceiling((_Count + 1d) / stepping)];
            fixed (long* resPtr = res)
            {
                long* currentPtr = resPtr;
                unchecked
                {
                    for (long current = _FromIncluding, len = ToIncluding; current <= len; *currentPtr = current, current += stepping, currentPtr++) ;
                }
            }
            return res;
        }
#endif

        /// <inheritdoc/>
        IEnumerator<long> IEnumerable<long>.GetEnumerator() => Range.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => Range.GetEnumerator();
    }
}
