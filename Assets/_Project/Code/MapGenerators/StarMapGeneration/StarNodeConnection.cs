namespace _Project.Code.MapGenerators.StarMapGeneration
{
    public class StarNodeConnection
    {
        public StarNode NodeA { get; private set; }
        public StarNode NodeB { get; private set; }

        public StarNodeConnection(StarNode nodeA, StarNode nodeB)
        {
            NodeA = nodeA;
            NodeB = nodeB;
        }

        public override string ToString()
        {
            return $"{NodeA.NodeName} <-> {NodeB.NodeName}";
        }
    }
}