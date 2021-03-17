namespace MoraES
{
    public static class MoraLogger
    {
        public static void Error(string error)
        {
            System.Console.WriteLine($"⛔ [MORA][WARNING][{System.DateTime.Now.ToUniversalTime().ToString()}]:{error}");

        }

        public static void Info(string info)
        {
            System.Console.WriteLine($"☝ [MORA][INFO][{System.DateTime.Now.ToUniversalTime().ToString()}]:{info}");

        }

        public static void Warning(string warning)
        {
            System.Console.WriteLine($"📣 [MORA][WARNING][{System.DateTime.Now.ToUniversalTime().ToString()}]:{warning}");
        }
    }
}