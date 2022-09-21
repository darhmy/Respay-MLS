using Microsoft.AspNetCore.Mvc;
using RespayMLS.Core.DTOs;
using RespayMLS.Core.Interface;
using RespayMLS.Core.Models;
using RespayMLS.Extension;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RespayMLS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("CreateCountry")]
        public async Task<IActionResult> Save([FromBody] CountryDTO countryDTO)
        {

            if (countryDTO == null)
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

                var country = new Country
                {
                    CountryName = countryDTO.CountryName,

                };
                await _unitOfWork.Country.Add(country);

                _unitOfWork.Complete();

                var apiResponse = new ApiResponse
                {
                    Code = "201",
                    IsSuccessful = true,
                    Message = "Successful request, A new Country has been created",
                    Data = countryDTO.CountryName
                };

                return Ok(apiResponse);
            }

            
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCountry(int Id)
        {
            var country = await _unitOfWork.Country.GetById(Id);

            if (country == null)
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
                var countryDetails = new CountryDTO
                {
                    CountryId = country.CountryId,
                    CountryName = country.CountryName
                };

                var apiResponse = new ApiResponse
                {
                    Code = "200",
                    IsSuccessful = true,
                    Message = "Successful Fetch",
                    Data = countryDetails
                };

                return Ok(apiResponse);
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCountry(int Id, [FromBody] CountryDTO countryDTO)
        {
            var getCountry = await _unitOfWork.Country.GetById(Id);

            if (getCountry == null)
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
                getCountry.CountryName = countryDTO.CountryName;

                _unitOfWork.Country.Update(getCountry);

                _unitOfWork.Complete();

                var apiResponse = new ApiResponse
                {
                    Code = "200",
                    IsSuccessful = true,
                    Message = "Update Successful",
                    Data = countryDTO
                };

                return Ok(apiResponse);

            }

        }

        [HttpGet("GetAllCountry")]
        public  IActionResult GetAllCountry()
        {
            var getAllCountry = _unitOfWork.Country.GetAll().Result.ToList();

            if (getAllCountry == null)
            {
                var apiResponse = new ApiResponse
                {
                    Code = "404",
                    IsSuccessful = false,
                    Message = "Countries can not be found ",
                    Data = null
                };
                return BadRequest(apiResponse);
            }
            else
            {
                List<CountryDTO> countries = new List<CountryDTO>();

                foreach(var country in getAllCountry)
                {
                    var countryDTO = new CountryDTO
                    {
                        CountryId = country.CountryId,
                        CountryName = country.CountryName
                    };
                    countries.Add(countryDTO);
                }

                var apiResponse = new ApiResponse
                {
                    Code = "200",
                    IsSuccessful = true,
                    Message = "List of Countries",
                    Data = countries
                };

                return Ok(apiResponse);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCountry(int Id)
        {
          var getCountry = await _unitOfWork.Country.GetById(Id);

            if (getCountry == null)
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
                _unitOfWork.Country.Delete(getCountry);

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
