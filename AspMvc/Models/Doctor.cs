namespace AspMvc.Models
{

    public class Doctor
    {
        public static string CheckTemperature(int temperature, string degrees)
        {
            string message = "Your temperature is normal.";
            int hypothermia = 35;
            int fever = 38;
            if (degrees == "fahrenheit")
            {
                hypothermia = 95;
                fever = 100;
            }
            if (temperature <= hypothermia)
                message = "You have hypothermia!";
            if (temperature >= fever)
                message = "You have a fever!";
            return message;
        }
    }

}
