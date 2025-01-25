using Robo.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Robo.Services.Robots;

namespace Robo.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RobotController : ControllerBase
    {
        private readonly IRobotService _robotService;
        public RobotController(IRobotService robotService)
        {
            _robotService = robotService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var response = _robotService.GetRobot();

                return Ok(response);
            }
            catch (DomainException ex)
            {
                return StatusCode(ex.StatusCode.GetHashCode(), ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("ChangeStateElbow")]
        public IActionResult ChangeStateElbow([FromQuery] bool contract, [FromQuery] bool right)
        {
            try
            {
                _robotService.ChangeStateElbow(contract, right);
                var response = _robotService.GetRobot();

                return Ok(response);
            }
            catch (DomainException ex)
            {
                return StatusCode(ex.StatusCode.GetHashCode(), ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("RotateWrist")]
        public IActionResult RotateWrist([FromQuery] bool next, [FromQuery] bool right)
        {
            try
            {
                _robotService.RotateWrist(next, right);
                var response = _robotService.GetRobot();

                return Ok(response);
            }
            catch (DomainException ex)
            {
                return StatusCode(ex.StatusCode.GetHashCode(), ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("InclinateHead")]
        public IActionResult InclinateHead([FromQuery] bool next)
        {
            try
            {
                _robotService.InclinateHead(next);
                var response = _robotService.GetRobot();

                return Ok(response);
            }
            catch (DomainException ex)
            {
                return StatusCode(ex.StatusCode.GetHashCode(), ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("RotateHead")]
        public IActionResult RotateHead([FromQuery] bool next)
        {
            try
            {
                _robotService.RotateHead(next);
                var response = _robotService.GetRobot();

                return Ok(response);
            }
            catch (DomainException ex)
            {
                return StatusCode(ex.StatusCode.GetHashCode(), ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}