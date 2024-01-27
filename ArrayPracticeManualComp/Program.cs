using System;
using System.Collections.Generic;
using System.Linq;

namespace ArrayPracticeProblemsManualComp {
    class Program {
        static void Main(string[] args) {
            int[] nums1 = { 1, 2, 3 };
            int[] nums2 = { 1, 1, 2 };
            CompArray(nums1, nums2);
            int[] nums3 = { 1, 2, 3 };
            int[] nums4 = { 2, 3, 5 };
            ResizeArray(nums3, nums4);
        }

        static void CompArray(int[] array1, int[] array2) {
            for(int i = 0; i < array1.Length; i++) {
                for (int i2 = 0; i2 < array2.Length; i2++) {
                    if(array1[i] == array2[i2]) {
                        Console.Write("Duplicate: " + array1[i] + "\n");
                    }
                }
            }
        }

        static void ResizeArray(int[] array1, int[] array2) {

            //State of living
            Console.WriteLine("------------------");

            //Declare largeArr, the combined array of array1 and array2
            int[] largeArr = new int[array1.Length + array2.Length];
            //Parameter 1 -destination & parameter 2 - starting index
            array1.CopyTo(largeArr, 0);
            array2.CopyTo(largeArr, array1.Length);
            int amountOfValues = array1.Length + array2.Length;

            //Sort array code from previous assignment ;-;
            for (int i2 = 1; i2 < largeArr.Length*largeArr.Length; i2++) {
                for (int i = 1; i < largeArr.Length; i++) {
                    while (largeArr[i] < largeArr[i - 1]) {
                        int temp = largeArr[i - 1];
                        largeArr[i - 1] = largeArr[i];
                        largeArr[i] = temp;
                    }
                }
            }

            //Since array is sorted, check for duplicates
            for(int g = 1; g < largeArr.Length; g++) {
                if (largeArr[g - 1] == largeArr[g]) {
                    //Largest int used as marker: 2147483647
                    largeArr[g] = 2147483647;
                    amountOfValues--;
                }
                else { }
            }

            //Sort the array again, now the duplicated elements will be put at the end
            for (int i4 = 1; i4 < largeArr.Length* largeArr.Length; i4++) {
                for (int i3 = 1; i3 < largeArr.Length; i3++) {
                    while (largeArr[i3] < largeArr[i3 - 1]) {
                        int temp = largeArr[i3 - 1];
                        largeArr[i3 - 1] = largeArr[i3];
                        largeArr[i3] = temp;
                    }
                }
            }

            //Index only the items that are not -2147483647
            Console.Write("Your NEW List! : {");
            for(int rptNmLs = 0; rptNmLs < amountOfValues; rptNmLs++) {
                Console.Write(largeArr[rptNmLs] );
                if(rptNmLs != amountOfValues - 1) {
                    Console.Write(", ");
                }
                else if(rptNmLs == amountOfValues - 1) {
                    Console.Write("}");
                }
            }

            //State of living
            Console.WriteLine("\n------------------");

        }
        }



    }