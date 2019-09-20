using System;

namespace BusinessLogic
{
    public static class _
    {
        /// <summary>
        /// Gets all but the first element of array. Order is preserved.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items to process.</param>
        /// <returns>Returns a new array that is everything except the first element of `items`.</returns>
        public static T[] Tail<T>(T[] items)
        {
            // create our destination array
            var results = new T[items.Length - 1];

            // begin the loop at 1 so then we skip the 0th element of the `items` array
            for (int i = 1; i < items.Length; i++)
            {
                // i-1 because we need to copy the 1st element of `items` to the 0th element of `results`
                results[i - 1] = items[i];
            }
            
            return results;
        }

        /// <summary>
        /// Find the specific items that exist in both collections.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a">The first collection.</param>
        /// <param name="b">The second collection.</param>
        /// <returns>Returns a list of distinct items that exist in both `a` and `b`.</returns>
        public static T[] Intersect<T>(T[] a, T[] b)
        {
            // find which array has more items
            int length = a.Length;
            if (b.Length > length)
            {
                length = b.Length;
            }

            // create the array that will store the results
            T[] results = new T[length];

            // count how many items were found in the intersect
            int i = 0;
            
            // loop through both collections to find values that exist in both

            // the "outer" collection
            foreach (T va in a)
            {
                // the "inner" collection
                foreach (T vb in b)
                {
                    // do any of the items in the inner collection, match the current item in the outer collection?
                    if (va.Equals(vb))
                    {
                        //bool add = true;
                        //foreach (var r in results)
                        //{
                        //    add = add && !r.Equals(va);
                        //}

                        //if (add)
                        {
                            // we have a match
                            // * store the value in `results`
                            // * increment the result counter
                            results[i++] = va;

                            // break out of this loop to avoid duplicates
                            break;
                        }
                    }
                }
            }

            // resize the `results` array based on how many items found in the intersect
            // we need to resize because we declared `results` as the size of the largest array, A or B
            Array.Resize(ref results, i);
            return results;
        }
    }
}
