//Ses project
//Make a bridge witch C# program and get "read" voice
//Thanks to MicroSpark.com team!

const int led1 = 13;

void setup ()
{
  Serial.begin (9600);
  pinMode(led1,OUTPUT);

}

void loop()
{
  if (Serial.available())
  {
    int c = Serial.read ();
    if(c=='1')
    {
      digitalWrite(led1, HIGH);
    }
    else if(c=='2')
    {
      
      digitalWrite(led1, LOW);

      }

  }

}
