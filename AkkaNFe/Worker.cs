using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNFe
{
    public class Worker : ReceiveActor
    {
        public Worker()
        {

        }


        protected override void PreStart()
        {
            Console.WriteLine("testando");
        }
    }
}
