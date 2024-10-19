using Api.Data;
using Api.Dtos;
using Api.Entities;
using Api.Mapping;
using Api.Models;
using Api.Services.FileService;
using Api.Services.NftService;
using Api.Services.UserService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NftController : ControllerBase
    {
        private readonly INftRepository _nftrepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        public NftController(INftRepository nftRepository,IMapper mapper,AppDbContext appDbContext, IUserService userService, IFileService fileService)
        {
            _nftrepository= nftRepository;
            _mapper = mapper;
            _context = appDbContext;
            _userService = userService;
            _fileService = fileService;
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

        [HttpGet("user")]
        [Authorize]
        public async Task<ActionResult<List<Nft>>>GetMyNfts() 
        {
            try
            {
                string userId = _userService.GetCurrentUserId();
                var nfts = await _context.Nfts.Where(nft => nft.UserId == userId).ToListAsync();
                return Ok(nfts);
            }
            catch (Exception er)
            {
                return BadRequest(er.Message);
            }
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


        [HttpGet("{nftId}/bids")]
        public async Task<IActionResult> GetNftBids(int nftId)
        {
            var nftBids = await _nftrepository.GetNftBidsOnlyAsync(nftId);

            if (nftBids == null)
            {
                return NotFound();
            }

            return Ok(nftBids);
        }

        [HttpPost]
        [Authorize(Roles = "Seller")]
        public async Task<ActionResult> CreateNft(NftDto nfts)
        {
            try
            {
                if (nfts == null)
                {
                    return BadRequest();
                }

                var isnft = _nftrepository.GetNfts()
                             .Where(n => n.Title.Trim().ToUpper() == nfts.Title.TrimEnd().ToUpper())
                             .FirstOrDefault();

                if (isnft != null)
                {
                    ModelState.AddModelError("", "Nft already Exists");
                    return StatusCode(422, ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string userID = _userService.GetCurrentUserId();
                var PhotoPath = await _fileService.SaveFileAsync(new UploadedFile.FileBuilder().File(nfts.Image).AllowImg().Build());

                Nft nft = nfts.ToEntity(userID, PhotoPath);

                if (!_nftrepository.CreateNft(nft))
                {
                    ModelState.AddModelError("", "Something went wrong when saving");
                    return StatusCode(500, ModelState);
                }

                return Ok(nft);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
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
