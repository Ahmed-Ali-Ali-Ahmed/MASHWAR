using System.Collections.Generic;

namespace MASHWAR.ExternalAPI
{
    public class GptRequestRoot
{
    public string model { get; set; }
    public List<GptMessage> messages { get; set; }
    public double temperature { get; set; }
}

    public class GptMessage
    {
        public string role { get; set; }
        public string content { get; set; }
    }


}
