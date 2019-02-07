using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;  

using System.Threading.Tasks;
using OperationContracts;

namespace Server_Films
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost serviceHost = new ServiceHost(typeof(LoginRegisterUser));
            serviceHost.Open();
            Console.WriteLine("Для завершения нажмите <ENTER>\n");
            Console.ReadLine();
            serviceHost.Close();
        }
    }
}
