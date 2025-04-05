using App.Models;

namespace App.Helpers
{
    public static class DatatablesHelper
    {
        public static DatatableRequestModel ConstructModel(HttpRequest request)
        {
            DatatableRequestModel datatablesRequest = new DatatableRequestModel();

            datatablesRequest.Draw = request.Form["draw"].FirstOrDefault();
            datatablesRequest.Start = request.Form["start"].FirstOrDefault();
            datatablesRequest.Length = request.Form["length"].FirstOrDefault();

            datatablesRequest.SortColIndex = Convert.ToInt32(request.Form["order[0][column]"]);

            datatablesRequest.SortColumnDirection = request.Form["order[0][dir]"];

            datatablesRequest.SearchValue = request.Form["search[value]"].FirstOrDefault();
            datatablesRequest.PageSize = datatablesRequest.Length != null ? Convert.ToInt32(datatablesRequest.Length) : 0;
            datatablesRequest.Skip = datatablesRequest.Start != null ? Convert.ToInt32(datatablesRequest.Start) : 0;

            return datatablesRequest;
        }
    }
}
