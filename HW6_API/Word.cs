using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace HW6_API
{
    public class Definition
    {
        public string type { get; set; }
        public string definition { get; set; }
        public string example { get; set; }
        public string image_url { get; set; }
        public string emoji { get; set; }
    }

    public class Word
    {
        public List<Definition> definitions { get; set; }
        public string word { get; set; }
        public string pronunciation { get; set; }
    }
}
