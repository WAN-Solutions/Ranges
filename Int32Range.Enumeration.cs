using System;
using System.Collections;
using System.Collections.Generic;

namespace wan24.Ranges
{
    public partial class Int32Range : IEnumerable<int>, IEnumerable
    {
        /// <summary>
        /// Enumerable range
        /// </summary>
        public IEnumerable<int> Range
        {
            get
            {
                for (int current = _FromIncluding, len = ToIncluding; current <= len; current++) yield return current;
            }
        }

        /// <summary>
        /// Get an enumerable range with stepping support
        /// </summary>
        /// <param name="stepping">Stepping</param>
        /// <returns>Enumerable range</returns>
        public IEnumerable<int> GetRange(int stepping)
        {
            if (stepping < 1 || stepping > _Count) throw new ArgumentOutOfRangeException(nameof(stepping));
            for (int current = _FromIncluding, len = ToIncluding; current <= len; current += stepping) yield return current;
        }

#if UNSAFE
        /// <summary>
        /// Create an array including all numbers from within the range
        /// </summary>
        /// <param name="stepping">Stepping</param>
        /// <returns>All numbers from within the range</returns>
        public unsafe int[] CreateRangeArray(int stepping = 1)
        {
            if (stepping < 1 || stepping > _Count) throw new ArgumentOutOfRangeException(nameof(stepping));
            int[] res = new int[(long)Math.Ceiling((_Count + 1d) / stepping)];
            fixed (int* resPtr = res)
            {
                int* currentPtr = resPtr;
                unchecked
                {
                    for (int current = _FromIncluding, len = ToIncluding; current <= len; *currentPtr = current, current += stepping, currentPtr++) ;
                }
            }
            return res;
        }
#endif

        /// <inheritdoc/>
        IEnumerator<int> IEnumerable<int>.GetEnumerator() => Range.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => Range.GetEnumerator();
    }
}
