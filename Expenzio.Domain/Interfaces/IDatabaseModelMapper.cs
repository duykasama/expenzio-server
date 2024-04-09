using Microsoft.EntityFrameworkCore;

namespace Expenzio.Domain.Interfaces;

public interface IDatabaseModelMapper
{
		void MapToDatabaseModel(ModelBuilder builder);
}
