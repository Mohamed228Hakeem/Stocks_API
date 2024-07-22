using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route ("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository CommentRepo,IStockRepository stockRepo)
        {
            _commentRepo = CommentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(){
            //this if Condition will act when any of the validation we did on the DTOS is not Performed
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await _commentRepo.GetAllAsync();

            var commentdto = comments.Select(x => x.ToCommentDto());

            return Ok(commentdto);
        }

        [HttpGet ("{id:int}")]
        public async Task<ActionResult> GetById([FromRoute] int id){

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());


    }

    [HttpPost("{StockId:int}")]

    public async Task<IActionResult> Create([FromRoute] int StockId,CreateCommentDto createCommentDto){
        
        if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

        if (!await _stockRepo.StockExists(StockId))
        {
            return BadRequest("Stock is not Availabe DUMMY !");
        }

        var commentModel = createCommentDto.ToCommentFromCreateDto(StockId);
        await _commentRepo.CreateAsync(commentModel);
        return CreatedAtAction(nameof(GetById),new {id = commentModel.Id},commentModel.ToCommentDto());
}

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateAsync ([FromRoute] int id,[FromBody]CommentUpdateDto updateComment){
        if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        var CommentModel = await _commentRepo.UpdateAsync(id,updateComment.ToCommentFromUpdateDto());

        if (CommentModel == null){return NotFound();}

        return Ok(CommentModel.ToCommentDto());
}


    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id){
        if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        var CommentModel = await _commentRepo.DeleteAsync(id);
       
        if (CommentModel == null){return NotFound("Comment doesnt exist YOU DUMMY");}
        
        return Ok(CommentModel);    

}
}
}