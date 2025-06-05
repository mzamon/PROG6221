using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberSecurityAwarenessAssistant;

namespace MzamoLM20
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Chatbot - Talkingbot AI";
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.Clear();
            interaction bot = new interaction();
        }
    }
}