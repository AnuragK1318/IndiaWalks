namespace IndiaWalks.APi.DTOs
{
    public class PaginationDTO
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }= 10;

        public PaginationDTO()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }

    }
}
