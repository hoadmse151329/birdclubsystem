namespace WebAPI.Utilities
{
    public class OtpGenerator
    {
        private int OtpLength;
        public OtpGenerator(int length)
        {
            OtpLength = length;
        }
        public string GenerateRandomOTP()

        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string sOTP = string.Empty;

            string sTempChars = string.Empty;

            Random rand = new Random();

            for (int i = 0; i < OtpLength; i++)

            {

                int p = rand.Next(0, saAllowedCharacters.Length);

                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                sOTP += sTempChars;

            }

            return sOTP;
        }
    }
}
