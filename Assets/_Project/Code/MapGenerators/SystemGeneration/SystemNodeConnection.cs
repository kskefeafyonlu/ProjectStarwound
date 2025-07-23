namespace _Project.Code.MapGenerator
{
    public class SystemNodeConnection
    {
        public string ConnectionName = "DefaultConnection";
        public SystemMapNode NodeA;
        public SystemMapNode NodeB;

        
        public SystemNodeConnection(string connectionName, SystemMapNode nodeA, SystemMapNode nodeB)
        {
            ConnectionName = connectionName;
            NodeA = nodeA;
            NodeB = nodeB;

        }
        
        public override string ToString()
        {
            return $"{NodeA.NodeName} <-> {NodeB.NodeName}";
        }
    }
}