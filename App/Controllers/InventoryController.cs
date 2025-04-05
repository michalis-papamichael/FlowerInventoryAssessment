using App.Helpers;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ServiceLayer.ServiceDtos.Flowers;
using ServiceLayer.ServiceResponder;
using ServiceLayer.Services;

namespace App.Controllers
{
    public class InventoryController : Controller
    {
        private readonly FlowersServices _flowersServices;
        public InventoryController(FlowersServices flowersServices)
        {
            _flowersServices = flowersServices;
        }
        public IActionResult Details()
        {
            // used to change layout from the Home controller to InventoryLayout see _ViewStart
            ViewData["Layout"] = "_InventoryLayout";
            ViewData["HeadingContent"] = "Here you can create new flowers, edit or delete existing ones.";
            return View();
        }
        // used for datatable calls
        [HttpPost]
        public async Task<IActionResult> GetFlowers()
        {
            DatatableRequestModel dtModel = DatatablesHelper.ConstructModel(Request);
            ServiceResponse<SFlowersPagingDto> response = await _flowersServices.GetFlowersWithPaging(dtModel.Skip, dtModel.PageSize);
            if (response.Success && response.Data != null)
            {
                var dto = response.Data.Flowers;
                if (dto.Count > 0)
                {
                    // sorting
                    //if (dtModel.CanSort)
                    //{
                    //    if (dtModel.IsAsc)
                    //    {
                    //    }
                    //    else
                    //    {
                    //    }
                    //}

                    // searching
                    if (!string.IsNullOrEmpty(dtModel.SearchValue))
                    {
                        string searchValue = dtModel.SearchValue.ToLower();
                        dto = dto.Where(x => x.Name.ToLower().Contains(searchValue)
                        || x.CategoryName.ToLower().Contains(searchValue)
                        || x.Price.ToString().ToLower().Contains(searchValue)).ToList();
                    }
                    int totalrecords = response.Data.TotalFlowers;
                    var json = new
                    {
                        draw = dtModel.Draw,
                        recordsFiltered = totalrecords,
                        recordsTotal = totalrecords,
                        data = dto,
                    };
                    return Ok(json);
                }

            }
            else
            {
                if (response.Exception != null)
                {
                    Log.Warning(response.Exception, response?.Message ?? "");
                }
                else
                {
                    Log.Warning(response?.Message ?? "");
                }
            }
            var errorjson = new
            {
                draw = dtModel.Draw,
                recordsTotal = 0,
            };
            return Ok(errorjson);
        }
    }
}
