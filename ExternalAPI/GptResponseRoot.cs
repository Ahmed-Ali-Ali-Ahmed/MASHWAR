using System.Collections.Generic;

namespace MASHWAR.ExternalAPI
{
    public class GptResponseRoot
    {
        public string id { get; set; }
        public string @object { get; set; }
        public int created { get; set; }
        public string model { get; set; }
        public GptUsage usage { get; set; }
        public List<Choice> choices { get; set; }
    }



    public class GptUsage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }


    public class Choice
    {
        public GptMessage message { get; set; }
        public string finish_reason { get; set; }
        public int index { get; set; }
    }

}
