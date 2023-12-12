using Domain.Entities;

namespace Application.UseCases.NewBoard
{
    public interface IOutputPort
    {
        void Invalid();
        void Ok(Board board);
    }
}
