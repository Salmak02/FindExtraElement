using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Problem
{

    public class Problem : ProblemBase, IProblem
    {
        #region ProblemBase Methods
        public override string ProblemName { get { return "FindExtraIndex"; } }

        public override void TryMyCode()
        {
            //last element
            int[] arr1 = { 2, 4, 6, 9, 10, 12, 13, 20 };
            int[] arr2 = { 2, 4, 6, 9, 10, 12, 13 };
            var output = FindExtraElement.FindIndexOfExtraElement(arr1, arr2);
            PrintCase(arr1, arr2, output, 7);
            Console.WriteLine();

            //right
            int[] arr11 = { 2, 4, 6, 9, 10, 11, 12, 13 };
            int[] arr12 = { 2, 4, 6, 9, 10, 12, 13 };
            output = FindExtraElement.FindIndexOfExtraElement(arr11, arr12);
            PrintCase(arr11, arr12, output, 5);
            Console.WriteLine();

            //It is Mid
            int[] arr111 = { 2, 4, 6, 7, 9, 10, 12, 13 };
            int[] arr222 = { 2, 4, 6, 9, 10, 12, 13 };
            output = FindExtraElement.FindIndexOfExtraElement(arr111, arr222);
            PrintCase(arr111, arr222, output, 3);
            Console.WriteLine();


            //It left
            int[] arr122 = { 2, 4, 5, 6, 7, 10, 12, 13 };
            int[] arr211 = { 2, 4, 6, 7, 10, 12, 13 };
            output = FindExtraElement.FindIndexOfExtraElement(arr122, arr211);
            PrintCase(arr122, arr211, output, 2);
            Console.WriteLine();


            //It first
            int[] arr123 = { 0, 2, 4, 6, 7, 10, 12, 13 };
            int[] arr213 = { 2, 4, 6, 9, 10, 12, 13 };
            output = FindExtraElement.FindIndexOfExtraElement(arr123, arr213);
            PrintCase(arr123, arr213, output, 0);
            Console.WriteLine();
        }

        Thread tstCaseThr;
        bool caseTimedOut ;
        bool caseException;

        protected override void RunOnSpecificFile(string fileName, HardniessLevel level, int timeOutInMillisec)
        {
            int testCases;
            int[] arr1;
            int[] arr2;
            var output = 0 ;

            Stream s = new FileStream(fileName, FileMode.Open);
            BinaryReader br = new BinaryReader(s);
   
            testCases = br.ReadInt32();

            int totalCases = testCases;
            int correctCases = 0;
            int wrongCases = 0;
            int timeLimitCases = 0;
 
            int i = 1;
            while (testCases-- > 0)
            {
                arr1 = GetArray(br);
                arr2 = GetArray(br);
                
                output = 0;
                caseTimedOut = true;
                caseException = false;
                var actualResult = br.ReadInt32();
                {
                    tstCaseThr = new Thread(() =>
                    {
                        try
                        {
                            int sum = 0;
                            int numOfRep = 10;
                            Stopwatch sw = Stopwatch.StartNew();
                            for (int x = 0; x < numOfRep; x++)
                            {
                                sum += FindExtraElement.FindIndexOfExtraElement(arr1, arr2);
                            }
                            output = sum / numOfRep;
                            sw.Stop();
                            //Console.WriteLine("N = {0}, time in ms = {1}", arr1.Length, sw.ElapsedMilliseconds);
                        }
                        catch
                        {
                            caseException = true;
                            output = int.MinValue;
                        }
                        caseTimedOut = false;
                    });

                    //StartTimer(timeOutInMillisec);
                    tstCaseThr.Start();
                    tstCaseThr.Join(timeOutInMillisec);
                }

                if (caseTimedOut)       //Timedout
                {
                    Console.WriteLine("Time Limit Exceeded in Case {0}.", i);
					tstCaseThr.Abort();
                    timeLimitCases++;
                }
                else if (caseException) //Exception 
                {
                    Console.WriteLine("Exception in Case {0}.", i);
                    wrongCases++;
                }
                else if (output == actualResult)    //Passed
                {
                    Console.WriteLine("Test Case {0} Passed!", i);
                    correctCases++;
                }
                else                    //WrongAnswer
                {
                    Console.WriteLine("Wrong Answer in Case {0}.", i);
                    Console.WriteLine(" your answer = " + output + ", correct answer = " + actualResult);
                    wrongCases++;
                }

                i++;
            }
            s.Close();
            br.Close();
            Console.WriteLine();
            Console.WriteLine("# correct = {0}", correctCases);
            Console.WriteLine("# time limit = {0}", timeLimitCases);
            Console.WriteLine("# wrong = {0}", wrongCases);
            Console.WriteLine("\nFINAL EVALUATION (%) = {0}", Math.Round((float)correctCases / totalCases * 100, 0)); 
        }

        protected override void OnTimeOut(DateTime signalTime)
        {
        }

        public override void GenerateTestCases(HardniessLevel level, int numOfCases)
        {
            throw new NotImplementedException();


        }

        #endregion

        #region Helper Methods
        private static void PrintCase(int[] arr1, int[] arr2, int output, int expected)
        {
            Console.Write("Arr1: ");
            for (int i = 0; i < arr1.Length; i++)
            {
                Console.Write(arr1[i] + "  ");
            }
            Console.WriteLine();
            Console.Write("Arr2: ");
            for (int i = 0; i < arr2.Length; i++)
            {
                Console.Write(arr2[i] + "  ");
            }
            Console.WriteLine();
            Console.WriteLine("Index = " + output);
            Console.WriteLine("Expected = " + expected);

        }

        #endregion
    }
}
