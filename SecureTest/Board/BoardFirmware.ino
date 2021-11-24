#include <EEPROM.h>
//for random and encryption
int maxSize = 252; //values like 255, 254 are reserved
int lenght = 8; int debug = 0; //helping for debug to not track entire array
int lastRandomNbr[8] = { 33, 07, 210, 188, 1, 255, 209, 74 };
int lastEncryptedNbr[8] = { 33, 07, 210, 188, 1, 255, 209, 74 };
int storedPrivateKey[8] = { 255, 255, 255, 255, 255, 255, 255, 255 }; //must be unique on each board
int newPrivateKey[8] = { maxSize, maxSize, maxSize, maxSize, maxSize, maxSize, maxSize, maxSize }; //must be unique on each board
bool changePrivateKey = false;
int restartCount = 0; //a number stored in EPROM to help the generation Random Number on board initialization

//TimeOut between last random number and received available encryption
int encryptTOCount = 0;
int encryptTODuration = 100;

//for communication
unsigned char in_buffer[9]; // 0- reserved command nbr; 1 - 8 for encrypted nbr
int RelayID = 1; //ID of the relay that will be executed
int RelayOn = 0; //Command: Relay On = 1/Off = 0

//TimeOut WatchDog
int watchDogTOCount = 0;
int watchDogTODuration = -1;
int relayAddress = 10;

void setup() {

  //Setup the Relay (LED from the Arduino board)
	pinMode(relayAddress, OUTPUT); 

  //Setup the serial communication, data rate to 9600 bps
	Serial.begin(9600);

  //EPROM contains the following informations
  // 0 to  0: for restartCount
  // 1 to  9: for lastRandomNbr
  //10 to 10: for watchDogTODuration
  //11 to 18: for storedPrivateKey
  
  //Update EPROM with restartCount that need for Random Nbr;
  restartCount = (int)EEPROM.read(0);
  if (restartCount >= 254)  restartCount = 0; else restartCount += 1;
  EEPROM.write(0, restartCount);
  
  //Update EPROM last generation number
  for (int i = 0; i < lenght; i++)
  {
    lastRandomNbr[i] = (int)EEPROM.read(i+1);
  }  
	int * randomResult = SetupNewRandomNbr(restartCount);
	for (int i = 0; i < lenght; i++)
	{
		EEPROM.write(i+1, randomResult[i]);
	}

  //Get WatchDoc TimeOut
  watchDogTODuration = (int)EEPROM.read(10);

  //Read Private Key from EPROM
  for (int i = 0; i < lenght; i++)
  {
    storedPrivateKey[i] = (int)EEPROM.read(i+11);
  } 
}

