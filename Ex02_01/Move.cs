
namespace Ex02_Othelo
{
    public class Move
    {
        public Move(int rowIndex, int columnIndex)
        {
            this.RowIndex = rowIndex;
            this.ColumnIndex = columnIndex;
        }

        public int RowIndex { get; set; }

        public int ColumnIndex { get; set; }
    }
}
