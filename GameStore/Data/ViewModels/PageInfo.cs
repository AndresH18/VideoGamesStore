namespace GameStore.Data.ViewModels;

public class PageInfo
{
    public uint TotalItems { get; set; }
    public uint ItemsPerPage { get; set; }
    public uint CurrentPage { get; set; }

    public uint TotalPages => (uint) Math.Ceiling((decimal) TotalItems / ItemsPerPage);
}