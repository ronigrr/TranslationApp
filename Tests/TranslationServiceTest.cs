using TranslationApp.Abstractions.Entities;
using TranslationApp.Infrastructure;
using TranslationApp.Services;
using Xunit;

namespace TranslationApp.Tests;
using Moq;

public class TranslationServiceTest
{
    [Fact]
    public async Task TranslateToHebrewAsync_ReturnsCachedTranslation_IfExists()
    {
        // Arrange
        const string inputWord = "world";
        const string expectedTranslation = "עולם";

        var existingEntry = new TranslationEntry(inputWord, expectedTranslation);

        var repositoryMock = new Mock<ITranslationRepository>();
        repositoryMock.Setup(r => r.GetTranslation(inputWord))
            .ReturnsAsync(existingEntry);

        var service = new GoogleTranslationService(repositoryMock.Object);

        // Act
        var result = await service.TranslateToHebrewAsync(inputWord);

        // Assert
        Assert.Equal(expectedTranslation, result);
        repositoryMock.Verify(r => r.GetTranslation(inputWord), Times.Once);
        repositoryMock.Verify(r => r.SaveTranslation(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }
}