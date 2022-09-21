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
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("CreateCity")]
        public async Task<IActionResult> Save([FromBody] CityDTO cityDTO)
        {
            if (cityDTO == null)
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
                var checkState = await _unitOfWork.State.GetById(cityDTO.StateId);

                if (checkState == null)
                {
                    var apiResponse = new ApiResponse
                    {
                        Code = "400",
                        IsSuccessful = false,
                        Message = "State Not Found. Check the StateID",
                        Data = null
                    };
                    return BadRequest(apiResponse);
                }
                else
                {

                    var city = new City
                    {
                        StateId = cityDTO.StateId,
                        CityName = cityDTO.CityName,
                        
                    };

                    await _unitOfWork.City.Add(city);

                    _unitOfWork.Complete();

                    var apiResponse = new ApiResponse
                    {
                        Code = "201",
                        IsSuccessful = true,
                        Message = "Successful request, A new City has been created",
                        Data = cityDTO
                    };

                    return Ok(apiResponse);
                }
            }

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCity(int Id)
        {
            var city = await _unitOfWork.City.GetById(Id);

            if (city == null)
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
                var cityDetails = new CityDTO
                {
                    CityName = city.CityName,
                    StateId = city.StateId,
                    CityId = city.CityId
                };

                var apiResponse = new ApiResponse
                {
                    Code = "200",
                    IsSuccessful = true,
                    Message = "Successful Fetch",
                    Data = cityDetails
                };

                return Ok(apiResponse);
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCity(int Id, [FromBody] CityDTO cityDTO)
        {
            var getCity = await _unitOfWork.City.GetById(Id);

            if (getCity == null)
            {
                var apiResponse = new ApiResponse
                {
                    Code = "400",
                    IsSuccessful = false,
                    Message = "Not Found. Check the CityID",
                    Data = null
                };
                return BadRequest(apiResponse);
            }
            else
            {
                var checkState = await _unitOfWork.State.GetById(cityDTO.StateId);

                if (checkState == null)
                {
                    var apiResponse = new ApiResponse
                    {
                        Code = "400",
                        IsSuccessful = false,
                        Message = "State Not Found. Check the StateID",
                        Data = null
                    };
                    return BadRequest(apiResponse);
                }
                else
                {
                    getCity.CityName = cityDTO.CityName;
                    getCity.StateId = cityDTO.StateId;

                    _unitOfWork.City.Update(getCity);

                    _unitOfWork.Complete();

                    var apiResponse = new ApiResponse
                    {
                        Code = "200",
                        IsSuccessful = true,
                        Message = "Update Successful",
                        Data = cityDTO
                    };

                    return Ok(apiResponse);
                }

            }

        }

        [HttpGet("GetAllCity")]
        public async Task<IActionResult> GetAllCity()
        {
            var getAllCity= await _unitOfWork.City.GetAll();


            if (getAllCity == null)
            {
                var apiResponse = new ApiResponse
                {
                    Code = "404",
                    IsSuccessful = false,
                    Message = "City can not be found ",
                    Data = null
                };
                return BadRequest(apiResponse);
            }
            else
            {
                List<CityDTO> cities = new List<CityDTO>();

                foreach (var city in getAllCity)
                {
                    var cityDTO = new CityDTO
                    {
                        CityId = city.CityId,
                        CityName = city.CityName,
                        StateId = city.StateId
                    };
                    cities.Add(cityDTO);
                }

                var apiResponse = new ApiResponse
                {
                    Code = "200",
                    IsSuccessful = true,
                    Message = "List of Cities",
                    Data = cities
                };

                return Ok(apiResponse);
            }
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCity(int Id)
        {
            var getCity = await _unitOfWork.City.GetById(Id);

            if (getCity == null)
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
                _unitOfWork.City.Delete(getCity);

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
