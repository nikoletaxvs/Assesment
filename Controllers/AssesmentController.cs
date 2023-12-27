using Assesment.DTOs;
using Assesment.Models;
using Assesment.Repositories;
using Assesment.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Assesment.Controllers
{
    public class AssesmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly CountryApiService _countryApiService;
        private readonly ICountryRepository _countryRepository;
        private readonly ICacheServise _cacheServise;
        public AssesmentController(IMapper mapper,
            CountryApiService countryApiService,
            ICountryRepository countryRepository,
            ICacheServise cacheServise)
        {
            _mapper = mapper;
            _countryApiService = countryApiService;
            _countryRepository = countryRepository;
            _cacheServise = cacheServise;
        }
        /*QUESTION 1
         *Implement a HTTP Post endpoint that will receive a JSON body of the following class.
         *The second largest integer of the array should be returned.
         *
         */
        [HttpPost("find_second_largest")]
        public async Task<IActionResult> FindSecondLargest(RequestObj requestObj)
        {
            try
            {
                //Validate that RequestObj exists
                if (requestObj != null && requestObj.RequestArrayObj != null && requestObj.RequestArrayObj.Any())
                {
                    IEnumerable<int> numbers = requestObj.RequestArrayObj;

                    if (numbers.Count() < 2)
                    {
                        return BadRequest(new { error = "The given array should have at least two integers" });
                    }

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
                // TODO :Handle exceptions
                return StatusCode(500, new { error = "Internal Server Error" });
            }
        }
        /*QUESTION 2
         *HTTP Get endpoint that will be calling a 3rd Party API. 
         * Return IEnumerable<Country> with common name,capital and borders as its fields
         */
        [HttpGet("get_countries")]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries()
        {
            var apiUrl = "https://restcountries.com/v3.1/all";
            bool countriesExistInDatabase = _countryRepository.DatabaseIsEmpty();
            List <Country> countries;
            var cacheData = _cacheServise.GetData<IEnumerable<CountryDto>>("countries");
            try
            {
                if (cacheData !=null && cacheData.Count()>0)
                {
                    return Ok(cacheData);
                }

                if(countriesExistInDatabase)
                {
                     countries = _countryRepository.GetCoutries();
                }
                else
                {
                    //Get countries from 3rd party api
                     countries = await _countryApiService.GetCountriesAsync(apiUrl);
                    //Store countries in db
                    _countryRepository.AddCoutryList(countries);
                }
                //Add data to cache and set an expiry time
                var expiryTime = DateTimeOffset.Now.AddSeconds(15);
                _cacheServise.SetData<IEnumerable<CountryDto>>("countries", countries.Select(coutryDto => _mapper.Map<CountryDto>(coutryDto)),expiryTime);

                //Respond to get request with countries list
                return Ok(new { Countries = countries.Select(coutryDto => _mapper.Map<CountryDto>(coutryDto)) });
            }
            catch (JsonReaderException ex)
            {
                return BadRequest(ex);
            }
        }


    }


}
