using Assesment.DTOs;
using Assesment.Models;
using Assesment.Repositories;
using Assesment.Services.Cache;
using Assesment.Services.CountryService;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Assesment.Controllers
{
    public class AssesmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountryApiService _countryApiService;
        private readonly ICountryRepository _countryRepository;
        private readonly ICacheService _cacheService;
        public AssesmentController(IMapper mapper,
            ICountryApiService countryApiService,
            ICountryRepository countryRepository,
            ICacheService cacheServise)
        {
            _mapper = mapper;
            _countryApiService = countryApiService;
            _countryRepository = countryRepository;
            _cacheService = cacheServise;
        }
        /*QUESTION 1
         *Input: A JSON body of RequestObj
         *Output: The second largest integer of the array gets returned.
         */
        [HttpPost("FindSecondLargest")]
        public async Task<IActionResult> FindSecondLargest(RequestObj requestObj)
        {
            try
            {
                //Validate that RequestObj exists and has an array of numbers
                if (requestObj.RequestArrayObj!=null && requestObj.RequestArrayObj.Any())
                {
                    IEnumerable<int> numbers = requestObj.RequestArrayObj;

                    if (numbers.Count() < 2)
                    {
                        return BadRequest(new { error = "The given array should have at least two integers" });
                    }
                    //If numbers are at least 2 return the second largest
                    int secondLargest = await Task.Run(() => numbers.OrderByDescending(n => n).Distinct().Skip(1).First());

                    return Ok(new { secondLargest });
                }
                else
                {
                    return BadRequest(new { error = "Invalid request payload" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal Server Error" });
            }
        }
        /*QUESTION 2
         *Output: IEnumerable<Country> with common name,capital and borders as its fields
         *which will be retrieved either by the third-party api, the cache or the database.
        */
        [HttpGet("get_countries")]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries()
        {
            var apiUrl = "https://restcountries.com/v3.1/all";
            try
            {
                //Try to get data from cache and then from db
                var countryList = _cacheService.GetFromCacheOrDatabase();
                if (countryList!=null && countryList.Any())
                {
                    return Ok(countryList);
                }
                //Else try to fetch countries from 3rd party api  
                List<Country> countries = await _countryApiService.GetCountriesFromApiAsync(apiUrl);

                //Save data to cache and db
                _cacheService.SetCountryDataToCache(countries);
                _countryRepository.AddCoutryList(countries);

                return Ok(new { Countries = countries.Select(coutryDto => _mapper.Map<CountryDto>(coutryDto)) });
            }
            catch (JsonReaderException ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("DeleteAllCountriesUtility")]
        public ActionResult DeleteCountries()
        {
            try
            {
                bool success = _countryRepository.DeleteAll();
                if (success) {
                    return Ok(new { answer="Deleted all countries and their borders" });
                }
                else { return BadRequest(new { answer = "There was some issue" }); }
            }catch (Exception ex)
            {
                return StatusCode(500, new { ex });
            }
        }
       
    }


}
