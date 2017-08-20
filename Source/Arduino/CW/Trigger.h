// Trigger.h

#ifndef _TRIGGER_h
#define _TRIGGER_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

class TriggerClass
{
 protected:
	uint32_t _setAt;
	bool _enabled;

 public:
	void init();
	void check();
	void setEnabled();
	void setDisabled();
	bool isEnabled();
};

extern TriggerClass Trigger;

#endif

