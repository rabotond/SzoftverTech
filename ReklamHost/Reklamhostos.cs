﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ReklamServiceLibrary;

namespace ReklamHost
{
    class Reklamhost
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(ServiceReklam)))
            {
                host.Open();
                Console.WriteLine("Fut!");
                Console.ReadKey();
            }
        }
    }
}
