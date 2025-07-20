namespace _Project.Code.MapGenerator
{
    public class NodeConnection
    {
        public string ConnectionName = "DefaultConnection";
        public MapNode NodeA;
        public MapNode NodeB;

        
        public NodeConnection(string connectionName, MapNode nodeA, MapNode nodeB)
        {
            ConnectionName = connectionName;
            NodeA = nodeA;
            NodeB = nodeB;

        }
    }
}