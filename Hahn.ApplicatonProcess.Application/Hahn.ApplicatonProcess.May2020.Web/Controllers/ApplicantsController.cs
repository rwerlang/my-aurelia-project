using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.May2020.Domain;
using Hahn.ApplicatonProcess.May2020.Domain.Entities;
using Hahn.ApplicatonProcess.May2020.Domain.Interfaces;
using Hahn.ApplicatonProcess.May2020.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicatonProcess.May2020.Web.Controllers
{
    /// <summary>
    /// API with actions to interact with applicant's data
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ApplicantsController : ControllerBase
    {
        private readonly IApplicantRepository _repo;
        private readonly CountryService _countryService;
        private readonly ILogger<ApplicantsController> _logger;

        /// <summary>
        /// Creates a new instance of the object
        /// </summary>
        /// <param name="repo">The repository for Applicant</param>
        /// <param name="countryService">Service to retrieve a country</param>
        /// <param name="logger">Loggin object</param>
        public ApplicantsController(
            IApplicantRepository repo,
            CountryService countryService,
            ILogger<ApplicantsController> logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Registers a new applicant in the database
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="201">Returns the newly created item.</response>
        /// <response code="400">Returns the corresponding error information.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApplicantViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] ApplicantInputModel model)
        {
            _logger.LogInformation("POST applicants");
            SetRepoEndpoint();

            var validation = await new ApplicantValidator(_countryService).ValidateAsync(model);
            if (!validation.IsValid)
            {
                _logger.LogWarning("BadRequest");
                return BadRequest(validation);
            }

            var applicant = new Applicant(model);
            _repo.Add(applicant);
            await _repo.SaveUnitOfWorkAsync(HttpContext.RequestAborted);

            var result = applicant.GetViewModel(_repo.BaseEndpointUrl);
            var location = result.Links.First(l => l.Action == "GET");

            return Created(location.Href, result);
        }

        /// <summary>
        /// Returns an applicant data for the given id
        /// </summary>
        /// <param name="id">The id that identifies the applicant</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">The object for the given id.</response>
        /// <response code="404">Object not found for id.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicantViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int id)
        {
            _logger.LogInformation("GET applicants/" + id);
            SetRepoEndpoint();

            var result = await _repo.GetSingleByIdAsync(id, HttpContext.RequestAborted);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.GetViewModel(_repo.BaseEndpointUrl));
        }

        /// <summary>
        /// Returns the list of applicants
        /// </summary>
        /// <returns>List of RestEntityModel with applicants</returns>
        [HttpGet]
        public Task<IReadOnlyList<ApplicantViewModel>> ListAsync()
        {
            _logger.LogInformation("GET applicants");
            SetRepoEndpoint();

            return _repo.ListByNameAsync(HttpContext.RequestAborted);
        }

        /// <summary>
        /// Updates an applicant's data for the given id
        /// </summary>
        /// <param name="id">The id that identifies the applicant</param>
        /// <param name="input">Object with the data to be changed.</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">The updated object for the given id.</response>
        /// <response code="400">Returns the corresponding error information.</response>
        /// <response code="404">Object not found for id.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicantViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] ApplicantInputModel input)
        {
            _logger.LogInformation("PUT applicants/" + id);
            SetRepoEndpoint();

            var validation = await new ApplicantValidator(_countryService).ValidateAsync(input);
            if (!validation.IsValid)
            {
                _logger.LogWarning("BadRequest");
                return BadRequest(validation);
            }

            var model = await _repo.GetSingleByIdAsync(id, HttpContext.RequestAborted, trackChanges: true);
            if (model == null)
            {
                return NotFound();
            }

            model.UpdateFromModel(input);
            await _repo.SaveUnitOfWorkAsync(HttpContext.RequestAborted);

            return Ok(model.GetViewModel(_repo.BaseEndpointUrl));
        }

        /// <summary>
        /// Deletes the applicant record from the database
        /// </summary>
        /// <param name="id">The id that identifies the applicant</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Success if the record was deleted.</response>
        /// <response code="404">Object not found for id.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("DELETE applicants/" + id);
            SetRepoEndpoint();

            var model = await _repo.GetSingleByIdAsync(id, HttpContext.RequestAborted, trackChanges: true);
            if (model == null)
            {
                return NotFound();
            }

            _repo.Delete(model);
            await _repo.SaveUnitOfWorkAsync(HttpContext.RequestAborted);

            return Ok();
        }

        private void SetRepoEndpoint()
        {
            var req = HttpContext.Request;
            var url = $"{req.Scheme}://${req.Host}/api/";

            _repo.BaseEndpointUrl = url;
        }
    }
}
