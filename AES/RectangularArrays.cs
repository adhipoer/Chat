
internal static partial class RectangularArrays
{
    internal static sbyte[][] ReturnRectangularSbyteArray(int size1, int size2)
    {
        sbyte[][] newArray;
        if (size1 > -1)
        {
            newArray = new sbyte[size1][];
            if (size2 > -1)
            {
                for (int array1 = 0; array1 < size1; array1++)
                {
                    newArray[array1] = new sbyte[size2];
                }
            }
        }
        else
            newArray = null;

        return newArray;
    }
}