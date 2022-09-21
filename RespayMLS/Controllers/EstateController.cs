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
    public class EstateController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EstateController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("CreateEstate")]
        public async Task<IActionResult> Save([FromBody] EstateDTO estateDTO)
        {
            if (estateDTO == null)
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
                var checkArea = await _unitOfWork.Area.GetById(estateDTO.AreaId);

                if (checkArea == null)
                {
                    var apiResponse = new ApiResponse
                    {
                        Code = "400",
                        IsSuccessful = false,
                        Message = "Area Not Found. Check the AreaID",
                        Data = null
                    };
                    return BadRequest(apiResponse);
                }
                else
                {

                    var estate = new Estate
                    {
                        AreaId = estateDTO.AreaId,
                        EstateName = estateDTO.EstateName
                    };

                    await _unitOfWork.Estate.Add(estate);

                    _unitOfWork.Complete();

                    var apiResponse = new ApiResponse
                    {
                        Code = "201",
                        IsSuccessful = true,
                        Message = "Successful request, A new Estate has been created",
                        Data = estateDTO
                    };

                    return Ok(apiResponse);
                }
            }

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEstate(int Id)
        {
            var estate = await _unitOfWork.Estate.GetById(Id);

            if (estate == null)
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
                var estateDetails = new EstateDTO
                {
                    AreaId = estate.AreaId,
                    EstateId = estate.EstateId,
                    EstateName = estate.EstateName
                };

                var apiResponse = new ApiResponse
                {
                    Code = "200",
                    IsSuccessful = true,
                    Message = "Successful Fetch",
                    Data = estateDetails
                };

                return Ok(apiResponse);
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateEstate(int Id, [FromBody] EstateDTO estateDTO)
        {
            var getEstate = await _unitOfWork.Estate.GetById(Id);

            if (getEstate == null)
            {
                var apiResponse = new ApiResponse
                {
                    Code = "400",
                    IsSuccessful = false,
                    Message = "Not Found. Check the EstateID",
                    Data = null
                };
                return BadRequest(apiResponse);
            }
            else
            {
                var checkArea = await _unitOfWork.Area.GetById(estateDTO.AreaId);

                if (checkArea == null)
                {
                    var apiResponse = new ApiResponse
                    {
                        Code = "400",
                        IsSuccessful = false,
                        Message = "Area Not Found. Check the AreaID",
                        Data = null
                    };
                    return BadRequest(apiResponse);
                }
                else
                {
                    getEstate.AreaId = estateDTO.AreaId;
                    getEstate.EstateName = estateDTO.EstateName;

                    _unitOfWork.Estate.Update(getEstate);

                    _unitOfWork.Complete();

                    var apiResponse = new ApiResponse
                    {
                        Code = "200",
                        IsSuccessful = true,
                        Message = "Update Successful",
                        Data = estateDTO
                    };

                    return Ok(apiResponse);
                }

            }

        }

        [HttpGet("GetAllEstate")]
        public async Task<IActionResult> GetAllEstate()
        {
            var getAllEstate = await _unitOfWork.Estate.GetAll();


            if (getAllEstate == null)
            {
                var apiResponse = new ApiResponse
                {
                    Code = "404",
                    IsSuccessful = false,
                    Message = "Estate can not be found ",
                    Data = null
                };
                return BadRequest(apiResponse);
            }
            else
            {
                List<EstateDTO> estates = new List<EstateDTO>();

                foreach (var estate in getAllEstate)
                {
                    var estateDTO = new EstateDTO
                    {
                        AreaId = estate.AreaId,
                        EstateId = estate.EstateId,
                        EstateName = estate.EstateName
                    };
                    estates.Add(estateDTO);
                }

                var apiResponse = new ApiResponse
                {
                    Code = "200",
                    IsSuccessful = true,
                    Message = "List of Estates",
                    Data = estates
                };

                return Ok(apiResponse);
            }
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEstate(int Id)
        {
            var getEstate = await _unitOfWork.Estate.GetById(Id);

            if (getEstate == null)
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
                _unitOfWork.Estate.Delete(getEstate);

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
