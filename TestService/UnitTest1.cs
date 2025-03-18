using Services.Parser;

namespace TestService
{
    public class UnitTest1
    {
        public UnitTest1()
        {

        }
        public static string _command = "  create database йоха";

        [Fact]
        public void Trim()
        {   // Arrange

            var start = 0;
            var span = _command.AsSpan();
            // Act
            var result = TokenParser.Trim(ref span, start);

            // Assert
            Assert.Equal(2, result);
        }
        [Fact]
        public void GetFirstChar()
        {
            // Arrange
            var span = _command.AsSpan();// "  create database йоха".AsSpan();
            var start = 0;
            // Act
            var result = TokenParser.GetFirstChar(ref span, ref start);

            // Assert
        }
        [Fact]
        public void GetKeyword()
        {
            // Arrange
            var span = _command.AsSpan();
            var start = 0;
            char? key;

            // Act
            key = TokenParser.GetFirstChar(ref span, ref start);
            var result = TokenParser.GetKeyword(ref span, ref start, key);
            // Assert
        }
        [Fact]
        public void GetToken()
        {
            var span = _command.AsSpan();
            //var start = 0;
            // var token = TokenParser.ParseToken(ref span, ref start);
        }
    }
}
