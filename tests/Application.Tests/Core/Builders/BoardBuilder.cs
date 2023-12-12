using Application.UseCases.NewBoard;
using Bogus;

namespace Application.Tests.Core.Builders
{
    public class BoardBuilder
    {
        private static Faker faker = new Faker();
        public static NewBoardRequest BuildNewBoardRequest()
        {
            return new NewBoardRequest
            {
                Name = $"Board {faker.Company.CompanyName()}",
                Description = faker.Lorem.Sentence(faker.Random.Int(10, 20))
            };
        }

        public static Domain.Entities.Board NewBoardEntity()
        {
            return new Domain.Entities.Board(
                $"Board {faker.Company.CompanyName()}",
                faker.Lorem.Sentence(faker.Random.Int(10, 20))
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
