namespace VillaAPI.Logging
{
    public class Logging : ILogging
    {
        public void log(string message, string type)
        {
            if(type == "error")
            {
                Console.WriteLine("ERROR - " +  message);
            }
            else Console.WriteLine(message);
        }
    }
}
