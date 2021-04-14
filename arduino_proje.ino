#include "DHT.h"
#define DHTPIN 10
#define DHTTYPE DHT11 
DHT dht(DHTPIN, DHTTYPE);
#include <OneWire.h>
#include <DallasTemperature.h>
#define ONE_WIRE_BUS 3
int Led = 12 ;
int Sensor = 11; 
int val; 
int TempPin = A0;
int sicaklik = 0;
int isik = 0;
char t;
long uzaklik;
long sure;
int CLK = 5;  
int DT = 2;  
int RotPosition = 0;
int rotation;
int value;
boolean LeftRight;
int blue = 6;
int green = 7;
int red = 13;
int digitalPin = 4; 
int analogPin = A2; 
int digitalVal; 
int analogVal; 
OneWire oneWire(ONE_WIRE_BUS);
DallasTemperature sensors(&oneWire);
void setup()
{
  pinMode(1, OUTPUT);
  pinMode(8, OUTPUT); 
  pinMode(9, INPUT);
  Serial.begin(9600);
  sensors.begin();
  pinMode (3, OUTPUT);
  dht.begin();
  pinMode (Led, OUTPUT) ; 
  pinMode (Sensor, INPUT) ; 
  pinMode (CLK, INPUT);
  pinMode (DT, INPUT);
  pinMode(red, OUTPUT);
  pinMode(green, OUTPUT);
  pinMode(blue, OUTPUT);
  pinMode(digitalPin, INPUT);
  rotation = digitalRead(CLK);
}
void loop() {
  if (Serial.available())
  {
    t = Serial.read();

  }
  //  LM-35 SENSÖRÜ
  if (t == '1')
  {
    sicaklik = analogRead(TempPin);
    Serial.println(sicaklik);
    delay(100);
  }
  //  Ky-016 RBG
  if (t == '2') {
  int a= random(0,255);
  int b= random(0,255);
  int c= random(0,255);
  analogWrite(red, a);
  analogWrite(green,b);
  analogWrite(blue,c);
  Serial.print(a);
  Serial.print("*");
  Serial.print(b);
  Serial.print("*");
  Serial.println(c);
  delay(500);


  }
  if (t == '0')
  {
   analogWrite(red, LOW);
  analogWrite(green,LOW);
  analogWrite(blue,LOW);
    
  }
  //  uzaklık
  if (t == '3')
  {
    digitalWrite(8, LOW);
    delayMicroseconds(5);
    digitalWrite(8, HIGH);
    delayMicroseconds(10);
    digitalWrite(8, LOW);
    sure = pulseIn(9, HIGH);
    uzaklik = sure / 2 / 29.1;
    if (uzaklik > 200)
      uzaklik = 150;
    Serial.println(uzaklik);
    delay(100);

  }
  //    ışık şiddeti LDR
  if (t == '4')
  {
    isik = analogRead(A1);
    Serial.println(isik);
    delay(100);
  }
  //    ky-001
  if (t == '5')
  {
    sensors.requestTemperatures(); // Send the command to get temperatures
    int derece = sensors.getTempCByIndex(0);
    Serial.println(derece);
  }
  // ky-027
  if (t == '6')
  {
    val = digitalRead (Sensor) ; 

    if (val == HIGH) 
    {
      digitalWrite (Led, LOW);
      Serial.println(val);
      delay(100);
    }
    else
    {
      digitalWrite (Led, HIGH);
      Serial.println(val);
      delay(100);
    }
  }
  //ky-040
  if (t == '7')
  {
    value = digitalRead(CLK);
    if (value != rotation) { 
      if (digitalRead(DT) != value) { 
        RotPosition ++;
        LeftRight = true;
      } else { 
        LeftRight = false;
        RotPosition--;
      }
      

      Serial.println(RotPosition);
      delay(10);
      

    }
    rotation = value;
  }
  //ky-015
  if (t == '8')
  {
     delay(500);
 
  // Measurement of humidity
  int h = dht.readHumidity();
  // Measurement of temperature
  int t = dht.readTemperature();
   
  // The measurements will be tested of errors here
  // If an error is detected, an error message will be displayed
  if (isnan(h) || isnan(t)) {
    Serial.println("Error while reading the sensor");
    return;
  }
 
  // Output at the serial console
  Serial.print(h);
  Serial.print("*");
  Serial.println(t);
 

  }
  // ky-021
  if (t == 'a')
  {
    digitalVal = digitalRead(digitalPin);

    if(digitalVal==HIGH)
    {
         Serial.println(digitalVal); 
    }
    else
    {
      
 Serial.println(digitalVal); 
    }

    delay(100);
  }
}
