namespace IndiaWalks.APi.DTOs
{
    public class WalkListReqDto : PaginationDTO
    {
        public string? filterOn { get; set; }
        public string? filterQuery { get; set; }
    }
}
