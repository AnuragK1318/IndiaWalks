namespace IndiaWalks.APi.DTOs
{
    public class PaginationDTO
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }= 10;
        public string? route { get; set; }
        public string? sortProperty { get; set; }
        public bool IsDescending { get; set; }

        public PaginationDTO()
        {
            this.route = string.Empty;
            this.PageNumber = 1;
            this.PageSize = 10;
            this.sortProperty = "Name";
            this.IsDescending = true;
        }

    }
}
