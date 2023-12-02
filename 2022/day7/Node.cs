public class Node {
    public int Size {get;set;}
    public List<Node> Childs = new List<Node>();

    public bool IsLeaf() {
        return Childs.Count() == 0;
    }

    public Node(int size) => this.Size = size;

    public void AddNode(Node node) {
        Childs.Add(node);
    }


}