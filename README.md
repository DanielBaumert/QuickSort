# QuickSort for C#  as example


```csharp
PointF[] exampleArray = new PointF[]{
  new PointF(.0F, 5f),
  new PointF(.0F, 2f),
  new PointF(.0F, 4f),
  new PointF(.0F, 4f),
  new PointF(.0F, 1f),
  new PointF(.0F, 3f),
};
End is the select item which is to compare with selected(start)
Windows.QuickSort(ref exampleArray, (start, end) => {
  float val = end.Y - start.Y;
  return val != 0 ? val > 0 ? 1 : -1 : 0;
});
```
