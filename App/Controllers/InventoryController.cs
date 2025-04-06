using App.Dtos.Categories;
using App.Dtos.Flowers;
using App.Helpers;
using App.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ServiceLayer.ServiceDtos.Categories;
using ServiceLayer.ServiceDtos.Flowers;
using ServiceLayer.ServiceResponder;
using ServiceLayer.Services;

namespace App.Controllers
{
    public class InventoryController : Controller
    {
        private readonly FlowersServices _flowersServices;
        private readonly CategoriesServices _categoriesServices;
        private readonly IMapper _mapper;
        public InventoryController(FlowersServices flowersServices, CategoriesServices categoriesServices, IMapper mapper)
        {
            _flowersServices = flowersServices;
            _categoriesServices = categoriesServices;
            _mapper = mapper;
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
            try
            {
                if (response.Success && response.Data != null)
                {
                    FlowersPagingDto pagingDto = _mapper.Map<FlowersPagingDto>(response.Data);
                    List<FlowerDto> dto = pagingDto.Flowers;
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
                        int totalrecords = pagingDto.TotalFlowers;
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
            }
            catch (Exception ex)
            {
            }
            var errorjson = new
            {
                draw = dtModel.Draw,
                recordsFiltered = 0,
                recordsTotal = 0,
                data = new List<FlowersPagingDto>(),
            };
            return Ok(errorjson);
        }
        public async Task<IActionResult> CreateFlower()
        {
            ViewData["Layout"] = "_InventoryLayout";
            ServiceResponse<List<SCategoryDto>> response = await _categoriesServices.GetCategories();
            if (response.Success && response.Data != null)
            {
                List<CategoryDto> categories = _mapper.Map<List<CategoryDto>>(response.Data);
                CreateFlowerDto dto = new CreateFlowerDto();
                dto.Categories = categories;
                return View(dto);
            }
            Log.Warning(response.Message, response.Exception);
            //todo handle badrequest globally
            return BadRequest();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFlower(object model)
        {
            ViewData["Layout"] = "_InventoryLayout";
            return View();
        }
    }
}
