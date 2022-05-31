namespace Backend.Helpers.Models.Responses;
using Backend.DataAccess.Models;

public class GetTablesResponse : BaseResponse
{
    public List<Table>? Tables {get; set;}
}
