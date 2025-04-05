namespace App.Models
{
    public class DatatableRequestModel
    {
        public string? Draw { get; set; }
        public string? Start { get; set; }
        public string? Length { get; set; }
        public int SortColIndex { get; set; }
        public string? SortColumnDirection { get; set; }
        public string? SearchValue { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public bool IsAsc
        {
            get
            {
                if (SortColumnDirection == "asc")
                {
                    return true;
                }
                return false;
            }
        }
        public bool CanSort
        {
            get
            {
                if (SortColIndex >= 0 && !string.IsNullOrEmpty(SortColumnDirection))
                {
                    return true;
                }
                return false;
            }
        }
    }
}
