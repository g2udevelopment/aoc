public abstract class Packet {
    public Packet CreatePacket(string line) {
        Stack<ListPacket> packets = new Stack<ListPacket>(); //
       
        packets.Push(new ListPacket());
        foreach (var item in line[1..^1])
        {
            ListPacket current = packets.Peek();
            if (item == '[') {
                var newpacket = new ListPacket();
                current.Packets.Add(newpacket);
                packets.Push(newpacket);
            } else if (item == ']') {
                packets.Pop();
            } else if (char.IsNumber(item)) {
                
            }
        }

        return packets.Pop(); //last one is the full parsed list
    }

    public abstract bool Compare(Packet other);
}

public class ListPacket : Packet {
    public List<Packet> Packets = new List<Packet>();
    public override bool Compare(Packet other)
    {
        
    }
}

public class ValuePacket : Packet {
    int value;
}
    
