using Bogus;
using Enterprise.Template.Application.Models.Boards;

namespace Enterprise.Template.Application.Tests.Core.Builders
{
    public class BoardBuilder
    {
        private static readonly Faker Faker = new();
        public static NewBoardRequest BuildNewBoardRequest()
        {
            return new NewBoardRequest
            {
                Name = $"Board {Faker.Company.CompanyName()}",
                Description = Faker.Lorem.Sentence(Faker.Random.Int(10, 20))
            };
        }

        public static Domain.Entities.Board NewBoardEntity()
        {
            return new Domain.Entities.Board(
                $"{Faker.Company.CompanyName()} Board",
                Faker.Lorem.Sentence(Faker.Random.Int(10, 20))
            );
        }

        public static List<Domain.Entities.Board> ManyNewBoardEntity(int quantity)
        {
            var boards = new List<Domain.Entities.Board>();
            for (var i = 0; i < quantity; i++)
                boards.Add(NewBoardEntity());
            return boards;
        }
    }
}
