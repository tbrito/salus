namespace Salus.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using TheJoyOfCode.QualityTools;

    public class TesteDePoco<T> where T : new()
    {
        protected T entity;

        [SetUp]
        public void SetupPropertyAndConstructorTest()
        {
            this.entity = new T();
        }

        [Test]
        public void ChecaPropriedades()
        {
            var propertyTester = new PropertyTester(new T());
            this.IgnorePropertiesOfCollectionType(propertyTester);
            propertyTester.TestProperties();
        }

        [Test]
        public void ChecaConstrutores()
        {
            new ConstructorTester(typeof(T)).TestConstructors(true);
        }

        private void IgnorePropertiesOfCollectionType(PropertyTester propertyTester)
        {
            typeof(T).GetProperties()
                .Where(x => x.PropertyType == typeof(ICollection<>) == false)
                .Select(x => x)
                .ToList()
                .ForEach(x => propertyTester.IgnoredProperties.Add(x.Name));
        }
    }
}