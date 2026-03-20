namespace IndiaWalks.APi.DTOs
{
    public class RegionListRequestDto : PaginationDTO
    {
        //For Filtering
        public string? filterOn { get; set; }
        public string? filterQuery { get; set; }

        //For Sorting
        public string? sortBy { get; set; }
        public bool isAscending { get; set; } = true; 
    }
}
