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
    public class StreetController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StreetController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("CreateStreet")]
        public async Task<IActionResult> Save([FromBody] StreetDTO streetDTO)
        {
            if (streetDTO == null)
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
                var checkEstate= await _unitOfWork.Estate.GetById(streetDTO.EstateId);

                if (checkEstate == null)
                {
                    var apiResponse = new ApiResponse
                    {
                        Code = "400",
                        IsSuccessful = false,
                        Message = "Estate Not Found. Check the EstateID",
                        Data = null
                    };
                    return BadRequest(apiResponse);
                }
                else
                {

                    var street = new Street
                    {
                        EstateId = streetDTO.EstateId,
                        StreetName = streetDTO.StreetName,
                        StreetNumber = streetDTO.StreetNumber,
                    };

                    await _unitOfWork.Street.Add(street);

                    _unitOfWork.Complete();

                    var apiResponse = new ApiResponse
                    {
                        Code = "201",
                        IsSuccessful = true,
                        Message = "Successful request, A new Street has been created",
                        Data = streetDTO
                    };

                    return Ok(apiResponse);
                }
            }

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetStreet(int Id)
        {
            var street = await _unitOfWork.Street.GetById(Id);

            if (street == null)
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
                var stateDetails = new StreetDTO
                {
                    EstateId = street.EstateId,
                    StreetId = street.StreetId,
                    StreetName = street.StreetName,
                    StreetNumber = street.StreetNumber
                };

                var apiResponse = new ApiResponse
                {
                    Code = "200",
                    IsSuccessful = true,
                    Message = "Successful Fetch",
                    Data = stateDetails
                };

                return Ok(apiResponse);
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateStreet(int Id, [FromBody] StreetDTO streetDTO)
        {
            var getStreet = await _unitOfWork.Street.GetById(Id);

            if (getStreet == null)
            {
                var apiResponse = new ApiResponse
                {
                    Code = "400",
                    IsSuccessful = false,
                    Message = "Not Found. Check the StreetID",
                    Data = null
                };
                return BadRequest(apiResponse);
            }
            else
            {
                var checkEstate = await _unitOfWork.Estate.GetById(streetDTO.EstateId);

                if (checkEstate == null)
                {
                    var apiResponse = new ApiResponse
                    {
                        Code = "400",
                        IsSuccessful = false,
                        Message = "Estate Not Found. Check the EstateID",
                        Data = null
                    };
                    return BadRequest(apiResponse);
                }
                else
                {
                    getStreet.EstateId = streetDTO.EstateId;
                    getStreet.StreetName = streetDTO.StreetName;
                    getStreet.StreetNumber = streetDTO.StreetNumber;

                    _unitOfWork.Street.Update(getStreet);

                    _unitOfWork.Complete();

                    var apiResponse = new ApiResponse
                    {
                        Code = "200",
                        IsSuccessful = true,
                        Message = "Update Successful",
                        Data = streetDTO
                    };

                    return Ok(apiResponse);
                }

            }

        }

        [HttpGet("GetAllEstate")]
        public async Task<IActionResult> GetAllStreet()
        {
            var getAllStreet = await _unitOfWork.Street.GetAll();


            if (getAllStreet == null)
            {
                var apiResponse = new ApiResponse
                {
                    Code = "404",
                    IsSuccessful = false,
                    Message = "Street can not be found ",
                    Data = null
                };
                return BadRequest(apiResponse);
            }
            else
            {
                List<StreetDTO> streets = new List<StreetDTO>();

                foreach (var street in getAllStreet)
                {
                    var streetDTO = new StreetDTO
                    {
                        EstateId = street.EstateId,
                        StreetId = street.StreetId,
                        StreetName = street.StreetName,
                        StreetNumber = street.StreetNumber
                    };
                    streets.Add(streetDTO);
                }

                var apiResponse = new ApiResponse
                {
                    Code = "200",
                    IsSuccessful = true,
                    Message = "List of Streets",
                    Data = streets
                };

                return Ok(apiResponse);
            }
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteStreet(int Id)
        {
            var getStreet = await _unitOfWork.Street.GetById(Id);

            if (getStreet == null)
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
                _unitOfWork.Street.Delete(getStreet);

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
