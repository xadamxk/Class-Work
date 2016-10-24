using System;
using System.Text;
/* Made by: Adam K
 */

namespace MyPrograms
{
    internal class SortingClass
    {
        int count = 0;

        private static void Main()
        {
            // Local Variables
            long[] inputArray;

            // Instantiate Class
            SortingClass exampleData = new SortingClass();

            // Create array
            exampleData.Clear(out inputArray);

            // Merge Sort array
            exampleData.MergeSort(inputArray);

            // Reset array
            exampleData.Clear(out inputArray);

            // Heap Sort array
            exampleData.HeapSort(inputArray);

            // Reset array
            exampleData.Clear(out inputArray);

            // Quick SOrt array
            exampleData.QuickSort(inputArray);

            Console.ReadLine();
        }
        // Methods

        // Reset input array
        private void Clear(out long[] inputArray)
        {
            inputArray = new long[] {145, 545, 128, 68, 466};
        }

        // Swap
        private void Swap(ref long valOne, ref long valTwo)
        {
            valOne = valOne + valTwo;
            valTwo = valOne - valTwo;
            valOne = valOne - valTwo;
        }

        // Swap temp
        private void SwapWithTemp(ref long valOne, ref long valTwo)
        {
            long temp = valOne;
            valOne = valTwo;
            valTwo = temp;
        }

        // Display Output
        public string Display(long[] inputArray)
        {
            StringBuilder resultString = new StringBuilder();
            for (int i = 0; i < inputArray.Length; i++)
            {
                string temp = ", ";
                if (i == inputArray.Length-1)
                    temp = "";
                // Merge into one string
                resultString.Append(inputArray[i] + temp);
            }
            return resultString.ToString();
        }

        // Merge Sort Methods

        // Merge Sort
        private void MergeSort(long[] inputArray)
        {
            count = 0;
            Console.WriteLine("\nSort [" + Display(inputArray) + "] (Merge Sort):");
            int left = 0;
            int right = inputArray.Length - 1;
            RecursiveMergeSort(inputArray, left, right);
            Console.WriteLine("\tFinished: " +Display(inputArray));
        }

        // Recursion Merge Sort
        private void RecursiveMergeSort(long[] inputArray, int left, int right)
        {
            int mid;

            if (left < right)
            {
                mid = (left + right)/2;
                // Left
                RecursiveMergeSort(inputArray, left, mid);
                // Right
                RecursiveMergeSort(inputArray, (mid + 1), right);

                // Recursion baby!
                MergeSorted(inputArray, left, mid, right);
                Console.WriteLine("\tStep " + count + ": " + Display(inputArray));
                count++;
            }
        }

        // Merge sorted lists
        private void MergeSorted(long[] inputArray, int left, int mid, int right)
        {
            int index = 0;
            int total_elements = right - left + 1; //BODMAS rule
            int right_start = mid + 1;
            int temp_location = left;
            long[] tempArray = new long[total_elements];

            // If left is less than & right is greater
            while ((left <= mid) && right_start <= right)
            {
                if (inputArray[left] <= inputArray[right_start])
                    tempArray[index++] = inputArray[left++];

                else
                    tempArray[index++] = inputArray[right_start++];
            }

            // swap if left > mid
            if (left > mid)
                for (int j = right_start; j <= right; j++)
                    tempArray[index++] = inputArray[right_start++];

                // otherwise keep
            else
                for (int j = left; j <= mid; j++)
                    tempArray[index++] = inputArray[left++];

            // rebuild
            for (int i = 0, j = temp_location; i < total_elements; i++, j++)
                inputArray[j] = tempArray[i];
        }

        // Heap Sort Methods

        // Heap Sort
        private void HeapSort(long[] inputArray)
        {
            Console.WriteLine("\nSort [" + Display(inputArray) + "] (Heap Sort):");
            for (int index = (inputArray.Length/2) - 1; index >= 0; index--)
                Heapify(inputArray, index, inputArray.Length);

            for (int index = inputArray.Length - 1; index >= 1; index--)
            {
                // Recursion :D
                SwapWithTemp(ref inputArray[0], ref inputArray[index]);
                Heapify(inputArray, 0, index - 1);
            }
            Console.WriteLine("\tFinished: " + Display(inputArray));
        }

        // Recursion Heap Sort - Heapify (Black magic)
        private void Heapify(long[] inputArray, int root, int bottom)
        {
            bool completed = false;

            // If root is not leaf & if heap is not done
            while ((root*2 <= bottom) && (!completed))
            {
                int maxChild;
                if (root*2 == bottom)
                    maxChild = root*2;
                else if (inputArray[root*2] > inputArray[root*2 + 1])
                    maxChild = root*2;
                else
                    maxChild = root*2 + 1;

                if (inputArray[root] < inputArray[maxChild])
                {
                    count++;
                    Swap(ref inputArray[root], ref inputArray[maxChild]);
                    root = maxChild;
                    Console.WriteLine("\tStep" + count + ": " + Display(inputArray));
                    
                }
                else
                {
                    completed = true;
                }
            }
        }

        // Quick Sort Methods 

        // Quick Sort
        private void QuickSort(long[] inputArray)
        {
            Console.WriteLine("\nSort [" + Display(inputArray) + "] (Quick Sort):");
            int left = 0;
            int right = inputArray.Length - 1;
            RecursiveQuickSort(inputArray, left, right);
            Console.WriteLine(Display(inputArray));
        }

        // Recursion Quick Sort
        private void RecursiveQuickSort(long[] inputArray, int left, int right)
        {
            int pivotNewIndex = Partition(inputArray, left, right);

            if (left < pivotNewIndex - 1)
            {
                RecursiveQuickSort(inputArray, left, pivotNewIndex - 1);
                //Console.WriteLine(Display(inputArray));
            }

            if (pivotNewIndex < right)
            {
                RecursiveQuickSort(inputArray, pivotNewIndex, right);
                //Console.WriteLine(Display(inputArray));
            }
        }

        // Quick Sort Merge
        private int Partition(long[] inputArray, int left, int right)
        {
            int i = left, j = right;
            long pivot = inputArray[(left + right)/2];

            // If left is less than right
            while (i <= j)
            {
                while (inputArray[i] < pivot)
                    i++;
                while (inputArray[j] > pivot)
                    j--;
                if (i <= j)
                {
                    SwapWithTemp(ref inputArray[i], ref inputArray[j]);
                    i++;
                    j--;
                    //Console.WriteLine(Display(inputArray));
                }
            }
            return i;
        }
    }
}