void loop() {

	if (encryptTOCount > 0) encryptTOCount -= 1;
  if (watchDogTOCount >= 0) watchDogTOCount -= 1;
  if (watchDogTOCount == 0)
  {
    digitalWrite(relayAddress, LOW);
  }

	if (Serial.available() > 8) {
		in_buffer[0] = Serial.read();  // FF 01 01 00 00 00 00 00 00

    //Receive initial command
		if (in_buffer[0] == 0xFF || in_buffer[0] == 0xFD) {
    
      //Receive change private key command
      if (in_buffer[0] == 0xFD) {
        newPrivateKey[0] = Serial.read();
        newPrivateKey[1] = Serial.read();
        newPrivateKey[2] = Serial.read();
        newPrivateKey[3] = Serial.read();
        newPrivateKey[4] = Serial.read();
        newPrivateKey[5] = Serial.read();
        newPrivateKey[6] = Serial.read();
        newPrivateKey[7] = Serial.read();   
        changePrivateKey = true;   
      }
      else
      {
        RelayID = (int)Serial.read();  // ID
        RelayOn = (int)Serial.read();  // On/Off
        changePrivateKey = false;  
      }
      
      int * randomResult = SetupNewRandomNbr(restartCount);
      Serial.print((char)0xFF);
      for (int i = 0; i < lenght; i++)
      {
        Serial.print((char)randomResult[i]);
      }
      Serial.println("");

      encryptTOCount = encryptTODuration;
      watchDogTOCount = (watchDogTODuration * 10);           
		}
   
    //Receive encrypted replay
		else if (in_buffer[0] == 0xFE) {
			in_buffer[1] = Serial.read();
			in_buffer[2] = Serial.read();
			in_buffer[3] = Serial.read();
			in_buffer[4] = Serial.read();
			in_buffer[5] = Serial.read();
			in_buffer[6] = Serial.read();
			in_buffer[7] = Serial.read();
			in_buffer[8] = Serial.read();

			int * encryptedResult = SetupEncryptedRandomNbr(lastRandomNbr);
			bool value = true;
			for (int i = 0; i < lenght; i++)
			{
        if ((int)in_buffer[i + 1] != encryptedResult[i])
        {
          value = false;
        }
			}

			if (encryptTOCount <= 0)
			{
				Serial.print((char)255);
				Serial.print((char)RelayID);
				Serial.print((char)98); //Encryption TimeOut Error
			}
			else if (value == true)
			{
        if (changePrivateKey == true)
        {
          for (int i = 0; i < lenght; i++)
          {
            storedPrivateKey[i] = newPrivateKey[i];
            EEPROM.write(i+11, storedPrivateKey[i]);
          } 
          Serial.print((char)253);
        }
        else {
          if (RelayID == 224)
          {
            if (RelayOn == 0) watchDogTODuration = -1; else watchDogTODuration = RelayOn;          
            EEPROM.write(10, watchDogTODuration);
            watchDogTOCount = (watchDogTODuration * 10);
          }
  				else if (RelayOn == 1)
  				{
  					digitalWrite(relayAddress, HIGH);
  				}
  				else if (RelayOn == 0)
  				{
  					digitalWrite(relayAddress, LOW);
  				}
          else
          {
            RelayOn = digitalRead(relayAddress);
          }
          //Success
  				Serial.print((char)255);
  				Serial.print((char)RelayID);
          Serial.print((char)RelayOn);
        }
			}
			else
			{
        encryptTOCount = 0;
        Serial.print((char)255);
        Serial.print((char)RelayID);
        Serial.print((char)99); //Encryption error
			}
     Serial.println("");
		}
		Serial.flush();		
	}

 delay(100);
}

int * SetupNewRandomNbr(int restartCount)
{
	int total = 0;
	for (int i = 0; i < lenght; i++)
	{
		int carValue = lastRandomNbr[i];
		//Serial.print("last: ");
		//Serial.print(carValue);

		carValue = GetRandomValue(carValue, ((i * restartCount) % 33));
		//carValue = GetRandomValue(73, 177);
		total += carValue;
		lastRandomNbr[i] = carValue;
	}

	if (lenght > 1) //for debug reasons
	{
		lastRandomNbr[0] = lastRandomNbr[5];

		lastRandomNbr[5] = lastRandomNbr[2];

		lastRandomNbr[2] = lastRandomNbr[7];

		lastRandomNbr[7] = lastRandomNbr[6];

		lastRandomNbr[6] = lastRandomNbr[4];

		lastRandomNbr[4] = lastRandomNbr[1];

		lastRandomNbr[1] = lastRandomNbr[3];

		lastRandomNbr[3] = (total % maxSize);
	}

	for (int i = 0; i < lenght - 1; i++)
	{
		if (lastRandomNbr[i] == lastRandomNbr[i + 1]) {
			lastRandomNbr[i] += 1;
			lastRandomNbr[i] = (lastRandomNbr[i] % maxSize);
		}
	}
	return lastRandomNbr;
}

