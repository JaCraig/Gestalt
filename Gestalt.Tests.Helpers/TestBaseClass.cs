using Mecha.Core;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Gestalt.Tests.Helpers
{
    /// <summary>
    /// Test base class
    /// </summary>
    /// <typeparam name="TTestObject">The type of the test object.</typeparam>
    public abstract class TestBaseClass<TTestObject> : TestBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestBaseClass{TTestObject}"/> class.
        /// </summary>
        protected TestBaseClass()
        {
            ObjectType = null;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [discover inherited methods].
        /// </summary>
        /// <value><c>true</c> if [discover inherited methods]; otherwise, <c>false</c>.</value>
        protected bool DiscoverInheritedMethods { get; set; } = false;

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected override Type? ObjectType { get; set; }

        /// <summary>
        /// Gets or sets the test object.
        /// </summary>
        /// <value>The test object.</value>
        protected TTestObject? TestObject { get; set; }

        /// <summary>
        /// Attempts to break the object.
        /// </summary>
        /// <returns>The async task.</returns>
        [Fact]
        public Task BreakObject()
        {
            if (TestObject is null)
                return Task.CompletedTask;
            var ExceptionHandlers = new ExceptionHandler();
            for (var X = 0; X < ExceptionsToIgnore.Length; ++X)
            {
                Type ExceptionToIgnore = ExceptionsToIgnore[X];
                _ = (IgnoreMethod?.MakeGenericMethod(ExceptionToIgnore).Invoke(ExceptionHandlers, new object?[] { null }));
            }

            return Mech.BreakAsync(TestObject, new Options
            {
                MaxDuration = MaxDuration,
                ExceptionHandlers = ExceptionHandlers,
                DiscoverInheritedMethods = DiscoverInheritedMethods
            });
        }
    }

    /// <summary>
    /// Test base class
    /// </summary>
    public abstract class TestBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestBaseClass{TTestObject}"/> class.
        /// </summary>
        protected TestBaseClass()
        {
            lock (_LockObject)
            {
                _ = Mech.Default;
            }
        }

        /// <summary>
        /// The lock object
        /// </summary>
        private static readonly object _LockObject = new();

        /// <summary>
        /// Gets the ignore method.
        /// </summary>
        /// <value>The ignore method.</value>
        protected static MethodInfo? IgnoreMethod { get; } = typeof(ExceptionHandler).GetMethod(nameof(ExceptionHandler.IgnoreException));

        /// <summary>
        /// Gets or sets the exceptions to ignore.
        /// </summary>
        /// <value>The exceptions to ignore.</value>
        protected Type[] ExceptionsToIgnore { get; set; } = new[] {
            typeof(NotImplementedException),
            typeof(ArgumentOutOfRangeException),
            typeof(ArgumentException),
            typeof(FormatException),
            typeof(ObjectDisposedException),
            typeof(EndOfStreamException),
            typeof(OutOfMemoryException)
        };

        /// <summary>
        /// Gets or sets the maximum duration.
        /// </summary>
        /// <value>The maximum duration.</value>
        protected int MaxDuration { get; set; } = 200;

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected abstract Type? ObjectType { get; set; }

        /// <summary>
        /// Attempts to break the object.
        /// </summary>
        /// <returns>The async task.</returns>
        [Fact]
        public Task BreakType()
        {
            if (ObjectType is null)
                return Task.CompletedTask;
            var ExceptionHandlers = new ExceptionHandler();
            for (var X = 0; X < ExceptionsToIgnore.Length; ++X)
            {
                Type ExceptionToIgnore = ExceptionsToIgnore[X];
                _ = (IgnoreMethod?.MakeGenericMethod(ExceptionToIgnore).Invoke(ExceptionHandlers, new object?[] { null }));
            }

            return Mech.BreakAsync(ObjectType, new Options
            {
                MaxDuration = MaxDuration,
                ExceptionHandlers = ExceptionHandlers
            });
        }
    }
}