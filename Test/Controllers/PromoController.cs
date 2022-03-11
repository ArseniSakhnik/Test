using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test.Exceptions;
using Test.Interfaces;
using Test.Models;
using Test.Services;

namespace Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromoController : ControllerBase
    {
        private readonly IPromoService _promoService;

        public PromoController(IPromoService promoService)
        {
            _promoService = promoService;
        }

        [HttpPost]
        public IActionResult AddPromoReq(PromoActionRequest request) => Ok(_promoService.AddPromo(request));

        [HttpGet]
        public IActionResult GetPromotions() => Ok(_promoService.GetPromotions());

        [HttpGet("{id:int}")]
        public IActionResult GetPromotionById(int id)
        {
            try
            {
                var response = _promoService.GetPromotionById(id);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult ChangePromoById(int id, PromoActionRequest request)
        {
            try
            {
                _promoService.ChangePromoById(id, request);
                return Ok();
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletePromoById(int id)
        {
            try
            {
                _promoService.DeletePromoById(id);
                return Ok();
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id:int}/participant")]
        public IActionResult AddParticipant(int id, AddParticipantRequest request)
        {
            try
            {
                return Ok(_promoService.AddParticipant(id, request));
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{promoId:int}/participant/{participantId:int}")]
        public IActionResult RemoveParticipantFromPromo(int promoId, int participantId)
        {
            try
            {
                _promoService.RemoveParticipantFromPromo(promoId, participantId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id:int}/prize")]
        public IActionResult AddPrizeToPromo(int id, AddPrizeToPromoRequest request)
        {
            try
            {
                return Ok(_promoService.AddPrizeToPromo(id, request));
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/promo/{promoId:int}/prize/{prizeId:int}")]
        public IActionResult DeletePrizeFromPromo(int promoId, int prizeId)
        {
            try
            {
                _promoService.DeletePrizeFromPromo(promoId, prizeId);
                return Ok();
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id:int}/raffle")]
        public IActionResult Raffle(int id)
        {
            try
            {
                var isValid = _promoService.RaffleValidate(id);

                if (!isValid)
                {
                    return StatusCode(409);
                }

                return Ok(_promoService.Raffle(id));
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}