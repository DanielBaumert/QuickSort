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
        public enum CompareResult
        {
            Equal = 0, 
            Diffrent = 1
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] 
        public static void QuickSort<T>(this T[] items, Func<T, T, CompareResult> comparer) where T : unmanaged =>  items.QuickSort(0, items.Length - 1, comparer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static void QuickSort<T>(this T[] items, int startIndex, int endIndex, Func<T, T, CompareResult> comparer) where T : unmanaged
        {
            if (items.Length < 2) return;
            Stack<int> bounds = new Stack<int>();
            fixed (T* tPtr = items)
            {
                do
                {
                    if (bounds.Count != 0)
                    {
                        endIndex = bounds.Pop();
                        startIndex = bounds.Pop();
                    }

                    T pivot = *(tPtr + startIndex);
                    int pivotIndex = startIndex;

                    for (int i = startIndex + 1; i <= endIndex; i++)
                    {
                        if (comparer(pivot, *(tPtr + i)) > 0)
                        {
                            pivotIndex++;
                            if (pivotIndex != i)
                            {
                                Swap(tPtr, pivotIndex, i);
                            }
                        }
                    }

                    if (startIndex != pivotIndex)
                    {
                        Swap(tPtr, startIndex, pivotIndex);
                    }

                    if (pivotIndex + 1 < endIndex)
                    {
                        bounds.Push(pivotIndex + 1);
                        bounds.Push(endIndex);
                    }

                    if (startIndex < pivotIndex - 1)
                    {
                        bounds.Push(startIndex);
                        bounds.Push(pivotIndex - 1);
                    }

                } while (bounds.Count != 0);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe static void Swap<T>(T* tPtr, int i, int j) where T : unmanaged
        {
            T temp = *(tPtr + i);
            *(tPtr + i) = *(tPtr + j);
            *(tPtr + j) = temp;
        }
    }
}
