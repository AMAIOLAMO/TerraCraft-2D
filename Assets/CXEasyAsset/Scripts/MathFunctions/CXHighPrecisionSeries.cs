using System;
using System.Collections.Generic;

namespace CXEasyAsset
{
    class HPInt
    {
        public List<int> ValList = new List<int>();
        public HPInt(string value)
        {
            ValList.Clear();
            for (int i = 0; i < value.Length; i++)
            {
                ValList.Add((int)value[i]);
            }
        }
        public HPInt(int value)
        {
            ValList.Clear();
            do
            {

                ValList.Add(value % 10);
                value /= 10;

            } while (value != 0);
            ValList.Reverse();
        }
        // public void Add(int value)
        // {
        //     //this will add the val in the val list
        //     int currentPointer = ValList - 1;
        //     ValList[currentPointer] += value;
        //     int lastMore = 0;
        //     while (ValList[currentPointer] < 0 || ValList[currentPointer] > 9)
        //     {
        //         //check if the current pointer is smaller than 0
        //         if (currentPointer > 0)
        //         {
        //             if (ValList[currentPointer] < 0)
        //             {
        //                 lastMore = ValList[currentPointer];
        //                 ValList[currentPointer] += value;
        //             }
        //             else if (ValList[currentPointer] > 9)
        //             {

        //             }
        //         }

        //     }
        // }

    }

}