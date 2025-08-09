// 代码生成时间: 2025-08-10 05:04:51
using System;
using System.Linq;

// 定义一个类用于实现排序算法
public class SortingAlgorithm
{
    // 选择排序算法的实现
    public void SelectionSort(int[] array)
# FIXME: 处理边界情况
    {
        if (array == null)
# 优化算法效率
        {
            throw new ArgumentNullException(nameof(array), "数组不能为空");
        }

        for (int i = 0; i < array.Length; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < array.Length; j++)
# NOTE: 重要实现细节
            {
                if (array[j] < array[minIndex])
                {
                    minIndex = j;
                }
            }
            if (minIndex != i)
            {
                Swap(ref array[i], ref array[minIndex]);
            }
        }
    }

    // 插入排序算法的实现
    public void InsertionSort(int[] array)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array), "数组不能为空");
        }

        for (int i = 1; i < array.Length; i++)
# 改进用户体验
        {
            int key = array[i];
            int j = i - 1;

            while (j >= 0 && array[j] > key)
            {
                array[j + 1] = array[j];
                j = j - 1;
            }
            array[j + 1] = key;
        }
    }

    // 冒泡排序算法的实现
    public void BubbleSort(int[] array)
    {
        if (array == null)
        {
# NOTE: 重要实现细节
            throw new ArgumentNullException(nameof(array), "数组不能为空");
        }

        for (int i = 0; i < array.Length - 1; i++)
# 优化算法效率
        {
            for (int j = 0; j < array.Length - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    Swap(ref array[j], ref array[j + 1]);
                }
# TODO: 优化性能
            }
        }
    }

    // 快速排序算法的实现
# FIXME: 处理边界情况
    public void QuickSort(int[] array, int low, int high)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array), "数组不能为空");
# TODO: 优化性能
        }

        if (low < high)
        {
            int pi = Partition(array, low, high);
            QuickSort(array, low, pi - 1);
            QuickSort(array, pi + 1, high);
        }
    }

    // 快速排序算法中的划分方法
    private int Partition(int[] array, int low, int high)
    {
        int pivot = array[high];
        int i = (low - 1);
        for (int j = low; j < high; j++)
        {
# FIXME: 处理边界情况
            if (array[j] < pivot)
            {
                i++;
# TODO: 优化性能
                Swap(ref array[i], ref array[j]);
            }
        }
        Swap(ref array[i + 1], ref array[high]);
        return (i + 1);
    }

    // 辅助方法，用于交换两个整数的值
    private void Swap(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
# NOTE: 重要实现细节
    }
}
