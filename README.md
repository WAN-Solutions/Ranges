# Ranges

Not sure why the numeric range support in .NET is limited to 
`Enumerable.Range`, but for a project I required something more, and that let 
me write a numeric range class, that has some advantages compared to the .NET 
built in enumerable - for example:

- It's not an enumerable type (but can provide one)
- Readonly is optional (a range may be moved, shrinked and expanded as 
required)
- Creates a range array using pointer (which should be faster than creating an 
array from an enumerable)
- Contains several operators for making range specific operations more easy

If (parts) your code contain a lot on numeric range handling, you migh want to 
use this library.

These two types are exported:

- `Int32Range` - for ranges that match the full signed 32 bit integer numeric 
range
- `Int64Range` - for ranges that match the full signed 64 bit integer numeric 
range

Those can be casted in ary direction, as long as the numbers fit the range of 
the target integer type.

## NuGET

With the coming release version 1.0.0 a NuGET packet will be available.

## Basic usages

```cs
using System;
using wan24.Ranges;

// Create a range from 1 (including) to 10 (including)
Int32Range range = Int32Range.FromTo(1, 10);
Console.WriteLine(range);// 1..10 (10)
Console.WriteLine(range[0]);// 1

// The same using the constructor
Int32Range range = new(1, 9);// 9 is the number to count (1 + 9 will be the "to including" value)
Console.WriteLine(range);// 1..10 (10)

// Get a range array
int[] range = Int32Range.FromTo(1, 10).CreateRangeArray();
Console.WriteLine($"{string.Join(",", range)}");// 1,2,3,4,5,6,7,8,9,10
range = Int32Range.FromTo(1, 10).CreateRangeArray(2);
Console.WriteLine($"{string.Join(",", range)}");// 1,3,5,7,9 (because using stepping 2 when creating the array)

// Enumerate the range
foreach(int n in Int32Range.FromTo(1, 10).Range)
	Console.WriteLine(n);

// Enumerate until int.MaxValue or unless break
foreach(int n in Int32Range.EnumerateFrom(1))
{
	Console.WriteLine(n);
	if(n == 10) break;
}

// Combine two ranges
Int32Range range1 = Int32Range.FromTo(1, 10);
Int32Range range2 = Int32Range.FromTo(15, 20);
Int32Range combined = range1 + range2;
Console.WriteLine(combined);// 1..20 (20)

// Expand and shrink a range
Console.WriteLine(Int3Range.FromTo(1, 10) + 1);// 1..11 (11)
Console.WriteLine(Int3Range.FromTo(1, 10) - 1);// 1..9 (9)

// (Un)Shift a range
Console.WriteLine(Int3Range.FromTo(1, 10) >> 1);// 2..11 (10)
Console.WriteLine(Int3Range.FromTo(1, 10) << 1);// 0..9 (10)

// Split a range
Int32Range range = Int32Range.FromTo(1, 12);
Int32Range[] ranges[] = range / 5;
Console.WriteLine(ranges[0]);// 1..5 (5)
Console.WriteLine(ranges[1]);// 6..10 (5)
Console.WriteLine(ranges[2]);// 11..12 (2)

// Determine if a number matches within a range
Int32Range range = Int32Range.FromTo(1, 10);
Console.WriteLine(range == 1);// True
Console.WriteLine(range == 11);// False

// Split a numeric array into contained ranges
int[] arr = new int[] { 1, 2, 3, 5, 7, 6 };// Will be sorted, if unsorted
Int32Range[] ranges = Int32Range.FromArray(arr).ToArray();
Console.WriteLine(ranges[0]);// 1..3 (3)
Console.WriteLine(ranges[1]);// 5..7 (3)
```

### Good to know

If you use a readonly range with operators, a resulting range will always be a 
new range instance which is writable - avoid this pitfall:

```cs
Int32Range range = Int32Range.FromTo(1, 10, isReadonly: true);

// The ++ operator on a readonly source will create a new instance and set it to the variable
Int32Range newRange = range;
Console.WriteLine(newRange.IsReadonly);// True
newRange++;
Console.WriteLine(newRange.IsReadonly);// False
Console.WriteLine(range.Equals(newRange));// False
Console.WriteLine(range.IsReadonly);// True
Console.WriteLine(range);// 1..10 (10)
Console.WriteLine(newRange);// 1..11 (11)

// The + operator will create a new instance on a readonly source
newRange = range + 1;
Console.WriteLine(range.Equals(newRange));// False
```

## Compiler constants

### `UNSAFE`

Remove this compiler constant to create a build without "unsafe" methods. This 
will affect:

- `Int32Range.CreateRangeArray`
- `Int64Range.CreateRangeArray`

## Version history

### Version 0.0.0 (2021-08-21)

- Initial prototype version (MIT license)
- 32 bit signed integer range
- 64 bit signed integer range
- GitHub open source project
