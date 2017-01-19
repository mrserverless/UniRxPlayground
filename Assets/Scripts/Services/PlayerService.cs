using System.Collections.Generic;
using ZeroFormatter;
using Models;

public class PlayerService  {

    public byte[] SendPlayer(Player player)
    {
        var bytes = ZeroFormatterSerializer.Serialize(player);
        return bytes;
    }


}
