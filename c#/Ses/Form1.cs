/*
 * Created by SharpDevelop.
 * User: ziyaddin
 * Date: 22.12.2013
 * Time: 14:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;
using System.IO.Ports;

namespace Ses
{
    public partial class Form1 : Form
    {
    	private SerialPort serialPort1 = new SerialPort("COM4", 9600);
    	
        public Form1()
        {
            InitializeComponent();
            
            serialPort1.Open();
        }
        
        SpeechSynthesizer synt = new SpeechSynthesizer();
        PromptBuilder build = new PromptBuilder();
        SpeechRecognitionEngine engine = new SpeechRecognitionEngine();
        
        void Button1Click(object sender, EventArgs e)
		{
            build.ClearContent();
            build.AppendText(textBox1.Text);
            synt.Speak(build);
            synt.Volume = 100;
            synt.Rate = 2;
		}
		
		void Button2Click(object sender, EventArgs e)
		{
            Choices list = new Choices();
            list.Add(new string[] {"on","off"});
            Grammar gr = new Grammar(new GrammarBuilder(list));
            try
            {
                engine.RequestRecognizerUpdate();
                engine.LoadGrammar(gr);
                engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognized);
                engine.SetInputToDefaultAudioDevice();
                engine.RecognizeAsync(RecognizeMode.Multiple);

            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
		}

        void engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

    /*        else if (e.Result.Text.ToString() == "Exit" || e.Result.Text.ToString() == "Close")
            {
                Application.Exit();
            }
            */
            if (e.Result.Text.ToString() == "on")
            {
                serialPort1.WriteLine("1");
                textBox1.Text = "Turned On!";
            }
            else if (e.Result.Text.ToString() == "off")
            {
                serialPort1.WriteLine("2");
                textBox1.Text = "Turned Off!";
            }
        }
        
        void Form1Load(object sender, EventArgs e)
		{
			
		}
        
      }
    }