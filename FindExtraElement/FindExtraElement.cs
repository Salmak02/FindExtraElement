using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class FindExtraElement
    {
        #region YOUR CODE IS HERE
        /// <summary>
        /// Find index of extra element in first array
        /// </summary>
        /// <param name="arr1">first sorted array with an extra element</param>
        /// <param name="arr2">second sorted array</param>
        /// <returns>index of the extra element in arr1</returns>
        public static int FindIndexOfExtraElement(int[] arr1, int[] arr2)
        {
            int e = Math.Max(arr1.Length, arr2.Length), s = 0, mid,e2 =e;
            //bool x = arr1.GetLength(0) > arr2.GetLength(0);
            //if (arr1.Length < arr2.Length) return 10;
            int ans = e;
            if (e == 1)
            {
                return 0;
            }else if (arr1[e-1] != arr2[e-2] && arr1[e-2] == arr2[e-2])
            {
                return e - 1;
            }
            e -= 1;
            while (s < e)
            {          
                // 1 2 1 
               
                mid = (s + e) / 2;
                 if(mid == e2 - 1)
                {
                    ans = mid;break;
                }
                if (mid == 0 || mid != 0 && arr1[mid] != arr2[mid] && arr1[mid - 1] == arr2[mid - 1])  //1 2 2 2 2 2
                {
                    ans = mid; break;

                }
                else if ( arr1[mid] != arr2[mid] )
                {
                    e = mid;

                    ans = mid;
                }
                else
                {
                    s = mid;
                }
            }
            return ans;
        }

        #endregion
    }
}
