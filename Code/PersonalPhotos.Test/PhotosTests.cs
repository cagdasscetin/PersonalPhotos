using System.Text;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PersonalPhotos.Controllers;
using PersonalPhotos.Models;

namespace PersonalPhotos.Test;

public class PhotosTests
{
    [Fact]
    public async Task? Upload_GivenFileName_ReturnsDisplayAction()
    {
        // Arrange
        var session = Mock.Of<ISession>();
        session.Set("User", Encoding.UTF8.GetBytes("test@gmail.com"));
        var httpContext = Mock.Of<HttpContext>(x => x.Session == session);
        var accessor = Mock.Of<IHttpContextAccessor>(x => x.HttpContext == httpContext);

        var keyGenerator = Mock.Of<IKeyGenerator>();
        var photoMetaData = Mock.Of<IPhotoMetaData>();
        var fileStorage = Mock.Of<IFileStorage>();

        var formFile = Mock.Of<IFormFile>();
        var photoViewModel = Mock.Of<PhotoUploadViewModel>(x => x.File == formFile);
        
        var controller = new PhotosController(keyGenerator, accessor, photoMetaData, fileStorage);
        
        // Act
        var result = await controller.Upload(photoViewModel) as RedirectToActionResult;
        
        // Assert
        Assert.Equal("Display", result.ActionName, ignoreCase: true);
    }
}