using Moq;
using Microsoft.Extensions.Logging;

namespace GymManager.Tests.Helpers;

public static class TestHelper
{
    public static ILogger<T> GetLogger<T>() => new Mock<ILogger<T>>().Object;
}
