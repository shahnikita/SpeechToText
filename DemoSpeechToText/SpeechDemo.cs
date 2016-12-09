using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace DemoSpeechToText
{
    public class SpeechDemo
    {


        private SpeechRecognitionEngine MySpeechRecognitionEngine = null;

        private string textdata = "";
        private string label2 = "";
        private void MySpeechRecognitionEnginee_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

            if (e.Result != null)
            {

                SpeechRecognizer recognizer = new SpeechRecognizer();

                  DisplayBasicPhraseInfo(label2, e.Result, recognizer);

                textdata += e.Result.Text + " \n ";
                Console.Write(" \n "+textdata + " \n ");
                // Thread.Sleep(3000);

            }

        }

        private void MySpeechRecognitionEngine_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
        {


            Console.Write(e.AudioLevel.ToString());

        }

        private void MySpeechRecognitionEnginee_AudioStateChanged(object sender, AudioStateChangedEventArgs e)
        {

            Console.Write(e.AudioState.ToString());

        }

        private void MySpeechRecognitionEngine_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {

        }



        public void InitializeSpeechRecognitionEngine(String filePath)
        {
            MySpeechRecognitionEngine = new SpeechRecognitionEngine();
            //MySpeechRecognitionEngine.SetInputToDefaultAudioDevice();

            MySpeechRecognitionEngine.UnloadAllGrammars();

            try
            {

                MySpeechRecognitionEngine.SetInputToWaveFile(filePath);

                Process.Start("C:\\Program Files\\Windows Media Player\\wmplayer.exe", ("\"" + filePath + "\""));

              

                MySpeechRecognitionEngine.LoadGrammar(new DictationGrammar());

                MySpeechRecognitionEngine.RecognizeAsync(RecognizeMode.Single);

                MySpeechRecognitionEngine.AudioLevelUpdated += MySpeechRecognitionEngine_AudioLevelUpdated;

                MySpeechRecognitionEngine.SpeechRecognized += MySpeechRecognitionEnginee_SpeechRecognized;

                MySpeechRecognitionEngine.AudioStateChanged += MySpeechRecognitionEnginee_AudioStateChanged;

                MySpeechRecognitionEngine.RecognizeCompleted += MySpeechRecognitionEngine_RecognizeCompleted;
                

            }

            catch (Exception ex)
            {

                Console.Write(ex.Message.ToString());

            }

        }

        internal static void DisplayBasicPhraseInfo(string text, RecognizedPhrase result, SpeechRecognizer recognizer)
        {

            if (result != null && text != null)
            {

                // Blankif (recognizer != null)

                {

                    //Clearlabel.Text += String.Format(" Recognizer currently at: {0} mSec\n" +" Audio Device currently at: {1} mSec\n",recognizer.RecognizerAudioPosition.TotalMilliseconds,recognizer.AudioPosition.TotalMilliseconds);

                }

                if (result != null)
                {

                    RecognitionResult recResult = result as RecognitionResult;

                    if (recResult != null)
                    {

                        RecognizedAudio resultRecognizedAudio = recResult.Audio;

                        if (resultRecognizedAudio == null)
                        {

                            text += String.Format(" Emulated input\n");

                        }

                        else
                        {

                            text +=
                                String.Format(
                                    " Candidate Phrase at: {0} mSec\n" + " Phrase Length: {1} mSec\n" +
                                    " Input State Time: {2}\n" + " Input Format: {3}\n",
                                    resultRecognizedAudio.AudioPosition.TotalMilliseconds,
                                    resultRecognizedAudio.Duration.TotalMilliseconds,
                                    resultRecognizedAudio.StartTime.ToShortTimeString(),
                                    resultRecognizedAudio.Format.EncodingFormat.ToString());

                        }

                    }
                    text += String.Format(" Confidence Level: {0}\n", result.Confidence);

                    if (result.Grammar != null)
                    {

                        text += String.Format(" Recognizing Grammar: {0}\n" + " Recognizing Rule: {1}\n",
                            ((result.Grammar.Name != null) ? (result.Grammar.Name) : "None"),
                            ((result.Grammar.RuleName != null) ? (result.Grammar.RuleName) : "None"));

                    }

                    if (result.ReplacementWordUnits.Count != 0)
                    {

                        text += String.Format(" Replacement text:\n");

                        foreach (ReplacementText rep in result.ReplacementWordUnits)
                        {

                            text += String.Format(" At index {0} for {1} words. Text: {2}\n", rep.FirstWordIndex,
                                rep.CountOfWords, rep.Text);

                        }

                        text += String.Format("\n\n");
                        Console.WriteLine(text);
                    }

                }

            }

        }
    }
}
