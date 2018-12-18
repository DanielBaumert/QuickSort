#region MIT License

// Copyright (c) 2018-2022 QuickSort - Daniel Baumert
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#endregion 

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections {

    public static class Windows {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void QuickSort<T>(ref T[] items, Func<T, T, int> comparer) => QuickSort(ref items, 0, items.Length - 1, comparer);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void QuickSort<T>(ref T[] items, int startIndex, int endIndex, Func<T, T, int> comparer) {
        if (items.Length < 2) return;
        Stack<int> bounds = new Stack<int>();
        do {
            if (bounds.Count != 0) {
                endIndex = bounds.Pop();
                startIndex = bounds.Pop();
            }

            T pivot = items[startIndex];
            int pivotIndex = startIndex;

            for (int i = startIndex + 1; i <= endIndex; i++) {
                if (comparer(pivot, items[i]) > 0) {
                    pivotIndex++;
                    if (pivotIndex != i) {
                        Swap(ref items, pivotIndex, i);
                    }
                }
            }

            if (startIndex != pivotIndex) {
                Swap(ref items, startIndex, pivotIndex);
            }

            if (pivotIndex + 1 < endIndex) {
                bounds.Push(pivotIndex + 1);
                bounds.Push(endIndex);
            }

            if (startIndex < pivotIndex - 1) {
                bounds.Push(startIndex);
                bounds.Push(pivotIndex - 1);
            }

        } while (bounds.Count != 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Swap<T>(ref T[] items, int i, int j) {
        T temp = items[i];
        items[i] = items[j];
        items[j] = temp;
    }
}
