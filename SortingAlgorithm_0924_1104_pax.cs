// 代码生成时间: 2025-09-24 11:04:37
using System;
using System.Collections.Generic;
using System.Linq;

// 排序算法实现
namespace SortingAlgorithmDemo
{
    public class SortingAlgorithm
    {
        // 冒泡排序实现
        public int[] BubbleSort(int[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                throw new ArgumentException("Array is null or empty");
            }
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        // 交换元素
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
            return arr;
        }

        // 插入排序实现
        public int[] InsertionSort(int[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                throw new ArgumentException("Array is null or empty");
            }
            for (int i = 1; i < arr.Length; i++)
            {
                int key = arr[i];
                int j = i - 1;

                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
            return arr;
        }

        // 快速排序实现
        public int[] QuickSort(int[] arr, int low, int high)
        {
            if (arr == null || arr.Length == 0)
            {
                throw new ArgumentException("Array is null or empty");
            }
            if (low < high)
            {
                int pi = Partition(arr, low, high);

                QuickSort(arr, low, pi - 1);
                QuickSort(arr, pi + 1, high);
            }
            return arr;
        }

        private int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
            int temp = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp;
            return i + 1;
        }

        // 测试排序算法
        public static void Main(string[] args)
        {
            SortingAlgorithm sa = new SortingAlgorithm();
            int[] arr = { 64, 34, 25, 12, 22, 11, 90 };

            Console.WriteLine("Original array: ");
            foreach (int i in arr)
            {
                Console.Write(i + " ");
            }

            try
            {
                Console.WriteLine("
Sorted array using Bubble Sort: ");
                int[] sortedArr = sa.BubbleSort(arr.ToList().ToArray());
                foreach (int i in sortedArr)
                {
                    Console.Write(i + " ");
                }

                Console.WriteLine("
Sorted array using Insertion Sort: ");
                sortedArr = sa.InsertionSort(arr.ToList().ToArray());
                foreach (int i in sortedArr)
                {
                    Console.Write(i + " ");
                }

                Console.WriteLine("
Sorted array using Quick Sort: ");
                sortedArr = sa.QuickSort(arr.ToList().ToArray(), 0, arr.Length - 1);
                foreach (int i in sortedArr)
                {
                    Console.Write(i + " ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}