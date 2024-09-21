using Api.Dtos;
using Api.Entities;
using Api.Services.NftService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NftController : ControllerBase
    {
        private readonly INftRepository _nftrepository;
        private readonly IMapper _mapper;

        public NftController(INftRepository nftRepository,IMapper mapper)
        {
            _nftrepository= nftRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<NftDto>))]
        public IActionResult GetNfts()
        {
            var Nfts=_mapper.Map<List<NftDto>>(_nftrepository.GetNfts());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Nfts);
        }

        [HttpGet("{nftId}")]
        [ProducesResponseType(200, Type = typeof(Nft))]
        [ProducesResponseType(400)]
        public IActionResult GetNft(int nftId)
        {
            if(!_nftrepository.NftExist(nftId))
            {
                return NotFound();
            }

            var nft=_mapper.Map<NftDto>(_nftrepository.GetNftById(nftId));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(nft);
        }

        [HttpPost]
        [Authorize(Roles = "Seller")] 
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateNft([FromBody] NftDto nfts)
        {
            if (nfts == null)
            {
                return BadRequest();
            }

            var nft = _nftrepository.GetNfts()
                         .Where(n => n.Title.Trim().ToUpper() == nfts.Title.TrimEnd().ToUpper())
                         .FirstOrDefault();

            if (nft != null) 
            {
                ModelState.AddModelError("", "Nft already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             
            var nftMap = _mapper.Map<Nft>(nfts);

            if (!_nftrepository.CreateNft(nftMap))
            {
                ModelState.AddModelError("", "Something went wrong when saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }


        [HttpPut("{nftId}")]
        [Authorize(Roles = "Seller")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateNft(int nftId, [FromBody] NftDto updatedNft)
        {
            if (updatedNft == null)
            {
                return BadRequest(ModelState);
            }

            if (!_nftrepository.NftExist(nftId))
            {
                return NotFound();
            }

            var nft = _nftrepository.GetNftById(nftId);

            if (nft == null)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (nft.UserId != currentUserId)
            {
                return Forbid("You do not have permission to update this NFT.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var NftMap = _mapper.Map<Nft>(updatedNft);
            NftMap.Id = nftId;

            if (!_nftrepository.UpdateNft(NftMap))
            {
                ModelState.AddModelError("", "something went Wrong updateing nft");
                return StatusCode(500,ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{NftId}")]
        [Authorize(Roles = "Seller")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteNft(int NftId)
        {
            if(!_nftrepository.NftExist(NftId))
            {
                return NotFound("NFT not found.");
            }

            var nftToDelete=_nftrepository.GetNftById(NftId);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!_nftrepository.DeleteNft(nftToDelete))
            {
                ModelState.AddModelError("", "Somhting wromg in delerting nft");
            }

            return Ok("NFT successfully deleted.");
        }






    }
}
