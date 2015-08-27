namespace MP3tager
{
    static class Messeges
    {
        public static string ShortHelp = "Something wrong.\nFor getting help run app with \"help\" key.";
        //todo: Rewrite long help
        public static string LongHelp = "Insert arguments as folowing string" +
                                        "\n(mode) (path) (pattern)\n" +
                                        "\nmode\t( rename | retag ) working with alone files" +
                                        "\n\t( diff | synch ) working with directories" +
                                        "\npattern is string with folowing tags"+
                                        "\n\t<tr>\t- track id" +
                                        "\n\t<ar>\t- artist" +
                                        "\n\t<ti>\t- title" +
                                        "\n\t<al>\t- album" +
                                        "\n\t<ye>\t- year";
        public static string FileNotExist = "File not exist or path not valid.";
        public static string FilesToRename = "Files to rename";
        public static string FilesToTag = "Files to retag";
    }
}
