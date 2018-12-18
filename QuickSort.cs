 [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void QuickSort<T>(ref T[] items, int startIndex, int endIndex, Func<T, T, int> comparer) {
            if(items.Length < 2) return; 
            Stack<int> bounds = new Stack<int>();
            do {
                if(bounds.Count != 0) {
                    endIndex = bounds.Pop();
                    startIndex = bounds.Pop();
                }

                T pivot = items[startIndex];
                int pivotIndex = startIndex;

                for(int i = startIndex + 1; i <= endIndex; i++) {
                    if(comparer(pivot, items[i]) > 0) {
                        pivotIndex++;
                        if(pivotIndex != i) {
                            Swap(ref items,pivotIndex, i);
                        }
                    }
                }

                if(startIndex != pivotIndex) {
                    Swap(ref items, startIndex, pivotIndex);
                }

                if(pivotIndex + 1 < endIndex) {
                    bounds.Push(pivotIndex + 1);
                    bounds.Push(endIndex);
                }

                if(startIndex < pivotIndex - 1) {
                    bounds.Push(startIndex);
                    bounds.Push(pivotIndex - 1);
                }

            } while(bounds.Count != 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Swap<T>(ref T[] items, int i, int j) {
            T temp = items[i];
            items[i] = items[j];
            items[j] = temp;
        }
