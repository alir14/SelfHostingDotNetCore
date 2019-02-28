using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using HosingConsole.Model;

namespace HosingConsole.MyTask
{
    public class BGTask: IBGTask
    {
        private readonly IStateManager _stateMenager;
        public BGTask(IStateManager stateMenager)
        {
            _stateMenager = stateMenager;
        }
        public void Run()
        {
            var counter = 0;
            while (true)
            {
                //Do something 
                Thread.Sleep(5000);

                _stateMenager.SetServiceState(new StateModel
                {
                    Counter = counter,
                    LastState = "asd",
                    LastTry = DateTime.UtcNow
                });

                Console.WriteLine($"Done {counter}");

                counter++;
            }
        }
    }
}
