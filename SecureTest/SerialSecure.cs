using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SecureRelay
{
    class SerialSecure
    {
        Int16 lenght = 8;
        Int16[] privateKey = { 0, 0, 0, 0, 0, 0, 0, 0 };

        private Int16 maxSize = 252;

        public SerialSecure(Int16[] privateKey)
        {
            this.privateKey = privateKey;
        }

        public Int16[] SetupEncryptedRandomNbr(Int16[] randomNbr)
        {
            Int16[] encryptedNbr = { maxSize, maxSize, maxSize, maxSize, maxSize, maxSize, maxSize, maxSize };

            for (int i = 0; i < lenght; i++)
            {
                Int16 carValue = randomNbr[i];
                carValue = GetRandomValue(carValue, privateKey[i]);
                encryptedNbr[i] = carValue;
            }

            Int16[] encryptMixtedNbr = { 0, 0, 0, 0, 0, 0, 0, 0 };
            encryptMixtedNbr[0] = (Int16)(encryptedNbr[0] + encryptedNbr[4] + encryptedNbr[7]);
            encryptMixtedNbr[1] = (Int16)(encryptedNbr[1] - encryptedNbr[4] > 0 ? encryptedNbr[1] - encryptedNbr[4] : encryptedNbr[4] - encryptedNbr[1]);
            encryptMixtedNbr[2] = (Int16)(encryptedNbr[2] * (encryptedNbr[6] / 7) + encryptedNbr[0]);
            encryptMixtedNbr[3] = (Int16)(encryptedNbr[3] / (encryptedNbr[5] == 0 ? 1 : encryptedNbr[5]));
            encryptMixtedNbr[4] = (Int16)(encryptedNbr[4] + encryptedNbr[0] / 3);
            encryptMixtedNbr[5] = (Int16)(encryptedNbr[5] - encryptedNbr[3] >= 0 ? encryptedNbr[5] - encryptedNbr[3] / 3 : encryptedNbr[3] - encryptedNbr[5] / 3);
            encryptMixtedNbr[6] = (Int16)(encryptedNbr[6] * (encryptedNbr[1] / 7 + encryptedNbr[2] / 9));
            encryptMixtedNbr[7] = (Int16)(encryptedNbr[7] / (encryptedNbr[2] / 3 == 0 ? 1 : encryptedNbr[2])); //To be checked

            for (int i = 0; i < lenght; i++)
            {
                encryptedNbr[i] = (Int16)(encryptMixtedNbr[i] % maxSize);
            }

            return encryptedNbr;
        }

        private Int16 GetRandomValue(Int16 carValue, Int16 key)
        {
            Int16 newAmt = (Int16)((key / 6 + 1) * carValue + key);
            if (newAmt % 5 == 0)
            {
                newAmt -= key;
            }
            else if (newAmt % 7 == 0)
            {
                newAmt += 27;
            }
            else if (newAmt % 9 == 0)
            {
                newAmt += 177;
            }
            else if (newAmt % 11 == 0)
            {
                newAmt += 1;
            }
            else if (newAmt % 2 == 0)
            {
                newAmt -= 100;
            }

            if (newAmt < 0) newAmt *= -1;

            var strValue = newAmt.ToString();
            if (strValue.IndexOf('2') > 0)
            {
                if (newAmt > 3000)
                {
                    newAmt = (Int16)(newAmt / 3 + 2);
                }
                else
                {
                    newAmt = (Int16)(newAmt * 4 - 7);
                }
            }
            else if (strValue.IndexOf('4') > 0)
            {
                if (newAmt > 3000)
                {
                    newAmt = (Int16)(newAmt / 7 + 2);
                }
                else
                {
                    newAmt = (Int16)(newAmt * 3 - 9);
                }
            }
            else if (strValue.IndexOf('8') > 0)
            {
                newAmt += (Int16)(key / 7 * key);

                newAmt = (Int16)(maxSize - ((newAmt - 1) % maxSize));
            }
            else
            {
                newAmt = (Int16)((newAmt + key * 21 + carValue) ^ key);
            }

            newAmt -= 1;
            if (newAmt == carValue) newAmt = (Int16)(newAmt + 17);

            if (newAmt < 0) newAmt *= -1;

            newAmt = (Int16)(newAmt % maxSize);

            return newAmt;
        }
    }
}