int * SetupEncryptedRandomNbr(int * randomNbr)
{
	int encryptedNbr[8];

	for (int i = 0; i < lenght; i++)
	{
		int carValue = randomNbr[i];
		carValue = GetRandomValue(carValue, storedPrivateKey[i]);
		encryptedNbr[i] = carValue;
	}

	int encryptMixtedNbr[8];

	if (debug == 1) { Serial.print("Mix: ");	Serial.print(encryptedNbr[0]); }
	if (debug == 1) { Serial.print(" ");	Serial.print(encryptedNbr[4]); }
	if (debug == 1) { Serial.print(" ");	Serial.print(encryptedNbr[7]); }
	encryptMixtedNbr[0] = encryptedNbr[0] + encryptedNbr[4] + encryptedNbr[7];  //Max: 762
	if (debug == 1) { Serial.print(" R: "); Serial.print(encryptMixtedNbr[0]); }

	encryptMixtedNbr[1] = encryptedNbr[1] - encryptedNbr[4] > 0 ? encryptedNbr[1] - encryptedNbr[4] : encryptedNbr[4] - encryptedNbr[1];  //Max: 254
	encryptMixtedNbr[2] = encryptedNbr[2] * (encryptedNbr[6] / 7) + encryptedNbr[0];  //Max: 9398
	encryptMixtedNbr[3] = encryptedNbr[3] / (encryptedNbr[5] == 0 ? 1 : encryptedNbr[5]);  //Max: 254
	encryptMixtedNbr[4] = encryptedNbr[4] + encryptedNbr[0] / 3;  //Max: 508
	encryptMixtedNbr[5] = encryptedNbr[5] - encryptedNbr[3] >= 0 ? encryptedNbr[5] - encryptedNbr[3] / 3 : encryptedNbr[3] - encryptedNbr[5] / 3;  //Max: 254
	encryptMixtedNbr[6] = encryptedNbr[6] * (encryptedNbr[1] / 7 + encryptedNbr[2] / 9);  //Max: 6642
	encryptMixtedNbr[7] = encryptedNbr[7] / (encryptedNbr[2] / 3 == 0 ? 1 : encryptedNbr[2]);  //Max: 254

	for (int i = 0; i < lenght; i++)
	{
		lastEncryptedNbr[i] = (encryptMixtedNbr[i] % maxSize);
	}

	if (debug == 1) { Serial.print(" M: "); Serial.println(lastEncryptedNbr[0]); }

	return lastEncryptedNbr;
}

int GetRandomValue(int carValue, int key)
{
	if (debug == 1) { Serial.print("carValue: ");	Serial.print(carValue); }
	if (debug == 1) { Serial.print(" key: ");	Serial.print(key); }

	int newAmt = (int)((key / 6 + 1) * carValue + key); //max: 11176

	if (newAmt % 5 == 0) //Max: 10922
	{
		newAmt -= key;
	}
	else if (newAmt % 7 == 0) //Max: 11203
	{
		newAmt += 27;
	}
	else if (newAmt % 9 == 0) //Max: 11353
	{
		newAmt += 177;
	}
	else if (newAmt % 11 == 0) //Max: 11177
	{
		newAmt += 1;
	}
	else if (newAmt % 2 == 0) //Max: 11076
	{
		newAmt -= 100;
	}

	if (newAmt < 0) newAmt *= -1;

	String strValue = String(newAmt);
	if (strValue.indexOf('2') > 0) //Max: 11993
	{
		if (newAmt > 3000)
		{
			newAmt = newAmt / 3 + 2;
		}
		else
		{
			newAmt = newAmt * 4 - 7;
		}

		if (debug == 1) { Serial.print(" newAmt '2': ");		Serial.print(newAmt); }
	}
	else if (strValue.indexOf('4') > 0) //Max: 8991
	{
		if (newAmt > 3000)
		{
			newAmt = newAmt / 7 + 2;
		}
		else
		{
			newAmt = newAmt * 3 - 9;
		}

		if (debug == 1) { Serial.print(" newAmt '4': ");		Serial.print(newAmt); }
	}
	else if (strValue.indexOf('8') > 0) //Max: 9470
	{
		newAmt += key / 7 * key;

		newAmt = maxSize - ((newAmt - 1) % maxSize);

		if (debug == 1) { Serial.print("newAmt '8': ");		Serial.print(newAmt); }
	}
	else //Max: 6000 aprox
	{
		newAmt = (newAmt + key * 21 + carValue) ^ key;

		if (debug == 1) { Serial.print(" newAmt 'E': ");		Serial.print(newAmt); }
	}

	newAmt -= 1;
	if (newAmt == carValue) newAmt = newAmt + 17;  //Max: 11370

	if (newAmt < 0) newAmt *= -1;

	//Serial.print("newAmt2: ");
	//Serial.println(newAmt);

	newAmt = (newAmt % maxSize);

	if (debug == 1) { Serial.print(" result: ");	Serial.println(newAmt); }

	return newAmt;
}
