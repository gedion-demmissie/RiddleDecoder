using System;
using System.Text;

namespace RidddleDecoderApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string sampleInputOne = "86111105991011081011151153210511632991141051011154432871051101031081011151153210210811711611610111411544328411111111610410810111511532981051161011154432771111171161041081011151153210911711611610111411546";
            string sampleInputTwo = "5468652073756d2074686520746f74616c206f66207468652061736369692076616c7565732066726f6d2065616368206c657474657220696e2074686520616e7377657220746f2074686520726964646c652e";
            string sampleInputThree = "507473543466548535531534555466536545548466531466546538545544535466549533548535535544467";


            Console.WriteLine("Test Decoding");
            Console.WriteLine("=============");
            Console.WriteLine($"Sample Input one \n{sampleInputOne}\nDecoded Infomration\n{DecodeRiddle(sampleInputOne)}");
            Console.WriteLine("==========================================================================================");
            Console.WriteLine($"Sample Input two \n{sampleInputTwo}\nDecoded Infomration\n{DecodeRiddle(sampleInputTwo)}");
            Console.WriteLine("==========================================================================================");
            Console.WriteLine($"Sample Input three \n{sampleInputThree}\nDecoded Infomration\n{DecodeRiddle(sampleInputThree)}");
            Console.WriteLine("==========================================================================================");
            Console.ReadKey();
        }

        static string DecodeRiddle(string characterInput)
        {
            const int UPPERLIMITOFPRINTABLECHARACTER = 127;
            if (!string.IsNullOrEmpty(characterInput))
            {
                StringBuilder sbDecodedResult = new StringBuilder();
                //check if the input is only digit
                if (System.Text.RegularExpressions.Regex.IsMatch(characterInput, @"^[0-9]+$"))
                {
                    //Decoding decimal information by proper partitioning ( by taking length of 3 or 2 digits at a time).
                    int startIndex = 0, decimalValue;
                    int remainingLegth = characterInput.Length;
                    while(remainingLegth >= 3 )
                    {                      
                        decimalValue = Int32.Parse(characterInput.Substring(startIndex, 3));
                        if (decimalValue <= UPPERLIMITOFPRINTABLECHARACTER)
                        {
                            sbDecodedResult.Append((char)decimalValue);
                            remainingLegth -= 3;
                            startIndex += 3;
                        }
                        else  
                        {
                            //consider two digits if  the three digit decimal value is greater than 127
                            decimalValue = Int32.Parse(characterInput.Substring(startIndex, 2));
                            sbDecodedResult.Append((char)decimalValue);
                            remainingLegth -= 2;
                            startIndex += 2;
                        }
                    }

                    if(remainingLegth > 0)
                    {
                        decimalValue = Int32.Parse(characterInput.Substring(startIndex, remainingLegth));
                        sbDecodedResult.Append((char)decimalValue);
                    }
                }
                else if(System.Text.RegularExpressions.Regex.IsMatch(characterInput, @"^[a-zA-Z0-9]+$")) // if input is alphanumeric only
                {
                    //Decoding haxadecimal information
                    for (int i = 0; i < characterInput.Length; i += 2)
                    {                       
                        var  hexString = characterInput.Substring(i, 2);
                        uint decimalValue = System.Convert.ToUInt32(hexString, 16);
                        char decodedCharacter = System.Convert.ToChar(decimalValue);
                        sbDecodedResult.Append(decodedCharacter);
                    }
                }
                //Decoded information
                return sbDecodedResult.ToString();
            }
            // ToDo: We need clarification how should it behave when the input is invalid
            //if the input is empty or null then return the input itself
            return "invalid input, cannot be decoded!;";
        }
    }
}
