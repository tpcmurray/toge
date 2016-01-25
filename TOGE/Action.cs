namespace TOGE
{
    public class Action
    {
        public Action(string letter, string command)
        {
            CommandLetter = letter;
            CommandFull = command;
        }
        public string CommandLetter { get; set; }
        public string CommandFull { get; set; }
    }
}
