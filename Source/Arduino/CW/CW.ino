#include <IRremoteInt.h>
#include <IRremote.h>

#include "Trigger.h"

TriggerClass trigger;

int RecivePin = 11;

IRrecv reciver(RecivePin);
IRsend sender;
decode_results result;

String zerrofill(uint32_t number, int len);

void setup()
{
	Serial.begin(9600);
	reciver.enableIRIn();
	
}

void loop()
{
	if (Serial.available()) 
	{
		byte com = Serial.read();

		if (com == 'S' || com == 's')
		{
			int len = Serial.parseInt();
			if (Serial.read() == '@') {
				uint32_t code = Serial.parseInt();
				if (Serial.read() == '@')
				{
					int type = Serial.parseInt();
					unsigned int buff[] = { (unsigned)code };
					sender.sendRaw(buff, 32, 38000);
					Serial.read();
					uint16_t adress = Serial.parseInt();

					switch (type)
					{
					case UNKNOWN:
						
						break;
					case AIWA_RC_T501:
						sender.sendAiwaRCT501(code);
						break;
					case DENON:
						sender.sendDenon(code, len);
						break;
					case DISH:
						sender.sendDISH(code, len);
						break;
					case JVC:
						sender.sendJVC(code, len, false);
						break;
					case LEGO_PF:
						sender.sendLegoPowerFunctions(code);
						break;
					case LG:
						sender.sendLG(code, len);
						break;
					case NEC:
						sender.sendNEC(code, len);
						break;
					case PANASONIC:
						
						sender.sendPanasonic(adress, code);
						break;
					case RC5:
						sender.sendRC5(code, len);
						break;
					case RC6:
						sender.sendRC6(code, len);
						break;
					case SAMSUNG:
						
						sender.sendSAMSUNG(code, len);
						break;
					case SHARP:
						
						sender.sendSharp(adress, code);
						break;
					case SONY:
						sender.sendSony(code, len);
						break;
					case WHYNTER:
						sender.sendWhynter(code, len);
						break;
					default:
						sender.sendNEC(code, 32);
						break;
					}
				}

			}
		}
		
			

		
	
	
		if (com == 'C' || com == 'c')
		{
			trigger.setDisabled();
			delay(30);
			Serial.print("EEE@EEEEEEEEEE@EE@EEEEE");
		}
		if (com == 'R' || com == 'r')
		{
			trigger.setEnabled();
			reciver.resume();
		
		}
	}
	if(trigger.isEnabled() && (bool)reciver.decode(&result))
	{
		Serial.print(zerrofill(result.bits, 3));
		Serial.print('@');
		Serial.print(zerrofill(result.value, 10));
		Serial.print('@');
		Serial.print(zerrofill(result.decode_type, 2));
		Serial.print('@');
		Serial.print(zerrofill(result.address, 5));
		trigger.setDisabled();
		reciver.resume();
	}
	
	
	trigger.check();

}
String zerrofill(uint32_t number, int len)
{
	String res = String(number);
	while (res.length() < len)
	{
		res = "0" + res;
	}
	return(res);


}



