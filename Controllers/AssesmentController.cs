using Assesment.DTOs;
using Assesment.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Assesment.Controllers
{
    public class AssesmentController : Controller
    {
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
                    //TODO :Could check if numbers are distinct
                    int secondLargest = await Task.Run(() => numbers.OrderByDescending(n => n).Distinct().Skip(1).First());

                    return  Ok(new { secondLargest });
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
         *
         */
        [HttpGet("get_countries")]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            var apiUrl = "https://restcountries.com/v3.1/all";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(apiUrl);

                try
                {
                    //var countries = JsonConvert.DeserializeObject<List<Country>>(response);
                    var details = JArray.Parse(response);
                    
                    List<Country> result = new List<Country>();
                    for (int i=0; i<details.Count();i++) {
                        Country country = new Country();
                        country.Id = i;
                        country.CommonName = details[i]["name"]?["common"]?.ToString();
                        country.Capital = details[i]["capital"]?[0]?.ToString();
                        //country.Borders = new List<Border>();
                        //country.Borders = details[i]["borders"]?.ToObject<List<string>>() ?? new List<string>();
                        var borders = details[i]["borders"]?.ToObject<List<string>>() ?? new List<string>();
                        for (int j=0; j < borders.Count(); j++)
                        {
                            Border border = new Border()
                            {
                                Id = j,
                                Name = borders[j].ToString(),
                                CountryId = country.Id
   
                            };
                            country.Borders.Add(border);
                            
                        }
                        result.Add(country);

                    }


                   


                    return Ok(new { Countries = result });
                }
                catch (JsonReaderException ex)
                {
                    return BadRequest(ex);
                }
            }

            
        }

    }
}
