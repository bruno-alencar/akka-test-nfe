using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNFe
{
    public static class ActorPath
    {
        public static readonly ActorMetaData InitConsole = new ActorMetaData("init", "akka://NFeIssuanceActors/user/init");
        public static readonly ActorMetaData Read = new ActorMetaData("read", "akka://NFeIssuanceActors/user/read");
        public static readonly ActorMetaData Coordinator = new ActorMetaData("coordinator", "akka://NFeIssuanceActors/user/init/read/coordinator");
    }

    /// <summary>
    /// Meta-data class
    /// </summary>
    public class ActorMetaData
    {
        public ActorMetaData(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public string Name { get; private set; }

        public string Path { get; private set; }
    }
}
