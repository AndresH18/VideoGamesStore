namespace GameStore.Data.ViewModels;

public class PageInfo
{
    public required uint TotalItems { get; set; }
    public required uint ItemsPerPage { get; set; }
    public required uint CurrentPage { get; set; }

    public uint TotalPages => (uint) Math.Ceiling((decimal) TotalItems / ItemsPerPage);
}