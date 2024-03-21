namespace Gestalt.Core.Tests.ExtensionMethods
{
    using Gestalt.Core.ExtensionMethods;
    using Gestalt.Tests.Helpers;
    using System;
    using Xunit;
    using TObject = System.String;

    public class GenericExtensionsTests : TestBaseClass
    {
        protected override Type ObjectType { get; set; } = typeof(GenericExtensions);

        [Fact]
        public void CanCallWhen()
        {
            // Arrange
            const TObject Obj = "TestValue1468559414";
            const bool FalsePredicate = false;
            const bool TruePredicate = true;
            Func<TObject, TObject> Method = _ => "TestValue651528331";

            // Act
            var FalseResult = Obj.When(FalsePredicate, Method);
            var TrueResult = Obj.When(TruePredicate, Method);

            // Assert
            Assert.Equal("TestValue1468559414", FalseResult);
            Assert.Equal("TestValue651528331", TrueResult);
        }

        [Fact]
        public void CanCallWhenWithNullMethod()
        {
            var Result = "TestValue1255837734".When(true, default);
            Assert.Equal("TestValue1255837734", Result);
        }
    }
}