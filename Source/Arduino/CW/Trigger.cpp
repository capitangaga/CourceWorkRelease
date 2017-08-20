// 
// 
// 

#include "Trigger.h"

void TriggerClass::init()
{


}

void TriggerClass::check()
{
	if(_enabled && millis() - _setAt > 60000)
	{
		Serial.println("TTT@TTTTTTTTTT@TT@TTTTT");
		_enabled = false;
	} 
}

void TriggerClass::setEnabled()
{
    if(_enabled)
    {
        _enabled = false;
        Serial.println("TTT@TTTTTTTTTT@TT@TTTTT");
        return;
    }
	_enabled = true;
	_setAt = millis();
}

void TriggerClass::setDisabled()
{
	_enabled = false;
}

bool TriggerClass::isEnabled()
{
	return _enabled;
}


TriggerClass Trigger;

