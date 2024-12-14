using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IImageRepository imageRepository;

		public ImagesController(IImageRepository imageRepository) 
		{
			this.imageRepository = imageRepository;
		}

		[HttpPost]
		[Route("Upload")]
		public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
		{
			ValidateFileUpload(request);

			if (ModelState.IsValid) 
			{
				var imageDomainModel = new Image
				{
					File = request.File,
					FileExtension = Path.GetExtension(request.File.FileName),
					FileSizeInBytes = request.File.Length,
					FileName = request.FileName,
				};

				await imageRepository.Upload(imageDomainModel);

				return Ok(imageDomainModel);
			}

			return BadRequest(ModelState);
		}
		private void ValidateFileUpload(ImageUploadRequestDto request)
		{
			var allowedExtensions = new string[] { ".jpg", ".jped", ".png" };

			if(!allowedExtensions.Contains(Path.GetExtension(request.File.FileName))) 
			{
				ModelState.AddModelError("file", "Unsopported file extension");
			}

			if(request.File.Length > 10485760)
			{
				ModelState.AddModelError("file", "File size is more than 10MB, Required smaller size.");
			}
		}
	}
}
