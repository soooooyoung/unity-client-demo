using Mirror;
using Newtonsoft.Json;
// NOTE: use protocol buffer for better optimization later
public struct ChatMessage : NetworkMessage
{
    public string Text;
}
