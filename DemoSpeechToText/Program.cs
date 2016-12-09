using System;
using System.IO;
using System.Collections.Generic;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;
using System.Speech.Recognition;
using System.Speech.Recognition.SrgsGrammar;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO.Ports;
using System.IO.IsolatedStorage;
using System.IO.Compression;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace DemoSpeechToText
{


    class Program
    {



        public static void Main(string[] args)
        {

            SpeechDemo demo = new SpeechDemo();
            demo.InitializeSpeechRecognitionEngine( "sample1.wav");
            Console.ReadLine();
        }



    }
}
