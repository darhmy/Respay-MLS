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
    public class StateController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StateController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("CreateState")]
        public async Task<IActionResult> Save([FromBody] StateDTO stateDTO)
        {
            if (stateDTO == null)
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
                var checkCountry = await _unitOfWork.Country.GetById(stateDTO.CountryId);

                if (checkCountry == null)
                {
                    var apiResponse = new ApiResponse
                    {
                        Code = "400",
                        IsSuccessful = false,
                        Message = "Country Not Found. Check the CountryID",
                        Data = null
                    };
                    return BadRequest(apiResponse);
                }
                else
                {

                    var state = new State
                    {
                        StateName = stateDTO.StateName,
                        CountryId = stateDTO.CountryId,
                    };

                    await _unitOfWork.State.Add(state);

                    _unitOfWork.Complete();

                    var apiResponse = new ApiResponse
                    {
                        Code = "201",
                        IsSuccessful = true,
                        Message = "Successful request, A new State has been created",
                        Data = stateDTO
                    };

                    return Ok(apiResponse);
                }
            }

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetState(int Id)
        {
            var state = await _unitOfWork.State.GetById(Id);

            if (state == null)
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
                var stateDetails = new StateDTO
                {
                    StateId = state.StateId,
                    StateName = state.StateName,
                    CountryId=state.CountryId,
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
        public async Task<IActionResult> UpdateState(int Id, [FromBody] StateDTO stateDTO)
        {
            var getState = await _unitOfWork.State.GetById(Id);

            if (getState == null)
            {
                var apiResponse = new ApiResponse
                {
                    Code = "400",
                    IsSuccessful = false,
                    Message = "Not Found. Check the StateID",
                    Data = null
                };
                return BadRequest(apiResponse);
            }
            else
            {
                var checkCountry = await _unitOfWork.Country.GetById(stateDTO.CountryId);

                if (checkCountry == null)
                {
                    var apiResponse = new ApiResponse
                    {
                        Code = "400",
                        IsSuccessful = false,
                        Message = "Country Not Found. Check the CountryID",
                        Data = null
                    };
                    return BadRequest(apiResponse);
                }
                else
                {
                    getState.StateName = stateDTO.StateName;
                    getState.CountryId = stateDTO.CountryId;

                    _unitOfWork.State.Update(getState);

                    _unitOfWork.Complete();

                    var apiResponse = new ApiResponse
                    {
                        Code = "200",
                        IsSuccessful = true,
                        Message = "Update Successful",
                        Data = stateDTO
                    };

                    return Ok(apiResponse);
                }

            }

        }

        [HttpGet("GetAllState")]
        public async Task<IActionResult> GetAllState()
        {
            var getAllState = await _unitOfWork.State.GetAll();


            if (getAllState == null)
            {
                var apiResponse = new ApiResponse
                {
                    Code = "404",
                    IsSuccessful = false,
                    Message = "States can not be found ",
                    Data = null
                };
                return BadRequest(apiResponse);
            }
            else
            {
                List<StateDTO> states = new List<StateDTO>();

                foreach (var state in getAllState)
                {
                    var stateDTO = new StateDTO
                    {
                        CountryId = state.CountryId,
                        StateName = state.StateName,
                        StateId = state.StateId,
                    };
                    states.Add(stateDTO);
                }

                var apiResponse = new ApiResponse
                {
                    Code = "200",
                    IsSuccessful = true,
                    Message = "List of States",
                    Data = states
                };

                return Ok(apiResponse);
            }
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteState(int Id)
        {
            var getState = await _unitOfWork.State.GetById(Id);

            if (getState == null)
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
                _unitOfWork.State.Delete(getState);

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
