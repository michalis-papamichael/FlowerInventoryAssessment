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
            return BadRequest();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFlower(CreateFlowerDto model)
        {
            ViewData["Layout"] = "_InventoryLayout";
            try
            {
                ServiceResponse<List<SCategoryDto>> resp = await _categoriesServices.GetCategories();
                if (resp.Success)
                {
                    model.Categories = _mapper.Map<List<CategoryDto>>(resp.Data);
                }
                if (!ModelState.IsValid)
                {
                    model.Messages.Add(new ViewMessage()
                    {
                        Message = "Invalid Input",
                        Status = Enums.MessageStatus.Warning,
                    });
                    return View(model);
                }
                SCreateFlowerDto sdto = _mapper.Map<SCreateFlowerDto>(model);
                ServiceResponse<SFlowerDto> response = await _flowersServices.CreateFlower(sdto);
                if (response.Success && response.Data != null)
                {
                    CreateFlowerDto dto = new CreateFlowerDto();
                    dto.Messages.Add(new ViewMessage()
                    {
                        Message = "Successful creation",
                        Status = Enums.MessageStatus.Success,
                    });
                    dto.Categories = model.Categories;
                    if (dto.Categories.Count>0)
                    {
                        return View(dto);
                    }
                    return RedirectToAction("Details");
                }
                if (model.Categories.Count>0)
                {
                    model.Messages.Add(new ViewMessage()
                    {
                        Message = response.Message,
                        Status = Enums.MessageStatus.Warning,
                    });
                    return View(model);
                }
                return RedirectToAction("Details");
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(CreateFlower)} operation", ex);
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> EditFlower(int id)
        {
            ViewData["Layout"] = "_InventoryLayout";
            ServiceResponse<SFlowerDto> response = await _flowersServices.GetFlowerByIdAsync(id);
            if (response.Success && response.Data != null)
            {
                EditFlowerDto dto = new EditFlowerDto();
                dto = _mapper.Map<EditFlowerDto>(response.Data);
                ServiceResponse<List<SCategoryDto>> resp = await _categoriesServices.GetCategories();
                if (resp.Success && resp.Data != null)
                {
                    List<CategoryDto> categories = _mapper.Map<List<CategoryDto>>(resp.Data);
                    dto.Categories = categories;
                }
                return View(dto);
            }
            Log.Warning(response.Message, response.Exception);
            return RedirectToAction("Details");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFlower(EditFlowerDto model)
        {
            ViewData["Layout"] = "_InventoryLayout";
            try
            {
                ServiceResponse<List<SCategoryDto>> resp = await _categoriesServices.GetCategories();
                if (resp.Success)
                {
                    model.Categories = _mapper.Map<List<CategoryDto>>(resp.Data);
                }
                if (!ModelState.IsValid)
                {
                    model.Messages.Add(new ViewMessage()
                    {
                        Message = "Invalid Input",
                        Status = Enums.MessageStatus.Warning,
                    });
                    return View(model);
                }
                SEditFlowerDto sdto = _mapper.Map<SEditFlowerDto>(model);
                ServiceResponse<SFlowerDto> response = await _flowersServices.EditFlower(sdto);
                if (response.Success && response.Data != null)
                {
                    EditFlowerDto dto = new EditFlowerDto();
                    dto.Messages.Add(new ViewMessage()
                    {
                        Message = "Edit successfully",
                        Status = Enums.MessageStatus.Success,
                    });
                    dto.Categories = model.Categories;
                    if (dto.Categories.Count > 0)
                    {
                        return View(dto);
                    }
                    return RedirectToAction("Details");
                }
                if (model.Categories.Count > 0)
                {
                    model.Messages.Add(new ViewMessage()
                    {
                        Message = response.Message,
                        Status = Enums.MessageStatus.Warning,
                    });
                    return View(model);
                }
                return RedirectToAction("Details");
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(EditFlower)} operation", ex);
            }
            return BadRequest();
        }
    }
}
