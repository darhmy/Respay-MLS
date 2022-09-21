using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RespayMLS.Core.DTOs;
using RespayMLS.Core.Interface;
using RespayMLS.Core.Models;
using RespayMLS.Extension;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RespayMLS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AreaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("CreateArea")]
        public async Task<IActionResult> Save([FromBody] AreaDTO areaDTO)
        {
            if (areaDTO == null)
            {
                var apiResponse = new ApiResponse
                {
                    Code = "400",
                    IsSuccessful = false,
                    Message = "Unable to create. Check all parameters",
                    Data = null
                };

                return BadRequest(apiResponse);
            }
            else
            {
                var checkCity = await _unitOfWork.City.GetById(areaDTO.CityId);

                if (checkCity == null)
                {
                    var apiResponse = new ApiResponse
                    {
                        Code = "400",
                        IsSuccessful = false,
                        Message = "City Not Found. Check the CityID",
                        Data = null
                    };
                    return BadRequest(apiResponse);
                }
                else
                {

                    var area = new Area
                    {
                        CityId = areaDTO.CityId,
                        AreaName = areaDTO.AreaName
                        
                    };

                    await _unitOfWork.Area.Add(area);

                    _unitOfWork.Complete();

                    var apiResponse = new ApiResponse
                    {
                        Code = "201",
                        IsSuccessful = true,
                        Message = "Successful request, A new Area has been created",
                        Data = areaDTO
                    };

                    return Ok(apiResponse);
                }
            }

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetArea(int Id)
        {
            var area = await _unitOfWork.Area.GetById(Id);

            if (area == null)
            {
                var apiResponse = new ApiResponse
                {
                    Code = "400",
                    IsSuccessful = false,
                    Message = "Not Found. Check the ID",
                    Data = null
                };
                return BadRequest(apiResponse);
            }
            else
            {
                var areaDetails = new AreaDTO
                {
                    AreaName = area.AreaName,
                    CityId = area.CityId,
                    AreaId = area.AreaId
                };

                var apiResponse = new ApiResponse
                {
                    Code = "200",
                    IsSuccessful = true,
                    Message = "Successful Fetch",
                    Data = areaDetails
                };

                return Ok(apiResponse);
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateArea(int Id, [FromBody] AreaDTO areaDTO)
        {
            var getArea = await _unitOfWork.Area.GetById(Id);

            if (getArea == null)
            {
                var apiResponse = new ApiResponse
                {
                    Code = "400",
                    IsSuccessful = false,
                    Message = "Not Found. Check the AreaID",
                    Data = null
                };
                return BadRequest(apiResponse);
            }
            else
            {
                var checkCity= await _unitOfWork.City.GetById(areaDTO.CityId);

                if (checkCity == null)
                {
                    var apiResponse = new ApiResponse
                    {
                        Code = "400",
                        IsSuccessful = false,
                        Message = "City Not Found. Check the CityID",
                        Data = null
                    };
                    return BadRequest(apiResponse);
                }
                else
                {
                    getArea.AreaName = areaDTO.AreaName;

                    getArea.CityId = areaDTO.CityId;

                    _unitOfWork.Area.Update(getArea);

                    _unitOfWork.Complete();

                    var apiResponse = new ApiResponse
                    {
                        Code = "200",
                        IsSuccessful = true,
                        Message = "Update Successful",
                        Data = areaDTO
                    };

                    return Ok(apiResponse);
                }

            }

        }

        [HttpGet("GetAllArea")]
        public async Task<IActionResult> GetAllArea()
        {
            var getAllArea = await _unitOfWork.Area.GetAll();


            if (getAllArea == null)
            {
                var apiResponse = new ApiResponse
                {
                    Code = "404",
                    IsSuccessful = false,
                    Message = "Area can not be found ",
                    Data = null
                };
                return BadRequest(apiResponse);
            }
            else
            {
                List<AreaDTO> areas = new List<AreaDTO>();

                foreach (var area in getAllArea)
                {
                    var areaDTO = new AreaDTO
                    {
                        CityId = area.CityId,
                        AreaName = area.AreaName,
                        AreaId = area.AreaId
                    };
                    areas.Add(areaDTO);
                }

                var apiResponse = new ApiResponse
                {
                    Code = "200",
                    IsSuccessful = true,
                    Message = "List of Areas",
                    Data = areas
                };

                return Ok(apiResponse);
            }
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteArea(int Id)
        {
            var getArea = await _unitOfWork.Area.GetById(Id);

            if (getArea == null)
            {
                var apiResponse = new ApiResponse
                {
                    Code = "400",
                    IsSuccessful = false,
                    Message = "Not Found. Check the ID",
                    Data = null
                };
                return BadRequest(apiResponse);
            }
            else
            {
                _unitOfWork.Area.Delete(getArea);

                _unitOfWork.Complete();

                var apiResponse = new ApiResponse
                {
                    Code = "200",
                    IsSuccessful = true,
                    Message = "Successfully Deleted",
                    Data = null
                };


                return Ok(apiResponse);

            }

        }

    }
}
