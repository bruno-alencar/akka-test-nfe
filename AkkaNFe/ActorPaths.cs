namespace AkkaNFe
{
    public static class ActorPaths
    {
        public static readonly ActorMetaData InitConsole = new ActorMetaData("init", "akka://NFeIssuanceActors/user/init");
        public static readonly ActorMetaData Read = new ActorMetaData("read", "akka://NFeIssuanceActors/user/read");
        public static readonly ActorMetaData Coordinator = new ActorMetaData("coordinator", "akka://NFeIssuanceActors/user/read/coordinator");
        public static readonly ActorMetaData Commander = new ActorMetaData("commander", "akka://NFeIssuanceActors/user/commander");
        public static readonly ActorMetaData Generator = new ActorMetaData("generator", "akka://NFeIssuanceActors/user/generator");
        public static readonly ActorMetaData Worker = new ActorMetaData("worker", "akka://NFeIssuanceActors/user/init/read/coordinator/worker");
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
