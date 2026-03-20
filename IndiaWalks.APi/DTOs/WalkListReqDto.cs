namespace IndiaWalks.APi.DTOs
{
    public class WalkListReqDto : PaginationDTO
    {
        //Filtering
        public string? filterOn { get; set; }
        public string? filterQuery { get; set; }

        //Sorting
        public string? sortBy { get; set; }
        public bool isAscending { get; set; }=true;
    }
}
