﻿using Logistics.Application.v1.Controllers.Base;
using Logistics.Domain.Constants;
using Logistics.Domain.Dto.Occurrences;
using Logistics.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Logistics.Application.v1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/occurrence/")]
    public class OccurrenceController : MainController
    {
        private readonly IOccurrenceService _occurrenceService;

        public OccurrenceController(IOccurrenceService occurrenceService)
        {
            _occurrenceService = occurrenceService;
        }

        [SwaggerOperation("Returns a data base occurrence by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(OccurrenceResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, ReturnMessageOccurrence.MessageOccurenceNotFound, typeof(string))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOccurrenceById(int id)
        {
            return Ok(await _occurrenceService.GetOccurrenceById(id));
        }

        [SwaggerOperation("List all database occurrences")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(IList<OccurrencesResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, ReturnMessageOccurrence.MessageOccurencesNotFound, typeof(string))]
        [HttpGet]
        public async Task<IActionResult> GetOccurrences()
        {
            return Ok(await _occurrenceService.GetOccurrences());
        }

        [SwaggerOperation("Insert new occurrence")]
        [SwaggerResponse(StatusCodes.Status200OK, ReturnMessageOccurrence.MessageInsertOcorrence, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, ReturnMessageOrder.MessageOrderNotFound, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, ReturnMessageOccurrence.MessageOccurenceType, typeof(string))]

        [HttpPost]
        public async Task<IActionResult> InsertOccurrence(OccurrenceRequest ocurrence)
        {
            return Ok(await _occurrenceService.InsertOccurrence(ocurrence));
        }

        [SwaggerOperation("Delete occurrence by id")]
        [SwaggerResponse(StatusCodes.Status200OK, ReturnMessageOccurrence.MessageDeleteOccurrence, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, ReturnMessageOccurrence.MessageOccurrenceStatus, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, ReturnMessageOccurrence.MessageOccurenceNotFound, typeof(string))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOccurrence(int id)
        {
            return Ok(await _occurrenceService.DeleteOccurrence(id));
        }

        [SwaggerOperation("Update occurrence by id")]
        [SwaggerResponse(StatusCodes.Status200OK, ReturnMessageOccurrence.MessageUpdateOccurrence, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, ReturnMessageOrder.MessageOrderNotFound, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, ReturnMessageOccurrence.MessageOccurenceNotFound, typeof(string))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOccurrence(UpdateOccurrenceRequest ocurrence, int id)
        {
            return Ok(await _occurrenceService.UpdateOccurrence(ocurrence, id));
        }
    }
}
