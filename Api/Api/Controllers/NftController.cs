using Api.Dtos;
using Api.Entities;
using Api.Services.NftService;
using AutoMapper;
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
        [ProducesResponseType(200,Type=typeof(IEnumerable<Nft>))]
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult CreateNft([FromBody] NftDto nfts)
        {
            if(nfts==null)
            {
                return BadRequest();
            }

            var nft= _nftrepository.GetNfts().Where(n=>n.Title.Trim().ToUpper()==nfts.Title.TrimEnd().ToUpper()).FirstOrDefault();

            if(nft==null)
            {
                ModelState.AddModelError("", "Nft already Exsits");
                return StatusCode(422,ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var NftMap = _mapper.Map<Nft>(nfts);

            if (!_nftrepository.CreateNft(NftMap))
            {
                ModelState.AddModelError("", "Something whent wrong when saving");
                return StatusCode(500,ModelState);
            }

            return Ok("sucessdully Created");
        }

        [HttpPut("{nftId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateNft(int nftId, [FromBody] NftDto updatedNft)
        {
            if (updatedNft == null)
            {
                return BadRequest(ModelState);
            }

            if (nftId != updatedNft.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_nftrepository.NftExist(nftId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var NftMap = _mapper.Map<Nft>(updatedNft);

            if (!_nftrepository.UpdateNft(NftMap))
            {
                ModelState.AddModelError("", "something went Wrong updateing nft");
                return StatusCode(500,ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{NftId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteNft(int NftId)
        {
            if(!_nftrepository.NftExist(NftId))
            {
                return NotFound();
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

            return NoContent();

        }






    }
}
