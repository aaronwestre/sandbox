/******************************************************/
//       THIS IS A GENERATED FILE - DO NOT EDIT       //
/******************************************************/

#include "application.h"
#line 1 "/Users/aaron/Desktop/synthings_basic_input/src/synthings_basic_input.ino"
void setup();
void loop();
bool withinAnalogTolerance(float reading, float previousReading);
void readAnalogInputsAndPublish();
void readDigitalInputsAndPublish();
#line 1 "/Users/aaron/Desktop/synthings_basic_input/src/synthings_basic_input.ino"
String pingEventName = "ping";
float analogThreshold = 0.05f;
float analogDifference;
uint16_t analogPins[6] = {A0, A1, A2, A3, A4, A5};
float analogValues[6] = {0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
String analogEventNames[6] = {"synthings_input_analog_0", "synthings_input_analog_1", "synthings_input_analog_2", "synthings_input_analog_3", "synthings_input_analog_4", "synthings_input_analog_5"};
float analogReading = 0.0f;
uint16_t digitalPins[6] = {D0, D1, D2, D3, D4, D5};
int digitalValues[6] = {LOW, LOW, LOW, LOW, LOW, LOW};
String digitalEventNames[6] = {"synthings_input_digital_0", "synthings_input_digital_1", "synthings_input_digital_2", "synthings_input_digital_3", "synthings_input_digital_4", "synthings_input_digital_5"};
int digitalReading = LOW;
String publishedValueLow = "Off";
String publishedValueHigh = "On";

void setup() 
{
    for (int index = 0; index < arraySize(analogPins); index++) 
    { 
        pinMode(analogPins[index], INPUT);
    }
    for (int index = 0; index < arraySize(digitalPins); index++) 
    { 
        pinMode(digitalPins[index], INPUT_PULLDOWN);
    }
}

void loop() 
{
    if (Particle.connected())
    {
        readAnalogInputsAndPublish();
        readDigitalInputsAndPublish();
    }
    delay(1000);
}

bool withinAnalogTolerance(float reading, float previousReading) 
{
    analogDifference = reading - previousReading;
    if (abs(analogDifference) < analogThreshold) 
    {
        return true;
    }
    return false;
}

void readAnalogInputsAndPublish()
{
    for (int index = 0; index < arraySize(analogPins); index++) 
    { 
        analogReading = (float)analogRead(analogPins[index]);
        analogReading = map(analogReading, 0.0, 4095.0, 0.0, 1.0);
        if (!withinAnalogTolerance(analogReading, analogValues[index])) 
        {
            analogValues[index] = analogReading;
            Particle.publish(analogEventNames[index], String::format("%.2f", analogValues[index]), 10, PRIVATE);
            analogReading = 0;
        }
    }
}

void readDigitalInputsAndPublish()
{
    for (int index = 0; index < arraySize(digitalPins); index++) 
    { 
        digitalReading = digitalRead(digitalPins[index]);
        if (digitalReading != digitalValues[index])
        {
            digitalValues[index] = digitalReading;
            if (digitalValues[index] == HIGH)
            {
                Particle.publish(digitalEventNames[index], publishedValueHigh, 10, PRIVATE);
            } else {
                Particle.publish(digitalEventNames[index], publishedValueLow, 10, PRIVATE);
            }
        }
        digitalReading = LOW;
    }
}