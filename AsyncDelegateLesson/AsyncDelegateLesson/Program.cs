using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace AsyncDelegateLesson
{
    class Program
    {
        private delegate int AsyncDelegate(int first, int second);
        static void Main(string[] args)
        {
            GetCurrentThreadInfo();
            //var executor = new AsyncDelegate(Sum);
            AsyncDelegate executor = Sum;
            var asyncResult = executor.BeginInvoke(10,15,SumCallBack,"Результат равен");


            //var result = executor.EndInvoke(asyncResult);
            //Sum(10,15);

            Console.ReadLine();
        }

        public static void SumCallBack(IAsyncResult result)
        {
            GetCurrentThreadInfo();

            var stringData = result.AsyncState.ToString();
            var executor = (result as AsyncResult).AsyncDelegate as AsyncDelegate;
            var dataResult = executor.EndInvoke(result);
            Console.WriteLine(stringData + dataResult);
        }

        /// <summary>
        /// Данный метод имитирует длительную работу посредством сна в 5 секунд и сложения чисел. Потециально это можт быть любая работа
        /// </summary>
        /// <param name="firstNumber">Первое число</param>
        /// <param name="secondNumber">Второе число</param>
        static public int Sum(int firstNumber, int secondNumber)
        {
            GetCurrentThreadInfo();
            Thread.Sleep(5000);
            //Console.WriteLine($"{firstNumber.ToString()} + {secondNumber.ToString()}" + $"={firstNumber + secondNumber}");
            return firstNumber + secondNumber;
        }

        private static void GetCurrentThreadInfo()
        {
            var currentThread = Thread.CurrentThread;
            Console.WriteLine($"Происходит работа в потоке номер {currentThread.ManagedThreadId}");
            
        }
    }
}
