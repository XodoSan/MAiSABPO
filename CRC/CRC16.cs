namespace CRCFUCK
{
    public static class CRCCustom
    {
        public static string ExecuteCRC(string stringMessage, string stringPolinom)
        {
            int passCounter = 0;
            string dividend = stringMessage;
            string pastDividend = dividend;

            while (dividend.Length >= stringPolinom.Length)
            {
                pastDividend = dividend;
                dividend = "";
                bool isTrue = false;
                int iteratePassCounter = 0;

                for (int counter = 0; counter < stringPolinom.Length; counter++)
                {
                    int xor = pastDividend[counter] ^ stringPolinom[counter];
                    if (xor == 1)
                        isTrue = true;

                    if (!(xor == 0 && !isTrue))
                        dividend += pastDividend[counter] ^ stringPolinom[counter];
                    else
                    {
                        passCounter++;
                        iteratePassCounter++;
                    }
                }

                pastDividend = dividend;
                if (dividend.Length < stringPolinom.Length)
                {
                    for (int i = passCounter + pastDividend.Length; i < passCounter + pastDividend.Length + iteratePassCounter; i++)
                    {
                        if (stringMessage.Length <= i)
                        {
                            break;
                        }
                        dividend += stringMessage[i];
                    }
                }
            }

            return dividend;
        }
    }
}