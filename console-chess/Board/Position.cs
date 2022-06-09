namespace board
{
    internal class Position
    {
        public int Lines { get; set; }
        public int Columns { get; set; }

        public Position(int line, int column)
        {
            Lines = line;
            Columns = column;
        }

        public override string ToString()
        {
            return Lines
                + ", "
                +Columns;
        }
    }
}
