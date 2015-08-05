namespace MP3tager
{
    static class Messeges
    {
        public static string ShortHelp = "Please enter path to song at first argument and pattern at second.\nFor more infomation run app with \"help\" key.";
        //todo: Rewrite long help
        public static string LongHelp = "Insert file name and pattern with folowing tags:\n\t<tr>\t- track id\n\t<ar>\t- artist\n\t<ti>\t- title\n\t<al>\t- album\n\t<ye>\t- year";
        public static string FileNotExist = "File not exist or path not valid.";
        public static string NotValidPatter = "Pattern is not correct";
        public static string KeyNotFound = "Tag not supported";
        //todo: write new messeges
        public static string InvalidFirsArg = "";
        public static string RetagmodeHelp = "";
        public static string RenamemodeHelp = "";
    }
}
